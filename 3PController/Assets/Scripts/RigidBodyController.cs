using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyController : MonoBehaviour
{
    [SerializeField]
    private float accelerationForce = 10.0f;
    [SerializeField]
    private float maxSpeed = 0.5f;
    [SerializeField]
    private PhysicMaterial stoppingPhysicsMaterial, movingPhysicsMaterial;

    private new Rigidbody rigidbody;
    private Vector2 input;
    private new Collider collider;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        var inputDirection = new Vector3(input.x, 0, input.y);
        
        if(inputDirection.magnitude > 0)
        {
            collider.material = movingPhysicsMaterial;
        }
        else
        {
            collider.material = stoppingPhysicsMaterial;
        }

        if(rigidbody.velocity.magnitude < maxSpeed)
        {
            rigidbody.AddForce(inputDirection * accelerationForce, ForceMode.Acceleration);
        }
    }

    private void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

    }

}
