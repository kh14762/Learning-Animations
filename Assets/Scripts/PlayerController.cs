using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float verticalInput;
    public float horizontalInput;
    private Rigidbody playerRigidBody;
    public Transform playerCamera;
    public bool isOnGround = true;
    public float jumpForce;
    public float gravityModifier;

    public float playerWalkSpeed = 15.0f;
    public float playerRunSpeed = 25.0f;
    public float playerSpeed;
    public Vector3 MoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // movement
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // camera rotation
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        forward.y = 0;
        forward.Normalize();
        right.Normalize();

        // Move Direction
        MoveDirection = forward * verticalInput + right * horizontalInput;

        // Move sojourner
        playerRigidBody.velocity = new Vector3(MoveDirection.x * playerSpeed, playerRigidBody.velocity.y, MoveDirection.z * playerSpeed);

        // Rotate sojourner in the direction they are moving
        if (MoveDirection != new Vector3(0, 0, 0))
        {
            transform.rotation = Quaternion.LookRotation(MoveDirection);
        }
    }

    private void Update()
    {
        // sprint speed
        if (Input.GetKey(KeyCode.LeftShift) && isOnGround)
        {
            playerSpeed = playerRunSpeed;
        }
        else
        {
            playerSpeed = playerWalkSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}
