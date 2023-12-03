using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RedEnemyShooting : MonoBehaviour
{
    private float lastTimeShot;
    public float rate;
    public int damage;
    public Transform gunPoint;
    public int range;
    public LineRenderer line;
    RaycastHit hit;
    private Vector3 realOrigin;
    private Transform player;
    public Vector3 aimOffset;
    public AimConstraint[] aimConstraint;
    public bool canAim;
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        canAim = false;
        player = GameObject.FindGameObjectWithTag("EnemyAim").transform;
        line.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(canAim) 
        { 
            gunPoint.transform.LookAt(player.position);
            line.SetPosition(0, gunPoint.position);

            lastTimeShot += Time.deltaTime;
            if(lastTimeShot > rate)
            {
                RaycastHit hit;
                if (Physics.Raycast(gunPoint.localToWorldMatrix.GetPosition(), gunPoint.forward, out hit, range,mask))
                {
                    line.SetPosition(1, hit.point);
                    if (hit.transform.CompareTag("Player") )
                    {
                        if (hit.transform.GetComponent<PlayerHealth>() != null)
                        {
                            hit.transform.GetComponent<PlayerHealth>().takeDamage(damage);
                        }

                    }
                    if (hit.transform.CompareTag("EnemyAim"))
                    {
                    }

                } else
                {
                    line.SetPosition(1, gunPoint.localToWorldMatrix.GetPosition() + gunPoint.forward*range);
                }
                line.enabled = enabled;
                lastTimeShot = 0;
            }

            if(lastTimeShot > rate/5) 
            {
                line.enabled = false;
            }
        }
    }
    public void activateAim()
    {

        line.enabled = true;
        foreach (AimConstraint item in aimConstraint)
        {
            ConstraintSource source = new ConstraintSource();
            source.sourceTransform = player.transform;
            source.weight = 1;
            item.AddSource(source);
            item.constraintActive = true;
            item.rotationOffset = new Vector3(0, 0, 0);
        }
        canAim = true;
    }

    public void deactivateAim()
    {
        line.enabled = false;
        foreach (AimConstraint item in aimConstraint)
        {
            item.rotationOffset = new Vector3(0, 0, 0);
        }
        canAim = false;
    }
}
