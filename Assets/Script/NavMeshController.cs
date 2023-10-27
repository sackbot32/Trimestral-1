using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public bool workAsap;
    public Transform target;
    private NavMeshAgent agent;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            anim.SetBool("Running", false);
        } else
        {
            anim.SetBool("Running", true);
        }
    }
}
