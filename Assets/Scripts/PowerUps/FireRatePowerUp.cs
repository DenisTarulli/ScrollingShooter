using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerUp : PowerUps
{
    private void Update()
    {
        Movement();
    }

    protected override void PowerUpEffect()
    {
        PlayerCombat player = FindObjectOfType<PlayerCombat>();

        player.FireRate += 0.7f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        PowerUpEffect();
        Destroy(gameObject);
    }
}
