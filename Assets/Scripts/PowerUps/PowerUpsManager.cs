using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public WeightedRandomList<GameObject> lootPool;

    public void DropLoot(Vector3 spawnPosition)
    {
        GameObject item = lootPool.GetRandom();
        Instantiate(item, spawnPosition, Quaternion.identity);
    }
}
