using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum RifleType { Regular, Super, Pompa }
public enum ShootType {OnPress,OnHold,OnRelease }

public class RifleHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab, _pompaBullet, _bulletContainer;
    [SerializeField] private Transform _bulletOriginTr;
    [SerializeField] private RifleType _rifleType;
    [SerializeField] private ShootType _shootType;
    [SerializeField] private Material _grappleMat;
    [SerializeField] private float _minPompaDistance = 2f, _maxPompaDistance = 20f, _pompaStartWidth = 0.25f, _pompaEndWidth = 0.25f, _pompaPullSpeed = 10;
    [SerializeField] private bool _isToBeDestoryed;
    [SerializeField] private float _destructionTime;

    private GameObject _pompaCurrentProjectile;
    private Transform _pompaCurrentProjectileTr;
    private StickToObjectByWeight _pompaCurrentProjectileScript;
    private Rigidbody _attachedToPompaObjectRb;
    private LineRenderer _lineRenderer;

    private const string _dorTag = "Dor";
    private bool _isEquiped = false, _canFire = false, _isPompaLive = false, _isPompaReturning, _attachedObject = false;
    
    public InputActionProperty LeftpinchAnimationAction, RightpinchAnimationAction;
    public float BulletSpeedMultiplier;

    #region MonoBehaviour Callbacks
    private void Update()
    {
        //Debug.Log($"{LeftpinchAnimationAction.action.ReadValue<float>()}");
        //Debug.Log($"{RightpinchAnimationAction.action.ReadValue<float>()}");

        if (_isEquiped && _canFire)
        {
            if (_shootType == ShootType.OnPress)
            {
                WeaponFireBtnDown();
            }
            else if (_shootType == ShootType.OnHold)
            {
                WeaponFireBtn();
            }
            else if (_shootType == ShootType.OnRelease)
            {
                WeaponFireUp();
            }
        }

        if (_rifleType == RifleType.Pompa && _isEquiped && _pompaCurrentProjectileScript && !_isPompaReturning)
        {
            if (_pompaCurrentProjectileScript.ConnectedObjectRb || (Vector3.Distance(_pompaCurrentProjectileTr.position, _bulletOriginTr.position) > _maxPompaDistance))
            {
                _isPompaReturning = true;
                _pompaCurrentProjectileScript.Rb.velocity = Vector3.zero;
                //_pompaCurrentProjectileScript.Rb.useGravity = false;
                StartCoroutine(PullPompaConnectedObject());
                _pompaCurrentProjectileScript.Rb.useGravity = false;
                //_pompaCurrentProjectileScript.Rb.isKinematic = true;
            }
        }

        PompaFire();

        if (_attachedObject)
            _attachedToPompaObjectRb.position = _bulletOriginTr.position;
    }
    #endregion

    #region Fire Handling
    private void WeaponFireBtnDown()
    {
        if (RightpinchAnimationAction.action.WasPressedThisFrame())
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
                NormalFireDown();
                break;
            case RifleType.Super:
                return;
            case RifleType.Pompa:
                PompaFireDown();
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
                //PompaFire();
                return;
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
                //PompaFireUp();
                return;
            default:
                break;
        }
    }
    #endregion

    #region Pompa Behavior
    private void PompaFireDown()
    {
        // remove previously attached object
        if (_attachedObject)
        {
            _attachedObject = false;
            _attachedToPompaObjectRb.useGravity = true;

            foreach (Rigidbody childRb in _attachedToPompaObjectRb.GetComponentsInChildren<Rigidbody>())
            {
                childRb.useGravity = true;
                childRb.isKinematic = false;
            }

            _attachedToPompaObjectRb.isKinematic = false;
            _attachedToPompaObjectRb.GetComponent<Collider>().enabled = true;
            return;
        }

        // check if there is already a projectile
        if (_pompaCurrentProjectile)
            return;

        // create projectile and get relevant components
        _pompaCurrentProjectile = Instantiate(_pompaBullet, _bulletOriginTr.position, Quaternion.identity, _bulletContainer.transform);
        _pompaCurrentProjectile.GetComponent<Rigidbody>().AddForce(transform.up.normalized * BulletSpeedMultiplier, ForceMode.Impulse);
        _pompaCurrentProjectileScript = _pompaCurrentProjectile.GetComponent<StickToObjectByWeight>();

        // Initialize Line -------------------------------
        _pompaCurrentProjectile.AddComponent<LineRenderer>();
        _lineRenderer = _pompaCurrentProjectile.GetComponent<LineRenderer>();
        _lineRenderer.material = _grappleMat;
        _lineRenderer.startWidth = _pompaStartWidth;
        _lineRenderer.endWidth = _pompaEndWidth;
        // ----------------------------------------

        // Set Line -------------------------------
        Vector3[] lrPositions = new Vector3[2];
        lrPositions[0] = _bulletOriginTr.position;
        lrPositions[1] = _pompaCurrentProjectile.transform.position;
        _lineRenderer.SetPositions(lrPositions);
        _pompaCurrentProjectileTr = _pompaCurrentProjectile.transform;
        // ----------------------------------------

        _isPompaLive = true;
    }
    private void PompaFire()
    {
        if (_isPompaLive)
        {
            if (_lineRenderer)
            {
                UpdatePompaLine();
            }
            else
            {
                Debug.LogError("No LineRenderer");
                return;
            }
        }
        else
        {
            //Debug.Log("Pompa is not live");
            return;
        }
    }

    private void UpdatePompaLine()
    {
        // set new start and end positions for the line renderer
        Vector3[] lrPositions = new Vector3[2];
        lrPositions[0] = _bulletOriginTr.position;
        lrPositions[1] = _pompaCurrentProjectileTr.position;
        _lineRenderer.SetPositions(lrPositions);
    }

    private void RemovePompaProjectile()
    {
        // reset all logic for pompa
        _isPompaLive = false;
        Destroy(_pompaCurrentProjectile);
        Destroy(_lineRenderer);
        _pompaCurrentProjectile = null;
        _lineRenderer = null;
        _isPompaReturning = false;
    }

    private IEnumerator PullPompaConnectedObject()
    {
        // happens if hit relevant object
        while (_pompaCurrentProjectileScript.ObjectConnected || Vector3.Distance(_pompaCurrentProjectileTr.position, _bulletOriginTr.position) > _maxPompaDistance)
        {
            Vector3 direction = (_pompaCurrentProjectileTr.position - _bulletOriginTr.position).normalized;
            _pompaCurrentProjectileScript.Rb.velocity -= direction * _pompaPullSpeed * Time.deltaTime;

            if (_pompaCurrentProjectileScript.ObjectConnected && Vector3.Distance(_pompaCurrentProjectileTr.position, _bulletOriginTr.position) <= _minPompaDistance)
            {
                _attachedToPompaObjectRb = _pompaCurrentProjectileScript.ConnectedObjectRb;
                RemovePompaProjectile();
                _attachedObject = true;
                break;
            }
            else
            {
                yield return null;
            }
        }

        // happens when don't hit relevant object
        while (!(Vector3.Distance(_pompaCurrentProjectileTr.position, _bulletOriginTr.position) <= _minPompaDistance))
        {
            Vector3 direction = (_pompaCurrentProjectileTr.position - _bulletOriginTr.position).normalized;
            _pompaCurrentProjectileScript.Rb.velocity -= direction * _pompaPullSpeed * Time.deltaTime;
            yield return null;
        }

        RemovePompaProjectile();
    }
    #endregion

    #region SuperRifle Behavior
    private void SuperFire()
    {
        var firedBullet = Instantiate(_bulletPrefab, _bulletOriginTr.position, Quaternion.identity, _bulletContainer.transform);
        firedBullet.GetComponent<Rigidbody>().AddForce(this.transform.up.normalized*BulletSpeedMultiplier, ForceMode.Impulse);
        DestroyBullets(firedBullet);
    }
    #endregion

    #region Fire Behavior
    private void NormalFireDown()
    {
        GameObject firedBullet = Instantiate(_bulletPrefab, _bulletOriginTr.position, Quaternion.identity, _bulletContainer.transform);
        firedBullet.GetComponent<Rigidbody>().AddForce(transform.up.normalized * BulletSpeedMultiplier, ForceMode.Impulse);
        DestroyBullets(firedBullet);
    }
    private void DestroyBullets(GameObject bullet)
    {
        if (_isToBeDestoryed)
        {
            Destroy(bullet, _destructionTime);
        }
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
