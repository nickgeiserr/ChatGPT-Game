using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // A reference to the player game object
    public GameObject player;

    // The speed at which the enemy moves
    public float enemySpeed = 5.0f;

    // A reference to the Rigidbody2D component of the enemy game object
    private Rigidbody2D enemyRb;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get the player's position
        Vector2 playerPosition = player.transform.position;

        // Calculate the direction from the enemy to the player
        Vector2 direction = playerPosition - enemyRb.position;

        // Set the enemy's velocity in the direction of the player
        enemyRb.AddForce(direction * enemySpeed);
    }
}