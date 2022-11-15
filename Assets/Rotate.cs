using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < 5)
        {
            //change position
        }

        transform.Rotate(new Vector3(0f, 1f, 0f));
    }
}
