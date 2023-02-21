using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    private static AIManager _instance;
    public static AIManager Instance => _instance;

    [SerializeField] private Transform[] _patrolPoses;
    public Transform[] PatrolPoses => _patrolPoses;

    private void Awake()
    {
        _instance = this;
    }

    public Transform ChooseRandomPatrolPos()
    {
        int randomPoint = Random.Range(0, _patrolPoses.Length);
        return _patrolPoses[randomPoint];
    }
}
