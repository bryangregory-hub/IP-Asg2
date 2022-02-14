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
    public ButtonType buttonType;
    

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
    private bool Play = false;
    private bool _playCheck = true;
    private bool _PlaySoundCheck = false;
    public float musicSpeed;
    public GameObject musicBar;
    public Animator anim;

    public void Start()
    {
       
    }
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
        if (anim.GetBool("pB") == true) 
        {

        }
        
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.onHoverEnter.AddListener(SetSelectMaterial);
        interactable.onHoverExit.AddListener(SetOriginalMaterial);
    }

    private void OnDestroy()
    {
        interactable.onHoverEnter.RemoveListener(SetSelectMaterial);
        interactable.onHoverExit.RemoveListener(SetOriginalMaterial);
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
        if (_PlaySoundCheck == true && _playCheck == true)
        {
            print("wdawdawdawd");
            anim.SetTrigger("check");
            
           
            _playCheck = false;
        }
        else if (_PlaySoundCheck == true && _playCheck == false)
        {
            anim.SetTrigger("check");
            _playCheck = true;
        }

    }

    private void SetOriginalMaterial(XRBaseInteractor interactor)
    {
        
        meshRenderer.material = originalMaterial;
    }
}
