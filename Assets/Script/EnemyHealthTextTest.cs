using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthTextTest : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        //enemyHealth = transform.root.GetComponent<EnemyHealth>();
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Health:" + enemyHealth.GetCurrentHealth();
    }
}
