using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipCombat : MonoBehaviour
{
    [Header("Stats")]
    public float damage;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float shotSpeed;
    [SerializeField] protected float maxHealth;
    protected float currentHealth;
    protected float nextTimeToFire;
    protected readonly float bulletLifeTime = 3f;

    [Header("References")]
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform spawnPoint;
    [SerializeField] protected Transform bulletOrientation;

    protected void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, bulletOrientation.rotation);
            bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * shotSpeed;
        }
    }

    protected virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
