using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsGiver : MonoBehaviour
{
    public static event System.EventHandler<int> TryGivePoint;

    [SerializeField] private string _tagLayer;
    [SerializeField] private int _pointsToGive = 0;
    [SerializeField] private bool _givePointsOnCollision = true, _givePointsOnClick = false, _givePointsOnTrigger = false, _givePointsOnCondition;
    //[SerializeField] private string[] _incomingObjectTag;
    //[SerializeField] private int[] _pointsPerTag;

    private void OnCollisionEnter(Collision collision)
    {
        if (_givePointsOnCollision && collision.gameObject.CompareTag(_tagLayer))
        {
            TryGivePoint?.Invoke(this, _pointsToGive);
            //ScoreManager.Instance.Score += _pointsToGive;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_givePointsOnTrigger && other.gameObject.CompareTag(_tagLayer))
        {
            TryGivePoint?.Invoke(this, _pointsToGive);
            //ScoreManager.Instance.Score += _pointsToGive;
        }
    }
    public void GivePointsOnClick()
    {
        TryGivePoint?.Invoke(this, _pointsToGive);
        //ScoreManager.Instance.Score += _pointsToGive;
    }
    public void GivePointsOnCondition(bool condition)
    {
       if (condition)
            TryGivePoint?.Invoke(this, _pointsToGive);
        //ScoreManager.Instance.Score += _pointsToGive;
    }
}
