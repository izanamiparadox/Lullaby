using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttack : MonoBehaviour
{
    [SerializeField] PlayerStatus playerStats;
    public bool bearAttacked;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStatus>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            float dmg = Random.Range(10f, 40f);
            playerStats.health -= dmg;
            bearAttacked = true;

            if (playerStats.health == 0)
            {
                playerStats.deathEnd = 2;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        bearAttacked = false;
    }
}
