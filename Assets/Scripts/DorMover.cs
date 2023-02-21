using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _shouldStartCoroutine = false, _shouldStopLerping = false;
    [SerializeField] private Rigidbody _rb;
    public bool ShouldStopLerping { get => _shouldStopLerping; set => _shouldStopLerping = value; }

    private Transform _transformToMove, _targetPos;
    private Vector3 _positionToMoveTo;

    private void Awake()
    {
        if (_rb.useGravity)
            _rb.useGravity = false;

        if (_rb.isKinematic)
            _rb.isKinematic = true;
    }
    private void Start()
    {
        _targetPos = AIManager.Instance.ChooseRandomPatrolPos();
    }
    private void Update()
    {
        if (_shouldStartCoroutine)
        {
            _positionToMoveTo = _targetPos.position;
            StartCoroutine(LerpPosition(_positionToMoveTo, 10));
            _shouldStartCoroutine = false;
        }

        if (_shouldStartCoroutine && transform.position == _targetPos.position)
        {
            _positionToMoveTo = _targetPos.position;

            StartCoroutine(LerpPosition(_positionToMoveTo, 5));
            _shouldStartCoroutine = false;
        }

        if (_shouldStopLerping)
            StopCoroutine(LerpPosition(_positionToMoveTo, 5));
    }

    private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = _transformToMove.position;
        while (time < duration)
        {
            _transformToMove.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        _transformToMove.position = targetPosition;
    }
}
