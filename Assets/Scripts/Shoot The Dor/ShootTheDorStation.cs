using UnityEngine;

public class ShootTheDorStation : MiniGame
{
    static ShootTheDorStation _instance;
    public static ShootTheDorStation Instance => _instance;
    [SerializeField] StartTarget _startTarget;
    [SerializeField] TrackManager _trackManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        _instance = this;
    }
    public void GameEnded()
    {
        _startTarget.gameObject.SetActive(true);
        DeactivateGame();
    }
}
