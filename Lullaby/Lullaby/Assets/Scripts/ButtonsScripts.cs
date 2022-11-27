using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScripts : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] Timer timer;
    [SerializeField] BathroomSystem bs;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = FindObjectOfType<Timer>();
        bs = FindObjectOfType<BathroomSystem>();
    }


    public void onTeleportTo(Transform spawner)
    {
        player.transform.position = spawner.position;
    }

    public void onStopTimer()
    {
        timer.interuptTime = !timer.interuptTime;
    }

    public void onStopPeeSystem()
    {
        bs.peeSystemOn = !bs.peeSystemOn;
    }
}
