using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [SerializeField] List<TrackHandler> _trackHandlers;
    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        foreach (var track in _trackHandlers)
        {
            track.StartTrack();
        }
    }
}
