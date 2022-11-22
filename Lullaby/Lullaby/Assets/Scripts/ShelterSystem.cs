using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterSystem : MonoBehaviour
{
    [SerializeField] PlayerStatus playerStats;
    [SerializeField] ShelterCount sheltersCount;
    [SerializeField] Transform[] shelters;
    [SerializeField] Timer timer;
    [SerializeField] bool bearAttack;
    [SerializeField] float attackTimer;


    public void Awake()
    {
        StartUp();
    }
    private void Update()
    {
        BearAttackIm();
        HandleSystem();
    }

    void StartUp()
    {
        playerStats = FindObjectOfType<PlayerStatus>();
        sheltersCount = FindObjectOfType<ShelterCount>();
        timer = FindObjectOfType<Timer>();
    }
    

    void HandleSystem()
    {
        shelters = sheltersCount.shelters.ToArray();
        
        for (int i = 0; i < shelters.Length; i++)
        {
            var getID = shelters[i].GetComponentInChildren<CaveID>();

            var getCave = shelters[i].GetComponentInChildren<EnterCaveScript>();

            switch(getID.id)
            {
                case 0:
                    // Good Cave, No beart
                    if (getCave.isEntered)
                    {
                        StartCoroutine(GoodCave());
                    }
                    break;
                case 1:
                    // Bad Cave, Bear Near
                    if (getCave.isEntered)
                    {
                        bearAttack = true;
                        StartCoroutine(GoodCave());
                    }
                    else
                    {
                        bearAttack = false;
                    }
                    break;
                case 2:
                    // Good Shelter
                    if (getCave.isEntered)
                    {
                        StartCoroutine(GoodCave());
                    }
                    break;
                case 3:
                    // Bad Shelter
                    return;
                default:
                    return;
            }
        }
    }

   


    void BearAttackIm()
    {
        if (bearAttack)
        {
            RandomAttack();
        }
    }

    void RandomAttack()
    {
        if (attackTimer >= 0f)
        {
            attackTimer += Time.deltaTime;
        }
        if (attackTimer >= 30f)
        {
            playerStats.health -= 20f;
            attackTimer = 0f;
            
        }
    }
    IEnumerator GoodCave()
    {
        timer.interuptTime = true;
        yield return new WaitForSeconds(120);
        if (!playerStats.isWarm)
        {
            timer.interuptTime = false;
        }
    }
}
