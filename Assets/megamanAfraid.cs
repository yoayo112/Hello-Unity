using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class megamanAfraid : MonoBehaviour
{

    private Transform player;
    private Transform megaman;
    private Rigidbody mmBody;
    public float speed = 5.0f;
    public float fearDistance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Zombie1").GetComponent<Transform>();
        megaman = GameObject.Find("megaman").GetComponent<Transform>();
        mmBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //find distance
        float xd = Math.Abs(player.position.x - megaman.position.x);
        float zd = Math.Abs(player.position.z - megaman.position.z);

        //if either zombie is near
        if (xd <= fearDistance || zd <= fearDistance)
        {
            //find the slope of the line between zombie and megaman.
            Vector3 direction = player.position - (new Vector3(megaman.position.x, 0, megaman.position.z));

            //find the rotation between that slope and megamans current face.
            direction = megaman.forward - direction;

            //rotate that much
            megaman.forward = direction.normalized;

            //move megaman
            mmBody.AddForce(megaman.forward * speed, ForceMode.Acceleration);
        }
    }
}
