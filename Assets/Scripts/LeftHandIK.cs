using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LeftHandIK : MonoBehaviour
{
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Animator _animator;

    private void OnAnimatorIK(int layerIndex)
    {
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHand.position);
        _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHand.rotation);
    }   
}
