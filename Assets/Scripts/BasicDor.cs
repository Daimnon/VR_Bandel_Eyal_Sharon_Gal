using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDor : MonoBehaviour
{
    [SerializeField] private Rigidbody _hipsRb;
    public Rigidbody HipsRb => _hipsRb;

    [SerializeField] private bool _stopAllRagdollOnStart = false, _useGravityAllOnStart = true;
    [SerializeField] private bool _constainPosX = false, _constainPosY = false, _constainPosZ = false;
    [SerializeField] private bool _constainRotX = false, _constainRotY = false, _constainRotZ = false;
    [SerializeField] private bool _constainAllPos = false, _constainAllRot = false, _constainAll = false;

    private void Start()
    {
        if (_stopAllRagdollOnStart)
        {
            StopAllRagdolls();
            _useGravityAllOnStart = false;
        }
        else if (!_stopAllRagdollOnStart && !_useGravityAllOnStart)
        {
            StopAllGravity();
        }

        if (_constainAllPos)
        {
            _constainPosX = _constainPosY = _constainPosZ = true;
            ConstrainAllPos(_constainAllPos);
        }
        else
        {
            ConstrainPosX(_constainPosX);
            ConstrainPosY(_constainPosY);
            ConstrainPosZ(_constainPosZ);
        }

        if (_constainAllRot)
        {
            _constainRotX = _constainRotY = _constainRotZ = true;
            ConstrainAllRot(_constainAllRot);
        }
        else
        {
            ConstrainRotX(_constainRotX);
            ConstrainRotY(_constainRotY);
            ConstrainRotZ(_constainRotZ);
        }

        if (!(_constainPosX && _constainPosY && _constainPosZ))
            _constainAllPos = false;

        if (!(_constainRotX && _constainRotY && _constainRotZ))
            _constainAllRot = false;

    }

    /* Unused Dor States
     * 
     * private delegate void DorState();
     * private DorState _state;
     * 
     * private void Awake()
     * {
     *     _state = Ragdoll;
     * }
     * private void Update()
     * {
     *     //_state.Invoke();
     * }
     * 
     * private void Ragdoll()
     * {
     * 
     * }
     * private void NonRagdoll()
     * {
     * 
     * }
     */

    public void StartAllRagdolls()
    {
        foreach (Rigidbody childRb in GetComponentsInChildren<Rigidbody>())
        {
            childRb.useGravity = true;
            childRb.isKinematic = false;
        }
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
    public void ConstrainPosX(bool constraintPosX)
    {
        if (!constraintPosX)
            return;

        foreach (Rigidbody childRb in GetComponentsInChildren<Rigidbody>())
            childRb.constraints = RigidbodyConstraints.FreezePositionX;
    }
    public void ConstrainPosY(bool constraintPosY)
    {
        if (!constraintPosY)
            return;

        foreach (Rigidbody childRb in GetComponentsInChildren<Rigidbody>())
            childRb.constraints = RigidbodyConstraints.FreezePositionY;
    }
    public void ConstrainPosZ(bool constraintPosZ)
    {
        if (!constraintPosZ)
            return;

        foreach (Rigidbody childRb in GetComponentsInChildren<Rigidbody>())
            childRb.constraints = RigidbodyConstraints.FreezePositionZ;
    }
    public void ConstrainRotX(bool constraintRotX)
    {
        if (!constraintRotX)
            return;

        foreach (Rigidbody childRb in GetComponentsInChildren<Rigidbody>())
            childRb.constraints = RigidbodyConstraints.FreezeRotationX;
    }
    public void ConstrainRotY(bool constraintRotY)
    {
        if (!constraintRotY)
            return;

        foreach (Rigidbody childRb in GetComponentsInChildren<Rigidbody>())
            childRb.constraints = RigidbodyConstraints.FreezeRotationY;
    }
    public void ConstrainRotZ(bool constraintRotZ)
    {
        if (!constraintRotZ)
            return;

        foreach (Rigidbody childRb in GetComponentsInChildren<Rigidbody>())
            childRb.constraints = RigidbodyConstraints.FreezeRotationZ;
    }
    public void ConstrainAllPos(bool constrainAllPos)
    {
        if (!constrainAllPos)
            return;

        foreach (Rigidbody childRb in GetComponentsInChildren<Rigidbody>())
            childRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }
    public void ConstrainAllRot(bool constrainAllRot)
    {
        if (!constrainAllRot)
            return;

        foreach (Rigidbody childRb in GetComponentsInChildren<Rigidbody>())
            childRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
    public void ConstrainAllPosRot(bool freezeAll)
    {
        if (!freezeAll)
            return;

        foreach (Rigidbody childRb in GetComponentsInChildren<Rigidbody>())
            childRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
}
