using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    System.Random newRandX = new System.Random();
    System.Random newRandZ = new System.Random();
    private Vector3 newPosition = new Vector3();
    float distance = 3, timer = 0;
    int timeTillTp = 15;

    private void Update()
    {
        if (timer >= timeTillTp)
        {
            timer = 0;
            ToTeleport();
        }
        else
            timer += Time.deltaTime;
    }

    private void ToTeleport()
    {
        int _newX = newRandX.Next(-50, 50);
        int _newZ = newRandZ.Next(-50, 50);

        newPosition = new Vector3(_newX, 0, _newZ);
        transform.position = newPosition;

    }

}