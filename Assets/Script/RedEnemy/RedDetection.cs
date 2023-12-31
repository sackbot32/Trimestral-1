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
            if(redEnemyBehaviours.Length > 0)
            {
                foreach (RedEnemyBehaviour redEnemy in redEnemyBehaviours)
                {
                    if(redEnemy != null)
                    {
                        redEnemy.transform.GetComponent<RedEnemyShooting>().activateAim();
                        redEnemy.navMeshController.canWalk = true;
                        redEnemy.navMeshController.SetTarget(redEnemy.target.position);
                    }
                }
            }

        }
    }
}
