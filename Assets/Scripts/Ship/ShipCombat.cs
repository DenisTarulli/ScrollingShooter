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
    
    protected virtual void Shoot(Transform shootPosition)
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, bulletOrientation.rotation);
            bullet.GetComponent<Rigidbody>().velocity = shootPosition.forward * shotSpeed;
        }
    }

    protected virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
