using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    COMP397Labs inputs;
    Vector2 move;

    [SerializeField] CharacterController characterController;

    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float gravity = -30f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] Vector3 velocity;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundRadius = 0.5f;
    [SerializeField] bool isGrounded;

    private void Awake() 
    {
        characterController = GetComponent<CharacterController>();
        inputs = new COMP397Labs();
        inputs.Enable();
        //inputs = GetComponent<COMP397Labs>();
        inputs.Player.Move.performed += context => move = context.ReadValue<Vector2>();
        inputs.Player.Move.canceled += context => move = Vector2.zero;
        inputs.Player.Jump.performed += context => Jump();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && velocity.y <0.0f)
        {
            velocity.y = -2.0f;
        }

        Vector3 movement = new Vector3(move.x, 0, move.y) * speed * Time.fixedDeltaTime;
        characterController.Move(movement);

        velocity.y += gravity * Time.fixedDeltaTime;
        characterController.Move(velocity * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    void Jump()
    {
        if(isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }

    private void LogMessage(InputAction.CallbackContext context)
    {
        //Debug.Log(msg);
    }
}
