using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeactivateKinematicWhenEnemyDead : MonoBehaviour
{
    public GameObject sceneEnemies;
    public Rigidbody body;

    void Update()
    {
        if (sceneEnemies.transform.childCount <= 0)
        {
            body.isKinematic = false;
        }
    }
}
