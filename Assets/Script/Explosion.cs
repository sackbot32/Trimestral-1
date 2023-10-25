using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage;
    public float maxSize;
    public float howMuchGrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x < maxSize)
        {
            transform.localScale += new Vector3(howMuchGrow,howMuchGrow,howMuchGrow)*Time.deltaTime;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().GetHit(damage,"Red");
        }
        if (other.GetComponent<Rigidbody>())
        {

        }
    }
}
