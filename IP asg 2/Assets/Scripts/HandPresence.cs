using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class HandPresence : MonoBehaviour
{
    private InputDevice targetDevice;
    // Start is called before the first frame update
    void Start()
    {
        
    }
     void FixedUpdate()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        if (devices.Count>0)
        {
            targetDevice = devices[0];
        }
    }
    // Update is called once per frame
    void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.secondary2DAxisClick, out bool primaryButtonValue);
        if (primaryButtonValue)
        {
            Debug.Log("Pressing Primary Button");
        }
        
        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.1f)
        {
            Debug.Log("Pressing trigger Button" + triggerValue);

        }
        targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 Primary2D);
        if (Primary2D != Vector2.zero)
        {
            Debug.Log("Pressing TouchPad" + Primary2D);

        }
    }
}
