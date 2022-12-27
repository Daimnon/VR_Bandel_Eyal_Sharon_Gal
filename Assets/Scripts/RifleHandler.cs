using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum RifleType { Regular, Super, Pompa }

public class RifleHandler : MonoBehaviour
{
    [SerializeField] GameObject _bulletPrefab, _bulletContainer;
    [SerializeField] Transform _bulletSpawnPoint, _grappleOrigin;
    [SerializeField] RifleType _rifleType;
    [SerializeField] Material _grappleMat;
    [SerializeField] float _grappleStartWidth, _grappleEndWidth;
    
    public InputActionProperty LeftpinchAnimationAction, RightpinchAnimationAction;
    public float BulletSpeedMultiplier;
    
    private LineRenderer _lineRenderer;
    private bool _isEquiped = false, _canFire = false;
    
    private void Update()
    {
        Debug.Log($"{LeftpinchAnimationAction.action.ReadValue<float>()}");
        Debug.Log($"{RightpinchAnimationAction.action.ReadValue<float>()}");

        if (RightpinchAnimationAction.action.triggered)
        {
            switch (_rifleType)
            {
                case RifleType.Regular:
                    Fire();
                    break;
                //case RifleType.Super:
                //    SuperFire();
                //    break;
                case RifleType.Pompa:
                    PompaFire();
                    break;
                default:
                    break;
            }
        }
        if (LeftpinchAnimationAction.action.IsPressed() || RightpinchAnimationAction.action.IsPressed())
        {
            if (_isEquiped && _canFire)
            {
                switch (_rifleType)
                {
                    //case RifleType.Regular:
                    //    Fire();
                    //    break;
                    case RifleType.Super:
                        SuperFire();
                        break;
                    //case RifleType.Pompa:
                    //    PompaFire();
                    //    break;
                    default:
                        break;
                }
            }
        }
    }

    private void PompaFire()
    {

        GameObject projectile = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity, _bulletContainer.transform);
        projectile.GetComponent<Rigidbody>().AddForce(this.transform.up.normalized * BulletSpeedMultiplier, ForceMode.Impulse);

        // Initialize Line -------------------------------
        projectile.AddComponent<LineRenderer>();
        _lineRenderer = projectile.GetComponent<LineRenderer>();
        _lineRenderer.material = _grappleMat;
        _lineRenderer.startWidth = _grappleStartWidth;
        _lineRenderer.endWidth = _grappleEndWidth;
        // ----------------------------------------

        // Set Line -------------------------------
        Vector3[] lrPositions = new Vector3[2];
        lrPositions[0] = _grappleOrigin.position;
        lrPositions[1] = projectile.transform.position;
        _lineRenderer.SetPositions(lrPositions);
        // ----------------------------------------



    }
    private void SuperFire()
    {
        var firedBullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity, _bulletContainer.transform);
        firedBullet.GetComponent<Rigidbody>().AddForce(this.transform.up.normalized*BulletSpeedMultiplier, ForceMode.Impulse);

    }
    private void Fire()
    {
        GameObject firedBullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity, _bulletContainer.transform);
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
