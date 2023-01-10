using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class DorBasketRunScore : MiniGame
{
    public static DorBasketRunScore Instance { get; private set; }

    [SerializeField] float timeBetweenShots = 2f;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform gameContainerTransform;

    private List<Transform> gameContainer = new List<Transform>();
    private WaitForSeconds waitTimeBetweenShots;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
    }

    private void Start()
    {
        waitTimeBetweenShots = new WaitForSeconds(timeBetweenShots);
    }

    private void Update()
    {
        if (!GetIsActive())
            return;

        if (gameContainer.Count >= 5)
        {
            SetIsActive(false);
            StartCoroutine(ShootAllAmmo());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dor"))
        {
            if (!GetIsActive())
                GameSystemsManager.Instance.TryStartMiniGame(this);

            gameContainer.Add(other.transform);

            IncreaseScoreBy(1);
            other.transform.position = gameContainerTransform.position;
        }
        print($"Score:{GetPlayerScore()}, Contains: {gameContainer[gameContainer.Count]} items");
    }

    private IEnumerator ShootAllAmmo()
    {
        int i = 0;
        foreach (var dor in gameContainer)
        {
            dor.transform.position = shootingPoint.position;
            yield return waitTimeBetweenShots;
            //shoot
            i++;
            print(i + "shot");
        }

    }

}