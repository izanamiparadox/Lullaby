using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HypoSystem : MonoBehaviour
{
    [Header("Main Stats")]
    public float urgentPeeTime;
    float savedMoveSpeed;

    [Header("Connections")]
    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] BathroomSystem bs;
    [SerializeField] GameObject hypo1;
    [SerializeField] GameObject hypo2;
    [SerializeField] GameObject hypo3;
    [SerializeField] Timer timer;
    [SerializeField] PlayerMovement playerMovement;


    private void Awake()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        playerMovement= FindObjectOfType<PlayerMovement>();
        bs = GetComponent<BathroomSystem>();
        timer = FindObjectOfType<Timer>();
        savedMoveSpeed = playerMovement.moveSpeed;
    }


    private void Update()
    {
        if (playerStatus.hypoMode)
        {
            HypoSystemRunning();
        }
        
    }


    void HypoSystemRunning()
    {
        if (!playerStatus.isWarm)
        {
            if (timer.timeValue < 240f)
            {
                hypo1.SetActive(true);
            }
            else
            {
                hypo1.SetActive(false);
            }
            if (timer.timeValue < 120f)
            {
                hypo2.SetActive(true);
            }
            else
            {
                hypo2.SetActive(false);
            }
            if (timer.timeValue < 60f)
            {
                hypo3.SetActive(true);
            }
            else
            {
                hypo3.SetActive(false);
            }
        }
        else
        {
            hypo1.SetActive(false);
            hypo2.SetActive(false); 
            hypo3.SetActive(false);
        }

        if (playerStatus.hypoMode)
        {
            playerMovement.moveSpeed /= 2f;
        }
        else
        {
            playerMovement.moveSpeed = savedMoveSpeed;
        }
    }
}
