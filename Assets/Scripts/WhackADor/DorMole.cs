using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorMole : MonoBehaviour
{
    private int _hammerLayer = 9;
    [SerializeField] static private Vector3 _dorMovementAmount;
    private bool _isDoorUp = false;
    private void OnCollisionEnter(Collision collision)
    {
        if(_isDoorUp && collision.collider.gameObject.layer == _hammerLayer)
        {
            // Add Score
            MoveDorDown();
            
            Debug.Log("MoleDor Hit!");
        }
    }

    public void MoveDor(Vector3 movement, bool isDoorUp)
    {
        transform.position += movement;
        _isDoorUp = isDoorUp;
    }

    public void MoveDorUp()
    {
        MoveDor(_dorMovementAmount, true);
    }

    public void MoveDorDown()
    {
        MoveDor(_dorMovementAmount, false);
    }

}
