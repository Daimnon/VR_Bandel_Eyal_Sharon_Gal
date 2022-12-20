using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScoreMode { HitDoor }

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    [SerializeField] private ScoreMode _scoreMode;
    [SerializeField] private TMPro.TextMeshProUGUI _scoreTMP;
    //[SerializeField] private List<Collider[]> _mobsHitColliders;
    [SerializeField] private List<PointsGiver> _pointsGivers;

    private int _score = 0;
    public int Score { get => _score; set => _score = value; }

    private delegate void GameScoreMode();
    private GameScoreMode _currentGameScoreMode;

    private void Awake()
    {
        _instance = this;
        _currentGameScoreMode = HitDorScoreMode;
    }
    private void Update()
    {
        _currentGameScoreMode.Invoke();
        _scoreTMP.text = _score.ToString();
    }

    private void HitDorScoreMode()
    {

    }
    private void OtherScoreMode()
    {

    }
    private void AnotherScoreMode()
    {

    }
    private void YetAnotherScoreMode()
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
