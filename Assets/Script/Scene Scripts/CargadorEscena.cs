using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class CargadorEscena : MonoBehaviour
{
    public static CargadorEscena cE;

    public int sceneCount;

    public GameObject[] portales;
    

    private void Awake()
    {
        cE = this;
        SceneManager.LoadScene("Scene I");
    }
    public void CargandoEscena()
    {
        Debug.Log("Loading New Scene");
    }
    public void MasDeUnaEscena()
    {
        if (sceneCount > 1)
        {
            Debug.Log("More than one scene loaded")
;       }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CargarlasEscenas();
        }
        
    }

    public void CargarlasEscenas()
    {
        
    }
}
