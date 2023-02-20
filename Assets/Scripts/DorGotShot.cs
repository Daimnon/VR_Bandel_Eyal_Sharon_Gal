using UnityEngine;

public class DorGotShot : MonoBehaviour
{
    [SerializeField] IncreaseScoreByCollisions _increaseScoreByCollisions;
    private TrackHandler _trackHandler;
    public TrackHandler TrackHandler { set => _trackHandler = value; }
    private void Awake()
    {
        _increaseScoreByCollisions.OnCollision.AddListener(DorWasHit);
    }
    void DorWasHit()
    {
        gameObject.SetActive(false);
        _trackHandler.TrackObjects.Remove(this);
    }
    private void OnDestroy()
    {
        _increaseScoreByCollisions.OnCollision.RemoveListener(DorWasHit);
    }

}
