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
    public float moveSpeed;
    

   

    [Header("Game Status")]
    public bool canStartFire = false;
    public bool settedFire = false;
    public bool isInShelter = false;
    public bool hasToPee = false;
    public bool canMove;
    public bool isRunning;

    [Header("Connections")]
    public Timer timer;
    public BathroomSystem bs;




    // Update is called once per frame
    void Update()
    {

        if (timer.timeValue <= 301)
        {
            hypoMode = true;
        }

        if (timer.timeValue == 0)
        {
            isDead = true;
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
