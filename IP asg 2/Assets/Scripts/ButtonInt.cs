using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;
using System;
public class ButtonInt : MonoBehaviour
{
    public enum ButtonType
    {
        ColorChange,
        PlaySound
    }
    ButtonType buttonType;
    

    private MeshRenderer meshRenderer = null;
    private XRBaseInteractable interactable = null;
    private Material originalMaterial = null;
    public Material selectMaterial = null;
    public bool ColorBlind = true;

    [Header("Color Blind Settings")]
    [SerializeField]
    public Material _Cblind;

    public TextMeshPro _tColor;
    private bool _ColorCheck = false;

    [Header("Play Sound Settings")]
    private bool _PlaySoundCheck = false;
    public void Update()
    {
        switch(buttonType)
        {
            case ButtonType.PlaySound:
                _PlaySoundCheck = true;
                break;
            case ButtonType.ColorChange:
                _ColorCheck = true;
                break;
        }
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.onHoverEntered.AddListener(SetSelectMaterial);
        interactable.onHoverExited.AddListener(SetOriginalMaterial);
    }

    private void OnDestroy()
    {
        interactable.onHoverEntered.RemoveListener(SetSelectMaterial);
        interactable.onHoverExited.RemoveListener(SetOriginalMaterial);
    }

    private void SetSelectMaterial(XRBaseInteractor interactor)
    {
        meshRenderer.material = selectMaterial;
        
        if (ColorBlind == true&& _ColorCheck == true)
        {
            _tColor.enabled = true;
            _tColor.color = Color.blue;
            _Cblind.color = Color.blue;
            ColorBlind = false;
            _tColor.text = "Color Blind Mode - Blue";
        }
        else if (ColorBlind == false &&  _ColorCheck == true)
        {
            _tColor.enabled = true;
            _tColor.color = Color.yellow;
            _Cblind.color = Color.yellow;
            ColorBlind = true;
            _tColor.text = "Color Blind Mode - Yellow";
        }
    }

    private void SetOriginalMaterial(XRBaseInteractor interactor)
    {
        
        meshRenderer.material = originalMaterial;
    }
}
