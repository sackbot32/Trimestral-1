using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHit : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        print("choca con algo");
        if(other.GetComponent<PlayerHealth>() != null)
        {
            print("choca con jugador");
            other.GetComponent<PlayerHealth>().takeDamage(damage);
        }
    }
}
