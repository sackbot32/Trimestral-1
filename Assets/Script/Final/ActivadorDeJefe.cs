using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorDeJefe : MonoBehaviour
{
    public NavMeshControllerFinal navMesh;
    public FinalBossBeheavioru boss;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            navMesh.canWalk = true;
            StartCoroutine(boss.ChangeAll());
        }
    }
}
