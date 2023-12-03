using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerForAim : MonoBehaviour
{
    private Transform player;
    public float speed;
    public bool forBlue;
    public Shooting whichWeapon;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (forBlue)
            {

                //DontDestroyOnLoad(this.gameObject);
                //if (GameObject.FindGameObjectsWithTag("EnemyAimBlue").Length > 1)
                //{
                //    Destroy(this.gameObject);
                //}
                whichWeapon = GameObject.FindGameObjectWithTag("GunBarrel").GetComponent<Shooting>();
            } else
            {

                //DontDestroyOnLoad(this.gameObject);
                //if (GameObject.FindGameObjectsWithTag("EnemyAim").Length > 1)
                //{
                //    Destroy(this.gameObject);
                //}
            }
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {

            player = GameObject.FindGameObjectWithTag("Player").transform;
            whichWeapon = GameObject.FindGameObjectWithTag("GunBarrel").GetComponent<Shooting>();
            if (!forBlue)
            {
                if(whichWeapon != null && player != null)
                {
                    transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * speed *(transform.position - player.position).magnitude);
                }
            } else
            {
                //whichWeapon.weaponCharacteristic[whichWeapon.chosenWeapon]

                if(whichWeapon != null && player != null)
                {
                    if(whichWeapon.weaponCharacteristic[whichWeapon.chosenWeapon].color != "Green")
                    {
                        transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * speed * (transform.position - player.position).magnitude * 4f);
                
                    } else
                    {
                        transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * speed * (transform.position - player.position).magnitude / 2f);
                    }
                }
            }
        }
    }
}
