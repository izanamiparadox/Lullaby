using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCaveScript : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public EnterCaveScript enterCaveScript;

    public bool isExit = false;

    // Start is called before the first frame update
    void Awake()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (enterCaveScript.isEntered == true)
            {
                isExit = true;
                enterCaveScript.isEntered = false;
            }
            
        }
    }

    void Update()
    {
        if (isExit == true && playerStatus.isInShelter == true)
        {
            playerStatus.isInShelter = false;
        }
    }
}
