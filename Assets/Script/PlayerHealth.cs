using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public bool canKillSelf;
    public Image healthBar;
    public GameObject pausaMenu;
    public GameObject gameOver;
    public InputActionReference pausa;
    public InputActionReference revive;
    public Shooting gun;
    private FirstPersonController controller;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<FirstPersonController>();
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
        if (pausa.action.WasPressedThisFrame() && currentHealth > 0)
        {
            pausaMenu.SetActive(!pausaMenu.activeSelf);
            if (pausaMenu.activeSelf)
            {
                gun.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                controller.cameraCanMove = false;
                Time.timeScale = 0;

            } else
            {
                gun.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                controller.cameraCanMove = true;
                Time.timeScale = 1;
            }
        }
        //Aqui iria revivir al morir, comprobar si el jugador tiene 0 de vida
        if (revive.action.WasPerformedThisFrame() && currentHealth <= 0)
        {
            Resurect();
        }
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gun.enabled = false;
            controller.cameraCanMove = false;
            pausaMenu.SetActive(false);
            currentHealth = 0;
            gameOver.SetActive(true);
            Time.timeScale = 0;
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

    private void Resurect()
    {
        
        Time.timeScale = 1;
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(CargadorEscena.cE.escenaPrincipal));
        gun.enabled = true;
        gameOver.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        controller.cameraCanMove = true;
        CargadorEscena.cE.ReCargarLasEscenas();
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pausaMenu.SetActive(false);
        controller.cameraCanMove = true;
        Time.timeScale = 1;
    }
}
