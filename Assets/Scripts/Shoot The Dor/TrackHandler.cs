using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackHandler : MonoBehaviour
{
    [SerializeField] Transform _startPos;
    [SerializeField] Transform _endPos;
    [SerializeField] SmallDorPool _dorPool;
    Vector3 _trackDirection;
    [SerializeField] List<GameObject> _trackObjects;
    [SerializeField] int _maxObjectsOnTrack;
    [SerializeField] float _trackTotalTime;
    float _currentTrackTime;
    [SerializeField] float _spawnCooldown;
    float _currentSpawnTime;
    [SerializeField] float _trackSpeed = 1;
    bool _isTrackActive;
    [SerializeField]TrackManager _trackManager; 
    public bool IsTrackActive => _isTrackActive;
    private void Awake()
    {
        if (!_startPos)
            throw new System.Exception("Track do not have start Pos");
        if (!_endPos)
            throw new System.Exception("Track do not have end Pos");
        if (!_dorPool)
            throw new System.Exception("Track do not have dor pool");

        _trackDirection = _endPos.localPosition - _startPos.localPosition;
    }
    private void OnValidate()
    {
        if (_trackManager == null)
        {
            var tm = GetComponentInParent<TrackManager>();
        }
    }
    private void Update()
    {
        if (_isTrackActive)
        {
            MoveDors();
            _currentTrackTime -= Time.deltaTime;
            if (_currentTrackTime <= 0)
            {
                if (_trackObjects.Count == 0)
                {
                    _isTrackActive = false;
                    _trackManager.EndedTracks.Add(true);
                }
            }
            else
            {
                if (_currentSpawnTime >= 0)
                {
                    _currentSpawnTime -= Time.deltaTime;
                }
                else
                {
                    if (_trackObjects.Count < _maxObjectsOnTrack)
                    {
                        _currentSpawnTime = _spawnCooldown;
                        _trackObjects.Add(_dorPool.SpawnDor(_startPos.gameObject));
                    }

                }

            }
        }
    }
    public void StartTrack()
    {
        //spawn dors every X seconds
        _isTrackActive = true;
        _currentSpawnTime = _spawnCooldown;
        _currentTrackTime = _trackTotalTime;
    }
    public void MoveDors()
    {
        var movement = (transform.localPosition + _trackDirection).normalized * _trackSpeed *Time.deltaTime;
        if (_trackObjects.Count != 0)
        {
            var toBeRomoved = new List<GameObject>();
            foreach (var dor in _trackObjects)
            {
                if (isReachedDestination(dor))
                {
                    dor.gameObject.SetActive(false);
                    toBeRomoved.Add(dor);
                }
                else
                {
                    dor.transform.Translate(movement);
                }
            }
            if (toBeRomoved.Count > 0)
            {
                foreach (var dor in toBeRomoved)
                {
                    _trackObjects.Remove(dor);
                }
            }
        }
    }
    bool isReachedDestination(GameObject dor)
    {
        float offset = 0.5f;
        var distance = dor.transform.position - _endPos.transform.position;
        if (distance.x <= offset && distance.y < offset && distance.z < offset)
        {
            //reached destination
            return true;
        }
        return false;
    }
}