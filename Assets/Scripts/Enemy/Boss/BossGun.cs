using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BossGun : ShipCombat
{
    [Header("Extra Stats")]
    [SerializeField] private Transform spawnPoint2;
    private BossCombat bossCombat;
    
    private PlayerCombat playerCombat;

    private void Start()
    {
        playerCombat = FindObjectOfType<PlayerCombat>();
        bossCombat = GetComponentInParent<BossCombat>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        Shoot(spawnPoint);
    }
    protected override void Shoot(Transform shootPosition)
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, bulletOrientation.rotation);
            bullet.GetComponent<Rigidbody>().velocity = shootPosition.forward * shotSpeed;

            GameObject bullet2 = Instantiate(bulletPrefab, spawnPoint2.position, bulletOrientation.rotation);
            bullet2.GetComponent<Rigidbody>().velocity = spawnPoint2.forward * shotSpeed;
        }
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

    private void OnDestroy()
    {
        bossCombat.DisableInvulnerability();
    }
}
