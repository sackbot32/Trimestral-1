using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNoParent : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.parent == null)
        {
            Invoke("SelfDestruct", 1);
        }
    }
    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
