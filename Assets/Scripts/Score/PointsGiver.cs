using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsGiver : MonoBehaviour
{
    [SerializeField] private string _tagLayer;
    [SerializeField] private int _pointsToGive = 0;
    [SerializeField] private bool _givePointsOnCollision = true, _givePointsOnClick = false, _givePointsOnTrigger = false, _givePointsOnCondition;
    //[SerializeField] private string[] _incomingObjectTag;
    //[SerializeField] private int[] _pointsPerTag;

    private void OnCollisionEnter(Collision collision)
    {
        if (_givePointsOnCollision && collision.gameObject.CompareTag(_tagLayer))
        {
            ScoreManager.Instance.Score += _pointsToGive;
        }

        //for (int i = 0; i < _incomingObjectTag.Length; i++)
        //{
        //    if (collision.gameObject.CompareTag(_incomingObjectTag[i]))
        //    {
        //        //_pointsToGive = _pointsPerTag[i];
        //        Debug.Log("Got Points");
        //        ScoreManager.Instance.Score += _pointsToGive;
        //    }
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_givePointsOnTrigger && other.gameObject.CompareTag(_tagLayer))
        {
            ScoreManager.Instance.Score += _pointsToGive;
        }
    }
    public void GivePointsOnClick()
    {
        ScoreManager.Instance.Score += _pointsToGive;
    }
    public void GivePointsOnCondition(bool condition)
    {
       if (condition)
            ScoreManager.Instance.Score += _pointsToGive;
    }
}
