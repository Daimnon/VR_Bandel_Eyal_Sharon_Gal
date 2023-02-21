using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RightHandIK : MonoBehaviour
{
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Animator _animator;

    private void OnAnimatorIK(int layerIndex)
    {
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHand.position);
        _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHand.rotation);
    }
}
