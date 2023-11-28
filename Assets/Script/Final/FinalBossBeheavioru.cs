using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FinalBossBeheavioru : MonoBehaviour
{
    private FinalEnemyShooting shooting;
    private NavMeshControllerFinal navMesh;
    private EnemyHealthFinal healthFinal;
    private string[] colorList = {"Red","Blue","Green"};
    private string currentColor;
    private Rigidbody rb;
    private Animator animator;
    private GameObject[] snipingPos;
    void Start()
    {
        snipingPos = GameObject.FindGameObjectsWithTag("SniperPos");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        shooting = GetComponent<FinalEnemyShooting>();
        navMesh = GetComponent<NavMeshControllerFinal>();
        healthFinal = GetComponent<EnemyHealthFinal>();
        currentColor = "Null";
        healthFinal.changeColor();
        StartCoroutine(ChangeAll());
        //Activadores
        navMesh.canWalk = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentColor)
        {
            case "Red":
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
                navMesh.StopIT();
                break;
            case "Green":
                if (navMesh.canWalk)
                {
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
    }

    IEnumerator ChangeAll()
    {
        while(true)
        {
            currentColor = colorList[Random.Range(0, colorList.Length)];
            ChangeAim();
            ChangeBodyColor();
            ChangeTarget();
            yield return new WaitForSeconds(10f);
        }
    }

    private void ChangeAim()
    {
        switch (currentColor)
        {
            case "Red":
                shooting.canAim = true;
                shooting.blue = false;
                break;
            case "Blue":
                shooting.canAim = true;
                shooting.blue = true;
                break;
            case "Green":
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
                navMesh.target = snipingPos[Random.Range(0,snipingPos.Length)].transform;
                break;
            case "Blue":
                transform.position = snipingPos[Random.Range(0, snipingPos.Length)].transform.position;
                break;
            case "Green":
                navMesh.target = GameObject.FindGameObjectWithTag("Player").transform;
                break;


            default:
                break;
        }
    }

}
