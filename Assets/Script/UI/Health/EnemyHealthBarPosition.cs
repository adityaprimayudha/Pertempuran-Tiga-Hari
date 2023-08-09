using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarPosition : MonoBehaviour
{
    public Transform enemy;
    public GameObject healthBarSlider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBarPosition();
    }

    private void UpdateHealthBarPosition()
    {
        // Set the position of the health bar above the enemy's head
        Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.position);
        healthBarSlider.transform.position = screenPos;
    }
}
