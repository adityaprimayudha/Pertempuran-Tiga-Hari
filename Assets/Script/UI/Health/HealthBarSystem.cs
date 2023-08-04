using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarSystem : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Debug.Log("Character Defeated!");
        }
    }

    private void UpdateHealthBar()
    {
        float healthNormalized = (float)currentHealth / maxHealth;
        healthBar.SetHealth(healthNormalized);
    }
}
