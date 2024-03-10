using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Subject<PlayerEnum>
{
    #region Private Fields
    COMP397Labs inputs;
    Vector2 move;
    Camera camera;
    Vector3 camForward, camRight;
    #endregion

    #region Serialize Fields
    [SerializeField] CharacterController characterController;
    [SerializeField] Joystick joystick;

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

    [Header("Respawn Locations")]
    [SerializeField] Transform respawnPoint;
    #endregion

    private void Awake() 
    {
        characterController = GetComponent<CharacterController>();
        inputs = new COMP397Labs();
        inputs.Enable();
        inputs.Player.Move.performed += context => move = context.ReadValue<Vector2>();
        inputs.Player.Move.canceled += context => move = Vector2.zero;
        inputs.Player.Jump.performed += context => Jump();

        camera = Camera.main;
    }

    private void OnEnable() => inputs.Enable();

    private void OnDisable() => inputs.Disable();

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && velocity.y <0.0f)
        {
            velocity.y = -2.0f;
        }

        move = joystick.Direction;
        camForward = camera.transform.forward;
        camRight = camera.transform.right;
        camForward.y = 0.0f;
        camRight.y = 0.0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 movement = (camRight * move.x + camForward * move.y) * speed * Time.fixedDeltaTime;

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
            NotifyObservers(PlayerEnum.Jump);
        }
    }

    private void LogMessage(InputAction.CallbackContext context)
    {
        //Debug.Log(msg);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Triggering with {other.gameObject.tag}");

        if(other.CompareTag("Death"))
        {
            characterController.enabled = false;
            transform.position = respawnPoint.position;
            characterController.enabled = true;
            NotifyObservers(PlayerEnum.Die);
        }
    }

    //IEnumerator Respawn()
    //{
    //    yield return null;

    //    transform.Translate(transform.position + new Vector3(0, 5, 0));
    //}
}
