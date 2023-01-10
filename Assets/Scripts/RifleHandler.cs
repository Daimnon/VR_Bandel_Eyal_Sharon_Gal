using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum RifleType { Regular, Super, Pompa }

public class RifleHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab, _pompaBullet, _bulletContainer;
    [SerializeField] private Transform _bulletOriginTr;
    [SerializeField] private RifleType _rifleType;
    [SerializeField] private Material _grappleMat;
    [SerializeField] private float _maxPompaDistance = 20f, _pompaStartWidth = 0.25f, _pompaEndWidth = 0.25f;
    
    private GameObject _pompaCurrentProjectile;
    private Transform _pompaCurrentProjectileTr;

    private StickToObjectByWeight _pompaCirrentProjectileScript;
    //public StickToObjectByWeight PompaCirrentProjectileScript { get => _pompaCirrentProjectileScript; set => value = _pompaCirrentProjectileScript; }

    private LineRenderer _lineRenderer;
    private bool _isEquiped = false, _canFire = false, _pompaLive = false;
    
    public InputActionProperty LeftpinchAnimationAction, RightpinchAnimationAction;
    public float BulletSpeedMultiplier;

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

        // problem - check
        if (_rifleType == RifleType.Pompa && _isEquiped && _pompaCirrentProjectileScript)
        {
            if (_pompaCirrentProjectileScript.ConnectedObjectRb && !(Vector3.Distance(_pompaCurrentProjectileTr.position, _bulletOriginTr.position) > _maxPompaDistance))
            {
                StartCoroutine(PullPompaConnectedObject());
            }
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
                PompaFireUp();
                break;
            default:
                break;
        }
    }
    #endregion

    #region Pompa Behavior
    private void PompaFireDown()
    {
        if (_pompaCurrentProjectile)
            return;

        _pompaCurrentProjectile = Instantiate(_pompaBullet, _bulletOriginTr.position, Quaternion.identity, _bulletContainer.transform);
        _pompaCurrentProjectile.GetComponent<Rigidbody>().AddForce(transform.up.normalized * BulletSpeedMultiplier, ForceMode.Impulse);
        _pompaCirrentProjectileScript = _pompaCurrentProjectile.GetComponent<StickToObjectByWeight>();

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

        _pompaLive = true;
    }
    private void PompaFire()
    {
        if (_pompaLive)
        {
            if (_lineRenderer)
            {
                Vector3[] lrPositions = new Vector3[2];
                lrPositions[0] = _bulletOriginTr.position;
                lrPositions[1] = _pompaCurrentProjectileTr.position;
                _lineRenderer.SetPositions(lrPositions);
            }
            else
            {
                Debug.Log("No LineRenderer");
                return;
            }
        }
    }
    private void PompaFireUp()
    {
    }


    // need fixing
    private IEnumerator PullPompaConnectedObject()
    {
        while (!_pompaCirrentProjectileScript.ConnectedObjectRb)
        {
            yield return null;
        }

        while (Vector3.Distance(_pompaCurrentProjectileTr.position, _bulletOriginTr.position) > _maxPompaDistance)
        {
            Vector3[] lrPositions = new Vector3[2];
            lrPositions[0] = _bulletOriginTr.position;
            lrPositions[1] = _pompaCurrentProjectileTr.position;
            _lineRenderer.SetPositions(lrPositions);

            // pull rigidbody towards rifle
            Vector3 direction = (_pompaCurrentProjectileTr.position - _bulletOriginTr.position).normalized;


            yield return null;
        }

        Destroy(_pompaCurrentProjectile);
        Destroy(_lineRenderer);
        _pompaCurrentProjectile = null;
        _lineRenderer = null;
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
    private void NormalFireDown()
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
