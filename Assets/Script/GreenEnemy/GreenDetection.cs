using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenDetection : MonoBehaviour
{
    public GreenEnemyBeheavioru[] greens;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GreenEnemyBeheavioru greenEnemy in greens)
            {
                if(greenEnemy != null)
                {
                    greenEnemy.navMeshController.SetTarget(greenEnemy.target.position);
                    greenEnemy.navMeshController.canWalk = true;
                }
            }

        }
    }
}
