using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class IncreaseScoreByCollisions : MonoBehaviour
{
    [SerializeField] int ObjectScore;
    [SerializeField] LayerMask _hittingProjectile;
    public UnityEvent OnCollision;
    bool _isHit;
    private void OnCollisionEnter(Collision collision)
    {
        if (!_isHit && collision.gameObject.layer == Mathf.Log(_hittingProjectile.value,2))
        {
            _isHit = true;
            ShootTheDorStation.Instance.IncreaseScoreBy(ObjectScore);
            OnCollision.Invoke();
        }
    }
    private void OnEnable()
    {
        _isHit = false;
    }
}
