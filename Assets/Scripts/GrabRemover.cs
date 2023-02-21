using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GrabRemover : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private LayerMask _removeLayer;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        GrabRemoveEvent.RemoveGrab += GrabRemoveEvent_RemoveGrab;
        ResetEvent.G_Reset += ResetEvent_G_Reset;
    }

    private void GrabRemoveEvent_RemoveGrab(object sender, GrabRemover e)
    {
        if (e == this)
            grabInteractable.enabled = false;
    }

    private void ResetEvent_G_Reset(object sender, System.EventArgs e)
    {
        if (grabInteractable)
            grabInteractable.enabled = true;
    }

}