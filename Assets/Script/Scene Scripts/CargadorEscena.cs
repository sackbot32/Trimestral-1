using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class CargadorEscena : MonoBehaviour
{
    public static CargadorEscena cE;

    public string escenaPrincipal;

    //public string[] currentSceneList;

    public string[] startingSceneList;

    public int sceneCount;

    public SceneObject sceneList;

    private void Awake()
    {
        cE = this;
        //print("Es null?= " + sceneList.SceneList == null ? "si" : "no");
        if(sceneList.SceneList != null)
        {
            startingSceneList = sceneList.SceneList;
        }
        if (GameObject.FindGameObjectsWithTag("SceneManager").Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            if (sceneList.SceneList != null)
            {
                CargarlasEscenas(sceneList.SceneList);
            } else
            {
                CargarlasEscenas(startingSceneList);
            }
        }


    }

    public void CargarlasEscenas(string[] listaDeEscenas)
    {
        sceneList.SceneList = listaDeEscenas;
        foreach (Scene escenaActiva in getAllActiveScenes())
        {
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
        startingSceneList = sceneList.SceneList;
        Time.timeScale = 1;
        SceneManager.LoadScene(escenaPrincipal);
        foreach (string escena in sceneList.SceneList)
        {
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

    public void LoadNewScene(string nuevaEscena)
    {
        //if (GameObject.FindGameObjectWithTag("Player") != null)
        //{
        //    SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("Player"), SceneManager.GetActiveScene());
        //}
        //if (GameObject.FindGameObjectWithTag("EnemyAim") != null)
        //{
        //    SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("EnemyAim"), SceneManager.GetActiveScene());
        //}
        //if (GameObject.FindGameObjectWithTag("EnemyAimBlue") != null)
        //{
        //    SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("EnemyAimBlue"), SceneManager.GetActiveScene());
        //}
        //if (GameObject.FindGameObjectWithTag("SceneManager") != null)
        //{
        //    SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("SceneManager"), SceneManager.GetActiveScene());
        //}
        //if(gameObject != null)
        //{
        //    SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        //}
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("EnemyAim"));
        Destroy(GameObject.FindGameObjectWithTag("EnemyAimBlue"));
        Destroy(GameObject.FindGameObjectWithTag("SceneManager"));
        Time.timeScale = 1;
        SceneManager.LoadScene(nuevaEscena);


    }
    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
