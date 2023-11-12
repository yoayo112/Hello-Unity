using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltZombieMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 0.01f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        //set movement relative to the orientation of the camera
        Vector3 x = Input.GetAxis("Horizontal") * gameObject.transform.right;
        Vector3 z = Input.GetAxis("Vertical") * gameObject.transform.forward;
        Vector3 move = x + z;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            animator.SetBool("isMoving", true);
        }
        else { animator.SetBool("isMoving", false); }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
