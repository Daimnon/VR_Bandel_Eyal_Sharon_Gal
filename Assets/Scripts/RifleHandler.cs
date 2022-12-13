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
    [SerializeField] bool _isSuperRifle;
    bool _isEquiped = false;
    bool _canFire = false;
    public float BulletSpeedMultiplier;
    private void Update()
    {
        Debug.Log($"{LeftpinchAnimationAction.action.ReadValue<float>()}");
        Debug.Log($"{RightpinchAnimationAction.action.ReadValue<float>()}");

        if (LeftpinchAnimationAction.action.ReadValue<float>() > 0 || RightpinchAnimationAction.action.ReadValue<float>() > 0)
        {
            if (_isEquiped && _canFire)
            {
                if (_isSuperRifle)
                {
                    SuperFire();
                }
                else
                {
                Fire();
                }
            }
        }
    }

    private void SuperFire()
    {
        var firedBullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity, _bulletContainer.transform);
        firedBullet.GetComponent<Rigidbody>().AddForce(this.transform.up.normalized*BulletSpeedMultiplier, ForceMode.Impulse);

    }
    private void Fire()
    {

        var firedBullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity, _bulletContainer.transform);
    }
    public void GrabbedRifle()
    {
        _isEquiped = true;
    }
    public void UnGrabbedRifle()
    {
        _isEquiped = false;
    }
    public void Shoot()
    {
        _canFire = true;
    }
    public void Unshoot()
    {
        _canFire = false;
    }
}
