using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float smoothSpeed = 0.125f; // Speed of camera movement

    public float minX = -10f; // Minimum X position for the camera
    public float maxX = 10f; // Maximum X position for the camera
    public float minY = -10f; // Minimum Y position for the camera
    public float maxY = 10f; // Maximum Y position for the camera

    void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the desired camera position based on the player's position
            Vector3 desiredPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

            // Apply limits to the desired camera position
            float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);
            desiredPosition = new Vector3(clampedX, clampedY, desiredPosition.z);

            // Smoothly move the camera towards the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
