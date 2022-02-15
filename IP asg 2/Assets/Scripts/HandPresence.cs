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
        yield return new WaitForSeconds(delayTime);
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        if (devices.Count > 0)
        {
            targetDevice = devices[0];

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimate = spawnedHandModel.GetComponent<Animator>();
        }
    }
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
