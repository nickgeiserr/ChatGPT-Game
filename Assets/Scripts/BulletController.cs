using UnityEngine;

public class BulletController : MonoBehaviour
{
    // A reference to the Bullet component
    public float damage = 5f;
    public GameObject explosion;

    // A function to handle collision events with triggers
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check if the colliding object is an enemy
        if (col.gameObject.CompareTag($"Enemy"))
        {
            Debug.Log("Hit enemy");
            // Get a reference to the Enemy component of the colliding object
            Debug.Log(col.gameObject.name);
            EnemyAI enemy = col.gameObject.GetComponent<EnemyAI>();
            Debug.Log("EnemyAI component:" + enemy );

            // Apply the bullet's damage to the enemy's health
            enemy.TakeDamage(damage);

            Instantiate(explosion, col.transform.position, Quaternion.identity);
            return;
        }
        // Check if the colliding object is an enemy
        if (col.gameObject.CompareTag($"Player"))
        {
            Debug.Log("hit player");

            PlayerController pc = col.gameObject.GetComponent<PlayerController>();

            if (pc == null)
            {
                return;
            }

            pc.Health -= damage;
            
            Instantiate(explosion, col.transform.position, Quaternion.identity);
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}