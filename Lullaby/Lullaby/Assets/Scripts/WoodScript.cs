using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodScript : MonoBehaviour
{
    public bool FireOn;
    [SerializeField] PlayerStatus playerStats;
    [SerializeField] SimpleLighter simlight;
    GameObject inventoryPanel;
    [SerializeField] GameObject fireUI;
    [SerializeField] float fireTimer;
    float timerSpeed = 1f;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStatus>();
        inventoryPanel = GameObject.Find("InventoryPanel");
        
        fireUI = GameObject.FindGameObjectWithTag("FireUI");
    }
    private void Update()
    {
        HandleWood();
        HandleSimlight();
    }

    void HandleSimlight()
    {
        if (inventoryPanel.GetComponentInChildren<SimpleLighter>() != null)
        {
            simlight = inventoryPanel.GetComponentInChildren<SimpleLighter>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            var childitem = fireUI.transform.GetChild(0);
            childitem.gameObject.SetActive(true);
            playerStats.canInteract = true;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerStats.settedFire)
            {
                if (simlight.lighterInInventory)
                {
                    if (playerStats.canStartFire)
                    {
                        if (playerStats.isInShelter)
                        {
                            FireOn = true;
                            var childitem = fireUI.transform.GetChild(0);
                            childitem.gameObject.SetActive(false);
                        }
                        
                    }
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerStats.canInteract = false;
        var childitem = fireUI.transform.GetChild(0);
        childitem.gameObject.SetActive(false);
    }


    void HandleWood()
    {
        if (FireOn)
        {
            // Set wood on fire
            fireTimer -= timerSpeed * Time.deltaTime;

            if (fireTimer <= 0)
            {
                fireTimer = 0;
                // Do effect
                FireOn = false;
            }
        }
    }
}
