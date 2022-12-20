using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
using System;

public class GameSystemsManager : MonoBehaviour
{
    public static GameSystemsManager Instance { get; private set; }
    public event EventHandler OnMiniGameStarted;
    public event EventHandler OnMiniGameEnded;

    private GameScore selectedGame;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
    }

    public void StartMiniGame(GameScore startedMiniGame)
    {
        selectedGame = startedMiniGame;
        OnMiniGameStarted?.Invoke(this, EventArgs.Empty);
    }

}