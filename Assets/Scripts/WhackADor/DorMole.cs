using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DorMole : MonoBehaviour
{
    [SerializeField] Transform _endPoint;
    [SerializeField] Transform _startPoint;
    [SerializeField] float _speedMultiplyer;
    [SerializeField] float _magnitudeOffset;
    private int _hammerLayer = 9;
    private bool _isDoorUp = false;

    private float _timeToPop = 0.3f;

    public int SCORE;

    private bool _hasOtherFunctionality;
    public UnityEvent OnHit;
    public bool IsDoorUp { get => _isDoorUp; }
    public bool HasOtherFunctionality { get => _hasOtherFunctionality; set => _hasOtherFunctionality = value; }

    private void OnCollisionEnter(Collision collision)
    {
        if(IsDoorUp && collision.collider.gameObject.layer == _hammerLayer)
        {
            if(HasOtherFunctionality)
            {
                OnHit?.Invoke();
            }
            else
            {
                // Add Score
                MoveDorDown();
            }
            
            
            Debug.Log("MoleDor Hit!");
        }
    }

    public void MoveDor(Vector3 movement, bool isDoorUp)
    {
        transform.position += movement;
        _isDoorUp = isDoorUp;
    }

    public IEnumerator MoveDor2(Transform endPoint, bool isDoorUp)
    {
        _isDoorUp = isDoorUp;
        //Vector3 startingPos = transform.position;

        //Vector3 direction = endPoint - startingPos;
        //direction = direction.normalized;

        
        //var mag = (transform.position - endPoint.position).magnitude;
        while ((transform.position - endPoint.position).magnitude >= _magnitudeOffset)
        {
            //transform.position += movement * Time.deltaTime / _timeToPop;
            transform.localPosition = Vector3.Lerp(transform.localPosition, endPoint.localPosition, Time.deltaTime*_speedMultiplyer);
            //transform.Translate(endPoint.localPosition * 0.75f * Time.deltaTime* _speedMultiplyer, Space.Self);
            yield return null;
        }
        Debug.Log("YES") ;


        transform.localPosition = endPoint.localPosition;


    }

    public void MoveDorUp()
    {
        MoveDor(_endPoint.localPosition, true);
        //StartCoroutine(MoveDor2(_endPoint, true));
    }

    public void MoveDorDown()
    {
        MoveDor(-_endPoint.localPosition, false);
        //StartCoroutine(MoveDor2(_startPoint, false));
    }

}
