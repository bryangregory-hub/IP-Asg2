/**
Author: Bryan Gregory Soh

Name of Class: teleport

Description of Class: the purpose of this script was to allow the player to teleport with indication from a ray curve.

Date Created: 5/02/2022
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class LocomotionController : MonoBehaviour
{
    public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;

    public bool enableLeftTeleport { get; set; } = true;
    public bool enableRightTeleport { get; set; } = true;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        if (leftTeleportRay)
        {
            leftTeleportRay.gameObject.SetActive(enableLeftTeleport&&CheckIfActivated(leftTeleportRay));
        }
        if (rightTeleportRay)
        {
            rightTeleportRay.gameObject.SetActive(enableRightTeleport&&CheckIfActivated(rightTeleportRay));
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}
