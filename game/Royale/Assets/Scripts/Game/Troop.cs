using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : Enemy
{
    void Start()
    {
        maxHealth = 100; // higher health
        currentHealth = maxHealth;
        moveSpeed = 5f; // Slower movement speed
        attackDamage = 10; // High attack damage
        attackCooldown = 1; // High cooldown 

        rb = GetComponent<Rigidbody2D>();
        target = FindNearestTower(); // Use the Enemy's method to find the nearest tower
    }
 
}
