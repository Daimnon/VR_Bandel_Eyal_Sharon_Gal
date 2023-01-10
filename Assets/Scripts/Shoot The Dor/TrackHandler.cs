using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackHandler : MonoBehaviour
{
    [SerializeField] Transform _startPos;
    [SerializeField] Transform _endPos;
    [SerializeField] SmallDorPool _dorPool;
    [SerializeField] List<GameObject> _trackObjects;
    [SerializeField] int _maxObjectsOnTrack;
    [SerializeField] float _trackTime;
    [SerializeField] float _spawnDelta;
    [SerializeField] float _currentTime;
    private void Awake()
    {
        if (!_startPos)
            throw new System.Exception("Track do not have start Pos");
        if (!_endPos)
            throw new System.Exception("Track do not have end Pos");
        if (!_dorPool)
            throw new System.Exception("Track do not have dor pool");
    }
    public void StartTrack()
    {
        //spawn dors every X seconds
        _currentTime = _trackTime;
        StartCoroutine(SpawnDors());
    }
    IEnumerator SpawnDors()
    {
        if (_currentTime >= 0)
        { 
            _currentTime -= Time.deltaTime;
            yield return new WaitForSeconds(_spawnDelta);
        }
    }
    private void MoveDors()
    {
        foreach (var dor in _trackObjects)
        {
            //dor.Move();
        }
    }
}
