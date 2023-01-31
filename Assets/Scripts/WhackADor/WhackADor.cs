using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackADor : MonoBehaviour
{
    bool _isRunning;
    [SerializeField] float _timeRemainingForGame;
    [SerializeField] float _gameTime;
    [SerializeField] float _popTimer;
    [SerializeField] private float _poppedDorTimer;
    [SerializeField] private List<DorMole> _freeDorMoles = new List<DorMole>();
    [SerializeField] private List<DorMole> _occupiedDorMoles = new List<DorMole>();

    private void Start()
    {
        StartCoroutine(RunGame());
    }

    private IEnumerator RunGame()
    {
        _timeRemainingForGame = _gameTime;

        System.Random rand = new System.Random();
        int numberOfDorsGoingUp;
        int randomIndex;
        DorMole activeDorMole;

        while (_timeRemainingForGame > 0)
        {
            // Determine how many Dors are going up
            numberOfDorsGoingUp = rand.Next(1, 3 + 1);

            for (int i = 0; i < numberOfDorsGoingUp; i++)
            {
                // Pop Random Dors
                randomIndex = rand.Next(0, _freeDorMoles.Count);

                activeDorMole = _freeDorMoles[randomIndex];

                StartCoroutine(PopDor(activeDorMole));
            }

            // make the cooldown between Dors popping up random, between half a second to 1 second
            float randomValue = rand.Next(5, 10 + 1);
            _popTimer = (randomValue / 10);
            yield return new WaitForSeconds(_popTimer);
            _timeRemainingForGame -= _popTimer;
            
        }
    }

    private IEnumerator PopDor(DorMole mole)
    {
        _freeDorMoles.Remove(mole);
        _occupiedDorMoles.Add(mole);
        mole.MoveDorUp();

        // wait until Dor is fully erect
        yield return new WaitForSeconds(_poppedDorTimer);
        mole.MoveDorDown();

        // wait until Dor is fully Down
        _freeDorMoles.Add(mole);
        _occupiedDorMoles.Remove(mole);
    }
}
