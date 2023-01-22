using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private float walkSpeed = 2f;
    [SerializeField]
    private float sprintSpeed = 10f;
    private float _moveSpeed;
    private float _sprintBonus;

    private CharacterController _controller;
    public Vector3 playerVelocity;
    [SerializeField]
    private float jumpHeight = 2f;
    [SerializeField]
    private float gravity = -10f;

    private bool _isGrounded;
    
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();

        _moveSpeed = walkSpeed;
        _sprintBonus = sprintSpeed - walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // ground check
        // isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        _isGrounded = _controller.isGrounded;

        if (_isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // sprint mechanic
        float sprintInput = Input.GetAxis("Sprint");
        // wasd player movement
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        Vector3 xzMove = (transform.right * xMove + transform.forward * zMove) * (_moveSpeed + sprintInput * _sprintBonus);

        // jump
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // final player move
        _controller.Move((playerVelocity + xzMove) * Time.deltaTime);

        //Debug.Log(controller.isGrounded);
    }
}