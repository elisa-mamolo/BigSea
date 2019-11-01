using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    public float maxHealth, maxThirst, maxHunger, maxOxigen;
    public float thirstIncreaseRate, hungerIncreaseRate, oxigenDecreaseRate;
    public float health, thirst, hunger, oxigen;

    private bool playerDead;

    // Functions
    public  void Start()
    {
        health = maxHealth;
        oxigen = maxOxigen;
    }

    public void Update()
    {
        // thirst and  hunger increase
        if(!playerDead)
        {
            thirst += thirstIncreaseRate * Time.deltaTime;
            hunger += hungerIncreaseRate * Time.deltaTime;
            oxigen -= oxigenDecreaseRate * Time.deltaTime;
        }

        if (thirst >= maxThirst)
            Die();
        if (hunger >= maxHunger)
            Die();
        if (oxigen <= 0)
            Die();

    }


    public  void Die()
    {
        playerDead = true;
        
    }




}
