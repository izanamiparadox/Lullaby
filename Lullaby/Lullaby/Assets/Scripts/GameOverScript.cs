using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] GameObject GOpanel;
    [SerializeField] PlayerStatus playerstats;
    [SerializeField] TouchCar carTouch;


    private void Awake()
    {
        playerstats = FindObjectOfType<PlayerStatus>();
        carTouch = FindObjectOfType<TouchCar>();
    }


    private void Update()
    {
        HandleDeath();
    }

    void HandleDeath()
    {
        if (playerstats.isDead)
        {
            if (playerstats.deathEnd == 1)
            {
                // Regular Ending
                Debug.Log("You saved yourself!");
            }
            else if (playerstats.deathEnd == 2)
            {
                if (carTouch.carSave)
                {
                    int value = Random.Range(0, 6);
                    if (value >= 3)
                    {
                        GOpanel.SetActive(true);
                    }
                    else
                    {
                        playerstats.deathEnd = 1;
                    }
                }
                // Game over ending
            }
        }
    }
}
