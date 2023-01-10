using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MiniGameInstanceTemplate : MiniGame
{
    private static MiniGameInstanceTemplate _instance;
    public static MiniGameInstanceTemplate Instance => _instance;

    [SerializeField] private int _scoreToAdd = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        _instance = this;
    }

    private void Update()
    {
        if (!GetIsActive())
            return;

        // DeactivateGame(); - put where game ends
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_pointGiverTags.Contains(other.gameObject.tag))
        {
            if (!GetIsActive())
                GameSystemsManager.Instance.TryStartMiniGame(this);

            IncreaseScoreBy(_scoreToAdd);
        }
    }
}
