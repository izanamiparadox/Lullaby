using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool isPaused;


    private void Update()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
   
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
