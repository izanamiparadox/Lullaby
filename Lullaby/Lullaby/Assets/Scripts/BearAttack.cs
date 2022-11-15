using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttack : MonoBehaviour
{
    [SerializeField] PlayerStatus playerStats;

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
        }
    }
}
