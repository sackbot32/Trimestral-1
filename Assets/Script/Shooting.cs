using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class WeaponCharacteristic{
    public string color;
    public int damage;
    public float rate;
    public float spread;
    public bool multiShot;
    public float range;
}
public class Shooting : MonoBehaviour
{
    //public int type;
    public WeaponCharacteristic[] weaponCharacteristic;
    public int chosenWeapon;
    public float effectTimer;
    private Ray ray;
    private float lastTimeShot;
    private int layerMask;
    private LineRenderer gunLine;
    private ParticleSystem muzzle;

    private void Start()
    {
        gunLine = GetComponent<LineRenderer>(); 
        gunLine.enabled = false;
        muzzle = transform.GetChild(0).GetComponent<ParticleSystem>();
        muzzle.Stop();
        muzzle.Clear();
        lastTimeShot = weaponCharacteristic[chosenWeapon].rate;
        changeColor(weaponCharacteristic[chosenWeapon].color);
        layerMask = LayerMask.GetMask("Shootable", "Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        lastTimeShot += Time.deltaTime;

        if (lastTimeShot > effectTimer)
        {
            gunLine.enabled = false;
            muzzle.Stop();
            muzzle.Clear();
            gunLine.SetPosition(1, transform.position);
        }

        if (Input.GetButton("Fire1") && lastTimeShot > weaponCharacteristic[chosenWeapon].rate)
        {
            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);
            ray.direction = transform.forward;
            ray.origin = transform.position;
            RaycastHit hit;
            lastTimeShot = 0;
            muzzle.Play();
            if (Physics.Raycast(ray,out hit, weaponCharacteristic[chosenWeapon].range,layerMask))
            {
                if (hit.transform.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    hit.transform.gameObject.GetComponent<EnemyHealth>().GetHit(weaponCharacteristic[chosenWeapon].damage,weaponCharacteristic[chosenWeapon].color);
                }
                gunLine.SetPosition(1, hit.point);
            } else
            {
                gunLine.SetPosition(1, transform.position + transform.forward * weaponCharacteristic[chosenWeapon].range);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            chosenWeapon += 1;
            if(chosenWeapon >= weaponCharacteristic.Length) 
            {
                chosenWeapon = 0;
            }
            changeColor(weaponCharacteristic[chosenWeapon].color);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            chosenWeapon -= 1;
            if (chosenWeapon < 0)
            {
                chosenWeapon = 2;
            }
            changeColor(weaponCharacteristic[chosenWeapon].color);
        }

        if (Input.GetKeyDown("1"))
        {
            chosenWeapon = 0;
            changeColor(weaponCharacteristic[chosenWeapon].color);
        }
        if (Input.GetKeyDown("2"))
        {
            chosenWeapon = 1;
            changeColor(weaponCharacteristic[chosenWeapon].color);
        }
        if (Input.GetKeyDown("3"))
        {
            chosenWeapon = 2;
            changeColor(weaponCharacteristic[chosenWeapon].color);
        }

    }

    private void changeColor(string color)
    {
        switch (color)
        {
            case "Red":
                gunLine.startColor = new Color(1,0,0);
                gunLine.endColor = new Color(1,0,0);
                muzzle.startColor = new Color(1, 0, 0);
                break;
            case "Blue":
                print("llega a blue ");
                gunLine.startColor = new Color(0, 0, 1);
                gunLine.endColor = new Color(0, 0, 1);
                muzzle.startColor = new Color(0, 0, 1);
                break;            
            case "Green":
                gunLine.startColor = new Color(0, 1, 0);
                gunLine.endColor = new Color(0, 1, 0);
                muzzle.startColor =  new Color(0, 1, 0);
                break;

            default:
                break;
        }
    }
}
