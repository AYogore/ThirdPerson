using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RigidBodyController : MonoBehaviour
{
    [SerializeField]
    private float accelerationForce = 10.0f;

    [SerializeField]
    private float maxSpeed = 0.5f;

    [SerializeField]
    private PhysicMaterial stoppingPhysicsMaterial, movingPhysicsMaterial;

    [SerializeField]
    [Tooltip ("How fast player turns. 0 = no turning, 1 = snapping/instant turning")]
    [Range(0,1)]
    private float turnSpeed = 0.1f;

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

        Vector3 cameraFlattenedForward = Camera.main.transform.forward;
        cameraFlattenedForward.y = 0;
        var cameraRotation = Quaternion.LookRotation(cameraFlattenedForward);

        Vector3 cameraRelativeInputDirection = cameraRotation * inputDirection;
        
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
            rigidbody.AddForce(cameraRelativeInputDirection * accelerationForce, ForceMode.Acceleration);
        }

        if(cameraRelativeInputDirection.magnitude > 0) //lets player stay facing direction on camera
        {
            var targetRotation = Quaternion.LookRotation(cameraRelativeInputDirection); //change rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed);
        }
        
    }

    private void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

    }

}
