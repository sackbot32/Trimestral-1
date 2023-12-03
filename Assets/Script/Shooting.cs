using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private int layerMask;
    private LineRenderer gunLine;
    private ParticleSystem muzzle;
    public Animator backpack;
    public InputActionReference shoot;
    public GameObject particle;
    public bool recharging;


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
        weaponCharacteristic[chosenWeapon].lastTimeShot = weaponCharacteristic[chosenWeapon].rate;
        recharging = false;
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
            if(weaponCharacteristic[chosenWeapon].lastTimeShot > weaponChangeTime)
            {
                canShoot = true;
            }
        }

        if (weaponCharacteristic[chosenWeapon].lastTimeShot > effectTimer)
        {
            if(weaponCharacteristic[chosenWeapon].color == "Blue" && Input.GetButton("Fire1"))
            {

            } else
            {
                backpack.SetBool("Shooting", false);
            }
            gunLine.enabled = false;
            muzzle.Stop();
            muzzle.Clear();
            gunLine.SetPosition(1, transform.position);
        }

        if (shoot.action.IsPressed() && weaponCharacteristic[chosenWeapon].lastTimeShot > weaponCharacteristic[chosenWeapon].rate && canShoot)
        {
            backpack.SetBool("Shooting", true);
            gunLine.enabled = true;
            ray.direction = transform.forward;
            gunLine.SetPosition(0, transform.position);
            weaponCharacteristic[chosenWeapon].lastTimeShot = 0;
            muzzle.Play();
            switch (weaponCharacteristic[chosenWeapon].color)
            {
                case "Red":
                    recharging = true;
                    break;
                case "Blue":
                    recharging = false;
                    backpack.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.SetColor("_BaseColor", new Color(0.5f, 0.5f, 1));
                    break;
                case "Green":
                    recharging = true;
                    break;

                default:
                    break;
            }
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
        } else if (weaponCharacteristic[chosenWeapon].color == "Blue" && weaponCharacteristic[chosenWeapon].lastTimeShot < weaponCharacteristic[chosenWeapon].rate)
        {
            player.walkSpeed = startingSpeed / 3;
        } else
        {
            player.walkSpeed = startingSpeed;
        }
        switch (weaponCharacteristic[chosenWeapon].color)
        {
            case "Red":
                if(weaponCharacteristic[chosenWeapon].lastTimeShot > weaponCharacteristic[chosenWeapon].rate && recharging)
                {
                    SetWhite();
                    recharging = false;
                }
                break;
            case "Blue":
                if (backpack.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.GetColor("_BaseColor") != new Color(0, 0, 1))
                {
                    if (weaponCharacteristic[chosenWeapon].lastTimeShot > weaponCharacteristic[chosenWeapon].rate)
                    {
                        backpack.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.SetColor("_BaseColor", new Color(0, 0, 1));
                    } else
                    {
                        backpack.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.SetColor("_BaseColor", new Color(0.5f - (weaponCharacteristic[chosenWeapon].lastTimeShot / weaponCharacteristic[chosenWeapon].rate), 0.5f - (weaponCharacteristic[chosenWeapon].lastTimeShot / weaponCharacteristic[chosenWeapon].rate), 1));
                    }
                }
                break;
            case "Green":
                if (weaponCharacteristic[chosenWeapon].lastTimeShot > weaponCharacteristic[chosenWeapon].rate && recharging)
                {
                    SetWhite();
                    recharging = false;
                }
                break;

            default:
                break;
        }

    }

    private void changeWeapon(int choice)
    {
        if (canShoot)
        {
            chosenWeapon = choice;
            changeColor(weaponCharacteristic[chosenWeapon].color);
            canShoot = false;
            //weaponCharacteristic[chosenWeapon].lastTimeShot = 0;
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
            //weaponCharacteristic[chosenWeapon].lastTimeShot = 0;
        }
        
    }
    private void Shoot()
    {
        RaycastHit hit;
        ray.origin = transform.position + new Vector3(Random.Range(-weaponCharacteristic[chosenWeapon].spread, weaponCharacteristic[chosenWeapon].spread), Random.Range(-weaponCharacteristic[chosenWeapon].spread, weaponCharacteristic[chosenWeapon].spread), 0);
        if (Physics.Raycast(ray, out hit, weaponCharacteristic[chosenWeapon].range, layerMask,QueryTriggerInteraction.Ignore))
        {
            
            if (hit.transform.tag == "Enemy")
            {

                if (hit.transform.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    hit.transform.gameObject.GetComponent<EnemyHealth>().GetHit(weaponCharacteristic[chosenWeapon].damage, weaponCharacteristic[chosenWeapon].color);
                    if(hit.transform.gameObject.GetComponent<EnemyHealth>().color == weaponCharacteristic[chosenWeapon].color)
                    {
                        transform.root.gameObject.GetComponent<PlayerHealth>().Heal(weaponCharacteristic[chosenWeapon].healing);
                    }
                } 
                else if (hit.transform.gameObject.GetComponent<EnemyHealthFinal>() != null)
                {
                    hit.transform.gameObject.GetComponent<EnemyHealthFinal>().GetHit(weaponCharacteristic[chosenWeapon].damage, weaponCharacteristic[chosenWeapon].color);
                    if (hit.transform.gameObject.GetComponent<EnemyHealthFinal>().color == weaponCharacteristic[chosenWeapon].color)
                    {
                        transform.root.gameObject.GetComponent<PlayerHealth>().Heal(weaponCharacteristic[chosenWeapon].healing);
                    }
                }
                if (hit.transform.gameObject.GetComponent<ColorWall>() != null)
                {
                    hit.transform.gameObject.GetComponent<ColorWall>().takeDamage(weaponCharacteristic[chosenWeapon].damage, weaponCharacteristic[chosenWeapon].color);
                }
            }
            if(weaponCharacteristic[chosenWeapon].color == "Red")
            {
                Instantiate(explosion, hit.point, explosion.transform.rotation);
            }
            

            gunLine.SetPosition(1, hit.point);
            CreateLight(hit.point);
        }
        else
        {
            gunLine.SetPosition(1, transform.position + transform.forward * weaponCharacteristic[chosenWeapon].range);
            CreateLight(transform.position + transform.forward * weaponCharacteristic[chosenWeapon].range);
        }
    }

    private void SetWhite()
    {
        switch (weaponCharacteristic[chosenWeapon].color)
        {
            case "Red":
                backpack.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.SetColor("_BaseColor", new Color(1, 0.7f, 0.7f));
                break;
            case "Blue":

                break;
            case "Green":
                backpack.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.SetColor("_BaseColor", new Color(0.7f, 1, 0.7f));
                break;

            default:
                break;
        }
        Invoke("SetColor",0.3f);
    }

    private void SetColor()
    {
        switch (weaponCharacteristic[chosenWeapon].color)
        {
            case "Red":
                backpack.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.SetColor("_BaseColor", new Color(1, 0, 0));
                break;
            case "Blue":

                break;
            case "Green":
                backpack.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.SetColor("_BaseColor", new Color(0, 1, 0));
                break;

            default:
                break;
        }
    }

    private void changeColor(string color)
    {
        //backpack.SetBool("ChangingWeapon",true);
        switch (color)
        {
            case "Red":
                backpack.transform.GetChild(1).transform.GetComponent<SkinnedMeshRenderer>().material.color = new Color(1, 0, 0);
                gunLine.startColor = new Color(1,0,0);
                gunLine.endColor = new Color(1,0,0);
                muzzle.startColor = new Color(1, 0, 0);
                break;
            case "Blue":
                backpack.transform.GetChild(1).transform.GetComponent<SkinnedMeshRenderer>().material.color = new Color(0, 0, 1);
                gunLine.startColor = new Color(0, 0, 1);
                gunLine.endColor = new Color(0, 0, 1);
                muzzle.startColor = new Color(0, 0, 1);
                break;            
            case "Green":
                backpack.transform.GetChild(1).transform.GetComponent<SkinnedMeshRenderer>().material.color = new Color(0, 1, 0);
                gunLine.startColor = new Color(0, 1, 0);
                gunLine.endColor = new Color(0, 1, 0);
                muzzle.startColor =  new Color(0, 1, 0);
                break;

            default:
                break;
        }

        Invoke("turnChagingOff",backpack.GetCurrentAnimatorClipInfo(0).Length);
    }

    public void turnChagingOff()
    {
        if (backpack.GetBool("ChangingWeapon"))
        {
            backpack.SetBool("ChangingWeapon", false);
        }
    }

    private void CreateLight(Vector3 lightPosition)
    {
        Instantiate(particle, lightPosition, Quaternion.identity);
    }
}
