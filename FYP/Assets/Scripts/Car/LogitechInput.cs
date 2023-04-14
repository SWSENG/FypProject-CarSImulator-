using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogitechInput : MonoBehaviour
{
    LogitechGSDK.LogiControllerPropertiesData properties;
    [Header("LogitechInput")]
    public float GasInput;
    public float BrakeInput;
    public float SteerInput;
    public float ClutchInput;
    public int GearInput;

    public Text startCarEngine;
    public Text clutchGear;


    // Start is called before the first frame update
    void Start()
    {
        print(LogitechGSDK.LogiSteeringInitialize(false));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GearShifter();
        if (LogitechGSDK.LogiButtonTriggered(0, 2))
        {
                if (GetComponent<CarControl>().IgnitionOn == false)
                {
                    GetComponent<CarControl>().IgnitionOn = true;
                    startCarEngine.gameObject.SetActive(false);
                    clutchGear.gameObject.SetActive(true);
                    if (GetComponent<CarControl>().ads.isPlaying == false)
                    {
                        GetComponent<CarControl>().StartCoroutine(GetComponent<CarControl>().PlayEngineSound());
                    }
                }
                else
                {
                    GetComponent<CarControl>().IgnitionOn = false;
                }
            
        }

        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES L_Input;
            L_Input = LogitechGSDK.LogiGetStateUnity(0);
            //Steeering
            // Steering wheel value  range [-32768, 32767]
            SteerInput = L_Input.lX / 32768f;
            //Accelerator
            // Throttle value  range [-32768, 32767] divide is to change the value to 1 or -1
            if (L_Input.lY > 0)
            {
                GasInput = 0;
            }
            else if (L_Input.lY < 0)
            {
                GasInput = L_Input.lY / -32768f;
            }
            //Brake
            // Brake value  range [-32768, 32767]
            if (L_Input.lRz > 0)
            {
                BrakeInput = 0;
            }
            else if (L_Input.lRz < 0)
            {
                BrakeInput = L_Input.lRz / -32768f;
            }
            //Clutch
            // Clutch value  range [-32768, 32767]
            if (L_Input.rglSlider[0] > 0)
            {
                ClutchInput = 0;
            }
            else if (L_Input.rglSlider[0] < 0)
            {
                ClutchInput = L_Input.rglSlider[0] / -32768f;
            }
        }
    }
    public void GearShifter()
    {
        if (ClutchInput > 0)
        {
            clutchGear.gameObject.SetActive(false);
            if (LogitechGSDK.LogiButtonIsPressed(0, 12))
            {
                GetComponent<CarControl>().CurrentGear = 1;
            }
            if (LogitechGSDK.LogiButtonIsPressed(0, 13))
            {
                GetComponent<CarControl>().CurrentGear = 2;
            }
            if (LogitechGSDK.LogiButtonIsPressed(0, 14))
            {
                GetComponent<CarControl>().CurrentGear = 3;
            }
            if (LogitechGSDK.LogiButtonIsPressed(0, 15))
            {
                GetComponent<CarControl>().CurrentGear = 4;
            }
            if (LogitechGSDK.LogiButtonIsPressed(0, 16))
            {
                GetComponent<CarControl>().CurrentGear = 5;
            }
            if (LogitechGSDK.LogiButtonIsPressed(0, 17))
            {
                GetComponent<CarControl>().CurrentGear = -1;
            }
        }
        for (int i = 12; i < 18; i++)
        {
            if (LogitechGSDK.LogiButtonReleased(0, i))
            {
                GetComponent<CarControl>().CurrentGear = 0;
            }
        }

    }  
}
