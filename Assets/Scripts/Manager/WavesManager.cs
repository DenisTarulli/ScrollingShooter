using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] shipPrefabs;
    [SerializeField] private int[] waveEnemiesAmount;
    [SerializeField] private float spawnDelaySeconds;

    private void Start()
    {
        StartCoroutine(WaveStart(shipPrefabs[0], shipPrefabs[1], waveEnemiesAmount[0]));
    }

    private void Update()
    {
        
    }

    private IEnumerator WaveStart(GameObject ship1, GameObject ship2, int enemiesAmount)
    {
        for (int i = 0; i < enemiesAmount; i++)
        {
            Instantiate(ship1, transform);
            Instantiate(ship2, transform);

            yield return new WaitForSeconds(spawnDelaySeconds);
        }
    }
}
