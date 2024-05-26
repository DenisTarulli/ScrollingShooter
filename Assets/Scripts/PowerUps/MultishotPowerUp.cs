using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotPowerUp : PowerUps
{
    [SerializeField] private float bonusDamageCompensation;
    private void Update()
    {
        Movement();   
    }

    protected override void PowerUpEffect()
    {
        base.PowerUpEffect();

        PlayerCombat player = FindObjectOfType<PlayerCombat>();

        if (player.Shots != 2)
            player.Shots++;
        else
            player.Damage += bonusDamageCompensation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        PowerUpEffect();
        Destroy(gameObject);
    }
}
