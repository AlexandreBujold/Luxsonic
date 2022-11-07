using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportToggler : MonoBehaviour
{
    [field: SerializeField]
    public bool RayEnabled { get; set; } = false;
    [SerializeField]
    private XRController teleportController;
    [SerializeField]
    private InputHelpers.Button activationButton;
    [SerializeField]
    private float activationThreshold = 0.1f;


    // Update is called once per frame
    void Update()
    {
        if (RayEnabled == false)
            return;

        if (teleportController)
        {
            teleportController.gameObject.SetActive(CheckIfActivated(teleportController));
        }
    }

    private bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, activationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}
