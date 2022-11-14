using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject[] prefabs;

    public bool[] canDrop;

    [SerializeField] PlayerStatus playerStats;


    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStatus>();
    }

    public void Update()
    {
        HandleUse();
    }

    void HandleUse()
    {
        for (int i = 0; i < slots.Length; i++)
        {

            if (slots[i].GetComponentInChildren<ItemData>() != null)
            {
                var slot = slots[i].GetComponentInChildren<ItemData>();
                if (slot.ID == 0)
                {
                    if (canDrop[i])
                    {
                        if (playerStats.isDroppingInventory)
                        {
                            Destroy(slot.gameObject);
                            isFull[i] = false;
                            canDrop[i] = false;
                        }
                    }
                }
                if (slot.ID == 1)
                {
                    if (canDrop[i])
                    {
                        if (playerStats.isDroppingInventory)
                        {
                            for (int x = 0; x < prefabs.Length; x++)
                            {
                                if (prefabs[x].GetComponent<PrefabData>() != null)
                                {
                                    var prefab = prefabs[x].GetComponent<PrefabData>();

                                    if (prefab.prefabID == slot.ID)
                                    {
                                        Instantiate(prefab, new Vector3(playerStats.transform.position.x, playerStats.transform.position.y, playerStats.transform.position.z + 0.5f), playerStats.transform.rotation);
                                        Destroy(slot.gameObject);
                                        isFull[i] = false;
                                        canDrop[i] = false;
                                        break;
                                    }
                                }
                            }
                        }

                    }
                }
                if (slot.ID == 2)
                {
                    if (canDrop[i])
                    {
                        if (playerStats.isDroppingInventory)
                        {
                            for (int x = 0; x < prefabs.Length; x++)
                            {
                                if (prefabs[x].GetComponent<PrefabData>() != null)
                                {
                                    var prefab = prefabs[x].GetComponent<PrefabData>();

                                    if (prefab.prefabID == slot.ID)
                                    {
                                        Instantiate(prefab, new Vector3(playerStats.transform.position.x , playerStats.transform.position.y, playerStats.transform.position.z + 0.5f), playerStats.transform.rotation);
                                        Destroy(slot.gameObject);
                                        isFull[i] = false;
                                        canDrop[i] = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
