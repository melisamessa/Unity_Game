using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 10f;
    private float gravity = -9.81f;
    Vector3 velocity;

    public Transform groundCheck;
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;
    bool isGrounded;

    public float jumpHeight = 3f;

    public bool isSprinting = false;
    public float sprintingSpeedMultiplier = 1.5f;
    private float sprintSpeed = 1f; //para cuando no estemos corriendo

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;   //si fuera cero puede dar error
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        JumpCheck();

        RunCheck();

        characterController.Move(move * speed * Time.deltaTime * sprintSpeed);
        
        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    public void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    public void RunCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting; //pulsa y corre cuando le damos de nuevo vuelve a caminar

            if(isSprinting == true)
            {
                sprintSpeed = sprintingSpeedMultiplier;
            }
            else
            {
                sprintSpeed = 1;
            }
        }
    }
}
