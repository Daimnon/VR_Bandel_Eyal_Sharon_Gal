using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _transformToMove;
    [SerializeField] private bool _shouldStartCoroutine = false, _shouldStopLerping = false;
    public bool ShouldStopLerping { get => _shouldStopLerping; set => _shouldStopLerping = value; }

    private Transform _targetPos;
    private Vector3 _positionToMoveTo;

    private void Start()
    {
        _targetPos = AIManager.Instance.ChooseRandomPatrolPos();
        _positionToMoveTo = _targetPos.position;
    }
    private void Update()
    {
        if (_shouldStartCoroutine)
        {
            StartCoroutine(LerpPosition(_positionToMoveTo, 10));
            _shouldStartCoroutine = false;
        }

        if (transform.position == _targetPos.position)
        {
            _targetPos = AIManager.Instance.ChooseRandomPatrolPos();
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
