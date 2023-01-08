using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class GameScore : MonoBehaviour
{
    [SerializeField] TextMeshPro scoreText;
    [SerializeField] protected float timeTillGameEnd = 10f;

    [SerializeField] protected int playerScore = 0;
    protected bool isActive;

    private void Awake()
    {
        scoreText.gameObject.SetActive(false);
        isActive = false;
        playerScore = 0;
    }

    private void Start()
    {
        GameSystemsManager.Instance.OnMiniGameStarted += GameSystemsManager_OnMiniGameStarted;
        GameSystemsManager.Instance.OnMiniGameEnded += GameSystemsManager_OnMiniGameEnded;
    }

    private void Update()
    {
        if (!isActive) return;

        scoreText.text = playerScore.ToString();
    }

    public bool IsGameActive() => isActive;

    protected virtual void GameSetUp()
    {
        isActive = true;
    }

    protected virtual void DeactivateGame()
    {
        isActive = false;
        playerScore = 0;
    }

    private void GameSystemsManager_OnMiniGameEnded(object sender, EventArgs e) { DeactivateGame(); }

    private void GameSystemsManager_OnMiniGameStarted(object sender, EventArgs e) { GameSetUp(); }

}