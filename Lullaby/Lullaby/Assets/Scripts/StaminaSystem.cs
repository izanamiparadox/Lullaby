using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{

    public PlayerMovement tpc;
    public float staminaAmount;
    [SerializeField] float maxStamina;
    public Text textStamina;

    public bool cD;

    public void Awake()
    {
        tpc = FindObjectOfType<PlayerMovement>();
        textStamina = GameObject.FindGameObjectWithTag("StaminaUI").GetComponent<Text>();
        staminaAmount = maxStamina;
    }

    public void Update()
    {
        HandleStamina();
        HandleUI();
        Retry();
    }

    void HandleUI()
    {
        float stamina = Mathf.RoundToInt(staminaAmount);

        textStamina.text = stamina.ToString();
    }


    void HandleStamina()
    {
        if (tpc.canRun)
        {
            if (staminaAmount >= 0f)
            {
                staminaAmount -= 1f * Time.deltaTime;
            }
            
            if (staminaAmount <= 1f)
            {
                staminaAmount = 0f;
                tpc.canRun = false;
                cD = true;
            }
        }
        else if (!cD && !tpc.canRun)
        {
            staminaAmount += 0.5f * Time.deltaTime;

            if (staminaAmount >= maxStamina)
            {
                staminaAmount = maxStamina;
            }
        }
    }

    void Retry()
    {
        if (cD)
        {
            staminaAmount += 2f * Time.deltaTime;

            if (staminaAmount >= maxStamina)
            {
                staminaAmount = maxStamina;
                cD = false;
            }
        }
        
    }
}
