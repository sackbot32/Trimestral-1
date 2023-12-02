using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActivateMeshOnCollide : MonoBehaviour
{
    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        agent.enabled = true;
    }
}
