using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FinalBossBeheavioru : MonoBehaviour
{
    public float timeBetweenChange;
    public int redDamage;
    public int blueDamage;
    private FinalEnemyShooting shooting;
    private NavMeshControllerFinal navMesh;
    private EnemyHealthFinal healthFinal;
    private string[] colorList = {"Red","Blue","Green"};
    private string currentColor;
    private Rigidbody rb;
    private Animator animator;
    private GameObject[] snipingPos;
    public Transform sword;
    public Vector3 originalPos;
    public Vector3 attackPos;
    public Quaternion originalRot;
    public Quaternion attackRot;
    public Collider attack;
    void Start()
    {
        snipingPos = GameObject.FindGameObjectsWithTag("SniperPos");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        shooting = GetComponent<FinalEnemyShooting>();
        navMesh = GetComponent<NavMeshControllerFinal>();
        healthFinal = GetComponent<EnemyHealthFinal>();
        currentColor = "Null";
        attack.enabled = false;

        //StartCoroutine(ChangeAll());
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentColor)
        {
            case "Red":
                animator.SetBool("Attack", false);
                if (navMesh.canWalk)
                {
                    navMesh.SetTarget(navMesh.target.position);
                }
                if (navMesh.canWalk && !navMesh.thereYet())
                {
                    navMesh.resumeIT();
                }
                if (navMesh.canWalk && navMesh.thereYet())
                {
                    rb.velocity = Vector3.zero;
                    navMesh.StopIT();
                }
                break;
            case "Blue":
                animator.SetBool("Attack", false);
                animator.SetBool("Running", false);
                if (navMesh.enabled)
                {
                    navMesh.StopIT();
                }
                break;
            case "Green":
                if (navMesh.canWalk)
                {
                    navMesh.resumeIT();
                    navMesh.SetTarget(navMesh.target.position);
                }
                if (navMesh.canWalk && !navMesh.thereYet())
                {
                    animator.SetBool("Attack", false);
                    navMesh.resumeIT();
                }
                if (navMesh.canWalk && navMesh.thereYet())
                {
                    animator.SetBool("Attack", true);
                    rb.velocity = Vector3.zero;
                    navMesh.StopIT();
                }
                break;


            default:
                break;
        }

        if (animator.GetBool("Attack"))
        {
            sword.localPosition = attackPos;
            sword.localRotation = attackRot;
        } else
        {
            sword.localPosition = originalPos;
            sword.localRotation = originalRot;
        }
    }

    public IEnumerator ChangeAll()
    {
        while(true)
        {
            currentColor = colorList[Random.Range(0, colorList.Length)];
            ChangeAim();
            ChangeBodyColor();
            ChangeTarget();
            yield return new WaitForSeconds(timeBetweenChange);
        }
    }

    private void ChangeAim()
    {
        switch (currentColor)
        {
            case "Red":
                shooting.damage = redDamage;
                shooting.activateAim();
                shooting.line.enabled = true;
                shooting.canAim = true;
                shooting.blue = false;
                break;
            case "Blue":
                shooting.damage = blueDamage;
                shooting.activateAim();

                shooting.line.enabled = true;
                shooting.canAim = true;
                shooting.blue = true;
                break;
            case "Green":
                shooting.deactivateAim();
                shooting.line.enabled = false;
                shooting.canAim = false;
                break;


            default:
                break;
        }
    }

    private void ChangeBodyColor()
    {
        switch (currentColor)
        {
            case "Red":
                healthFinal.color = currentColor;
                healthFinal.changeColor();
                break;
            case "Blue":
                healthFinal.color = currentColor;
                healthFinal.changeColor();
                break;
            case "Green":
                healthFinal.color = currentColor;
                healthFinal.changeColor();
                break;


            default:
                break;
        }
    }

    private void ChangeTarget()
    {
        switch (currentColor)
        {
            case "Red":
                navMesh.agent.enabled = true;
                navMesh.enabled = true;
                navMesh.target = snipingPos[Random.Range(0,snipingPos.Length)].transform;
                break;
            case "Blue":
                navMesh.agent.enabled = false;
                navMesh.enabled = false;
                transform.position = snipingPos[Random.Range(0, snipingPos.Length)].transform.position;
                break;
            case "Green":
                navMesh.agent.enabled = true;
                navMesh.enabled = true;
                navMesh.target = GameObject.FindGameObjectWithTag("Player").transform;
                break;


            default:
                break;
        }
    }
    public void ActivateAttack()
    {
        attack.enabled = true;
    }

    public void DeActivateAttack()
    {
        attack.enabled = false;
    }

}
