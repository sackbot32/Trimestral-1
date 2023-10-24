using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthTextTest : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = transform.parent.transform.parent.GetComponent<EnemyHealth>();
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Health:" + enemyHealth.GetCurrentHealth();
    }
}
