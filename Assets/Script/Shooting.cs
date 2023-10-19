using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponCharacteristic{
    public int damage;
    public float rate;
    public float spread;
    public bool multiShot;
    public float range;
}
public class Shooting : MonoBehaviour
{
    //public int type;
    public WeaponCharacteristic weaponCharacteristic;
    private Ray ray;
    private float lastTimeShot;
    private int layerMask;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Shootable", "Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        lastTimeShot += Time.deltaTime;

        if (Input.GetButton("Fire1") && lastTimeShot > weaponCharacteristic.rate)
        {
            ray.direction = transform.forward;
            ray.origin = transform.position;
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit, weaponCharacteristic.range,layerMask))
            {
       
            }

        }
    }
}
