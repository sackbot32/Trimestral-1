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
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!forBlue)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * speed *(transform.position - player.position).magnitude);
        } else
        {
            //whichWeapon.weaponCharacteristic[whichWeapon.chosenWeapon]


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
