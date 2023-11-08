using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public string color;
    public int startingHealth;
    private int currentHealth;
    public bool debugRes;
    public string[] debugColorChange;
    public int debugColorChangeNumber;
    private MeshRenderer meshRenderer;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (GetComponent<MeshRenderer>() != null)
        {
           meshRenderer = GetComponent<MeshRenderer>();
        }
        if(debugColorChange.Length > 0)
        {
            color = debugColorChange[debugColorChangeNumber];
        }
        if (GetComponent<MeshRenderer>() != null)
        {
            changeColor(color);
        }
        currentHealth = startingHealth;
    }

    private void Update()
    {
        //Debug Change Color
        if (Input.GetKeyDown(KeyCode.R) && debugRes)
        {
            debugColorChangeNumber = (debugColorChangeNumber + 1) % debugColorChange.Length;
            color = debugColorChange[debugColorChangeNumber];
            if (GetComponent<MeshRenderer>() != null)
            {
                changeColor(color);
            }

        }
    }

    // Update is called once per frame
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void GetHit(int damage, string hitColor)
    {

        if(hitColor == color)
        {
            currentHealth -= damage;
        } else
        {
            currentHealth -= damage - damage/2;
        }

        if(currentHealth <= 0)
        {
            if (debugRes)
            {
                currentHealth = startingHealth;
            } else
            {
                if (!anim.GetBool("Dead"))
                {
                    StartCoroutine(Death());
                }
            }
        }
    }

    IEnumerator Death()
    {
        switch (color)
        {
            case "Red":
                if(GetComponent<RedEnemyShooting>() != null)
                {
                    GetComponent<RedEnemyShooting>().deactivateAim();
                    GetComponent<NavMeshController>().StopIT();
                }
                break;
            case "Blue":
                if (GetComponent<BlueEnemyShooting>() != null)
                {
                    GetComponent<BlueEnemyShooting>().deactivateAim();
                }
                break;
            case "Green":

                break;

            default:
                break;
        }
        anim.SetBool("Dead",true);
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
    }

    private void changeColor(string color)
    {
        switch (color)
        {
            case "Red":
                meshRenderer.material.color = new Color(1, 0, 0);
                break;
            case "Blue":
                meshRenderer.material.color = new Color(0, 0, 1);
                break;
            case "Green":
                meshRenderer.material.color = new Color(0, 1, 0);
                break;

            default:
                break;
        }
    }
}
