using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armored : ShipCombat
{
    private PlayerCombat playerCombat;

    private void Start()
    {
        currentHealth = maxHealth;
        playerCombat = FindObjectOfType<PlayerCombat>();
    }

    private void Update()
    {
        Shoot(spawnPoint);
    }

    protected override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (currentHealth <= 0)
        {
            ExplosionEffect();
            AudioManager.instance.Play("Explosion");
            Destroy(gameObject);        
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            GameObject hitEffect = other.gameObject.GetComponent<Bullet>().hitEffectParticles;
            InstantiateHitEffect(hitEffect, other.transform.position);
            Destroy(other.gameObject);
            TakeDamage(playerCombat.damage);

            AudioManager.instance.Play("Hit");
        }
    }

    private void OnDestroy()
    {
        WavesManager wavesManager = FindObjectOfType<WavesManager>();

        if (wavesManager != null)
            wavesManager.AddEnemyKill();
    }
}
