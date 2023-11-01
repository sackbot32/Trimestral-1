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
    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.R))
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
                Destroy(gameObject);
            }
        }
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
