using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class CargadorEscena : MonoBehaviour
{
    public static CargadorEscena cE;

    public string[] currentSceneList;

    public int sceneCount;
    

    private void Awake()
    {
        cE = this;
        if(currentSceneList.Length == 0)
        {
            SceneManager.LoadScene("Scene I", LoadSceneMode.Additive);
        } else
        {
            CargarlasEscenas(currentSceneList);
        }
        
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
            if (!estaEnLaLista && escenaActiva.name != "Principal Scene")
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
