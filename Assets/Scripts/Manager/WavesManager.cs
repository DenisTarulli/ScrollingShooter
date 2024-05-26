using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [Header("Waves values")]
    [SerializeField] private int totalWaves;
    [SerializeField] private int[] waveEnemiesAmount;
    [SerializeField] private float[] spawnDelaySeconds;
    [SerializeField] private string[] waveShipPatterns;
    [SerializeField] private Transform bossSpawnPoint;
    private bool bossSpawned;
    private int enemiesAmountNextWaveTrigger;
    private int currentWave;
    
    [Header("Enemies prefabs")]
    [SerializeField] private GameObject[] shipPrefabs;
    [SerializeField] private GameObject bossObj;
    private int enemyKillCount;

    [Header("Wave Patterns")]
    [SerializeField] private GameObject patterns;

    // Specific array element names
    private GameObject bat;
    private GameObject nova;
    private GameObject scout;
    private float shortSpawnRate;
    private float normalSpawnRate;
    private float mediumSpawnRate;
    private float longSpawnRate;
    private string wavyPattern;
    private string widePattern;
    private string zigzagPattern;

    private void Start()
    {
        bat = shipPrefabs[0];
        nova = shipPrefabs[1];
        scout = shipPrefabs[2];

        shortSpawnRate = spawnDelaySeconds[0];
        normalSpawnRate = spawnDelaySeconds[1];
        mediumSpawnRate = spawnDelaySeconds[2];
        longSpawnRate = spawnDelaySeconds[3];

        wavyPattern = waveShipPatterns[0];
        widePattern = waveShipPatterns[1];
        zigzagPattern = waveShipPatterns[2];

        patterns.SetActive(true);
        enemyKillCount = 0;
        currentWave = 1;
        bossSpawned = false;

        StartCoroutine(WaveStart(bat, waveEnemiesAmount[0], waveShipPatterns[0], spawnDelaySeconds[0]));
        currentWave++;
    }

    private void Update()
    {
        WaveTrigger();            
    }

    private IEnumerator WaveStart(GameObject ship, int enemiesAmount, string pattern, float spawnDelay)
    {
        Transform[] routes = new Transform[2];

        enemiesAmountNextWaveTrigger += enemiesAmount * 2;
        
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
    }

    private void SpawnBoss()
    {
        if (!bossSpawned)
        {
            bossSpawned = true;
            Instantiate(bossObj, bossSpawnPoint);
        }
    }

    private void WaveTrigger()
    {
        if (enemyKillCount == enemiesAmountNextWaveTrigger)
        {
            switch (currentWave)
            {
                case 2:
                    StartCoroutine(WaveStart(nova, waveEnemiesAmount[1], widePattern, mediumSpawnRate));
                    currentWave++;
                    break;

                case 3:
                    StartCoroutine(WaveStart(bat, waveEnemiesAmount[2], wavyPattern, shortSpawnRate));
                    StartCoroutine(WaveStart(nova, waveEnemiesAmount[2], widePattern, mediumSpawnRate));
                    currentWave++;
                    break;

                case 4:
                    StartCoroutine(WaveStart(scout, waveEnemiesAmount[3], zigzagPattern, normalSpawnRate));
                    currentWave++;
                    break;

                case 5:
                    StartCoroutine(WaveStart(scout, waveEnemiesAmount[3], wavyPattern, normalSpawnRate));
                    StartCoroutine(WaveStart(nova, waveEnemiesAmount[3], widePattern, mediumSpawnRate));
                    currentWave++;
                    break;

                case 6:
                    StartCoroutine(WaveStart(bat, waveEnemiesAmount[4], wavyPattern, longSpawnRate));
                    StartCoroutine(WaveStart(nova, waveEnemiesAmount[4], widePattern, longSpawnRate));
                    currentWave++;
                    break;
                case 7:
                    StartCoroutine(WaveStart(scout, waveEnemiesAmount[5], wavyPattern, normalSpawnRate));
                    currentWave++;
                    break;
                case 8:
                    StartCoroutine(WaveStart(scout, waveEnemiesAmount[6], zigzagPattern, normalSpawnRate));
                    StartCoroutine(WaveStart(nova, waveEnemiesAmount[6], widePattern, mediumSpawnRate));
                    currentWave++;
                    break;
                case 9:
                    StartCoroutine(WaveStart(bat, waveEnemiesAmount[7], wavyPattern, normalSpawnRate));
                    StartCoroutine(WaveStart(nova, waveEnemiesAmount[7], widePattern, longSpawnRate));
                    currentWave++;
                    break;
                case 10:
                    StartCoroutine(WaveStart(bat, waveEnemiesAmount[8], wavyPattern, normalSpawnRate));
                    StartCoroutine(WaveStart(nova, waveEnemiesAmount[8], widePattern, longSpawnRate));
                    StartCoroutine(WaveStart(scout, waveEnemiesAmount[8], zigzagPattern, normalSpawnRate));
                    currentWave++;
                    break;
                case 11:
                    SpawnBoss();
                    break;
            }
        }
    }
}
