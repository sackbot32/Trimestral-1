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
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("EnemyAim").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.LookAt(player.position + aimOffset);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((player.position + aimOffset) - transform.position), Time.deltaTime * 5);
        line.SetPosition(0, offset.position);

        lastTimeShot += Time.deltaTime;
        if (Physics.Raycast(offset.position, offset.forward, out hit, range,7))
        {
            line.SetPosition(1, hit.point);
            if (hit.transform.CompareTag("Player") || hit.transform.CompareTag("EnemyAim"))
            {
                print("Player hit");
                lastTimeShot = 0;
            }

        } else
        {
            line.SetPosition(1, offset.localToWorldMatrix.GetPosition() + offset.forward*range);
        }
    }
}
