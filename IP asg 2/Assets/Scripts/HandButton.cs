using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class HandButton : XRBaseInteractable
{
    public enum ButtonType
    {
        note,
        piano
    }
    public ButtonType _buttonType;

    public void Update()
    {
        switch(_buttonType)
        {
            case ButtonType.note:
                break;
            case ButtonType.piano:
                break;
        }
    }

}
