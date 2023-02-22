using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDor : MonoBehaviour
{
    [SerializeField] private Rigidbody _hipsRb;
    public Rigidbody HipsRb => _hipsRb;

    [SerializeField] private bool _stopAllRagdollOnStart = false, _useGravityAllOnStart = true;

    private delegate void DorState();
    private DorState _state;

    private void Awake()
    {
        _state = Ragdoll;
    }
    private void Start()
    {
        if (_stopAllRagdollOnStart && !_useGravityAllOnStart)
        {
            StopAllRagdolls();
        }
        else if (!_stopAllRagdollOnStart && !_useGravityAllOnStart)
        {
            
        }
    }
    private void Update()
    {
        //_state.Invoke();
    }

    private void Ragdoll()
    {

    }
    private void NonRagdoll()
    {

    }
    public void StopAllRagdolls()
    {
        foreach (Rigidbody childRb in GetComponentsInChildren<Rigidbody>())
        {
            childRb.useGravity = false;
            childRb.isKinematic = true;
        }
    }
    public void StopAllGravity()
    {
        foreach (Rigidbody childRb in GetComponentsInChildren<Rigidbody>())
        {
            childRb.useGravity = false;
        }
    }
}
