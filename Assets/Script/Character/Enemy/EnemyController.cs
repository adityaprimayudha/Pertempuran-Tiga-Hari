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
    public float moveSpeed = 5f;
    private Vector2 direction;
    private float angle;
    private Rigidbody2D rb;
    private float distanceToPlayer;
    [SerializeField] private GameObject canvas;
    private Animator anim;
    private Vector2 previousPos;
    private Vector2 lastVelocity;
    private Vector2 velocity;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        previousPos = rb.position;
    }

    void Update()
    {
        // if (DialogueManager.GetInstance().IsDialoguePlaying)
        // {
        //     return;
        // }
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= chaseRange)
        {
            ChangeDirection();
            firePoint.transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        }
        else
        {
            direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        // if (DialogueManager.GetInstance().IsDialoguePlaying)
        // {
        //     return;
        // }
        // Check if the player is outside the attack range
        if (distanceToPlayer > attackRange)
        {
            CheckVelocityAndAnimation();
            rb.MovePosition(moveSpeed * Time.fixedDeltaTime * direction + rb.position);
        }
        else
        {
            if (Time.time - lastShotTime >= timeBetweenShots)
            {
                CheckVelocityAndAnimation();
                anim.SetFloat("lastX", lastVelocity.x);
                anim.SetFloat("lastY", lastVelocity.y);
                AttackPlayer();
            }
        }
    }
    private void AttackPlayer()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
        lastShotTime = Time.time;
        Debug.Log("ATTACK!");
    }

    private void ChangeDirection()
    {
        direction = (player.position - transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        anim.SetFloat("lastX", direction.x);
        anim.SetFloat("lastY", direction.y);
    }
    private void CheckVelocityAndAnimation()
    {
        velocity = (rb.position - previousPos) / Time.fixedDeltaTime;
        previousPos = rb.position;

        // Normalize the velocity to get the direction
        Vector2 normalizedVelocity = velocity.normalized;
        lastVelocity = normalizedVelocity;

        // Update the Animator parameter for direction
        anim.SetFloat("moveX", normalizedVelocity.x);
        anim.SetFloat("moveY", normalizedVelocity.y);

    }

    public void OnStop(bool stop)
    {

        rb.velocity = Vector2.zero;
        this.GetComponent<EnemyController>().enabled = stop;

    }

    public void Defeated()
    {
        canvas.SetActive(false);
    }
}
