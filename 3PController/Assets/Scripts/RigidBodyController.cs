using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyController : MonoBehaviour
{
    [SerializeField]
    private float accelerationForce = 10.0f;
    [SerializeField]
    private float maxSpeed = 0.5f;

    private new Rigidbody rigidbody;
    private Vector2 input;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var inputDirection = new Vector3(input.x, 0, input.y);
        //rigidbody.AddForce(inputDirection * accelerationForce);

        //rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        //overrides gravity and Unity physics   
        
        if(rigidbody.velocity.magnitude < maxSpeed)
        {
            rigidbody.AddForce(inputDirection * accelerationForce);
        }
    }

    private void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

    }

}
