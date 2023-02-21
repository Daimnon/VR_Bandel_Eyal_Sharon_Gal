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
        ResetEvent.G_Reset += ResetEvent_G_Reset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Mathf.Log(_removeLayer.value))
            grabInteractable.enabled = false;
    }

    private void ResetEvent_G_Reset(object sender, System.EventArgs e)
    {
        if (grabInteractable)
            grabInteractable.enabled = true;
    }

}