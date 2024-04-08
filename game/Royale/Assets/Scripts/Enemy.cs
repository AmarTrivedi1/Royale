//*********************************************************************
//NOTE:
/* Enemy functionality updated to follow its nearest tower horizontally automatically. Towers detect it and 
attack it depending on its range. Enemy takes damage from the tower shot object and dies when health drops.
ATTACH 2d Collider component to
*/
//*********************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the enemy
    private int currentHealth; // Current health of the enemy
    public float moveSpeed = 2f; // Enemy default speed for noww
    private Rigidbody2D rb;
    private Transform target; // Reference to the target (Tower)
    public int playerNum = 1; // Is it a player 1 or player 2 card? Setting this to 1 manually for testing purposes, but will be set when player places cards.
    public float attackCooldown = 5; // Seconds between attacks
    private float attackCooldownTimer = 0; // Timer to track cooldown between attacks
    public int attackDamage = 10; // Damage dealt to the tower on each attack


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find the nearest tower
        target = FindNearestTower();

        // Initialize current health to max health
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        UpdateMovementTowardsTarget();
        // Check if the target exists
        if (target != null)
        {
            // Recalculate the direction towards the target 
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 targetPosition = new Vector2(target.position.x, target.position.y);
            Vector2 direction = (targetPosition - currentPosition).normalized;

            // Move towards the target 
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

            // Decrement the cooldown timer
            if (attackCooldownTimer > 0)
            {
                attackCooldownTimer -= Time.fixedDeltaTime;
            }
        }
        else
        {
            // If there's no current target, find a new one
            target = FindNearestTower();
            if (target == null)
            {
                // Optionally handle the case where there are no more towers
                rb.velocity = Vector2.zero; // Stop moving if there are no targets
            }
        }
    }


    // Find the nearest tower to the enemy
    Transform FindNearestTower()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        Transform nearestTower = null;
        float shortestDistance = Mathf.Infinity;
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

        foreach (GameObject tower in towers)
        {
            Tower towerScript = tower.GetComponent<Tower>(); // Access the Tower script attached to the Tower GameObject
            if (towerScript != null && towerScript.playerNum != this.playerNum) // Ensure tower belongs to a different player
            {
                // Calculate the distance to each tower
                Vector2 directionToTower = new Vector2(tower.transform.position.x, tower.transform.position.y) - currentPosition;
                float distance = directionToTower.magnitude;

                // Check if this tower is closer than the previous closest tower
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestTower = tower.transform;
                }
            }

        }
        return nearestTower;
    }

    // Method to receive damage
    public void TakeDamage(int damage)
    {
        // Subtract the damage from enemies current health
        currentHealth -= damage;

        // Check if the enemy's health is depleted
        if (currentHealth <= 0)
        {
            Die(); // Enemy dies if the health drops below 0
        }
    }

    // Method to handle the enemy or troops death
    void Die()
    {
        Destroy(gameObject); // Destroy the enemy GameObject
    }

    // Method to handle tower destruction
    public void TowerDestroyed()
    {
        target = FindNearestTower();
        if (target != null)
        {
            /* Immediately update movement towards the new target
               Had to reimplement the velocity logic for the enemy tower tracking.
               For some reason the enemy velocity loads simultaneously with the enemy tracking and
               The velocity does not initialize when the new target is acquired. */
            UpdateMovementTowardsTarget();
        }
    }


    // Collision detection with tower
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            // Stop the enemy when it collides with the tower
            rb.velocity = Vector2.zero;

            Debug.Log("Enemy stopping at tower");
        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tower") && attackCooldownTimer <= 0)
        {
            Tower tower = collision.gameObject.GetComponent<Tower>();
            if (tower != null)
            {
                Debug.Log("Enemy attacking tower");
                tower.TakeDamage(attackDamage); // Deal damage to the tower
                attackCooldownTimer = attackCooldown; // Reset the attack cooldown timer
            }
        }
    }

    private void UpdateMovementTowardsTarget()
    {
        if (target != null)
        {
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = target.position;
            Vector2 direction = (targetPosition - currentPosition).normalized;

            rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero; 
        }
    }

}
