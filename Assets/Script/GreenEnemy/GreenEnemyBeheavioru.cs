using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class GreenEnemyBeheavioru : MonoBehaviour
{
    public NavMeshControllerGreen navMeshController;
    public Transform target;
    private Rigidbody rb;
    public AimConstraint spine;
    private Animator animator;
    public ParticleSystem particle;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
            animator.SetBool("Attack",false);
            spine.constraintActive = false;
            spine.transform.rotation = new Quaternion(0,0,0,0);
            navMeshController.agent.speed = 3.5f;
            navMeshController.SetTarget(target.position);
        }
        if (navMeshController.thereYet())
        {
            navMeshController.agent.speed = 0;
            animator.SetBool("Attack", true);
            spine.constraintActive = true;
            spine.rotationOffset = new Vector3(0, 0, 0);
            //rb.velocity = Vector3.zero;
            navMeshController.StopIT();
        }
    }
    
    public void Attack()
    {
        particle.Play();
        //Activar hitbox de ataque
    }

    public void EndAttack()
    {
        particle.Clear();
        //Desactivar hitbox de ataque
    }
}
