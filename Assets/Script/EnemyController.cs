using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float attackRange = 2f; 
    public int maxHealth = 100; 

    private Transform player; 
    private int currentHealth; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            Debug.Log("Enemy Attack!");
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy Defeated!");
            Destroy(gameObject);
        }
    }
}
