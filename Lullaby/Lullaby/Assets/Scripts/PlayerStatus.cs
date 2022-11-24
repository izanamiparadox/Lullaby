using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    [Header("Main Stat")]
    public bool hypoMode = false;
    public bool isDead = false;
    public bool isInteracting = false;
    public bool canInteract = false;
    public bool isDroppingInventory;
    public float health;
    public float maxHealth;
    public float moveSpeed;
    public int deathEnd;
    

   

    [Header("Game Status")]
    public bool canStartFire = false;
    public bool settedFire = false;
    public bool isInShelter = false;
    public bool hasToPee = false;
    public bool canMove;
    public bool isRunning;
    public bool isWarm;

    [Header("Connections")]
    public Timer timer;
    public BathroomSystem bs;

    public void Awake()
    {
        health = maxHealth;
    }
    public void canMovePlayer()
    {
        canMove = true;
    }


    // Update is called once per frame
    void Update()
    {

        if (timer.timeValue <= 300)
        {
            hypoMode = true;
        }

        if (timer.timeValue == 0)
        {
            health = 0;
            deathEnd = 1;
        }

        if (health <= 0)
        {
            health = 0;
            isDead = true;
        }
        else
        {
            isDead = false;
        }

       /* if(bs.isOn)
        {
            hasToPee = true;
        }
        else
        {
            hasToPee = false;
        }

        */
       
    }
}
