using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class ResetEvent : MonoBehaviour
{
    public static event EventHandler G_Reset;

    private void OnTriggerEnter(Collider other)
    {
        G_Reset?.Invoke(this, EventArgs.Empty);
    }

}