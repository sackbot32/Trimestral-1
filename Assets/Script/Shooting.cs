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
    public int healing;
    public float lastTimeShot;
}
public class Shooting : MonoBehaviour
{
    public float weaponChangeTime;
    public bool canShoot;
    private FirstPersonController player;
    private float startingSpeed;
    public WeaponCharacteristic[] weaponCharacteristic;
    public GameObject explosion;
    public int chosenWeapon;
    public float effectTimer;
    public int howManyMulti;
    private Ray ray;
    private float lastTimeShot;
    private int layerMask;
    private LineRenderer gunLine;
    private ParticleSystem muzzle;


    private void Start()
    {
        canShoot = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        startingSpeed = player.walkSpeed;
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
        foreach (WeaponCharacteristic item in weaponCharacteristic)
        {
            item.lastTimeShot += Time.deltaTime;
        }
        if (!canShoot)
        {
            if(lastTimeShot > weaponChangeTime)
            {
                canShoot = true;
            }
        }

        if (lastTimeShot > effectTimer)
        {
            gunLine.enabled = false;
            muzzle.Stop();
            muzzle.Clear();
            gunLine.SetPosition(1, transform.position);
        }

        if (Input.GetButton("Fire1") && weaponCharacteristic[chosenWeapon].lastTimeShot > weaponCharacteristic[chosenWeapon].rate && canShoot)
        {
            gunLine.enabled = true;
            ray.direction = transform.forward;
            gunLine.SetPosition(0, transform.position);
            weaponCharacteristic[chosenWeapon].lastTimeShot = 0;
            muzzle.Play();
            if (weaponCharacteristic[chosenWeapon].multiShot)
            {
                for (int i = 0; i < howManyMulti; i++)
                {
                    Shoot();
                }
            } else
            {
                Shoot();
            }
            
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            scrollWeapon(1);
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            scrollWeapon(-1);
        }

        if (Input.GetKeyDown("1"))
        {
            changeWeapon(0);
        }
        if (Input.GetKeyDown("2"))
        {
            changeWeapon(1);
        }
        if (Input.GetKeyDown("3"))
        {
            changeWeapon(2);
        }

        if(chosenWeapon == 1)
        {
            player.walkSpeed = startingSpeed * 2;
        } else if (weaponCharacteristic[chosenWeapon].color == "Blue" && lastTimeShot < weaponCharacteristic[chosenWeapon].rate)
        {
            player.walkSpeed = startingSpeed / 3;
        } else
        {
            player.walkSpeed = startingSpeed;
        }

    }

    private void changeWeapon(int choice)
    {
        if (canShoot)
        {
            chosenWeapon = choice;
            changeColor(weaponCharacteristic[chosenWeapon].color);
            canShoot = false;
            lastTimeShot = 0;
        }
    }

    private void scrollWeapon(int direction)
    {
        if (canShoot)
        {
            if(direction > 0)
            {
                chosenWeapon += 1;
                if (chosenWeapon >= weaponCharacteristic.Length)
                {
                    chosenWeapon = 0;
                }
            } else if( direction < 0)
            {
                chosenWeapon -= 1;
                if (chosenWeapon < 0)
                {
                    chosenWeapon = 2;
                }
            
            }
            changeColor(weaponCharacteristic[chosenWeapon].color);
            canShoot = false;
            lastTimeShot = 0;
        }
        
    }
    private void Shoot()
    {
        RaycastHit hit;
        ray.origin = transform.position + new Vector3(Random.Range(-weaponCharacteristic[chosenWeapon].spread, weaponCharacteristic[chosenWeapon].spread), Random.Range(-weaponCharacteristic[chosenWeapon].spread, weaponCharacteristic[chosenWeapon].spread), 0);
        if (Physics.Raycast(ray, out hit, weaponCharacteristic[chosenWeapon].range, layerMask,QueryTriggerInteraction.Ignore))
        {
            if(hit.transform.tag == "Enemy")
            {

                if (hit.transform.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    hit.transform.gameObject.GetComponent<EnemyHealth>().GetHit(weaponCharacteristic[chosenWeapon].damage, weaponCharacteristic[chosenWeapon].color);
                }
                else
                {
                    hit.transform.root.GetComponent<EnemyHealth>().GetHit(weaponCharacteristic[chosenWeapon].damage, weaponCharacteristic[chosenWeapon].color);
                }
            }
            if(weaponCharacteristic[chosenWeapon].color == "Red")
            {
                Instantiate(explosion, hit.point, explosion.transform.rotation);
            }
            

            gunLine.SetPosition(1, hit.point);
        }
        else
        {
            gunLine.SetPosition(1, transform.position + transform.forward * weaponCharacteristic[chosenWeapon].range);
        }
    }

    private void changeColor(string color)
    {
        switch (color)
        {
            case "Red":
                transform.parent.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
                gunLine.startColor = new Color(1,0,0);
                gunLine.endColor = new Color(1,0,0);
                muzzle.startColor = new Color(1, 0, 0);
                break;
            case "Blue":
                print("llega a blue ");
                transform.parent.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 1);
                gunLine.startColor = new Color(0, 0, 1);
                gunLine.endColor = new Color(0, 0, 1);
                muzzle.startColor = new Color(0, 0, 1);
                break;            
            case "Green":
                transform.parent.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
                gunLine.startColor = new Color(0, 1, 0);
                gunLine.endColor = new Color(0, 1, 0);
                muzzle.startColor =  new Color(0, 1, 0);
                break;

            default:
                break;
        }
    }
}
