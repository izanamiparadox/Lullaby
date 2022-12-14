using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCar : MonoBehaviour
{
    [SerializeField] CarFollow follow;
    [SerializeField] GameObject CarUI;
    [SerializeField] PlayerStatus playerStats;
    [SerializeField] BoxCollider boxCollider;
    public bool carSave;

    void Awake()
    {
        follow = GetComponent<CarFollow>();
        playerStats = FindObjectOfType<PlayerStatus>();
        boxCollider = GetComponent<BoxCollider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CarUI.SetActive(true);
            playerStats.canInteract = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerStats.isInteracting)
            {
                CarUI.SetActive(false);
                playerStats.deathEnd = 2;
                carSave = true;
                playerStats.health = 0;
                boxCollider.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CarUI.SetActive(false);
        playerStats.canInteract = false;
    }
}
