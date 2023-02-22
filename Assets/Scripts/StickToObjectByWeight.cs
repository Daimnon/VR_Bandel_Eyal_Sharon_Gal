using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToObjectByWeight : MonoBehaviour
{
    [SerializeField] private float _maxPullMass = 25;

    //private Vector3 _originalPos = Vector3.zero;

    private const string _weaponTag = "Rifle";
    private const string _dorTag = "Dor";

    [SerializeField] private Rigidbody _rb;
    public Rigidbody Rb => _rb;

    private Rigidbody _connectedObjectRb;
    public Rigidbody ConnectedObjectRb => _connectedObjectRb;

    private bool _objectConnected = false;
    public bool ObjectConnected => _objectConnected;

    private void Awake()
    {
        //_originalPos = transform.position;
    }
    private void Update()
    {
        if (_objectConnected && !_connectedObjectRb.gameObject.CompareTag(_weaponTag))
        {
            _connectedObjectRb.transform.position = transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(_weaponTag) && other.gameObject.TryGetComponent(out Rigidbody rb) && rb.mass <= _maxPullMass)
        {
            if (rb.transform.root.CompareTag(_dorTag))
                GrabDor(rb);
            else
                GrabNonDorObject(rb);
        }
    }

    private void GrabNonDorObject(Rigidbody rb)
    {
        _connectedObjectRb = rb;
        _connectedObjectRb.useGravity = false;
        _connectedObjectRb.isKinematic = true;
        _objectConnected = true;
    }
    private void GrabDor(Rigidbody rb)
    {
        BasicDor basicDor = rb.GetComponentInParent<BasicDor>();
        DorMover dorMover = rb.GetComponentInParent<DorMover>();
        _connectedObjectRb = basicDor.HipsRb;

        if (dorMover.IsLerping)
            dorMover.ShouldStopLerping = true;

        basicDor.StopAllRagdolls();

        _objectConnected = true;
    }
}
