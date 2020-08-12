using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

[RequireComponent(typeof(InputHandler), typeof(CharacterController), typeof(Camera))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("The Player movement speed coefficient")]
    [Range(1,30)]
    public float Speed = 6f;
    public float JumpHeigth = 3f;

    [Header("Phisics")]
    [Tooltip("The Player gravity coefficient")]
    [Range(-1,-100)]
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;

    [Header("Links")]
    [Tooltip("The Player camera view")]
    public Camera playerCamera;
    public Transform groudCheck;
    public LayerMask groudMask;

    private bool isGrounded;
    private Vector3 velocity;
    private float yRotation = 0f;
    private InputHandler inputHandler;
    private CharacterController controller;
    private PlayerProperties properties;

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        controller = GetComponent<CharacterController>();
        properties = GetComponent<PlayerProperties>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerProperties();
        UpdatePlayerGravity();
        UpdatePlayerMovement();
    }

    private void UpdatePlayerMovement()
    {
        float look = inputHandler.GetLook() * Time.deltaTime;
        yRotation -= look;
        yRotation = Mathf.Clamp(yRotation, -89f, 89f);
        playerCamera.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);

        transform.Rotate(Vector3.up * inputHandler.GetRotation() * Time.deltaTime);

        Vector3 move = (transform.forward * inputHandler.GetMovementAdvance() * Time.deltaTime * Speed) + (transform.right * inputHandler.GetMovementStrafe() * Time.deltaTime * Speed);   
        controller.Move(move);
    }

    private void UpdatePlayerGravity()
    {
        isGrounded = Physics.CheckSphere(groudCheck.position, groundDistance, groudMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (false == isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(JumpHeigth * -2 * gravity);
        }
        
        controller.Move(velocity * Time.deltaTime);
    }

    private void UpdatePlayerProperties()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            properties.TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            properties.UseMana(5);
        }
    }

}
