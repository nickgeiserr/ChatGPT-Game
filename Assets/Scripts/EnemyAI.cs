using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // The speed at which the enemy moves
    public float speed = 5.0f;
    
    //
    public GameObject nextLevel;
    
    // health
    public float health = 100.0f;

    // The distance at which the enemy will start chasing the player
    public float chaseDistance = 10.0f;

    // The distance at which the enemy will stop chasing the player
    public float stopChasingDistance = 15.0f;

    // The distance at which the enemy will start shooting at the player
    public float shootDistance = 5.0f;

    // The rate at which the enemy will shoot at the player
    public float shootRate = 1.0f;

    // A reference to the player game object
    public GameObject player;

    // A reference to the Rigidbody2D component of the enemy
    private Rigidbody2D _rb;

    // A reference to the Transform component of the enemy
    private Transform _enemyTransform;

    // A reference to the Transform component of the player
    private Transform _playerTransform;

    // A timer to track the time between shots
    private float _shootTimer = 0.0f;

    // A reference to the Bullet prefab
    public GameObject bulletPrefab;

    public Vector3 death_pos;

    // The speed at which the bullet travels
    public float bulletSpeed = 10.0f;

    void Start()
    {
        // Get a reference to the player game object
        player = GameObject.FindWithTag("Player");

        // Get a reference to the Rigidbody2D component of the enemy
        _rb = GetComponent<Rigidbody2D>();

        // Get a reference to the Transform component of the enemy
        _enemyTransform = GetComponent<Transform>();

        // Get a reference to the Transform component of the player
        _playerTransform = player.GetComponent<Transform>();
        
        // Constrain the player's rotation to prevent flipping over
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    
    public void TakeDamage(float bulletDamage)
    {
        Debug.Log("took damage");
        health -= bulletDamage;
        if (health <= 0)
        {
            death_pos = gameObject.transform.position;
            Destroy(gameObject);
            Invoke(nameof(spawn_next), 2);
        }
    }

    public void spawn_next()
    {
        Instantiate(nextLevel, death_pos, Quaternion.identity);
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }   
        // Calculate the distance between the enemy and the player
        float distance = Vector2.Distance(_enemyTransform.position, _playerTransform.position);

        // Check if the enemy is within the chase distance
        if (distance <= chaseDistance)
        {
            // Move the enemy towards the player
            Vector2 direction = _playerTransform.position - _enemyTransform.position;
            _rb.AddForce(direction.normalized * speed);
        }

        // Check if the enemy is within the stop chasing distance
        if (distance > stopChasingDistance)
        {
            // Stop moving the enemy
            _rb.velocity = Vector2.zero;
        }

        // Update the shoot timer
        _shootTimer += Time.deltaTime;

        // Check if the enemy is within the shoot distance and the shoot timer has reached the shoot rate
        if (distance <= shootDistance && _shootTimer >= shootRate)
        {
            // Reset the shoot timer
            _shootTimer = 0.0f;

// Calculate the direction from the enemy to the player
            Vector2 direction = _playerTransform.position - _enemyTransform.position;

// Generate a random offset for the bullet's position
            float offsetX = Random.Range(-5.0f, 5.0f);
            float offsetY = Random.Range(-5.0f, 5.0f);

// Calculate the bullet's initial position based on the enemy's position and the offset
            Vector3 thing = _enemyTransform.position;
            Vector2 bulletPosition = (new Vector2(thing.x, thing.y) + new Vector2(offsetX, offsetY));

// Instantiate a new bullet game object at the initial position
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);

// Get the Rigidbody2D component of the bullet
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

// Set the bullet's initial velocity in the direction of the player
            bulletRb.velocity = direction * bulletSpeed;

        }
    }
}
