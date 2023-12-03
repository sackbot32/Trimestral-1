using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorNuevaEscena : MonoBehaviour
{
    public string nuevaescena;
    private ResetLoad reset;

    private void Start()
    {
        reset = gameObject.GetComponent<ResetLoad>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            reset.Reseteo();
            CargadorEscena.cE.LoadNewScene(nuevaescena);
        }
    }
}
