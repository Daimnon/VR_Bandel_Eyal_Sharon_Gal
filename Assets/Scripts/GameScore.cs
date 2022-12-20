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

    public virtual void GameSystemsManager_OnMiniGameStarted(object sender, EventArgs e) 
    {
        isActive = true;
    }

    public virtual void GameSystemsManager_OnMiniGameEnded(object sender, EventArgs e) 
    {
        isActive = false;
    }

}