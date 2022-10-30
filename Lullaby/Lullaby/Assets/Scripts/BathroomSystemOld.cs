using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomSystemOld : MonoBehaviour
{

    public Timer timer;
    public PlayerStatus playerStatus;

    public float peeTimer;

    float forTimer = 30f;
    public float count;
    public float peeValue;
    public float CD;

    public float bladderFull;

    public bool gainingPee;
    public bool losingPee;

    public bool playerHasPeed;

    public bool peeSystemOn;

    public float drainingSpeed = 0.5f;

    public bool isOn;



   


    void Update()
    {
        if (timer.timeValue <= 541)
        {
            peeSystemOn = true;

            if (peeSystemOn)
            {
                if (!playerStatus.hasToPee)
                {
                    if (!losingPee)
                    {
                        gainingPee = true;
                    }
                      
                    PeeCountSystem();
                    
                }
                

                if (peeValue >= bladderFull)
                {
                    if (!losingPee)
                    {
                        PeeGameOn();
                    }
                    
                }
            }
        }
    }


    void PeeCountSystem()
    {

        if (gainingPee)
        {
           
            peeValue += (drainingSpeed * Time.deltaTime);
        

        }
        if (losingPee)
        {
            
            peeValue -= (drainingSpeed * Time.deltaTime);
            //Debug.Log("Decreasing pee..");
        
        }

        if (peeValue >= bladderFull && !losingPee)
        {
            peeValue = bladderFull;
        }

        if (peeValue < 0)
        {
            peeValue = 0;
            StartCoroutine(peeCoolDown());
        }
    }

    IEnumerator peeCoolDown()
    {
        //Debug.Log("Cooldown Started");
        yield return new WaitForSeconds(CD);

        if (losingPee)
        {
            peeTimer = forTimer;
            losingPee = false;
            gainingPee = true;

            if (playerHasPeed)
            {
                playerHasPeed = false;
            }
        }
    }

    void PeeGameOn()
    {

       isOn = true;
       peeTimer -= (drainingSpeed * Time.deltaTime);

       if (peeTimer > 0 && playerHasPeed)
        {
            timer.timeValue -= 20;
            isOn = false;
            gainingPee = false;
            losingPee = true;
            Debug.Log("Player has peed.");
        }

        if (peeTimer <= 0 && !playerHasPeed)
        {
            timer.timeValue -= 60;
            isOn = false;
            gainingPee = false;
            losingPee = true;;
            Debug.Log("Player has peed themselves.");
        }


        if (peeTimer < 0)
        {
            peeTimer = 0;
        }
    }

}
