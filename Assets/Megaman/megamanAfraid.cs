using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class megamanAfraid : MonoBehaviour
{

    private Transform player;
    private Transform megaman;
    private Transform mmHead;
    private Rigidbody mmBody;
    private Material megaMat;
    private Animator animator;

    public int startled = -1;
    public float speed = 5.0f;
    public float fearDistance = 10.0f;
    public int fearTime = 3000;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Zombie1").GetComponent<Transform>();
        megaman = GameObject.Find("megaman").GetComponent<Transform>();
        mmBody = GetComponent<Rigidbody>();
        mmHead = GameObject.Find("megaman/Armature/root/Head").GetComponent<Transform>();
        megaMat = GameObject.Find("megaman/Mesh").GetComponent<SkinnedMeshRenderer>().material;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //find distance
        float xd = Math.Abs(player.position.x - megaman.position.x);
        float zd = Math.Abs(player.position.z - megaman.position.z);

        //find jump scare, proximity, and if already frightened
        if (Input.GetButtonDown("Fire1") && (xd <= fearDistance && zd <= fearDistance) && startled == -1) { startled = fearTime; }

        if (startled > 0)
        {
            animator.SetTrigger("boo");
            animator.SetInteger("startled",startled);
            afraid();
            
        } // otherwise we're all good/
        else
        {
            friendly();
            startled = -1;
        }
    }

    void friendly()
    {
        //set facial expression via UV offset
        megaMat.SetFloat("_OffsetU",0.49F);
        megaMat.SetFloat("_OffsetV",-0.25F);

        //Was using this, but mm kept sliding slowly backwards??
        megaman.LookAt(new Vector3(player.position.x, megaman.position.y, player.position.z));

        //found this workaround - But Still Sliding!!
        //var look = Quaternion.LookRotation(new Vector3(player.position.x, megaman.position.y, player.position.z) - megaman.position);
        //look = Quaternion.Slerp(megaman.rotation, look, 10 * Time.deltaTime);
        //GetComponent<Rigidbody>().MoveRotation(look);

        //follows with head
        mmHead.LookAt(player);

        //was using this to try and stop the sliding behavior. might be useful somewhere else! (can also freeze rotation)
        //mmBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;

        //arrest velocity.? YES! This works with both "look at" methods
        mmBody.velocity = Vector3.zero;
        mmBody.angularVelocity = Vector3.zero;
    }

    void afraid()
    {

        //set facial expression via UV offset
        megaMat.SetFloat("_OffsetU", 0.49F);
        megaMat.SetFloat("_OffsetU", 0.995F);

        //mmBody.constraints = RigidbodyConstraints.None;

        //find distance *again* (he is still frightened, but will only sprint the fear distance. then he will wait the rest of the "startled" frames)
        float xd = Math.Abs(player.position.x - megaman.position.x);
        float zd = Math.Abs(player.position.z - megaman.position.z);

        //if you keep chasing him hell stal frightened for longer.
        if((xd <= fearDistance && zd <= fearDistance))
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
        else { startled -= 1; }
    }

}
