using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerForAim : MonoBehaviour
{
    private Transform player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * speed);
    }
}
