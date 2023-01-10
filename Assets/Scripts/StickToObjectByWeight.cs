using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToObjectByWeight : MonoBehaviour
{
    [SerializeField] private float _maxPullMass = 25, lerpDuration = 3;

    private const string _weaponTag = "Rifle";
    private Vector3 _targetPos = Vector3.zero;

    private Rigidbody _connectedObjectRb;
    public Rigidbody ConnectedObjectRb => _connectedObjectRb;

    private bool _objectConnected;
    public bool ObjectConnected => _objectConnected;

    private void Start()
    {
        _targetPos = transform.position;
    }
    private void Update()
    {
        if (_connectedObjectRb && !_connectedObjectRb.gameObject.CompareTag(_weaponTag))
        {
            if (!_objectConnected)
                _objectConnected = true;

            transform.position = _connectedObjectRb.transform.position;
        }
        else
        {
            _objectConnected = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(_weaponTag) && other.gameObject.TryGetComponent(out Rigidbody rb) && rb.mass <= _maxPullMass)
        {
            _connectedObjectRb = rb;
        }
    }
}
