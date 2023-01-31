using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [SerializeField] List<TrackHandler> _trackHandlers;
    [SerializeField] List<bool> _endedTracks;
    [SerializeField] ShootTheDorStation _shootTheDoorStation;
    public List<bool> EndedTracks => _endedTracks;
    public void StartGame()
    {
        foreach (var track in _trackHandlers)
        {
            track.StartTrack();
        }
    }
    private void Update()
    {
        if (_endedTracks.Count >= _trackHandlers.Count)
        {
            GameEnded();
        }
    }
    public void GameEnded()
    {
        _endedTracks.Clear();
        _shootTheDoorStation.GameEnded();
    }
}
