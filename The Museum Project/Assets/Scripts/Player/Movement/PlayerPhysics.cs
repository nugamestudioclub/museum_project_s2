using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float walkingAcceleration;


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
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        movement = movement.normalized * Time.deltaTime * walkingAcceleration;
        rb.AddRelativeForce(movement);
    }
}
