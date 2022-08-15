using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform body;
    [SerializeField] private float speed = 5f;

    private void Update()
    {
        HandleMovement();
        RotateCharacter();
    }

    private void HandleMovement()
    {
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");
        Vector3 direction = transform.right * dirX + transform.forward * dirY;

        animator.SetBool("isMoving", direction != Vector3.zero);

        controller.Move(direction * speed * Time.deltaTime);
    }

    private void RotateCharacter()
    {
        if (controller.velocity != Vector3.zero)
        {
            body.rotation = Quaternion.LookRotation(controller.velocity, Vector3.up);
        }
    }
}
