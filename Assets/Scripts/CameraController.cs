using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // A reference to the player game object
    public GameObject player;

    void Update()
    {
        // Get the player's position
        Vector3 playerPosition = player.transform.position;

        playerPosition.z = -10;

        // Smoothly interpolate between the camera's current position and the player's position
        Vector3 cameraPosition = Vector3.Lerp(Camera.main.transform.position, playerPosition, 1f);

        // Set the camera's position to the interpolated position
        Camera.main.transform.position = cameraPosition;
    }
}