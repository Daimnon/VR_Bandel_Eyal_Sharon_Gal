using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShuriken : MonoBehaviour
{
    [SerializeField] private Rigidbody _shurikenRb;
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Rotate(new Vector3(0f, _speed * Time.deltaTime, 0f));
    }

    //public void RotateByHandVelocity(Rigidbody hand)
    //{
    //    _shurikenRb.AddTorque(new Vector3(0f, _speed * Time.fixedDeltaTime, 0f), ForceMode.Impulse);
    //}
}
