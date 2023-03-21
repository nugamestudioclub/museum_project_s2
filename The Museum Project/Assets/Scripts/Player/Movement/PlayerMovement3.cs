using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private float walkSpeed = 2f;
    [SerializeField]
    private float sprintSpeed = 10f;
    private float _moveSpeed;
    private float _sprintBonus;

    private CharacterController _controller;
    public Vector3 playerAcceleration;
    public Vector3 playerVelocity;
    [SerializeField]
    private float jumpHeight = 2f;
    [SerializeField]
    private float gravity = -10f;

    private bool _isGrounded;

    private GrappleHook grappleHook;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();

        _moveSpeed = walkSpeed;
        _sprintBonus = sprintSpeed - walkSpeed;
        grappleHook = gameObject.GetComponent<GrappleHook>();
    }

    // Update is called once per frame
    void Update()
    {
        // an object by default has no forces acting on it, and therefore, zero acceleration
        playerAcceleration = Vector3.zero;

        // acceleration due to gravity
        playerAcceleration += new Vector3(0f, gravity, 0f);

        // ground check
        _isGrounded = _controller.isGrounded;

        // assuming platforms are horizontal and stationary
        if (_isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // instant force applied on jump
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // sprint mechanic
        float sprintInput = Input.GetAxis("Sprint");
        // wasd player movement
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        Vector3 transformedMove = transform.right * xMove + transform.forward * zMove;
        if (transformedMove.magnitude > 1f)
        {
            transformedMove = transformedMove.normalized;
        }
        Vector3 xzMove = transformedMove * (_moveSpeed + sprintInput * _sprintBonus);
        // instance force applied when moving
        _controller.Move(xzMove * Time.deltaTime);

        // force applied by grapple hook
        //playerVelocity += grappleHook.PullForce(transform.position) * Time.deltaTime;

        // apply acceleration to player velocity
        playerVelocity += playerAcceleration * Time.deltaTime;

        // apply velocity to change position
        _controller.Move(playerVelocity * Time.deltaTime);

        //Debug.Log(controller.isGrounded);
    }
}