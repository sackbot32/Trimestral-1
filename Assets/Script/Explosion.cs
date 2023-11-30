using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage;
    public float maxSize;
    public float howMuchGrow;
    public float force;
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
            if(other.GetComponent<EnemyHealth>() != null)
            {
                other.GetComponent<EnemyHealth>().GetHit(damage,"Red");
            } else
            {
                if(other.transform.GetComponent<EnemyHealthFinal>() != null)
                {
                    other.transform.GetComponent<EnemyHealthFinal>().GetHit(damage, "Red");
                }
            }
        }
        if (other.GetComponent<Rigidbody>())
        {
            //Vector3 dir = transform.position - other.transform.position;

            other.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, transform.localScale.x);
        } else
        {
            if (other.transform.root.GetComponent<Rigidbody>())
            {
                other.transform.root.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, transform.localScale.x);
            }
        }
    }
}
