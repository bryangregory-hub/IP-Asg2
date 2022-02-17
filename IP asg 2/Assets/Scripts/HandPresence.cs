/**
Author: Bryan Gregory Soh

Name of Class: vr hand 

Description of Class: if vr headset is dected place hands on the player with animation

Date Created: 12/02/2022
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class HandPresence : MonoBehaviour
{
    public bool showContoller = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    //private bool calledOnce = false;
    private Animator handAnimate;

    public GameObject lefthand_musicNote;
    public GameObject righthand_musicNote;

    public GameObject lefthand_interact;
    public GameObject righthand_interact;

    private bool _musicNoteActive=true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetDevices(5));
    }
    
    IEnumerator GetDevices(float delayTime)
    {
        //due to the headset having a delay with game in play i had to make a coroutuine wait a few seconds to detect the headset.
        yield return new WaitForSeconds(delayTime);
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        if (devices.Count > 0)
        {
            //if the vr hands are dectech hand models will spawn allowing it to animate the hands to grab and interact.
            targetDevice = devices[0];

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimate = spawnedHandModel.GetComponent<Animator>();
        }
    }
    //idntifty the different types of input type and controls on the hand set
    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerVal))
        {
           
            handAnimate.SetFloat("Trigger", triggerVal);
        }
        else
        {
            
            handAnimate.SetFloat("Trigger", 0);
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripVal))
        {
            handAnimate.SetFloat("Grip", gripVal);
        }
        else
        {
            handAnimate.SetFloat("Grip", 0);
        }
    }


    // Update is called once per frame
    void Update()
    {
        UpdateHandAnimation();

        targetDevice.TryGetFeatureValue(CommonUsages.secondary2DAxisClick, out bool primaryButtonValue);
        if (primaryButtonValue)
        {
            if (_musicNoteActive == false)
            {
                lefthand_musicNote.gameObject.SetActive(true);
                righthand_musicNote.gameObject.SetActive(true);


                _musicNoteActive = true;
            }
            else if (_musicNoteActive == true)
            {
                lefthand_musicNote.gameObject.SetActive(false);
                righthand_musicNote.gameObject.SetActive(false);


                _musicNoteActive = false;

                //Debug.Log("Pressing Primary Button");
            }

            targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
            if (triggerValue > 0.1f)
            {

                //Debug.Log("Pressing trigger Button" + triggerValue);

            }
            targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 Primary2D);
            if (Primary2D != Vector2.zero)
            {

                //Debug.Log("Pressing TouchPad" + Primary2D);

            }

        }
    }
}
