using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTarget : MonoBehaviour
{
    [SerializeField] ShootTheDorStation _shootTheDorStation;
    [SerializeField] LayerMask _bulletMask;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _bulletMask)
        {
            _shootTheDorStation.StartMiniGame();
            gameObject.SetActive(false);
        }
    }
}
