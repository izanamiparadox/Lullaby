using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterSysten : MonoBehaviour
{
    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] SimpleLighter lighter;

    public void Awake()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        lighter = GetComponent<SimpleLighter>();
    }


    private void Update()
    {
        HandleLighter();
    }

    void HandleLighter()
    {
        if (lighter.lighterInInventory)
        {
            playerStatus.canStartFire = true;
        }
    }
}
