using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dorball : MonoBehaviour
{
    public bool IsBaseDor;

    private void Start()
    {
        IsBaseDor = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsBaseDor && collision.gameObject.tag == "BaseDorBat")
        {
            DorBasketRunScore.Instance.IncreaseScoreBy(1);
            IsBaseDor = false;
        }
    }
}
