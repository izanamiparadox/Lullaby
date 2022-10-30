using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomSystem : MonoBehaviour
{
    [Header("Main Stats")]
    [SerializeField] float peeValue;
    public bool peeSystemOn;
    [SerializeField] bool playerHasPeed;
    [SerializeField] float maxPeeValue;
    [Space]
    [SerializeField] bool peeGaining;
    [SerializeField] bool peeLosing;
    [Space]
    [SerializeField] float peeCD;
    [SerializeField] bool cdOn;
    [SerializeField] float drainingSpeed;
    bool DoOnlyOnce;

    [Header("Timer")]
    public float peeTimer;
    float peeTimerSaved;
    float peeTimeOriginal;

    [Header("Connections")]
    [SerializeField] private Timer timer;
    public PlayerStatus playerStatus;
    [SerializeField] HypoSystem hypoSys;


    private void Awake()
    {
        timer = GetComponent<Timer>();
        playerStatus = FindObjectOfType<PlayerStatus>();
        peeTimerSaved = peeTimer;
        peeTimeOriginal = peeTimer;
        hypoSys = GetComponent<HypoSystem>();
    }

    private void Update()
    {
        if (timer.timeValue <= 541)
        {
            peeSystemOn = true;
            PeeSystemRunning();
        }
        else
        {
            peeSystemOn = false;
        }
        
    }

    void PeeSystemRunning()
    {
        if (!playerStatus.hasToPee)
        {
            if (!peeLosing)
            {
                if (playerStatus.hypoMode)
                {
                    if (!DoOnlyOnce)
                    {
                        peeTimer -= hypoSys.urgentPeeTime;
                        DoOnlyOnce = true;
                    }
                }
                else
                {
                    peeTimer = peeTimeOriginal;
                }
                
                peeGaining = true;
                GainingBladder();
            }
            else
            {
                LosingBladder();
            }
            
        }

        if (peeValue >= maxPeeValue)
        {
            if (!peeLosing)
            {
                peeGaining = false;
                playerStatus.hasToPee = true;
                peeValue = maxPeeValue;
                UrgentDuty();
            }
        }
    }

    void GainingBladder()
    {
        if (peeGaining)
        {
            peeValue += (drainingSpeed * Time.deltaTime);
        }
        
    }

    void LosingBladder()
    {
        if (peeLosing)
        {
            peeValue = 0;
            StartCoroutine(PeeCoolDown());
        }
    }

    void UrgentDuty()
    {
        if (peeTimer >= 0)
        {
            peeTimer -= (drainingSpeed * Time.deltaTime);
        }

        if (peeTimer > 0 && playerHasPeed)
        {
            timer.timeValue -= 30;
            peeLosing = true;
            cdOn = true;
            playerStatus.hasToPee = false;
        }
        else if (peeTimer <= 0 && !playerHasPeed)
        {
            timer.timeValue -= 60;
            peeLosing = true;
            cdOn = true;
            playerStatus.hasToPee = false;
        }
    }

    IEnumerator PeeCoolDown()
    {
        yield return new WaitForSeconds(peeCD);
        peeTimer = peeTimerSaved;
        peeLosing = false;
        cdOn = false;

        if (playerHasPeed)
        {
            playerHasPeed = false;
        }
    }

}
