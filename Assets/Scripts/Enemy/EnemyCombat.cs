using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : ShipCombat
{
    private PlayerCombat playerCombat;

    private void Start()
    {
        currentHealth = maxHealth;
        playerCombat = FindObjectOfType<PlayerCombat>();
    }

    private void Update()
    {
        Shoot();
    }    

    protected override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (currentHealth <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            TakeDamage(playerCombat.damage);
        }
    }
}
