using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    public Joystick joystick;
    public Animator animator;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 movementDirection = new Vector3(joystick.Horizontal * movementSpeed, rb.velocity.y,
            joystick.Vertical * movementSpeed);
        movementDirection.Normalize();
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);
        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
