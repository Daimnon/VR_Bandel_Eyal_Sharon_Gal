using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class DorBasketRunScore : GameScore
{
    public static DorBasketRunScore Instance { get; private set; }

    [SerializeField] float timeBetweenShots = 10f;
    //[SerializeField] private Transform shootingPoint;

    private List<GameObject> dorHolder = new List<GameObject>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dor"))
        {
            if (!isActive)
            {
                GameSystemsManager.Instance.TryStartMiniGame(this);
            }

            dorHolder.Add(other.gameObject);
            playerScore++;
        }
        print($"{playerScore}, {dorHolder[dorHolder.Count - 1]}");
        Destroy(other);
    }

    private void Update()
    {
        if (isActive)
        {
            if (dorHolder.Count >= 5)
                StartCoroutine(ShootAllAmmo());
        }
    }


    protected override void GameSetUp()
    {

    }

    private IEnumerator ShootAllAmmo()
    {

        yield return waitTimeBetweenShots;

    }

}