using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackADorStartButton : MonoBehaviour
{
    private int _hammerLayer = 9;
    [SerializeField] private WhackADor _station;

    private void OnTriggerEnter(Collider other)
    {
        if ((!_station.IsGameRunning) && (other.gameObject.layer == _hammerLayer))
        {
            Debug.Log("started game!");
            _station.StartGame();
        }
}
}
