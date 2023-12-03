using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public Rigidbody projectile;
    public float speed = 20;
    private Animator animator;
    private Transform spawn;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spawn = GameObject.Find("bulletSpawn").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("fire");
            Rigidbody newprojectile = Instantiate(projectile, spawn.position, spawn.rotation) as Rigidbody;
            newprojectile.velocity = transform.TransformDirection(new Vector3(-1 *speed, 0, 0));
        }
    }
}
