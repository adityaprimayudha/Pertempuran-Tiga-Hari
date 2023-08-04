using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Animator anim;
    private Vector2 moveDirection;
    public float attackRange = 2f;
    public LayerMask enemyLayerMask;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float timeBetweenShots = 0.5f;
    private float lastShotTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;

        checkAnimation(rb);
        // Input for movement (Horizontal and Vertical axis values)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement direction
        moveDirection = new Vector2(moveHorizontal, moveVertical).normalized;

        // Update the player's rotation
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        //Check attack range
        if (Input.GetMouseButtonDown(0) && Time.time - lastShotTime >= timeBetweenShots)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Cast a ray from the camera to the mouse position on the screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 aimDir = mousePosition - transform.position;
                aimDir.z = 0f;

                // Update the player's rotation to face the cursor
                if (aimDir != Vector3.zero)
                {
                    float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    anim.SetFloat("lastX", aimDir.x);
                    anim.SetFloat("lastY", aimDir.y);
                }
                Shoot();
            }
        }
    }


    private void Shoot()
    {
        // Instantiate the bullet prefab at the firePoint position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Record the time when the bullet was shot for time delay between shots
        lastShotTime = Time.time;

    }

    private void checkAnimation(Rigidbody2D rb)
    {
        anim.SetFloat("moveX", rb.velocity.x);
        anim.SetFloat("moveY", rb.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            anim.SetFloat("lastX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("lastY", Input.GetAxisRaw("Vertical"));

        }
    }

}
