using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevMenuScript : MonoBehaviour
{
    [SerializeField] GameObject devMenu;
    public bool devMenuOn;


    private void Update()
    {
        if (devMenuOn)
        {
            devMenu.SetActive(true);
        }
        else
        {
            devMenu.SetActive(false);
        }
    }
}
