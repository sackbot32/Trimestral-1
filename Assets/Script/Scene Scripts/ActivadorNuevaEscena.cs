using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorNuevaEscena : MonoBehaviour
{
    public string nuevaescena;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CargadorEscena.cE.LoadNewScene(nuevaescena);
        }
    }
}
