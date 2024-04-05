using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerShot : MonoBehaviour
{
    float speed = 3; //Turret shot speed
    Rigidbody2D rb; //The bullets rigid body (handles the physics)
    Vector2 directionToEnemy; //Which direction the turret has to shoot (mainly used for shooting left or right but also accounts of height)
    Vector2 velocity; //The velocity of the bullet

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>(); //Getting the bullets rigid body component
        //Calculation for the direction from the turret to the enemy
        directionToEnemy = GameObject.FindObjectOfType<Enemy>().transform.position - gameObject.transform.position;
        directionToEnemy.Normalize();
        //Setting the velocity based on the speed
        velocity = directionToEnemy * speed;
        rb.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //This is called when a collision component enters another
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroys the bullet once it hits a unit.
        Destroy(gameObject);
    }
}
