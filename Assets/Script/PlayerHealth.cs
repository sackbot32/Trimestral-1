using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int currentHealth;
    public int maxHealth;
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            //Kill player
        }
    }

    public void Heal(int healing)
    {
        currentHealth += healing;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
