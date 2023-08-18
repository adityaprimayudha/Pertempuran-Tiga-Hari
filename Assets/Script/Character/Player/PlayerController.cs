using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PixelCrushers;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private Animator anim;
    private Vector2 moveDirection;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float timeBetweenShots = 0.5f;
    private float lastShotTime;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // if (DialogueManager.GetInstance().IsDialoguePlaying)
        // {
        //     return;
        // }
        OnMove();
        Attack();
    }

    public void OnMove()
    {
        Vector2 move = InputManager.GetInstance().GetMoveValue();
        rb.velocity = move * speed;

        CheckAnimation(rb);

        moveDirection = move.normalized;

        // Update the player's rotation
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            firePoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void Attack()
    {
        bool attackPressed = InputManager.GetInstance().GetAttackPressed();
        if (attackPressed && Time.time - lastShotTime >= timeBetweenShots)
        {
            // Cast a ray from the camera to the mouse position on the screen
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, enemyLayerMask);

            // If the ray hit something on the enemy layer
            if (hit.collider != null)
            {
                // Calculate the distance between the player and the clicked enemy
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                // Check if the enemy is within attack range
                if (distanceToEnemy <= attackRange)
                {
                    // Get the direction towards the cursor
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    Vector3 aimDir = mousePosition - transform.position;
                    aimDir.z = 0f;

                    // Update the player's rotation to face the cursor
                    if (aimDir != Vector3.zero)
                    {
                        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
                        firePoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        anim.SetFloat("lastX", aimDir.x);
                        anim.SetFloat("lastY", aimDir.y);
                    }
                    Shoot();
                }
            }
        }
    }


    private void Shoot()
    {
        // Instantiate the bullet prefab at the firePoint position and rotation
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Record the time when the bullet was shot for time delay between shots
        lastShotTime = Time.time;

    }

    private void CheckAnimation(Rigidbody2D rb)
    {

        anim.SetFloat("moveX", rb.velocity.x);
        anim.SetFloat("moveY", rb.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            anim.SetFloat("lastX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("lastY", Input.GetAxisRaw("Vertical"));

        }
    }

    public void OnStop(bool stop)
    {
        rb.velocity = Vector2.zero;
        anim.SetFloat("moveY", 0);
        anim.SetFloat("moveX", 0);
        anim.SetFloat("lastX", 0);
        anim.SetFloat("lastY", 0);
        if (stop)
        {
            Debug.Log("Jalan");
        }
        else
        {
            Debug.Log("Stop");
        }
        this.GetComponent<PlayerController>().enabled = stop;
    }

}
