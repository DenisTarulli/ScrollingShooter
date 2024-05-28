using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : ShipCombat
{
    [Header("Extra stats & references")]
    [SerializeField] private GameObject invulnerabilityEffect;
    [SerializeField] private GameObject drone;
    [SerializeField] private GameObject dronePattern;
    [SerializeField] private Transform droneSpawnPointRight;
    [SerializeField] private Transform droneSpawnPointLeft;
    [SerializeField] private float droneDeployRate;
    [SerializeField] private string wavePattern;
    private PlayerCombat playerCombat;
    private float nextTimeToDeploy;
    private int gunsRemaining;
    private bool canTakeDamage;
    private bool secondPhase;

    private void Start()
    {
        currentHealth = maxHealth;
        dronePattern = GameObject.FindWithTag("BossDronePattern");

        playerCombat = FindObjectOfType<PlayerCombat>();
        secondPhase = false;
        gunsRemaining = 2;
        canTakeDamage = false;
        dronePattern.SetActive(true);
    }

    private void Update()
    {
        Shoot(spawnPoint);

        if (secondPhase)
            DeployDrone();
    }

    public void DisableInvulnerability()
    {
        gunsRemaining--;

        if (gunsRemaining == 0)
        {
            secondPhase = true;
            canTakeDamage = true;
            invulnerabilityEffect.SetActive(false);
        }
    }

    protected override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
            ShipDestroy();
        }
        
    }


    private void DeployDrone()
    {
        if (Time.time >= nextTimeToDeploy)
        {
            nextTimeToDeploy = Time.time + 1f / droneDeployRate;

            Transform[] routes = new Transform[2];

            for (int i = 0; i < routes.Length; i++)
            {
                routes[i] = GameObject.FindWithTag(wavePattern).transform.GetChild(i);
            }

            GameObject drone1 = Instantiate(drone, droneSpawnPointRight);
            drone1.GetComponent<EnemyMovement>().wavePattern = routes[0];

            GameObject drone2 = Instantiate(drone, droneSpawnPointLeft);
            drone2.GetComponent<EnemyMovement>().wavePattern = routes[1];
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            GameObject hitEffect = other.gameObject.GetComponent<Bullet>().hitEffectParticles;
            InstantiateHitEffect(hitEffect, other.transform.position);
            Destroy(other.gameObject);

            if (canTakeDamage) 
                TakeDamage(playerCombat.Damage / (playerCombat.Shots + 1));

            AudioManager.instance.Play("Hit");
        }
    }
}
