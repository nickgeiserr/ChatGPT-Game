using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // The speed at which the player moves
    public float speed = 8.0f;

    // A reference to the Rigidbody2D component
    private Rigidbody2D _rb;

    public float Health = 100.0f;

    private Vector2 moveVelocity;

    public GameObject bulletPrefab;

    // A reference to the Transform component of the player game object
    private Transform _playerTransform;

    // The speed at which the bullet travels
    public float bulletSpeed = 10.0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerTransform = GetComponent<Transform>();
        
        // Constrain the player's rotation to prevent flipping over
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        if (Health <= 0)
        {
            Application.Quit();
        }

        // Check if the player has clicked the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Create a new bullet game object at the player's position
            GameObject bullet = Instantiate(bulletPrefab, _playerTransform.position + new Vector3(3,3,0), Quaternion.identity, null);

            // Get the Rigidbody2D component of the bullet
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            // Calculate the direction from the player to the mouse position
            Vector2 direction = mousePos - new Vector2(_playerTransform.position.x, _playerTransform.position.y);

            // Set the bullet's initial velocity in the direction of the mouse position
            bulletRb.velocity = direction * bulletSpeed;
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
