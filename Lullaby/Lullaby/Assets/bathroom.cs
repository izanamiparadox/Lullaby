using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bathroom : MonoBehaviour
{

    [SerializeField] private float peeValue;
    [SerializeField] private bool hasPeed;
    [SerializeField] private bool gainingPee;
    [SerializeField] private float peeTimer;
    [SerializeField] private float lastPee;

    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private Timer timer;

    private void Start()
    {
        ResetPeeState();
    }

    public void ResetPeeState()
    {
        //Initialize variables

        peeValue = 0.0f;
        lastPee = 0.0f;
        peeTimer = 5.0f;
        gainingPee = false;
    }

    private void Update()
    {
        // if gainingpee, then 1 else -1
        int peeMultiplier = gainingPee ? 1 : -1;

        //Prevent peeValue from going below 0 or above 10
        peeValue += Mathf.Clamp(Time.deltaTime * peeMultiplier, 0.0f, 10.0f);

        //This only happens if we are gaining pee
        if (lastPee < 10.0f && peeValue >= 10.0f)
            peeTimer = 0.0f;

        //This only happens if we are losing pee
        if (lastPee > 0.0f && peeValue <= 0.0f)
            peeTimer = 0.0f;

        peeTimer += Time.deltaTime;

        CheckPeeStatus();
    }

    private void CheckPeeStatus()
    {
        //Prep for next frame
        lastPee = peeValue;

        //turn on GFX
        playerStatus.hasToPee = HasToPee();

        //Player has had to pee for 30 seconds
        if (HasToPee() && peeTimer > 30.0f)
            DoPenalty();

        //Player has been at pee level 0 for 5 seconds
        if (peeValue <= 0.0f && peeTimer > 5.0f)
            StartGainingPee();
    }

    public void StartLosingPee()
    {
        gainingPee = false;
    }

    public void StartGainingPee()
    {
        gainingPee = true;
    }

    public bool HasToPee()
    {
        return peeValue >= 10.0f;
    }

    public void DoPenalty()
    {
        //Penalty script here

        timer.timeValue = timer.timeValue - 60;
        playerStatus.hasToPee = false;

        ResetPeeState();
    }
}
