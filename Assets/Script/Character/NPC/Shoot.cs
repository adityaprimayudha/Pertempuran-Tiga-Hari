using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform firePoint;
    public float timeBetweenShots = 1f;
    private float lastShotTime;
    [SerializeField] private int damageAmount = 5;
    [SerializeField] private GameObject bulletPrefab;

    void Update()
    {
        if (Time.time - lastShotTime >= timeBetweenShots)
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<BulletBehaviour>().damageAmount = damageAmount;
        bullet.GetComponent<SpriteRenderer>().color = Color.green;
        bullet.transform.position = transform.position;
        lastShotTime = Time.time;
        SFXManager.Instance.GunShotSound();
    }
}
