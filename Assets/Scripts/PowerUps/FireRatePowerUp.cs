using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerUp : PowerUps
{
    [SerializeField] private float bonusFireRate;
    private void Update()
    {
        Movement();
    }

    protected override void PowerUpEffect()
    {
        base.PowerUpEffect();

        PlayerCombat player = FindObjectOfType<PlayerCombat>();

        player.FireRate += bonusFireRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        PowerUpEffect();
        Destroy(gameObject);
    }
}
