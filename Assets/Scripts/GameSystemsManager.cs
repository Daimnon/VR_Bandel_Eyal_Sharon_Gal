using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
using System;

public class GameSystemsManager : MonoBehaviour
{
    public static GameSystemsManager Instance { get; private set; }
    public event EventHandler OnAnyMiniGameStarted;

    private MiniGame selectedGame;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
    }

    public void TryStartMiniGame(MiniGame miniGameToStart)
    {
        if (selectedGame == null || !selectedGame.GetIsActive())
        {
            selectedGame = miniGameToStart;
            selectedGame.StartMiniGame();
            OnAnyMiniGameStarted?.Invoke(this, EventArgs.Empty);
        }
    }
}