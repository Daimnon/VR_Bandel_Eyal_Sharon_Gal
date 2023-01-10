using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using TMPro;

public abstract class MiniGame : MonoBehaviour
{
    public static event EventHandler OnAnyScoreIncrease;

    [SerializeField] protected string[] _pointGiverTags; 
    [SerializeField] private TextMeshPro scoreText;

    private int playerScore = 0;
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
        PointsGiver.TryGivePoint += PointGiver_TryGivePoint;
        playerScore = 0;
        scoreText.gameObject.SetActive(true);
        scoreText.text = playerScore.ToString();
    }

    protected virtual void DeactivateGame()
    {
        SetIsActive(false);
        PointsGiver.TryGivePoint -= PointGiver_TryGivePoint;
        scoreText.gameObject.SetActive(false);
    }

    private void PointGiver_TryGivePoint(object sender, int points)
    {
        IncreaseScoreBy(points);
    }
}