using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private float walkSpeed = 2f;
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
        //TODO: Friction

        // acceleration of an object by default is zero
        playerAcceleration = Vector3.zero;

        // determine if the player is grounded
        isGrounded = controller.isGrounded;

        // add acceleration due to gravity
        if (!isGrounded)
        {
            playerAcceleration += new Vector3(0f, gravity, 0f);
        }

        // add to player's velocity based on acceleartion
        playerVelocity += playerAcceleration * Time.deltaTime;

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
            Vector3 xzMove = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            xzMove = xzMove.normalized * moveSpeed * Time.deltaTime;
            playerVelocity += transform.right * xzMove.x + transform.forward * xzMove.z;
        }

        // add to the player's position based on velocity
        controller.Move(playerVelocity * Time.deltaTime);

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