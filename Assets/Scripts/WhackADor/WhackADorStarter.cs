using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackADorStarter : MonoBehaviour
{
    [SerializeField] private WhackADor _container;
    [SerializeField] private DorMole _mole;
    private int _hammerLayer = 9;

    private void OnCollisionEnter(Collision collision)
    {
        if (_mole.IsDoorUp && collision.collider.gameObject.layer == _hammerLayer)
        {

        }
    }
}
