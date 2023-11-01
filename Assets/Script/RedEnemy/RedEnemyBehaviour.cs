using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemyBehaviour : MonoBehaviour
{
    public NavMeshController navMeshController;
    public RedEnemyShooting enemyShooting;
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


}
