using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackADor : MonoBehaviour
{
    [SerializeField]
    private float _poppedDorTimer;
    [SerializeField] private List<DorMole> _dorMoles = new List<DorMole>();

    //public void BeginGame()
    //{

    //}

    //private IEnumerator RunGame()
    //{

    //}

    //private IEnumerator PopDor(DorMole mole)
    //{
    //    mole.MoveDorUp();
    //    yield return WaitUntil(_poppedDorTimer);
    //}
}
