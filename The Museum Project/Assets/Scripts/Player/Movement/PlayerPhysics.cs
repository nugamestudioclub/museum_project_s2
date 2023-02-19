using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float walkingForce;
    [SerializeField]
    private float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void FixedUpdate()
    {
        /*
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * walkingAcceleration);
        }
        */

        float currentVelocity = rb.velocity.magnitude;

        // get the input values for player movement
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        // calculate the force propelling the player along the x and z axis in this frame
        movement = movement.normalized * Time.deltaTime * walkingForce;
        if (currentVelocity < maxSpeed)
        {
            rb.AddRelativeForce(movement);
        }
        //movement *= (maxSpeed - currentVelocity) / maxSpeed;
        Debug.Log(rb.velocity.magnitude);


        /*
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        */
    }
}
