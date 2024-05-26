using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : ShipCombat
{
    [Header("Extra stats")]
    [SerializeField] private float invulnerabilityTime;
    [SerializeField] private float enemyContactDamage;
    [SerializeField] private float damage;
    private bool canTakeDamage;

    [Header("Shoot spawn points")]
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private Transform spawnPoint3;
    [SerializeField] private Transform spawnPoint4;
    [SerializeField] private Transform spawnPoint5;

    [Header("Multishot testing")]
    [SerializeField, Range(0, 2)] private int shots;

    public Action OnHurt { get; set; }
    public Action OnShoot { get; set; }

    public float MaxHealth { get => maxHealth; }
    public float CurrentHealth { get => currentHealth; }
    public int Shots { get => shots; set => shots = value; }
    public float Damage { get => damage; set => damage = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        canTakeDamage = true;
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

            if (shots >= 1)
                Multishot(spawnPoint2, spawnPoint3);

            if (shots == 2)
                Multishot(spawnPoint4, spawnPoint5);

            OnShoot?.Invoke();
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
        if (!canTakeDamage) return;

        base.TakeDamage(damage);
        StartCoroutine(nameof(InvulnerabilityTime));

        if (shots != 0)
            shots--;

        OnHurt?.Invoke();
    }
    
    private IEnumerator InvulnerabilityTime()
    {
        canTakeDamage = false;

        yield return new WaitForSeconds(invulnerabilityTime);

        canTakeDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            float damageToTake = other.GetComponent<Bullet>().bulletDamage;
            GameObject hitEffect = other.gameObject.GetComponent<Bullet>().hitEffectParticles;
            InstantiateHitEffect(hitEffect ,other.transform.position);
            TakeDamage(damageToTake);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Scout"))
        {
            Scout scout = collision.gameObject.GetComponent<Scout>();

            if (scout != null)
            {
                float damageToTake = scout.damage;
                TakeDamage(damageToTake);
            }
        }

        if (collision.gameObject.CompareTag("Bat") || collision.gameObject.CompareTag("Nova"))
        {
            TakeDamage(enemyContactDamage);
        }
    }
}
