using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScoreMode { HitDoor }

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ScoreMode _scoreMode;
    [SerializeField] private List<Collider[]> _mobsHitColliders;
    [SerializeField] private List<PointsGiver> _pointsGivers;

    private delegate void GameScoreMode();
    private GameScoreMode _currentGameScoreMode;

    private void Awake()
    {
        _currentGameScoreMode = HitDorScoreMode;
    }
    private void Update()
    {
        _currentGameScoreMode.Invoke();
    }

    private void HitDorScoreMode()
    {
        
    }

    public void ChangeGameScoreMode(int ScoreModeValue)
    {
        ScoreMode desiredScoreMode = (ScoreMode)ScoreModeValue;

        switch (desiredScoreMode)
        {
            case ScoreMode.HitDoor:
                _currentGameScoreMode = HitDorScoreMode;
                break;
            default:
                _currentGameScoreMode = HitDorScoreMode;
                break;
        }

        _scoreMode = desiredScoreMode;
        return;
    }
}
