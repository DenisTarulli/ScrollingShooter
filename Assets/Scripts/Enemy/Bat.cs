using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : ShipCombat
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
            ShipDestroy();      
    }

    protected override void Shoot(Transform shootPosition)
    {
        base.Shoot(shootPosition);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            GameObject hitEffect = other.gameObject.GetComponent<Bullet>().hitEffectParticles;
            InstantiateHitEffect(hitEffect, other.transform.position);
            Destroy(other.gameObject);
            TakeDamage(playerCombat.Damage);

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
