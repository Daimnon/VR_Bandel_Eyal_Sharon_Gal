using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class GrabRemoveEvent : MonoBehaviour
{
    public static event EventHandler<GrabRemover> RemoveGrab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dor"))
            RemoveGrab?.Invoke(this, other.GetComponent<GrabRemover>());
    }
}