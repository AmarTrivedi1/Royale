using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    void Start()
    {
        maxHealth = 300; // higher health
        currentHealth = maxHealth;
        moveSpeed = 1f; // Slower movement speed
        attackDamage = 75; // High attack damage
        attackCooldown = 5; // High cooldown 

        rb = GetComponent<Rigidbody2D>();
        target = FindNearestTower(); // Use the Enemy's method to find the nearest tower
        healthBar.setMaxHealth(maxHealth);
    }
}
