using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerCombat playerToObserve;
    [SerializeField] private Slider playerHealthBar;

    private void Start()
    {
        SetMaxHealth();
        playerToObserve.OnHurt += SetHealth;
    }

    private void SetMaxHealth()
    {
        playerHealthBar.maxValue = playerToObserve.MaxHealth;
        playerHealthBar.value = playerToObserve.CurrentHealth;
    }

    private void SetHealth()
    {
        playerHealthBar.value = playerToObserve.CurrentHealth;
    }
}
