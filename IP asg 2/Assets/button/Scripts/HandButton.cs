using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class HandButton : XRBaseInteractable
{
    private float yMin = 0.0f;
    private float yMax = 0.0f;
    private float previousHandHeight = 0.0f;
    private XRBaseInteractor hoverInteractor = null;
    protected override void Awake()
    {
        base.Awake();
        onHoverEnter.AddListener(StartPress);
        onHoverEnter.AddListener(EndPress);
    }
    private void OnDestroy()
    {
        onHoverEnter.RemoveListener(StartPress);
        onHoverEnter.RemoveListener(EndPress);
    }
    private void StartPress(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        previousHandHeight = interactor.transform.position.y;
    }
    private void EndPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        previousHandHeight = 0.0f;
    }
    private void Start()
    {
        SetMinMax();
    }
    private void SetMinMax()
    {
        Collider collider = GetComponent<Collider>();
        yMin = transform.position.y - (collider.bounds.size.y * 0.5f);
        yMax = transform.position.y;
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
    }
    private float GetLocalYPosition(Vector3 position)
    {
        return 0.0f;
    }
    private void CheckPress()
    {

    }
}
