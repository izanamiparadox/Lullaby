using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] GameObject GOpanel;
    [SerializeField] GameObject[] desc;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject Winpanel;
    [SerializeField] PlayerStatus playerstats;
    [SerializeField] TouchCar carTouch;
    [SerializeField] BearAttack bear;
    [SerializeField] ShelterSystem shelter;
    [SerializeField] PlayerInput playerinput;


    private void Awake()
    {
        playerstats = FindObjectOfType<PlayerStatus>();
        carTouch = FindObjectOfType<TouchCar>();
        bear = FindObjectOfType<BearAttack>();
        shelter = FindObjectOfType<ShelterSystem>();
        playerinput = new PlayerInput();
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
                Winpanel.SetActive(true);
                buttons.SetActive(true);
                playerstats.canMove = false;
                playerinput.Player.Disable();
                playerinput.UI.Enable();
                Debug.Log("You saved yourself!");
            }
            else if (playerstats.deathEnd == 2)
            {
                if (carTouch.carSave)
                {
                    int value = Random.Range(0, 6);
                    if (value >= 3)
                    {
                        // Game over ending 1
                        GOpanel.SetActive(true);
                        buttons.SetActive(true);
                        desc[0].SetActive(true);
                        playerstats.canMove = false;
                        carTouch.carSave = false;
                        playerinput.Player.Disable();
                        playerinput.UI.Enable();
                    }
                    else
                    {
                        playerstats.deathEnd = 1;
                        carTouch.carSave = false;
                    }
                }
                else if (bear.bearAttacked)
                {
                    // Game over ending 2
                    GOpanel.SetActive(true);
                    buttons.SetActive(true);
                    desc[1].SetActive(true);
                    playerstats.canMove = false;
                    playerinput.Player.Disable();
                    playerinput.UI.Enable();
                }
                else if (shelter.bearAttack)
                {
                    // Game over ending 3
                    GOpanel.SetActive(true);
                    buttons.SetActive(true);
                    desc[2].SetActive(true);
                    playerstats.canMove = false;
                    playerinput.Player.Disable();
                    playerinput.UI.Enable();
                }
                
            }
        }
    }
}
