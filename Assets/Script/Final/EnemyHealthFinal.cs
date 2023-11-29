using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthFinal : MonoBehaviour
{
    public string color;
    public int startingHealth;
    private int currentHealth;
    public bool debugRes;
    public string[] debugColorChange;
    public int debugColorChangeNumber;
    public SkinnedMeshRenderer meshRenderer;
    private Animator anim;
    public Transform healthBar;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if(debugColorChange.Length > 0)
        {
            color = debugColorChange[debugColorChangeNumber];
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

        }
    }

    // Update is called once per frame
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void GetHit(int damage, string hitColor)
    {

        currentHealth -= calculateDamage(damage,hitColor);
        healthBar.localScale = new Vector3(currentHealth/startingHealth,1,1);

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

    private int calculateDamage(int damage, string hitColor)
    {
        int newDamage = 0;
        switch (color)
        {
            case "Red":
                switch (hitColor)
                {
                    case "Red":
                        newDamage = damage;
                        break;
                    case "Blue":
                        newDamage = damage / 4;
                        break;
                    case "Green":
                        newDamage = damage / 4;
                        break;

                    default:
                        break;
                }
                break;
            case "Blue":
                switch (hitColor)
                {
                    case "Red":
                        newDamage = damage / 2;
                        break;
                    case "Blue":
                        newDamage = damage;
                        break;
                    case "Green":
                        newDamage = damage / 4;
                        break;

                    default:
                        break;
                }
                break;
            case "Green":
                switch (hitColor)
                {
                    case "Red":
                        newDamage = damage / 4;
                        break;
                    case "Blue":
                        newDamage = damage / 4;
                        break;
                    case "Green":
                        newDamage = damage;
                        break;

                    default:
                        break;
                }
                break;
            case "Null":
                switch (hitColor)
                {
                    case "Red":
                        newDamage = damage / 4;
                        break;
                    case "Blue":
                        newDamage = damage / 4;
                        break;
                    case "Green":
                        newDamage = damage / 4;
                        break;

                    default:
                        break;
                }
                break;
            default:
                break;
        }
        return newDamage;
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
                if (GetComponent<GreenEnemyBeheavioru>() != null)
                {
                    GetComponent<NavMeshControllerGreen>().StopIT();
                }
                break;

            default:
                break;
        }
        anim.SetBool("Dead",true);
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
    }

    public void changeColor()
    {
        switch (color)
        {
            case "Red":
                meshRenderer.material.SetColor("_BaseColor", new Color(1, 0, 0));
                break;
            case "Blue":
                meshRenderer.material.SetColor("_BaseColor", new Color(0, 0, 1));
                break;
            case "Green":
                meshRenderer.material.SetColor("_BaseColor", new Color(0, 1, 0));
                break;
            case "Null":
                meshRenderer.material.SetColor("_BaseColor", new Color(0.4762838f, 0, 0.7075472f));
                break;

            default:
                break;
        }
    }
}
