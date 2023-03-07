using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private float walkSpeed = 2f;
    [SerializeField]
    private float maxWalkSpeed = 10f;
    [SerializeField]
    private float sprintSpeed = 10f;
    private float moveSpeed;
    private float sprintBonus;

    [SerializeField]
    private float jumpHeight = 2f;
    [SerializeField]
    private float gravity = -10f;
    [SerializeField]
    private float frictionDefault = 1f;

    private CharacterController controller;
    public Vector3 playerVelocity;
    public Vector3 playerAcceleration;
    private bool isGrounded;
    
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        playerVelocity = Vector3.zero;
        playerAcceleration = Vector3.zero;

        moveSpeed = walkSpeed;
        sprintBonus = sprintSpeed - walkSpeed;
    }


    void Update()
    {
        // acceleration of an object by default is zero
        playerAcceleration = Vector3.zero;

        // determine if the player is grounded
        isGrounded = controller.isGrounded;

        playerAcceleration += new Vector3(0f, gravity, 0f);

        // add acceleration due to normal force, assuming platforms are stationary and parallel to the ground
        if (isGrounded)
        {
            playerAcceleration += new Vector3(0f, -gravity, 0f);
        }

        // when the player jumps on the ground, apply an instant force to the player's velocity
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            playerAcceleration.y += jumpHeight;
        }

        // handle force applied by player movement
        if (isGrounded)
        {
            if (playerVelocity.magnitude < maxWalkSpeed)
            {
                //Vector3 xzMove = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
                Vector3 xzMove = Vector3.zero;
                if (Input.GetKey(KeyCode.W))
                {
                    xzMove.z += 1f;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    xzMove.z += -1f;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    xzMove.x += 1f;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    xzMove.x += -1f;
                }

                xzMove = xzMove.normalized * moveSpeed;
                Vector3 transformedDisplacement = transform.right * xzMove.x + transform.forward * xzMove.z;
                playerAcceleration += transformedDisplacement;
            }
        }

        // add acceleration due to friction
        if (isGrounded)
        {
            Vector3 forceByFriction = Vector3.ProjectOnPlane(playerVelocity.normalized, transform.up) * frictionDefault;
            playerAcceleration -= forceByFriction;
        }

        // add to player's velocity based on acceleartion
        playerVelocity += playerAcceleration * Time.deltaTime;



        /*
        // assume all platforms are stationary and parallel to the ground
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // when the player jumps on the ground, apply an instant force to the player's velocity
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // apply an instant force to the player's velocity when on the ground when they try to move
        if (isGrounded)
        {
            if (playerVelocity.magnitude < maxWalkSpeed)
            {
                Vector3 xzMove = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
                xzMove = xzMove.normalized * moveSpeed * Time.deltaTime;
                playerVelocity += transform.right * xzMove.x + transform.forward * xzMove.z;
            }
        }
        */

        // add to the player's position based on velocity
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.magnitude);

    }

    /*
    // Update is called once per frame
    void Update()
    {
        // ground check
        // isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isGrounded = controller.isGrounded;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // sprint mechanic
        float sprintInput = Input.GetAxis("Sprint");
        // wasd player movement
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        Vector3 xzMove = (transform.right * xMove + transform.forward * zMove) * (moveSpeed + sprintInput * sprintBonus);

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // final player move
        controller.Move((playerVelocity + xzMove) * Time.deltaTime);

        //Debug.Log(controller.isGrounded);
    }
    */
}