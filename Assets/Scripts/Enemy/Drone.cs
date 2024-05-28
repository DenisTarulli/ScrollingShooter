using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : ShipCombat
{
    private PlayerCombat playerCombat;

    [Header("Extra stats")]
    [SerializeField] private float lifeTime;
    public float damage;

    private void Start()
    {
        currentHealth = maxHealth;
        playerCombat = FindObjectOfType<PlayerCombat>();

        Destroy(gameObject, lifeTime);
    }

    protected override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (currentHealth <= 0)
            ShipDestroy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            GameObject hitEffect = other.gameObject.GetComponent<Bullet>().hitEffectParticles;
            InstantiateHitEffect(hitEffect, other.transform.position);
            Destroy(other.gameObject);
            TakeDamage(playerCombat.Damage / (playerCombat.Shots + 1));

            AudioManager.instance.Play("Hit");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            ShipDestroy();
    }

    private void OnDestroy()
    {
        WavesManager wavesManager = FindObjectOfType<WavesManager>();

        if (wavesManager != null)
            wavesManager.AddEnemyKill();
    }
}
