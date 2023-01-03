using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum RifleType { Regular, Super, Pompa }

public class RifleHandler : MonoBehaviour
{
    [SerializeField] GameObject _bulletPrefab, _bulletContainer;
    [SerializeField] Transform _bulletOriginTr;
    [SerializeField] RifleType _rifleType;
    [SerializeField] Material _grappleMat;
    [SerializeField] float _grappleStartWidth, _grappleEndWidth;
    
    public InputActionProperty LeftpinchAnimationAction, RightpinchAnimationAction;
    public float BulletSpeedMultiplier;
    
    private LineRenderer _lineRenderer;
    private bool _isEquiped = false, _canFire = false, _pompaLive = false;
    private Vector3 _lastAnchoredPos = Vector3.up, _lastAnchoredDir = Vector3.zero;

    #region MonoBehaviour Callbacks
    private void Update()
    {
        Debug.Log($"{LeftpinchAnimationAction.action.ReadValue<float>()}");
        Debug.Log($"{RightpinchAnimationAction.action.ReadValue<float>()}");

        if (_isEquiped && _canFire)
        {
            WeaponFireBtnDown();
            WeaponFireBtn();
            WeaponFireUp();
        }
    }
    #endregion

    #region Fire Handling
    private void WeaponFireBtnDown()
    {
        if (RightpinchAnimationAction.action.triggered)
        {
            ChooseRifleOnFireTrigger();
        }
    }
    private void WeaponFireBtn()
    {
        if (LeftpinchAnimationAction.action.IsPressed() || RightpinchAnimationAction.action.IsPressed())
        {
            ChooseRifleOnFireContinue();
        }
    }
    private void WeaponFireUp()
    {
        if (RightpinchAnimationAction.action.WasReleasedThisFrame())
        {
            ChooseRifleOnRelease();
        }
    }
    #endregion

    #region Rifle Choosing
    private void ChooseRifleOnFireTrigger()
    {
        switch (_rifleType)
        {
            case RifleType.Regular:
                Fire();
                break;
            case RifleType.Super:
                return;
            case RifleType.Pompa:
                PompaFire();
                break;
            default:
                break;
        }
    }
    private void ChooseRifleOnFireContinue()
    {
        switch (_rifleType)
        {
            case RifleType.Regular:
                return;
            case RifleType.Super:
                SuperFire();
                break;
            case RifleType.Pompa:
                AdjustLineRendererToBullet();
                break;
            default:
                break;
        }
    }
    private void ChooseRifleOnRelease()
    {
        switch (_rifleType)
        {
            case RifleType.Regular:
                return;
            case RifleType.Super:
                return;
            case RifleType.Pompa:
                ReleasePompa();
                break;
            default:
                break;
        }
    }
    #endregion

    #region Pompa Behavior
    private void PompaFire()
    {
        GameObject projectile = Instantiate(_bulletPrefab, _bulletOriginTr.position, Quaternion.identity, _bulletContainer.transform);
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
        lrPositions[0] = _bulletOriginTr.position;
        lrPositions[1] = projectile.transform.position;
        _lineRenderer.SetPositions(lrPositions);
        _bulletOriginTr = projectile.transform;
        // ----------------------------------------

        _pompaLive = true;
    }
    private void AdjustLineRendererToBullet()
    {
        if (_pompaLive)
        {
            if (_lineRenderer)
            {
                _lineRenderer.SetPosition(0, _bulletOriginTr.position);
            }
            else
            {
                Debug.Log("No LineRenderer");
                return;
            }
        }
    }
    private void ReleasePompa()
    {
        _lastAnchoredPos = Vector3.up;
        _lastAnchoredDir = Vector3.zero;
    
        if (_lineRenderer)
            Destroy(_lineRenderer.gameObject);
    }
    #endregion

    #region SuperRifle Behavior
    private void SuperFire()
    {
        var firedBullet = Instantiate(_bulletPrefab, _bulletOriginTr.position, Quaternion.identity, _bulletContainer.transform);
        firedBullet.GetComponent<Rigidbody>().AddForce(this.transform.up.normalized*BulletSpeedMultiplier, ForceMode.Impulse);

    }
    #endregion

    #region Fire Behavior
    private void Fire()
    {
        GameObject firedBullet = Instantiate(_bulletPrefab, _bulletOriginTr.position, Quaternion.identity, _bulletContainer.transform);
    }
    #endregion

    #region OnClick
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
    #endregion
}
