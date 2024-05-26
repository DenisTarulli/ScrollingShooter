using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerUp : PowerUps
{
    [SerializeField] private float bonusDamage;

    private void Update()
    {
        Movement();
    }

    protected override void PowerUpEffect()
    {
        base.PowerUpEffect();

        PlayerCombat player = FindObjectOfType<PlayerCombat>();

        player.Damage += bonusDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        PowerUpEffect();
        Destroy(gameObject);
    }
}
