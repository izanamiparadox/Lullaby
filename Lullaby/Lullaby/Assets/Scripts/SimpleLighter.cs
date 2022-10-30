using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLighter : MonoBehaviour
{
    public bool lighterInInventory;
    [SerializeField] GameObject inventoryPanel;

    public void Awake()
    {
        inventoryPanel = GameObject.Find("InventoryPanel");
    }

    public void Update()
    {
        HandleLighter();
    }

    void HandleLighter()
    {
        if (inventoryPanel.GetComponentInChildren<LighterSysten>() != null)
        {
            lighterInInventory = true;
        }
        else
        {
            lighterInInventory = false;
        }
        

        
    }
}
