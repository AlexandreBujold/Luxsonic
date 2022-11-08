using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandAnimatorController : MonoBehaviour
{
    [SerializeField]
    private InputDeviceCharacteristics characteristics;

    private InputDevice targetDevice;
    [SerializeField]
    private Animator handAnimator = null;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        //Get Devices
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);

        if (devices.Count == 0)
        {
            return;
        }

        targetDevice = devices[0];
    }

    void Update()
    {
        //Keep trying to get the device if failed
        if (targetDevice.isValid == false)
        {
            Initialize();
        }

        UpdateHandAnimation();
    }

    private void UpdateHandAnimation()
    {
        if (handAnimator.gameObject.activeSelf == false)
            return;

        //Trigger
        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float trigger);
        handAnimator.SetFloat("Trigger", trigger);

        //Grip
        targetDevice.TryGetFeatureValue(CommonUsages.grip, out float grip);
        handAnimator.SetFloat("Grip", grip);
    }
}
