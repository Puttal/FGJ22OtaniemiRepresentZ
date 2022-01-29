using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerAssignment : MonoBehaviour
{
    InputDevice[] devices;
    void Awake()
    {
        InputSystem.onDeviceChange += DeviceChange;

        //devices = UnityEngine.InputSystem.InputControlList
    }
    

    // Update is called once per frame
    void Update()
    {
        var allGamepads = Gamepad.all;
        //Debug.Log(allGamepads);
    }

    private void DeviceChange(InputDevice device, InputDeviceChange change) {
        switch (change)
        {
            case InputDeviceChange.Added:
                Debug.Log("New device added: " + device);
                break;

            case InputDeviceChange.Removed:
                Debug.Log("Device removed: " + device);
                break;
        }
    }
}
