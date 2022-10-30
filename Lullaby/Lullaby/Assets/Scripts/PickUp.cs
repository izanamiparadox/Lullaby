using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    InventoryScript inventory;
    public GameObject itemButton;

    public GameObject itemUI;

    public PlayerStatus playerStats;

    public GameObject InventoryPanel;

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryScript>();
        playerStats = FindObjectOfType<PlayerStatus>();
        itemUI = GameObject.FindGameObjectWithTag("ItemUI");
        InventoryPanel = GameObject.Find("InventoryPanel");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerStats.canInteract = true;
            var childitem = itemUI.transform.GetChild(0);
            Text itemtext = childitem.GetComponentInChildren<Text>();
            itemtext.text = itemButton.name;
            childitem.gameObject.SetActive(true);
        }

           
            
           
    }

    private void OnTriggerExit(Collider other)
    {
        playerStats.canInteract = false;
        var childitem = itemUI.transform.GetChild(0);
        childitem.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        var childitem = itemUI.transform.GetChild(0);

        if (playerStats.isInteracting)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {

                if (inventory.isFull[i] == false)
                {
                    Debug.Log("Touched.");
                    // Add To Inventory
                    inventory.isFull[i] = true;
                    inventory.canDrop[i] = true;
                    GameObject item = Instantiate(itemButton, inventory.slots[i].transform, false);
                    item = InventoryPanel;
                    Destroy(gameObject);
                    childitem.gameObject.SetActive(false);
                    playerStats.isInteracting = false;
                    playerStats.canInteract = false;
                    break;
                }
            }
        }
    }
}
