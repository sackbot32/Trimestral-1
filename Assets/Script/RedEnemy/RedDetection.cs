using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDetection : MonoBehaviour
{
    public RedEnemyBehaviour[] redEnemyBehaviours;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (RedEnemyBehaviour redEnemy in redEnemyBehaviours)
            {
                redEnemy.enemyShooting.canAim = true;
                redEnemy.navMeshController.canWalk = true;
                redEnemy.navMeshController.SetTarget(redEnemy.target);
            }

        }
    }
}
