using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipCombat : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float fireRate;
    [SerializeField] protected float shotSpeed;
    [SerializeField] protected float maxHealth;
    protected float currentHealth;
    protected float nextTimeToFire;

    [Header("References")]
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform spawnPoint;
    [SerializeField] protected Transform bulletOrientation;

    [Header("Visual effects")]
    [SerializeField] protected GameObject explosionParticles;
    [SerializeField] protected GameObject damageEffect;
    [SerializeField] protected float damageEffectDurationSeconds;
    [SerializeField] protected float destroyEffectDelay;
    
    protected virtual void Shoot(Transform shootPosition)
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, bulletOrientation.rotation);
            bullet.GetComponent<Rigidbody>().velocity = shootPosition.forward * shotSpeed;
        }
    }

    protected void InstantiateHitEffect(GameObject hitEffectParticles, Vector3 particlePosition)
    {
        GameObject hitEffect = Instantiate(hitEffectParticles, particlePosition, Quaternion.identity);
        Destroy(hitEffect, destroyEffectDelay);
    }

    protected virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        StartCoroutine(nameof(TakeDamageEffect));
    }
    
    protected IEnumerator TakeDamageEffect()
    {
        damageEffect.SetActive(true);

        yield return new WaitForSeconds(damageEffectDurationSeconds);

        damageEffect.SetActive(false);
    }

    protected void ExplosionEffect()
    {
        GameObject explosionEffect = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(explosionEffect, destroyEffectDelay);
    }
}
