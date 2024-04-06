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
        rb = GetComponent<Rigidbody2D>(); // Getting the bullet's rigid body component
        MoveBullet(); // Start the bullet movement
    }

    void Update()
    {

    }

    void MoveBullet()
    {
        // Calculate the direction from the turret to the enemy
        Vector3 directionToEnemy = GameObject.FindObjectOfType<Enemy>().transform.position - transform.position;
        directionToEnemy.Normalize();

        // Set the velocity based on the speed
        rb.velocity = directionToEnemy * speed;
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
            }
        }

        // Destroy the bullet upon collision with any object
        Destroy(gameObject);
    }
}
