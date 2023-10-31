using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        SetTarget(target);
        anim.SetBool("Running", !thereYet());
    }

    // Update is called once per frame
    void Update()
    {

        anim.SetBool("Running", !thereYet());
    }

    public void SetTarget(Transform newTarget)
    {
        agent.SetDestination(newTarget.position);
        agent.isStopped = false;
    }

    public void StopIT()
    {
        agent.isStopped = true;
    }

    public bool thereYet()
    {
        return agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending;
    }
}
