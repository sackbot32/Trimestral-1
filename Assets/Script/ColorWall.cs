using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWall : MonoBehaviour
{
    public string color;
    public int health;

    public void takeDamage(int damage,string shotColor)
    {
        if(color == shotColor)
        {
            health -= damage;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
