using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FinalEnemyShooting : MonoBehaviour
{
    public float lastTimeShot;
    public float rate;
    public int damage;
    public Transform gunPoint;
    public int range;
    public LineRenderer line;
    RaycastHit hit;
    private Vector3 realOrigin;
    private Transform aimObjectBlue;
    private Transform aimObjectRed;
    public Vector3 aimOffset;
    public AimConstraint aimConstraint;
    public bool canAim;
    public LayerMask mask;
    public float shotDelay;
    public bool blue;
    // Start is called before the first frame update
    void Start()
    {
        line.enabled = false;
        canAim = false;

        aimObjectBlue = GameObject.FindGameObjectWithTag("EnemyAimBlue").transform;
        aimObjectRed = GameObject.FindGameObjectWithTag("EnemyAim").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (canAim)
        {
            if (blue)
            {
                BlueShooting();
            } else
            {
                RedShooting();
            }

        } else
        {
            line.enabled = false;
        } 
    }

    public void activateAim()
    {
        ConstraintSource source = new ConstraintSource();
        source.sourceTransform = aimObjectBlue.transform;
        source.weight = 1;
        aimConstraint.AddSource(source);
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

    private void BlueShooting()
    {

        gunPoint.transform.LookAt(aimObjectBlue.position);

        line.SetPosition(0, gunPoint.position);


        RaycastHit hit;
        if (Physics.Raycast(gunPoint.position, gunPoint.forward, out hit, range, mask))
        {
            line.SetPosition(1, hit.point);
            if (hit.transform.CompareTag("Player"))
            {
                lastTimeShot += Time.deltaTime;
                if (lastTimeShot > shotDelay / 2)
                {
                    if (lastTimeShot > shotDelay - shotDelay / 4)
                    {
                        if (lastTimeShot >= shotDelay)
                        {
                            hit.transform.GetComponent<PlayerHealth>().takeDamage(damage);
                            lastTimeShot = 0;
                        }
                    }
                }

            }
            else
            {
                lastTimeShot -= Time.deltaTime;
                if (lastTimeShot <= 0)
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

    private void RedShooting()
    {
        gunPoint.transform.LookAt(aimObjectRed.position);
        line.SetPosition(0, gunPoint.position);

        lastTimeShot += Time.deltaTime;
        if (lastTimeShot > rate)
        {
            RaycastHit hit;
            if (Physics.Raycast(gunPoint.localToWorldMatrix.GetPosition(), gunPoint.forward, out hit, range, mask))
            {
                line.SetPosition(1, hit.point);
                if (hit.transform.CompareTag("Player"))
                {
                    if (hit.transform.GetComponent<PlayerHealth>() != null)
                    {
                        print("choca con jugador");
                        hit.transform.GetComponent<PlayerHealth>().takeDamage(damage);
                    }

                }
                if (hit.transform.CompareTag("EnemyAim"))
                {
                    print("Player miss");
                }

            }
            else
            {
                line.SetPosition(1, gunPoint.localToWorldMatrix.GetPosition() + gunPoint.forward * range);
            }
            line.enabled = enabled;
            lastTimeShot = 0;
        }

        if (lastTimeShot > rate / 5)
        {
            line.enabled = false;
        }
    }
}
