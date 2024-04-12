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
    public Rigidbody2D rb;
    public Transform target; // Reference to the target (Tower)
    public int playerNum = 1; // Is it a player 1 or player 2 card? Setting this to 1 manually for testing purposes, but will be set when player places cards.
    public float attackCooldown = 5; // Seconds between attacks
    public float attackCooldownTimer = 0; // Timer to track cooldown between attacks
    public int attackDamage = 10; // Damage dealt to the tower on each attack
    private bool isMoving = true; // Controls troop movement
    private Component attackTargetComponent = null; // Used to store the current target to attack (tower or enemy troop)
    private bool isAttacking = false;
    public int elixirCost = 3; // Default to 3. Should be overwritten in all child classes


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

        // If no current tower target exists
        if (target == null)
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
    public Transform FindNearestTower()
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


    // Collision detection with tower - targeting system for attacking, and movement stopping
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAttacking)
        {
            // Tower targeting and movement stopping
            if (collision.gameObject.CompareTag("Tower"))
            {

                // Implement the same logic in the enemy targeting to ensure troops can never attack their own tower
                Tower tower = collision.gameObject.GetComponent<Tower>();
                if (tower.playerNum != this.playerNum)
                {
                    attackTargetComponent = collision.gameObject.GetComponent<Tower>(); // Set the attack target to the tower

                    rb.velocity = Vector2.zero; // Stop the enemy when it collides with the tower
                    isMoving = false; // Stop movement when colliding with a tower

                    isAttacking = true;
                    Debug.Log("Enemy stopping at tower");
                }

            }

            // Enemy troop targeting  
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                // Check if it is an opposing player troop before setting them as an attackTarget.
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy.playerNum != this.playerNum)
                {
                    attackTargetComponent = collision.gameObject.GetComponent<Enemy>(); // Set the attack target to the enemy

                    rb.velocity = Vector2.zero; // Stop the enemy when it collides with the other troop enemy
                    isMoving = false; // Stop movement when colliding with enemy

                    isAttacking = true;
                    Debug.Log("Enemy fighting a troop");
                }

            }
        }

    }


    // When the enemy kills the target, and it's collider disappears
    void OnTriggerExit2D(Collider2D collision)
    {
        // If the exiting collider matches the current attack target
        if (collision.gameObject.GetComponent<Component>() == attackTargetComponent)
        {
            attackTargetComponent = null; // Reset the target
            isAttacking = false; // Stop attacking

            // Make the enemy start moving again
            isMoving = true;
            FindAndMoveToNewTarget();

            Debug.Log("Enemy target lost");
        }
    }

    // Replaced the OnTriggerStay2D. There were collider issues, so I moved attacking into it's own function.
    private void Attack()
    {
        bool targetDefeated = false;

        if (attackTargetComponent != null)
        {
            // Attack enemy towers
            if (attackTargetComponent is Tower tower)
            {
                Debug.Log("Enemy attacking tower");
                tower.TakeDamage(attackDamage);

                // Check if the tower has been defeated
                targetDefeated = tower.health <= 0;
            }

            // Attack enemy troops
            else if (attackTargetComponent is Enemy enemy)
            {
                Debug.Log("Enemy attacking troop");
                enemy.TakeDamage(attackDamage);

                // Check if the enemy troop has been defeated
                targetDefeated = enemy.currentHealth <= 0;
            }

            attackCooldownTimer = attackCooldown; // Reset the cooldown
        }

        else
        {
            isAttacking = false;
        }

        // Check if the current attack target is still valid
        if (attackTargetComponent == null || attackTargetComponent.gameObject == null || targetDefeated)
        {
            attackTargetComponent = null; // Clear the target if it's defeated
            isAttacking = false; // Stop attacking mode
            FindAndMoveToNewTarget();
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

    private void FindAndMoveToNewTarget()
    {
        target = FindNearestTower(); // Attempt to find a new tower target
        if (target != null)
        {
            isMoving = true; // Resume movement if a new target is found
            UpdateMovementTowardsTarget();
            Debug.Log("Moving to new target");
        }
        else
        {
            rb.velocity = Vector2.zero;
            isMoving = false; // Stay put if no new target is available
            Debug.Log("No new targets available");
        }
    }

}
