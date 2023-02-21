using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakaDorStation : MiniGame
{
    static WakaDorStation _instance;
    public static WakaDorStation Instance => _instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        _instance = this;
    }
    public override void StartMiniGame()
    {
        base.StartMiniGame();

    }
    public void GameEnded()
    {
        DeactivateGame();

    }
}
