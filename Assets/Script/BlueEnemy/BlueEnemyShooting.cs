using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BlueEnemyShooting : MonoBehaviour
{
    public float lastTimeShot;
    public float rate;
    public int damage;
    public Transform gunPoint;
    public int range;
    public LineRenderer line;
    RaycastHit hit;
    private Vector3 realOrigin;
    private Transform aimObject;
    private Transform player;
    public Vector3 aimOffset;
    public AimConstraint aimConstraint;
    public bool canAim;
    public LayerMask mask;
    public float shotDelay;
    // Start is called before the first frame update
    void Start()
    {
        line.enabled = false;
        canAim = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        aimObject = GameObject.FindGameObjectWithTag("EnemyAimBlue").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (canAim)
        {
            //transform.LookAt(aimObject.position + aimOffset);
            //transform.LookAt(player.position + aimOffset );
            gunPoint.transform.LookAt(aimObject.position);
            
            line.SetPosition(0, gunPoint.position);

            
                RaycastHit hit;
                if (Physics.Raycast(gunPoint.position, gunPoint.forward, out hit, range, mask))
                {
                    line.SetPosition(1, hit.point);
                    if (hit.transform.CompareTag("Player"))
                    {
                        lastTimeShot += Time.deltaTime;
                        if (lastTimeShot > shotDelay/2)
                        {
                            print("sniper half way to shooting");
                            if (lastTimeShot > shotDelay - shotDelay/4)
                            {
                                print("sniper close to shooting");
                                if (lastTimeShot >= shotDelay)
                                {
                                    print("sniper hit player");
                                    hit.transform.GetComponent<PlayerHealth>().takeDamage(damage);
                                    lastTimeShot = 0;
                                }
                            }
                        }

                    } else
                    {
                        lastTimeShot -= Time.deltaTime;
                        if(lastTimeShot <= 0)
                        {
                            lastTimeShot = 0;
                        }
                    }

                }
                else
                {
                    line.SetPosition(1, /*offset.localToWorldMatrix.GetPosition() + */gunPoint.forward * range);
                }

        } 
    }

    public void activateAim()
    {
        line.enabled = true;
        aimConstraint.constraintActive = true;
        aimConstraint.rotationOffset = new Vector3 (0, 0, 0);
        canAim = true;
    }
    public void deactivateAim()
    {
        line.enabled = false;
        canAim = false;
    }
}
