using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemyBehaviour : MonoBehaviour
{
    private NavMeshController navMeshController;
    private RedEnemyShooting enemyShooting;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        navMeshController = GetComponent<NavMeshController>();
        enemyShooting = GetComponent<RedEnemyShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyShooting.canAim = true;
            navMeshController.canWalk = true;
            navMeshController.SetTarget(target);

        }
    }

}
