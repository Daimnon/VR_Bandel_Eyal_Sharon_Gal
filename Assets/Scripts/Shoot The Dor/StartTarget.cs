using UnityEngine;

public class StartTarget : MonoBehaviour
{
    [SerializeField] ShootTheDorStation _shootTheDorStation;
    [SerializeField] LayerMask _bulletMask;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision was triggered");
        if (collision.gameObject.layer == Mathf.Log(_bulletMask.value, 2))
        {
            Debug.Log("Mini Game Started");
            _shootTheDorStation.StartMiniGame();
            gameObject.SetActive(false);
        }
    }
}
