using UnityEngine;

public class IncreaseScoreByCollisions : MonoBehaviour
{
    [SerializeField] int ObjectScore;
    [SerializeField] LayerMask _hittingProjectile;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _hittingProjectile)
        {
            ShootTheDorStation.Instance.IncreaseScoreBy(ObjectScore);
        }
    }
}
