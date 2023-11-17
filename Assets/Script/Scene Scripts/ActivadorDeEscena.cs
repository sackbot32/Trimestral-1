using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorDeEscena : MonoBehaviour
{
    public string[] nombresDeEscenas;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CargadorEscena.cE.CargarlasEscenas(nombresDeEscenas);
        }
    }
}
