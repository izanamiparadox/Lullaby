using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{

    float currentHealth;
    [SerializeField] PlayerStatus playerStats;
    [SerializeField] Image bar;


    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStatus>();
        bar = GetComponent<Image>();
    }
    private void Update()
    {
        currentHealth = playerStats.health;

        bar.fillAmount = currentHealth / playerStats.maxHealth;
    }
}
