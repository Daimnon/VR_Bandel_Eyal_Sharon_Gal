using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScoreMode { HitDoor, Other, Another, YetAnother }

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    [SerializeField] private ScoreMode _scoreMode;
    [SerializeField] private TMPro.TextMeshProUGUI _scoreTMP;
    [SerializeField] private bool _isScorePerMiniGame = false; // -> last stopped here
    //[SerializeField] private List<Collider[]> _mobsHitColliders;
    //[SerializeField] private List<PointsGiver> _pointsGivers;

    private int _score = 0, _scorePerMiniGameCounter = 0;
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
        ValidateScoreMode(ScoreMode.HitDoor);
    }
    private void OtherScoreMode()
    {
        ValidateScoreMode(ScoreMode.Other);
    }
    private void AnotherScoreMode()
    {
        ValidateScoreMode(ScoreMode.Another);
    }
    private void YetAnotherScoreMode()
    {
        ValidateScoreMode(ScoreMode.YetAnother);
    }

    public void ChangeGameScoreMode(int ScoreModeValue)
    {
        ScoreMode desiredScoreMode = (ScoreMode)ScoreModeValue;
        switch (desiredScoreMode)
        {
            case ScoreMode.HitDoor:
                _currentGameScoreMode = HitDorScoreMode;
                break;
            case ScoreMode.Other:
                _currentGameScoreMode = OtherScoreMode;
                break;
            case ScoreMode.Another:
                _currentGameScoreMode = AnotherScoreMode;
                break;
            case ScoreMode.YetAnother:
                _currentGameScoreMode = YetAnotherScoreMode;
                break;
            default:
                _currentGameScoreMode = HitDorScoreMode;
                break;
        }

        _scoreMode = desiredScoreMode;
        return;
    }
    private void ValidateScoreMode(ScoreMode scoreMode)
    {
        if (_scoreMode != scoreMode)
        {
            ChangeGameScoreMode((int)_scoreMode);
        }
    }
}
