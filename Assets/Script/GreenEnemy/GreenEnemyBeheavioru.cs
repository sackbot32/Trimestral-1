using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class GreenEnemyBeheavioru : MonoBehaviour
{
    public bool yet;
    public NavMeshControllerGreen navMeshController;
    public Transform target;
    private Rigidbody rb;
    public AimConstraint spine;
    private Animator animator;
    public ParticleSystem particle;
    public Collider attackCollider;
    // Start is called before the first frame update
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        navMeshController = GetComponent<NavMeshControllerGreen>();
        attackCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        yet = navMeshController.thereYet();
        if (navMeshController.canWalk)
        {
            spine.constraintActive = true;
            spine.rotationOffset = new Vector3(0, 0, 0);
            navMeshController.SetTarget(target.position);
        }
        if (navMeshController.canWalk && !navMeshController.thereYet())
        {
            animator.SetBool("Attack",false);
            //spine.constraintActive = false;
            //spine.transform.rotation = new Quaternion(0,0,0,0);
            navMeshController.resumeIT();
        }
        if (navMeshController.canWalk && navMeshController.thereYet())
        {
            animator.SetBool("Attack", true);
            rb.velocity = Vector3.zero;
            navMeshController.StopIT();
        }
    }
    
    public void Attack()
    {
        particle.Play();
        attackCollider.enabled = true;
        //Activar hitbox de ataque
    }

    public void EndAttack()
    {
        particle.Clear();
        attackCollider.enabled = false;
        //Desactivar hitbox de ataque
    }
}
