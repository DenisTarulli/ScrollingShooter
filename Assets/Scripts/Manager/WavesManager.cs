using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] shipPrefabs;
    [SerializeField] private int[] waveEnemiesAmount;
    [SerializeField] private float[] spawnDelaySeconds;
    private int enemyKillCount;

    private void Start()
    {
        enemyKillCount = 0;
        StartCoroutine(WaveStart(shipPrefabs[0], waveEnemiesAmount[0], "Pattern1", "Pattern2", spawnDelaySeconds[0]));
    }

    private IEnumerator WaveStart(GameObject ship, int enemiesAmount, string patternOne, string patternTwo, float spawnDelay)
    {
        for (int i = 0; i < enemiesAmount; i++)
        {
            GameObject ship1 = Instantiate(ship, transform);
            ship1.GetComponent<EnemyMovement>().wavePattern = patternOne;
            GameObject ship2 = Instantiate(ship, transform);
            ship2.GetComponent<EnemyMovement>().wavePattern = patternTwo;

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void AddEnemyKill()
    {
        enemyKillCount++;

        if (enemyKillCount == (waveEnemiesAmount[0] * 2))
            StartCoroutine(WaveStart(shipPrefabs[1], waveEnemiesAmount[1], "Pattern1", "Pattern2", spawnDelaySeconds[1]));
    }
}
