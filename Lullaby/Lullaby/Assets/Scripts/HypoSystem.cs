using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypoSystem : MonoBehaviour
{
    [Header("Main Stats")]
    public bool denpaOn;
    public bool glitchOn;
    public float urgentPeeTime;

    [Header("Connections")]
    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] BathroomSystem bs;


    private void Awake()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        bs = GetComponent<BathroomSystem>();
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
        if (denpaOn)
        {
            // Do something
        }

        if (glitchOn)
        {
            // Do something
        }
    }
}
