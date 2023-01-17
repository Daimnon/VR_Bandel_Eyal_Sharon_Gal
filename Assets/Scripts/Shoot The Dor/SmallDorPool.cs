using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDorPool : MonoBehaviour
{
    [SerializeField] GameObject _smallDorPrefab;
    [SerializeField] GameObject _container;
    [SerializeField] List<GameObject> _dors;
    public GameObject SpawnDor(GameObject startPos)
    {
        var avilableDor = CheckForAvilableDor();
        if (!avilableDor)
        {
            avilableDor = Instantiate(_smallDorPrefab, _container.transform);
            _dors.Add(avilableDor);
        }
        avilableDor.transform.position = startPos.transform.position;
        avilableDor.SetActive(true);
        return avilableDor;
    }
    private GameObject CheckForAvilableDor()
    {
        if (_dors.Count == 0)
            return null;

        foreach (var dor in _dors)
        {
            if(!dor.activeSelf)
                return dor;
        }
        return null;
    }
}
