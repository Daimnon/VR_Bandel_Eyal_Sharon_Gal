using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackADor : MonoBehaviour
{
    bool _isRunning;
    [SerializeField] float _timeRemainingForGame;
    [SerializeField] float _gameTime;
    [SerializeField] float _popTimer;
    int _maximumNumberOfDors = 3;
    [SerializeField] private float _poppedDorTimer;
    int _numberOfCurrentlyActiveDors = 0;
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
        int maximumNumberOfDorsThatMayGoUp;
        DorMole activeDorMole;

        while (_timeRemainingForGame > 0)
        {
            // Determine how many Dors are going up
            maximumNumberOfDorsThatMayGoUp = _maximumNumberOfDors - _numberOfCurrentlyActiveDors;

            if(maximumNumberOfDorsThatMayGoUp <= 0)
            {
                _timeRemainingForGame -= Time.deltaTime;
                yield return null;
                continue;
            }

            numberOfDorsGoingUp = rand.Next(1, maximumNumberOfDorsThatMayGoUp + 1);

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
        _numberOfCurrentlyActiveDors++;

        yield return new WaitForSeconds(_poppedDorTimer);
        _freeDorMoles.Add(mole);
        _occupiedDorMoles.Remove(mole);
        _numberOfCurrentlyActiveDors--;
        mole.MoveDorDown();

    }
}
