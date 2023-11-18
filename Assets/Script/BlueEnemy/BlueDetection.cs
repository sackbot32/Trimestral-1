using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDetection : MonoBehaviour
{
    public BlueEnemyShooting[] blueEnemyShootings;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
                foreach (BlueEnemyShooting blue in blueEnemyShootings)
            {
                if (blue != null){

                    blue.activateAim();
                }
            }
        }
    }
}
