using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LeftLegIK : MonoBehaviour
{
    [SerializeField] private Transform _leftLeg;
    [SerializeField] private Animator _animator;

    private void OnAnimatorIK(int layerIndex)
    {
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
        _animator.SetIKPosition(AvatarIKGoal.LeftFoot, _leftLeg.position);
        _animator.SetIKRotation(AvatarIKGoal.LeftFoot, _leftLeg.rotation);
    }
}
