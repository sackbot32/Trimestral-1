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
    private LineRenderer line;
    RaycastHit hit;
    private Vector3 realOrigin;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0,offset.localToWorldMatrix.GetPosition());
        
        if (Physics.Raycast(offset.localToWorldMatrix.GetPosition(), offset.forward, out hit, range))
        {
            line.SetPosition(1, hit.point);
            if (hit.transform.CompareTag("Player"))
            {
                print("Player hit");
            }
        } else
        {
            line.SetPosition(1, offset.localToWorldMatrix.GetPosition() + offset.forward*range);
        }
    }
}
