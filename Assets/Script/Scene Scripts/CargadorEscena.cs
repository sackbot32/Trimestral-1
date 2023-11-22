using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class CargadorEscena : MonoBehaviour
{
    public static CargadorEscena cE;

    public string escenaPrincipal;

    public string[] currentSceneList;

    public string[] startingSceneList;

    public int sceneCount;

    public bool first;
    

    private void Awake()
    {
        cE = this;
        if (GameObject.FindGameObjectsWithTag("SceneManager").Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            if (!first)
            {
                print("llega aqui");
                CargarlasEscenas(startingSceneList);
                first = true;
            }
        }


    }

    private void OnEnable()
    {
        
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

    public void CargarlasEscenas(string[] listaDeEscenas)
    {
        currentSceneList = listaDeEscenas;
        foreach (Scene escenaActiva in getAllActiveScenes())
        {
            print("tenemos escenas activas");
            bool estaEnLaLista = false;
            foreach (string escena in listaDeEscenas)
            {
                if(escenaActiva.name == escena)
                {
                    estaEnLaLista = true;
                }
            }
            if (!estaEnLaLista && escenaActiva.name != escenaPrincipal)
            {
                SceneManager.UnloadSceneAsync(escenaActiva);
                print("eliminamos escena" + escenaActiva.name);
            }
        }
        foreach (string escena in listaDeEscenas)
        {
            if (!SceneManager.GetSceneByName(escena).isLoaded)
            {
                SceneManager.LoadScene(escena, LoadSceneMode.Additive);
            }
        }
    }

    public void ReCargarLasEscenas()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(escenaPrincipal);
        foreach (string escena in currentSceneList)
        {
           print("llega");
           SceneManager.LoadScene(escena, LoadSceneMode.Additive);
            
        }
    }

    private Scene[] getAllActiveScenes()
    {
        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
        }

        return loadedScenes;
    }
}
