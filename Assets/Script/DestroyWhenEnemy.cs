using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenEnemy : MonoBehaviour
{
    public GameObject sceneEnemies;
    
    void Update()
    {
        if(sceneEnemies.transform.childCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
