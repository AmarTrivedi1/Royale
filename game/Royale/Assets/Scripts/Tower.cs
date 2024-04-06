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
            //Loop through all current units on board (Using find objects with tags may need to be changed once units are working)
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
            {
                //Once a unit is within the turrets attack range (Using find objects with tag may need to be changed once units are working)
                if (Vector2.Distance(this.transform.position, GameObject.FindGameObjectsWithTag("Enemy")[i].transform.position) < attackRange)
                {
                    enemyTargeted = true;
                    //This is setting targeted enemy, will likely be changed once units are working.
                    targetedEnemy = GameObject.FindObjectsByType<Enemy>(FindObjectsSortMode.None)[i];
                }
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
    }

    void Die()
    {
        //Destroy the turret once it is dead
        Destroy(gameObject);
    }
}
