using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backLeft;
    [SerializeField] WheelCollider backRight;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform backLeftTransform;
    [SerializeField] Transform backRightTransform;

    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 15f;

    public float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    void Start()
    {
        Debug.Log(frontLeftTransform.position);
    }
    private void FixedUpdate()
    {
        // Get forward/reverse acceleration from the vertical axis (W and S keys)
        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        // If we are pressing space, give currentBrakeforce a value
        if(Input.GetKey(KeyCode.Space))
        {
            currentBreakForce = breakingForce;
        }
        else
        {
            currentBreakForce = 0f;
        }

        //Apply acceleration to front wheels
        frontLeft.motorTorque = currentAcceleration;
        frontRight.motorTorque = currentAcceleration;

        //Apply brake  force to all wheels
        frontLeft.brakeTorque = currentBreakForce;
        frontRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;

        //take care of the steering
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");

        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        //Update wheel meshes
        //UpdateWheel(frontLeft, frontLeftTransform);
        //UpdateWheel(frontRight, frontRightTransform);
        //UpdateWheel(backLeft, backLeftTransform);
        //UpdateWheel(backRight, backRightTransform);
    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        //Get wheel collider state
        Vector3 position;
        Quaternion rotation;

        col.GetWorldPose(out position, out rotation);

        //Set wheel transform state
        trans.position = position;
        trans.rotation = rotation;


    }
}
