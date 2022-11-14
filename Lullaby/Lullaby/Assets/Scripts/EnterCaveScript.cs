using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCaveScript : MonoBehaviour
{

    [SerializeField] PlayerStatus playerStatus;
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
             isEntered = true; 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerStatus.isInShelter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isEntered = false;
        playerStatus.isInShelter = false;
    }


}
