using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : ShipCombat
{
    [SerializeField] private float bulletDamageTaken;
    public Action OnHurt { get; set; }

    public float MaxHealth { get => maxHealth; }
    public float CurrentHealth { get => currentHealth; }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        Shoot();
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
            Destroy(other.gameObject);
            TakeDamage(bulletDamageTaken);
        }
    }
}
