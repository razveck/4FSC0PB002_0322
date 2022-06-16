using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    [SerializeField] private CharacterController controller;
    [SerializeField] private Vector3 playerVelocity;
    [SerializeField] private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    private InputManager inputManager;
    private Transform cameraTransform;
    public float originOffSet;
    public LayerMask groundLayer;
    Vector3 origin => transform.position + Vector3.up * originOffSet;
    RaycastHit hit;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
       // cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        groundedPlayer = Physics.Raycast(origin, Vector3.down, out hit, 0.5f, groundLayer);
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3 (movement.x, 0f, movement.y);
        move = transform.forward * move.z + transform.right * move.x;
        move.y = 0f;
        //rigidbody.velocity
        controller.Move(move * Time.deltaTime * playerSpeed);

        // if (move != Vector3.zero)
        //{
        //  gameObject.transform.forward = move;
        //}

        // Changes the height position of the player..
        if (inputManager.jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        inputManager.HandleJumpInput();

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    private void OnDrawGizmosSelected()
    {
        if(groundedPlayer) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + Vector3.down * 0.5f, groundedPlayer ? Color.white : Color.red);
    }
}

