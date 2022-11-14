using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BathroomSystem : MonoBehaviour
{
    [Header("Main Stats")]
    public bool peeSystemOn;
    [SerializeField] bool playerHasPeed;
    [SerializeField] float maxPeeValue;
    [SerializeField] float peeValue;
    [Space]
    [SerializeField] bool peeGaining;
    [SerializeField] bool peeLosing;
    [Space]
    [SerializeField] float peeCD;
    [SerializeField] bool cdOn;
    [SerializeField] float drainingSpeed;
    bool DoOnlyOnce;
    [SerializeField] float currentPeeValue;

    [Header("Timer")]
    public float peeTimer;
    float peeTimerSaved;
    float peeTimeOriginal;

    [Header("Connections")]
    [SerializeField] private Timer timer;
    public PlayerStatus playerStatus;
    [SerializeField] HypoSystem hypoSys;
    public Image peebar;
    [SerializeField] Animator anim;


    private void Awake()
    {
        timer = GetComponent<Timer>();
        playerStatus = FindObjectOfType<PlayerStatus>();
        peeTimerSaved = peeTimer;
        peeTimeOriginal = peeTimer;
        hypoSys = GetComponent<HypoSystem>();
        peebar = GameObject.FindGameObjectWithTag("BathroomUI").transform.GetChild(0).GetComponent<Image>();
        anim = GameObject.FindGameObjectWithTag("BathroomUI").transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        Pee();
        PeeUI();
        PeeGameplay();
    }

    void PeeGameplay()
    {
        if (playerStatus.settedFire && playerStatus.hasToPee)
        {
            playerHasPeed = true;
        }
    }

    private void Pee()
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
                anim.enabled = true;
                anim.Play("peebaranim");
                UrgentDuty();
            }
        }
    }

    void PeeUI()
    {
        currentPeeValue = peeValue;

        peebar.fillAmount = currentPeeValue / maxPeeValue;
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
            anim.enabled = false;
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
            timer.timeValue -= 10;
            peeLosing = true;
            cdOn = true;
            playerStatus.hasToPee = false;
        }
        else if (peeTimer <= 0 && !playerHasPeed)
        {
            timer.timeValue -= 15;
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
