using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Events;

public abstract class MiniGame : MonoBehaviour
{
    public static event EventHandler OnAnyScoreIncrease;

    [SerializeField] private TextMeshPro scoreText;

    [SerializeField] private int playerScore = 0;
    private bool isActive;

    private void Awake(){ DeactivateGame(); }

    public bool GetIsActive() => isActive;
    protected void SetIsActive(bool isActive) { this.isActive = isActive; }
    public int GetPlayerScore() => playerScore;

    public void IncreaseScoreBy(int increaseBy) 
    {
        playerScore += increaseBy;
        scoreText.text = playerScore.ToString();
        OnAnyScoreIncrease?.Invoke(this, EventArgs.Empty);
    }

    public virtual void StartMiniGame()
    {
        SetIsActive(true);
        playerScore = 0;
        scoreText.gameObject.SetActive(true);
        scoreText.text = playerScore.ToString();
    }

    protected virtual void DeactivateGame()
    {
        SetIsActive(false);
        scoreText.gameObject.SetActive(false);
    }

}