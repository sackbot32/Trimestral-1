using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLoad : MonoBehaviour
{
    public SceneObject tutorial;
    public SceneObject enMedio;
    public SceneObject final;
    // Start is called before the first frame update
    public void Reseteo()
    {
        string[] escenaTutorial = { "Tutorial1", "Tutorial2" };
        tutorial.SceneList = escenaTutorial;
        string[] escenaMedio = { "Enmedio1", "Enmedio2" };
        enMedio.SceneList = escenaMedio;
        string[] escenaFinal = { "EscenaFinal" };
        final.SceneList = escenaFinal;
    }
}
