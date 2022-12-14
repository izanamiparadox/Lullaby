using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeValue = 90f;
    public Text timeText;
    public bool timeStarted = false;

    public static bool activateMechanic;
    public float fValue;

    public float AddTime;
    public bool interuptTime;
    public bool timeAdded;

    public void StartGame()
    {
        timeStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!interuptTime)
        {
            TimerSystem();
        }
        InteruptTime();

    }

    private void TimerSystem()
    {
        if (timeStarted == true)
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                timeValue = 0;
            }

            DisplayTime(timeValue);
        }

        if (timeValue < fValue)
        {
            activateMechanic = true;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    void InteruptTime()
    {
        if (timeAdded)
        {
            AddTime += timeValue;
            timeAdded = false;
        }

    }
}
