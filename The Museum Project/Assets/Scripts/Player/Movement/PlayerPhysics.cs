using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField]
    private Vector3 gravity;

    private Rigidbody rb;
    [SerializeField]
    private float walkingForce;
    [SerializeField]
    private float maxWalkSpeed;
    [SerializeField]
    private float maxRunSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float airControl;

    // current maximum input speed of the player
    private float maxSpeed;

    // metrics for ground detection
    //private float playerHeight;
    //private Vector3 boxDim;


    private GrappleHook grappleHook;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        // ground detection constants
        //playerHeight = (transform.localScale.y / 2f);
        //Vector3 boxDim = transform.localScale;
        //boxDim = new Vector3(boxDim.x - 0.5f, boxDim.y + 0.5f, boxDim.z - 0.5f);

        grappleHook = gameObject.GetComponent<GrappleHook>();
    }

    // Update is called once per frame
    void Update()
    {
        // if jump key is pressed
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            // apply an upward force to the player
            rb.AddForce(new Vector3(0f, jumpForce, 0f));
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = maxRunSpeed;
        }
        else
        {
            maxSpeed = maxWalkSpeed;
        }
    }

    void FixedUpdate()
    {
        // get the input values for player movement
        Vector3 movement = GetMoveInputs();
        // calculate the force propelling the player along the x and z axis in this frame
        movement = movement.normalized * walkingForce;
        // scale movement inversely by current velocity and current maxSpeed
        Vector3 horizontalVel = Vector3.ProjectOnPlane(rb.velocity, transform.up);
        movement *= Mathf.Max(0f, 1 - (horizontalVel.magnitude / maxSpeed));
        // scale movement force if not on ground
        if (!isGrounded())
        {
            movement *= airControl;
        }
        // apply the movement force relative to the player's transform
        rb.AddRelativeForce(movement);

        // apply the force of a potential grapple hook to the player
        rb.AddForce(grappleHook.PullForce(transform.position));

        // apply the force of gravity to the player
        rb.AddForce(gravity);

        Debug.Log(isGrounded());
    }

    public Vector3 GetMoveInputs()
    {
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
        return xzMove;
    }

    public bool isGrounded()
    {
        // Raycast using ray
        //return Physics.Raycast(transform.position, -Vector3.up, playerHeight + 0.1f);

        // Raycast using box
        //Vector3 boxCenter = transform.position - new Vector3(0f, playerHeight, 0f);
        //return Physics.CheckBox(boxCenter, boxDim);

        // Raycast using box hardcoded
        Vector3 boxCenter = transform.position - new Vector3(0f, 0.5f, 0f);
        return Physics.CheckBox(boxCenter, new Vector3(0.45f, 0.55f, 0.45f));
    }
}
