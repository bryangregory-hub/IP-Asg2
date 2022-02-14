using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;
using System;
public class ColorChanger : MonoBehaviour
{
    public Material selectMaterial = null;

    private MeshRenderer meshRenderer = null;
    private XRBaseInteractable interactable = null;
    private Material originalMaterial = null;

    public bool ColorBlind = true;
    [SerializeField]
    public Material material;

    public TextMeshPro _tColor;
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
        
        if (ColorBlind == true)
        {
            _tColor.enabled = true;
            _tColor.color = Color.blue;
            material.color = Color.blue;
            ColorBlind = false;
            _tColor.text = "Color Blind Mode - Blue";
        }
        else if (ColorBlind == false)
        {
            _tColor.enabled = true;
            _tColor.color = Color.yellow;
            material.color = Color.yellow;
            ColorBlind = true;
            _tColor.text = "Color Blind Mode - Yellow";
        }
    }

    private void SetOriginalMaterial(XRBaseInteractor interactor)
    {
        
        meshRenderer.material = originalMaterial;
    }
}
