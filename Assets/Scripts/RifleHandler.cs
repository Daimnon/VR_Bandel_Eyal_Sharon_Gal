using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RifleHandler : MonoBehaviour
{
    public InputActionProperty LeftpinchAnimationAction;
    public InputActionProperty RightpinchAnimationAction;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _bulletSpawnPoint;
    [SerializeField] GameObject _bulletContainer;
    private void Update()
    {
        Debug.Log($"{LeftpinchAnimationAction.action.ReadValue<float>()}");
        Debug.Log($"{RightpinchAnimationAction.action.ReadValue<float>()}");

        if (LeftpinchAnimationAction.action.ReadValue<float>() > 0 || RightpinchAnimationAction.action.ReadValue<float>() > 0)
        {
            Fire();
        }
    }

    private void Fire()
    {
        var firedBullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity,_bulletContainer.transform);
    }
}
