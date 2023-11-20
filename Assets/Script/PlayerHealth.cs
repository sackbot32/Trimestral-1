using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public bool canKillSelf;
    public Image healthBar;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Destroy(this.gameObject);
        }
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            TestKill();
        }
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //Kill player
        }
        HealthBarUpdate();
    }

    public void Heal(int healing)
    {
        currentHealth += healing;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        HealthBarUpdate();
    }

    private void HealthBarUpdate()
    {
        healthBar.transform.localScale = new Vector3((float)currentHealth / (float)maxHealth, 1, 1);
    }

    private void TestKill()
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(CargadorEscena.cE.escenaPrincipal));
        CargadorEscena.cE.ReCargarLasEscenas();
    }
}
