using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemyBeheavioru : MonoBehaviour
{
    public NavMeshControllerGreen navMeshController;
    public Transform target;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
        navMeshController = GetComponent<NavMeshControllerGreen>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshController.canWalk && !navMeshController.thereYet())
        {
            navMeshController.agent.speed = 3.5f;
            navMeshController.SetTarget(target.position);
        }
        if (navMeshController.thereYet())
        {
            navMeshController.agent.speed = 0;
            navMeshController.StopIT();
        }
    }
}
