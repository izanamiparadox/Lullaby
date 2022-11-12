using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCaveScript : MonoBehaviour
{

    public PlayerStatus playerStatus;
    public ExitCaveScript exitCaveScript;
    public bool isEntered = false;

    // Start is called before the first frame update
    void Awake()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
    }

    void OnTriggerEnter(Collider other)
    {
       // Debug.Log("Is hit.");
        if (other.gameObject.tag == "Player")
        {
            if (exitCaveScript.isExit == true)
            {
                exitCaveScript.isExit = false;
                isEntered = true;
                playerStatus.isInShelter = true;
            }
            
        }
    }


}
