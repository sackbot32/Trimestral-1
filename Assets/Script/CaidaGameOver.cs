using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaGameOver : MonoBehaviour
{
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.takeDamage(damage);

        }
        if (other.tag == "Enemy")
        {
            enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.GetHit(damage, enemyHealth.color);
        }
    }
}
