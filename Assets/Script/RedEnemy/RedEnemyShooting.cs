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
            //transform.LookAt(player.position);
            //hip.transform.LookAt(player.position);
            gunPoint.transform.LookAt(player.position);
            //myRotation.LookAt(player.position + aimOffset);
            //transform.rotation = Quaternion.Slerp(transform.rotation, myRotation.rotation, Time.deltaTime * 5);
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
                        print("Player hit");
                        
                    }
                    if (hit.transform.CompareTag("EnemyAim"))
                    {
                        print("Player miss");
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
