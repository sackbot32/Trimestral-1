using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyShooting : MonoBehaviour
{
    public float lastTimeShot;
    public float rate;
    public int damage;
    public Transform offset;
    public int range;
    public LineRenderer line;
    RaycastHit hit;
    private Vector3 realOrigin;
    private Transform player;
    public Vector3 aimOffset;
    public bool canAim;
    public LayerMask mask;
    public float shotDelay;
    // Start is called before the first frame update
    void Start()
    {
        canAim = false;
        player = GameObject.FindGameObjectWithTag("EnemyAim").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (canAim)
        {
            transform.LookAt(player.position + aimOffset);
            line.SetPosition(0, offset.position);

            
                RaycastHit hit;
                if (Physics.Raycast(offset.position, offset.forward, out hit, range, mask))
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
                                    lastTimeShot = 0;
                                }
                            }
                        }

                    } else
                    {
                        lastTimeShot = 0;
                    }

                }
                else
                {
                    line.SetPosition(1, offset.localToWorldMatrix.GetPosition() + offset.forward * range);
                }

        }
    }
}
