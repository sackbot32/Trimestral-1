using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public bool debugRes;
    public Image healthBar;
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
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //Kill player
        }
        HealthBarUpdate();
    }

    public void Heal(int healing)
    {
        currentHealth += healing;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        HealthBarUpdate();
    }

    private void HealthBarUpdate()
    {
        healthBar.transform.localScale = new Vector3((float)currentHealth / (float)maxHealth, 1, 1);
    }
}
