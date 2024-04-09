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
    public int currentHealth; // Current health of the enemy
    public float moveSpeed = 2f; // Enemy default speed for noww
    private Rigidbody2D rb;
    private Transform target; // Reference to the target (Tower)
    public int playerNum = 1; // Is it a player 1 or player 2 card? Setting this to 1 manually for testing purposes, but will be set when player places cards.
    public float attackCooldown = 5; // Seconds between attacks
    public float attackCooldownTimer = 0; // Timer to track cooldown between attacks
    public int attackDamage = 10; // Damage dealt to the tower on each attack
    private bool isMoving = true; // Controls troop movement
    private Component attackTargetComponent = null; // Used to store the current target to attack (tower or enemy troop)
    private bool isAttacking = false;


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

        if (isMoving)
        {
            UpdateMovementTowardsTarget();
        }

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.fixedDeltaTime;
        }

        // Trigger the attack if in attacking state and cooldown has elapsed
        if (isAttacking && attackCooldownTimer <= 0)
        {
            Attack();
        }

        // Check if the target exists
        if (target != null)
        {
            // Decrement the cooldown timer

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

            isMoving = true;

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
            attackTargetComponent = collision.gameObject.GetComponent<Tower>(); // Set the attack target to the tower

            rb.velocity = Vector2.zero; // Stop the enemy when it collides with the tower
            isMoving = false; // Stop movement when colliding with a tower

            isAttacking = true;
            Debug.Log("Enemy stopping at tower");
        }

        // Implement else if here for troop/enemy targeting

    }

    /*
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tower") && attackCooldownTimer <= 0)
        {
            rb.velocity = Vector2.zero;
            isMoving = false; // Stop movement when colliding with a tower

            Tower tower = collision.gameObject.GetComponent<Tower>();
            if (tower != null)
            {
                Debug.Log("Enemy attacking tower");
                tower.TakeDamage(attackDamage); // Deal damage to the tower
                attackCooldownTimer = attackCooldown; // Reset the attack cooldown timer
            }
        }
    }
    */

    // When the enemy kills the target, and it's collider disappears
    void OnTriggerExit2D(Collider2D collision)
    {
        // If the exiting collider matches the current attack target
        if (collision.gameObject.GetComponent<Component>() == attackTargetComponent)
        {
            attackTargetComponent = null; // Reset the target
            isAttacking = false; // Stop attacking

            // Potentially, make the enemy start moving again, if desired

            Debug.Log("Enemy target lost");
        }
    }

    private void Attack()
    {
        if (attackTargetComponent != null)
        {
            if (attackTargetComponent is Tower tower)
            {
                Debug.Log("Enemy attacking tower");
                tower.TakeDamage(attackDamage);
            }

            // Implement else if here for troop/enemy attacking

            attackCooldownTimer = attackCooldown; // Reset the cooldown
        }

        else
        {
            isAttacking = false;
        }
    }

    private void UpdateMovementTowardsTarget()
    {
        if (target != null && isMoving)
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
