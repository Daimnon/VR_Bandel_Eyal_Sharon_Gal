using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class DorBasketRunScore : MiniGame
{
    public static DorBasketRunScore Instance { get; private set; }

    [SerializeField] Transform Player;
    [SerializeField] private float timeBetweenShots = 2f, shootingPower = 20f;
    [SerializeField] private Transform shootingPoint, lid;
    [SerializeField] private Transform gameContainerTransform;
    [SerializeField] private Vector3 shootPointOffSet = Vector3.zero;
    [SerializeField] private List<DorStartingPositions> dorManageTool;

    [Serializable]
    public struct DorStartingPositions
    {
        public Transform ballGO;
        public Transform ballStartingPos;

        public void ResetPosition() { ballGO.position = ballStartingPos.position; }
    }

    private List<Rigidbody> gameContainer = new List<Rigidbody>();
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
        ResetEvent.G_Reset += ResetEvent_G_Reset;
    }

    private void ResetEvent_G_Reset(object sender, EventArgs e)
    {
        foreach (var item in dorManageTool)
            item.ResetPosition();
        DeactivateGame();
        StartMiniGame();
    }

    private void Update()
    {
        if (!GetIsActive())
            return;

        if (gameContainer.Count >= 5)
        {
            shootingPoint.gameObject.transform.LookAt(Player.position + shootPointOffSet);
            SetIsActive(false);
            StartCoroutine(ShootAllAmmo());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in _pointGiverTags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                if (!GetIsActive())
                    GameSystemsManager.Instance.TryStartMiniGame(this);

                gameContainer.Add(other.gameObject.GetComponent<Rigidbody>());

                IncreaseScoreBy(1);

                other.transform.position = gameContainerTransform.position;

                break;
            }
        }
        //print($"Score:{GetPlayerScore()}, Contains: {gameContainer[gameContainer.Count]} items");
    }

    private IEnumerator ShootAllAmmo()
    {
        List<Rigidbody> listToRemove = new List<Rigidbody>();
        lid.gameObject.SetActive(false);

        foreach (Rigidbody dor in gameContainer)
        {
            yield return waitTimeBetweenShots;
            dor.transform.position = shootingPoint.position;
            dor.transform.rotation = shootingPoint.rotation;
            dor.AddForce(shootingPoint.forward * shootingPower, ForceMode.Impulse);
            listToRemove.Add(dor);
        }

        foreach (var dor in listToRemove)
            gameContainer.Remove(dor);

        yield return waitTimeBetweenShots;
        lid.gameObject.SetActive(true);
    }

}