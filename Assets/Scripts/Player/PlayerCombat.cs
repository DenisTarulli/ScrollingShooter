using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : ShipCombat
{
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        Shoot();
    }
}
