using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float chaseRange = 10f;
    public float attackRange = 2f;
    private Transform player;
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float timeBetweenShots = 1f;
    private float lastShotTime;
    private bool isChasing;
    public float moveSpeed = 5f;
    private Vector2 direction;
    private float angle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= chaseRange)
        {
            changeDirection();
            transform.rotation = Quaternion.AngleAxis(angle, transform.forward);

            // Check if the player is outside the attack range
            if (distanceToPlayer > attackRange)
            {
                transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
            }
            else
            {
                if (Time.time - lastShotTime >= timeBetweenShots)
                {
                    AttackPlayer();
                }
            }
        }
        else
        {

        }
    }
    private void AttackPlayer()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
        lastShotTime = Time.time;
        Debug.Log("ATTACK!");
    }

    private void changeDirection()
    {
        direction = (player.position - transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}
