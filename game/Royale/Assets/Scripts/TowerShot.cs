using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerShot : MonoBehaviour
{
    public int damage = 10; // Damage inflicted by the bullet (adjust as needed)
    public float speed = 3; // Turret shot speed
    private Rigidbody2D rb; // The bullet's rigid body

    void Start()
    {

    }

    void Update()
    {
        //Incase the bullets miss their target, destory them based on distance to clear memory (20 can be played around with).
        if (Vector2.Distance(transform.position, transform.parent.position) > 20)
        {
            Destroy(gameObject);
        }
    }

    // Is called from the Tower.cs script when the TowerShot is instantiated. Set's the target for the shot
    public void SetTarget(Vector2 targetPosition)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>(); // Ensure rb is set
        Vector2 directionToTarget = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = directionToTarget * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet collides with an enemy
        if (collision.CompareTag("Enemy"))
        {
            // Get the Enemy component from the collided GameObject
            Enemy enemy = collision.GetComponent<Enemy>();

            // Check if the enemy component exists
            if (enemy != null)
            {
                // Inflict damage to the enemy
                enemy.TakeDamage(damage);

                // Destroy the bullet upon collision with an enemy
                Destroy(gameObject);
            }
        }

        
    }
}
