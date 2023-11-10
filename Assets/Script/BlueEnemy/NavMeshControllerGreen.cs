using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshControllerGreen : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    private Animator anim;
    private Rigidbody rb;
    public bool canWalk;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canWalk)
        {

            Vector3 normalizedMovement = agent.desiredVelocity.normalized;

            Vector3 forwardVector = Vector3.Project(normalizedMovement, transform.forward);

            Vector3 rightVector = Vector3.Project(normalizedMovement, transform.right);

            float forwardVelocity = forwardVector.magnitude * Vector3.Dot(forwardVector, transform.forward);

            float rightVelocity = rightVector.magnitude * Vector3.Dot(rightVector, transform.right);
            anim.SetBool("Running", !thereYetGreen());
            anim.SetFloat("velY", forwardVelocity);
            anim.SetFloat("velX", rightVelocity);
        } else
        {
            anim.SetBool("Running", false);
        }
    }

    public void SetTarget(Vector3 newTarget)
    {
        agent.SetDestination(newTarget);
        agent.isStopped = false;
    }

    public void StopIT()
    {
        agent.isStopped = true;
    }

    public bool thereYet()
    {
        return agent.remainingDistance < agent.stoppingDistance && !agent.pathPending;
    }
    public bool thereYetGreen()
    {
        return agent.remainingDistance < agent.stoppingDistance + 5 && !agent.pathPending;
    }
}
