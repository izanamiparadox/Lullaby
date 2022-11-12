using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodTerrain : MonoBehaviour
{
    TerrainData terrainData;
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject[] numberofWood;
    public TreeInstance[] bush;


    private void Awake()
    {
        HandleWood();
    }

    void HandleWood()
    {
        terrainData = GetComponent<Terrain>().terrainData;

        //bush = terrainData.tree

        foreach(var instance in bush)
        {
            
        }

    }

}
