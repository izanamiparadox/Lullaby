using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterCount : MonoBehaviour
{
    public List<Transform> shelters = new List<Transform>();


    public void Awake()
    {
        StartUpShelters();
    }

    void StartUpShelters()
    {
        foreach(Transform child in transform)
        {
            shelters.Add(child);
        }
        Debug.Log(transform.childCount);
    }
}
