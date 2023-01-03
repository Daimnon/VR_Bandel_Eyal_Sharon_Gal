using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToObjectByWeight : MonoBehaviour
{
    [SerializeField] private float _maxPullMass = 25, lerpDuration = 3;

    private const string _weaponTag = "Rifle";
    private Rigidbody _objectRb;
    private Vector3 _targetPos = Vector3.zero;

    private void Start()
    {
        _targetPos = transform.position;
    }
    private void Update()
    {
        if (_objectRb && !_objectRb.gameObject.CompareTag(_weaponTag))
            transform.position = _objectRb.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(_weaponTag) && other.gameObject.TryGetComponent(out Rigidbody rb) && rb.mass <= _maxPullMass)
        {
            StartCoroutine(PullObject(_targetPos, lerpDuration));
            _objectRb = rb;
        }
    }

    IEnumerator PullObject(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            _objectRb.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
