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
        PlaySound,
        book
    }
    public ButtonType buttonType;
    

    private MeshRenderer meshRenderer = null;
    private XRBaseInteractable interactable = null;
    private Material originalMaterial = null;
    public Material selectMaterial = null;
    public bool ColorBlind = true;

    [Header("Color Blind Settings")]
    
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


    [Header("Play Sound Settings")]
    private bool _bookCheck = false;
    private int bookList;
    public GameObject[] pages;
    private int _pages;

    public void Start()
    {
        switch (buttonType)
        {
            case ButtonType.PlaySound:
                _PlaySoundCheck = true;
                break;
            case ButtonType.ColorChange:
                _ColorCheck = true;
                break;
            case ButtonType.book:
                _bookCheck = true;
                break;
        }
        if (_bookCheck ==true)
        {
            pages = new GameObject[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                pages[i] = transform.GetChild(i).gameObject;
            }
            foreach(GameObject go in pages)
            {
                go.SetActive(false);
            }
            if (pages[0])
            {
                pages[0].SetActive(true);
            }
        }
    }
    
    public void Update()
    {
       
        if (anim.GetBool("pB") == true) 
        {

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
    public void cycleBookBack ()
    {
        pages[_pages].SetActive(false);
        _pages--;
        if (_pages<0)
        {
            _pages = pages.Length - 1;
        }
        pages[_pages].SetActive(true);
    }
}
