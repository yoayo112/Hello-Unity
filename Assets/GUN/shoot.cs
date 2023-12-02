using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public Rigidbody projectile;
    public float speed = 20;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("Fire1"))
        {
            Rigidbody newprojectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            newprojectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            animator.SetTrigger("fire");
        }
    }
}
