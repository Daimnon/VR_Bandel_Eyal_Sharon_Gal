using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class DorBasketRunScore : GameScore
{
    public static DorBasketRunScore Instance { get; private set; }

    [SerializeField] private Transform dorShootingPoint;

    private List<GameObject> dorHolder = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
    }

    private void Update()
    {


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dor"))
        {
            if (!isActive)
            {
                isActive = true;
                GameSystemsManager.Instance.StartMiniGame(this);
            }

            dorHolder.Add(other.gameObject);
            playerScore++;
        }
        print($"{playerScore}, {dorHolder[dorHolder.Count - 1]}");
    }

    public override void GameSystemsManager_OnMiniGameStarted(object sender, EventArgs e)
    {
        base.GameSystemsManager_OnMiniGameStarted(sender, e);
    }

    public override void GameSystemsManager_OnMiniGameEnded(object sender, EventArgs e)
    {
        base.GameSystemsManager_OnMiniGameStarted(sender, e);
    }
}