using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //All set to public so they can be changed in unity editor (not sure how to make them private but still visible to the editor)
    public int health = 0; //The turrets health.
    public float attackSpeed = 0; //The attack speed for each turret in seconds.
    public float timeUntilAttack; //The turret's attack cool down.
    public int attackRange = 0; //The attack range for each turret in units (Have to play around with the units to find a good range).
    public string position = ""; //Whether the turret is in the top or bottom lane.
    public GameObject shotPrefab; //The bullet prefab (Found in the editor assets/prefabs folder.

    private Enemy targetedEnemy; //The targeted enemy unit.
    private bool enemyTargeted = false; //Whether there is an active targeted enemy unit.

    public int playerNum = 2; // Player 1 or Player 2 tower. Used for card/enemy targeting.

    // Start is called before the first frame update
    void Start()
    {
        timeUntilAttack = attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Once an enemy is targeted it should not switch targets until the current target has been destroyed
        if (enemyTargeted && targetedEnemy != null)
        {
            //Count down from the attack speed
            timeUntilAttack -= Time.deltaTime;
            //Once the count down is finished
            if (timeUntilAttack <= 0.0f)
            {
                //Attack and set the timer back to the attack speed
                Attack();
                timeUntilAttack = attackSpeed;
                //health -= 10; //This is just to test killing the turret (Most likely to be implemented in another way once units are working)
            }
        }
        //If there is no current target
        else
        {

            // Find all enemy game objects and save to this array
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float closestDistance = Mathf.Infinity;
            GameObject closestEnemy = null;

            // Loop through each enemy
            foreach (GameObject enemyGameObject in enemies)
            {
                // Get the Enemy.cs public vars
                Enemy enemy = enemyGameObject.GetComponent<Enemy>();
                // If the enemy is not the same player number as the Tower, set it to be the closest enemy on the board
                if (enemy != null && enemy.playerNum != this.playerNum)
                {
                    float distance = Vector2.Distance(transform.position, enemyGameObject.transform.position);
                    if (distance < closestDistance && distance < attackRange)
                    {
                        closestDistance = distance;
                        closestEnemy = enemyGameObject;
                    }
                }
            }

            // Set the closest enemy to the be the target.
            if (closestEnemy != null)
            {
                targetedEnemy = closestEnemy.GetComponent<Enemy>();
                enemyTargeted = true;
            }


        }

        //If the turret has 0 health, destroy it.
        if (health <= 0)
        {
            Die();
        }
    }

    void Attack()
    {
        //Create a turret shot (check TowerShot.cs for bullet code).
        GameObject towerShot = Instantiate(shotPrefab, transform.position, Quaternion.identity);

        // Passing the value for the target to the towershot
        TowerShot towerShotScript = towerShot.GetComponent<TowerShot>();
        if (targetedEnemy != null && towerShotScript != null)
        {
            towerShotScript.SetTarget(targetedEnemy.transform.position);
        }
    }

    void Die()
    {
        //Destroy the turret once it is dead
        Destroy(gameObject);
    }
}
