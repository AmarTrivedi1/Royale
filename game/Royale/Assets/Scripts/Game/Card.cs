using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string cardName; //The name of the unit the card represents.
    private int cardCost; //The amount of energy the card costs to play.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Returns the name of the card
    string getName()
    {
        return cardName;
    }

    //Sets the name of the card
    void setName(string name)
    {
        this.cardName = name;
    }

    //Returns the cost of the card
    int getCost()
    {
        return cardCost;
    }

    //Sets the cost of the card
    void setCost(int cost)
    {
        this.cardCost = cost;
    }
}
