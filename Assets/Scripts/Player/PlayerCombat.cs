using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : ShipCombat
{
    [Header("Damage stat")]
    public float damage;

    [Header("Shoot spawn points")]
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private Transform spawnPoint3;
    [SerializeField] private Transform spawnPoint4;
    [SerializeField] private Transform spawnPoint5;

    [Header("Multishot testing")]
    [SerializeField, Range(0, 2)] private int multishot;
    public Action OnHurt { get; set; }

    public float MaxHealth { get => maxHealth; }
    public float CurrentHealth { get => currentHealth; }

    private void Awake()
    {
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

            if (multishot >= 1)
                Multishot(spawnPoint2, spawnPoint3);

            if (multishot == 2)
                Multishot(spawnPoint4, spawnPoint5);
        }
    }

    private void Multishot(Transform shootPosition1, Transform shootPosition2)
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPosition1.position, bulletOrientation.rotation);
        bullet.GetComponent<Rigidbody>().velocity = shootPosition1.forward * shotSpeed;

        GameObject bullet2 = Instantiate(bulletPrefab, shootPosition2.position, bulletOrientation.rotation);
        bullet2.GetComponent<Rigidbody>().velocity = shootPosition2.forward * shotSpeed;
    }

    protected override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        OnHurt?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            float damageToTake = other.GetComponent<Bullet>().bulletDamage;
            TakeDamage(damageToTake);
            Destroy(other.gameObject);
        }
    }
}
