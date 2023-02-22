using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorMover : MonoBehaviour
{
    [SerializeField] private Transform _transformToMove;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private bool _shouldStartCoroutine = false, _isLerping = false, _shouldStopLerping = false;
    public bool IsLerping => _isLerping;
    public bool ShouldStopLerping { get => _shouldStopLerping; set => _shouldStopLerping = value; }

    private float _patrolTime = 0.0f;
    private Transform _targetTransform;
    private IEnumerator _lerpCoroutine;

    private void Start()
    {
        _targetTransform = AIManager.Instance.ChooseRandomPatrolPos();

        if (_speed <= 0)
            _speed = 1.0f;

        _patrolTime = Vector3.Distance(transform.position, _targetTransform.position) / _speed;
    }
    private void Update()
    {
        if (_shouldStopLerping)
        {
            StopCoroutine(_lerpCoroutine);
            _shouldStartCoroutine = false;
            _shouldStopLerping = false;
            return;
        }

        if (_shouldStartCoroutine)
        {
            transform.LookAt(_targetTransform);
            _lerpCoroutine = LerpPosition(_targetTransform.position, _patrolTime);
            StartCoroutine(_lerpCoroutine);
            _isLerping = true;
            _shouldStartCoroutine = false;
        }

        if (transform.position == _targetTransform.position)
        {
            _targetTransform = AIManager.Instance.ChooseRandomPatrolPos();

            if (_speed <= 0)
                _speed = 1.0f;

            _patrolTime = Vector3.Distance(transform.position, _targetTransform.position) / _speed;
            _targetTransform.position = _targetTransform.position;

            transform.LookAt(_targetTransform);
            _lerpCoroutine = LerpPosition(_targetTransform.position, _patrolTime);
            StartCoroutine(_lerpCoroutine);
            _shouldStartCoroutine = false;
        }
    }

    private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = _transformToMove.position;

        while (time < duration && !_shouldStopLerping)
        {
            _transformToMove.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        _transformToMove.position = targetPosition;
    }
}
