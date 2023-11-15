using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CargadorEscena : MonoBehaviour
{
    public static CargadorEscena cE;

    private void Awake()
    {
        cE = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Loading New Scene");
        SceneManager.LoadScene("Scene I", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
    }
}
