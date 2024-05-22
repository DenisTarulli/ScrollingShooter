using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotPowerUp : PowerUps
{
    private void Update()
    {
        Movement();   
    }

    protected override void PowerUpEffect()
    {
        PlayerCombat player = FindObjectOfType<PlayerCombat>();

        if (player.Shots != 2)
            player.Shots++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        PowerUpEffect();
        Destroy(gameObject);
    }
}
