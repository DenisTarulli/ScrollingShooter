using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [Header("Waves values")]
    [SerializeField] private int[] waveEnemiesAmount;
    [SerializeField] private float[] spawnDelaySeconds;
    [SerializeField] private string[] waveShipPatterns;
    private int[] enemiesAmountWaveTrigger;
    private int totalWaves;
    
    [Header("Enemies prefabs")]
    [SerializeField] private GameObject[] shipPrefabs;
    private int enemyKillCount;

    [Header("Wave Patterns")]
    [SerializeField] private GameObject patterns;

    private void Start()
    {
        patterns.SetActive(true);
        enemyKillCount = 0;

        totalWaves = waveEnemiesAmount.Length;
        enemiesAmountWaveTrigger = new int[totalWaves];

        CountTotalEnemiesAmount();
        StartCoroutine(WaveStart(shipPrefabs[0], waveEnemiesAmount[0], waveShipPatterns[0], spawnDelaySeconds[0]));
    }

    private IEnumerator WaveStart(GameObject ship, int enemiesAmount, string pattern, float spawnDelay)
    {
        Transform[] routes = new Transform[2];        
        
        for (int i = 0; i < routes.Length; i++)
        {
            routes[i] = GameObject.FindWithTag(pattern).transform.GetChild(i);
        }

        for (int i = 0; i < enemiesAmount; i++)
        {
            GameObject ship1 = Instantiate(ship, transform);
            ship1.GetComponent<EnemyMovement>().wavePattern = routes[0];

            GameObject ship2 = Instantiate(ship, transform);
            ship2.GetComponent<EnemyMovement>().wavePattern = routes[1];

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void AddEnemyKill()
    {
        enemyKillCount++;

        if (enemyKillCount == enemiesAmountWaveTrigger[0])
            StartCoroutine(WaveStart(shipPrefabs[1], waveEnemiesAmount[1], waveShipPatterns[1], spawnDelaySeconds[1]));

        if (enemyKillCount == enemiesAmountWaveTrigger[1])
        {
            StartCoroutine(WaveStart(shipPrefabs[0], waveEnemiesAmount[2], waveShipPatterns[0], spawnDelaySeconds[0]));
            StartCoroutine(WaveStart(shipPrefabs[1], waveEnemiesAmount[2], waveShipPatterns[1], spawnDelaySeconds[1]));
        }
    }

    private void CountTotalEnemiesAmount()
    {
        int amount = 0;

        for (int i = 0; i < waveEnemiesAmount.Length; i++)
        {
            amount += waveEnemiesAmount[i] * 2;
            enemiesAmountWaveTrigger[i] = amount;
        }
    }
}
