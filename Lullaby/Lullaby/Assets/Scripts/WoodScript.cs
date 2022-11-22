using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodScript : MonoBehaviour
{
    public bool FireOn;
    [SerializeField] PlayerStatus playerStats;
    [SerializeField] SimpleLighter simlight;
    GameObject inventoryPanel;
    [SerializeField] GameObject fireUI;
    [SerializeField] float fireTimer;
    [SerializeField] ParticleSystem fireparticles;
    float timerSpeed = 1f;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStatus>();
        inventoryPanel = GameObject.Find("InventoryPanel");
        
        fireUI = GameObject.FindGameObjectWithTag("FireUI");
        fireparticles = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        HandleWood();
        HandleSimlight();
        Fire();
    }

    void HandleSimlight()
    {
        if (inventoryPanel.GetComponentInChildren<SimpleLighter>() != null)
        {
            simlight = inventoryPanel.GetComponentInChildren<SimpleLighter>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerStats.isInShelter)
            {
                var childitem = fireUI.transform.GetChild(0);
                childitem.gameObject.SetActive(true);
                playerStats.canInteract = true;
            }
            
        }
    }


    private void OnTriggerStay(Collider other)
    {
        var childitem = fireUI.transform.GetChild(0);
        var text1 = childitem.GetChild(1);
        var text2 = childitem.GetChild(0);

        if (other.gameObject.tag == "Player")
        {
            if (playerStats.settedFire)
            {
                if (playerStats.canStartFire)
                {
                    if (simlight.lighterInInventory)
                    {
                        if (playerStats.isInShelter)
                        {
                            FireOn = true;
                            playerStats.isWarm = true;
                            childitem.gameObject.SetActive(false);
                        }

                    }
                }
                else
                {

                    childitem.gameObject.SetActive(true);
                    text1.gameObject.SetActive(true);
                    text2.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerStats.canInteract = false;
        var childitem = fireUI.transform.GetChild(0);
        var text1 = childitem.GetChild(1);
        var text2 = childitem.GetChild(0);
        childitem.gameObject.SetActive(false);
        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(true);
    }

    void Fire()
    {
        if (FireOn)
        {
            fireparticles.Play();
        }
        else
        {
            fireparticles.Stop();
        }
    }

    void HandleWood()
    {
        if (FireOn)
        {
            // Set wood on fire
            fireTimer -= timerSpeed * Time.deltaTime;

            if (fireTimer <= 0)
            {
                fireTimer = 0;
                // Do effect
                FireOn = false;
                StartCoroutine(ColdAgain());
            }
        }
    }

    IEnumerator ColdAgain()
    {
        yield return new WaitForSeconds(60f);
        playerStats.isWarm = false;
    }
}
