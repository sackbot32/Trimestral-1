using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemyShooting : MonoBehaviour
{
    private float lastTimeShot;
    public float rate;
    public int damage;
    public Transform offset;
    public int range;
    public LineRenderer line;
    RaycastHit hit;
    private Vector3 realOrigin;
    private Transform player;
    public Vector3 aimOffset;
    public Transform myRotation;
    public bool canAim;
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        canAim = false;
        player = GameObject.FindGameObjectWithTag("EnemyAim").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if(canAim) 
        { 
            transform.LookAt(player.position + aimOffset);
            //transform.rotation = Quaternion.Slerp(transform.rotation, myRotation.rotation, Time.deltaTime * 5);
            line.SetPosition(0, offset.position);

            lastTimeShot += Time.deltaTime;
            if(lastTimeShot > rate)
            {
                RaycastHit hit;
                if (Physics.Raycast(offset.position, offset.forward, out hit, range,mask))
                {
                    line.SetPosition(1, hit.point);
                    if (hit.transform.CompareTag("Player") || hit.transform.CompareTag("EnemyAim"))
                    {
                        print("Player hit");
                        
                    }

                } else
                {
                    line.SetPosition(1, offset.localToWorldMatrix.GetPosition() + offset.forward*range);
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
}
