using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControl : MonoBehaviour
{
    Rigidbody car;
    LogitechInput inputs;

    [Header("WheelColider")]
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backLeft;
    [SerializeField] WheelCollider backRight;
    [Header("WheelTransform")]
    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform backLeftTransform;
    [SerializeField] Transform backRightTransform;
    [Header("CarInform")]
    [SerializeField] private float Wheelradius = 11.4f;
    [SerializeField] private float MotorTorque;
    [SerializeField] private float BrakeTorque;
    [SerializeField] private float H_Input;
    [SerializeField] private float V_Input;
    [SerializeField] private float MaxSteerAngle;
    [SerializeField] private float Steerangle;
    [Header("Speed")]
    [SerializeField] private float GearRSpeed;
    [SerializeField] public float CurrentSpeed;
    [Header("CurrentCarInfo")]
    public int CurrentGear;
    public int[] Gearspeeds;
    public bool IgnitionOn;
    [Header("Text")]
    public Text GearText;
    [Header("Audio")]
    public AudioSource ads;
    public AudioClip StartCar;
    public AudioClip EngineSound;

    [Header("CarLight")]
    public Renderer brakelights;
    public Material brakelightON;
    public Material brakelightOFF;

    public Renderer headlights;
    public Material headlightsON;
    public Material headlightsOFF;

    public Light spotlightLEFT;
    public Light spotlightRIGHT;

    private bool rightSignalON = false;
    private bool leftSignalON = false;

    // Start is called before the first frame update
    void Start()
    {
        IgnitionOn = false;
        inputs = GetComponent<LogitechInput>();
        car = GetComponent<Rigidbody>();
        car.centerOfMass += new Vector3(0, -0.9f, 0);
        ads = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        H_Input = Input.GetAxis("Horizontal");
        V_Input = Input.GetAxis("Vertical");

        if (IgnitionOn == true)
        {
            //IgnitionPic.enabled = true;
            if (CurrentGear != 0)
            {
                Accelerate();
            }
            else
            {
                frontLeft.motorTorque = 0;
                frontRight.motorTorque = 0;
                ads.pitch = inputs.GasInput + 1;
            }
        }
        else
        {
            //IgnitionPic.enabled = false;
            frontLeft.motorTorque = 0;
            frontRight.motorTorque = 0;
            ads.Stop();
            ads.loop = false;
        }

        if (inputs.BrakeInput > 0)
        {
            brakelights.material = brakelightON;
            Brake();
        }
        else
        {
            brakelights.material = brakelightOFF;
            frontLeft.brakeTorque = 0;
            frontRight.brakeTorque = 0;
            backLeft.brakeTorque = 0;
            backRight.brakeTorque = 0;
        }

        Steer();
        //Update wheel meshes
        //UpdateWheel(frontLeft, frontLeftTransform);
        //UpdateWheel(frontRight, frontRightTransform);
        //UpdateWheel(backLeft, backLeftTransform);
        //UpdateWheel(backRight, backRightTransform);

        CurrentSpeed = Mathf.Round(car.velocity.magnitude * 3.6f);
        //SpeedText.text = CurrentSpeed.ToString() + " " + " KMPH ";
        GearText.text = "Gear :" + CurrentGear.ToString();

        if (CurrentGear == 0)
        {
            GearText.text = "Gear :N";
        }
        if (CurrentGear == -1)
        {
            GearText.text = "Gear :R";

        }

        if (Input.GetKeyDown("1") || LogitechGSDK.LogiButtonIsPressed(0, 0))
        {
            headlights.material = headlightsON;
            spotlightLEFT.intensity = 8f;
            spotlightRIGHT.intensity = 8f;
        }
        if (Input.GetKeyDown("2") || LogitechGSDK.LogiButtonIsPressed(0, 1))
        {
            headlights.material = headlightsOFF;
            spotlightLEFT.intensity = 0f;
            spotlightRIGHT.intensity = 0f;
        }
      

        if (leftSignalON)
        {
            float floor = 0f;
            float ceiling = 1f;
            float emission = floor + Mathf.PingPong(Time.time * 2f, ceiling - floor);
        }
        if (rightSignalON)
        {
            float floor = 0f;
            float ceiling = 1f;
            float emission = floor + Mathf.PingPong(Time.time * 2f, ceiling - floor);
        }
    }

    public void Accelerate()
    {
        if (CurrentGear > 0)
        {
            ads.pitch = 1 + (CurrentSpeed / Gearspeeds[CurrentGear - 1]);
            if (inputs.GasInput <= 0 && inputs.ClutchInput > 0.1 && inputs.ClutchInput < 1)
            {
                if (CurrentGear == 1)
                {
                    float ClutchSpeed = Mathf.Round(Gearspeeds[CurrentGear - 1] - (inputs.ClutchInput * Gearspeeds[CurrentGear - 1]));
                    if (CurrentSpeed <= ClutchSpeed / 2)
                    {
                        frontLeft.motorTorque = inputs.ClutchInput * (MotorTorque * 2.5f / 2);
                        frontRight.motorTorque = inputs.ClutchInput * (MotorTorque * 2.5f / 2);
                        frontLeft.brakeTorque = 0;
                        frontRight.brakeTorque = 0;
                        backLeft.brakeTorque = 0;
                        backRight.brakeTorque = 0;
                    }
                    else if (inputs.ClutchInput == 0 || inputs.ClutchInput >= 1)
                    {
                        frontLeft.motorTorque = 0;
                        frontRight.motorTorque = 0;
                    }
                    else
                    {
                        frontLeft.motorTorque = 0;
                        frontRight.motorTorque = 0;
                    }
                }
            }
            else if (inputs.ClutchInput < 0.75 && inputs.GasInput > 0)
            {
                float gspeed = Mathf.Round(Gearspeeds[CurrentGear - 1] * inputs.GasInput);
                if (CurrentSpeed <= gspeed)
                {
                    frontLeft.motorTorque = inputs.GasInput * (MotorTorque * 2.5f / 2);
                    frontRight.motorTorque = inputs.GasInput * (MotorTorque * 2.5f / 2);
                    frontLeft.brakeTorque = 0;
                    frontRight.brakeTorque = 0;
                    backLeft.brakeTorque = 0;
                    backRight.brakeTorque = 0;
                }
                else
                {
                    frontLeft.motorTorque = 0;
                    frontRight.motorTorque = 0;
                }
            }
            else
            {
                frontLeft.motorTorque = 0;
                frontRight.motorTorque = 0;
            }
        }
        else if (CurrentGear <= -1)
        {
            float rgspeed = Mathf.Round(GearRSpeed * inputs.GasInput);
            ads.pitch = 1 + (CurrentSpeed / GearRSpeed);
            if (inputs.ClutchInput <= 0.75 && inputs.GasInput > 0)
            {
                if (CurrentSpeed <= rgspeed)
                {
                    frontLeft.motorTorque = -inputs.GasInput * (MotorTorque * 2.5f / 2);
                    frontRight.motorTorque = -inputs.GasInput * (MotorTorque * 2.5f / 2);
                    frontLeft.brakeTorque = 0;
                    frontRight.brakeTorque = 0;
                    backLeft.brakeTorque = 0;
                    backRight.brakeTorque = 0;
                }
                else
                {
                    frontLeft.motorTorque = 0;
                    frontRight.motorTorque = 0;
                }
            }
            else
            {
                frontLeft.motorTorque = 0;
                frontRight.motorTorque = 0;
            }
        }
    }

    public void Steer()
    {
        if (inputs.SteerInput > 0)
        {
            frontLeft.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.78f / (Wheelradius + (1.515f / 2))) * inputs.SteerInput * 2;
            frontRight.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.78f / (Wheelradius - (1.515f / 2))) * inputs.SteerInput * 2;
        }
        else if (inputs.SteerInput < 0)
        {
            frontLeft.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.78f / (Wheelradius - (1.515f / 2))) * inputs.SteerInput * 2;
            frontRight.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.78f / (Wheelradius + (1.515f / 2))) * inputs.SteerInput * 2;
        }
        else
        {
            frontLeft.steerAngle = 0;
            frontRight.steerAngle = 0;
        }
    }

    public void Brake()
    {
        frontLeft.motorTorque = 0;
        frontRight.motorTorque = 0;
        frontLeft.brakeTorque = BrakeTorque;
        frontRight.brakeTorque = BrakeTorque;
        backLeft.brakeTorque = BrakeTorque / 2;
        backRight.brakeTorque = BrakeTorque / 2;
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

    public IEnumerator PlayEngineSound()
    {
        //Debug.Log("StartCoroutine");
        ads.clip = StartCar;
        ads.Play();
        yield return new WaitForSeconds(ads.clip.length);
        //Debug.Log("StartCoroutine1");
        ads.clip = EngineSound;
        ads.Play();
        ads.loop = true;
    }
}
