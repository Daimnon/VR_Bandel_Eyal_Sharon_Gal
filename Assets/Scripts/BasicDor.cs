using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDor : MonoBehaviour
{
    [SerializeField] private Rigidbody _hipsRb;
    public Rigidbody HipsRb => _hipsRb;

    [SerializeField] private bool _stopAllRagdoll = false, _useGravityAll = true;

    private delegate void DorState();
    private DorState _state;

    private void Awake()
    {
        _state = Ragdoll;
    }
    private void Start()
    {
        if (_stopAllRagdoll && !_useGravityAll)
        {
            foreach (Rigidbody childRb in GetComponentsInChildren<Rigidbody>())
            {
                childRb.useGravity = false;
                childRb.isKinematic = true;
            }
        }
        else if (!_stopAllRagdoll && !_useGravityAll)
        {
            foreach (Rigidbody childRb in GetComponentsInChildren<Rigidbody>())
            {
                childRb.useGravity = false;
            }
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
}
