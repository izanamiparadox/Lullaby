using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFollow : MonoBehaviour
{
    public PathCreator pathCreator;
    public Timer timer;
    public GameObject Player;
    public float speed;
    public float playerRange;
    float distanceToTarget = Mathf.Infinity;
    float distanceTravelled;
    [SerializeField] float secondsOnEvent;

    public bool playerNear;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        HandleCarPath();
        HandleVars();
        DetectRange();
    }

    void HandleVars()
    {
        distanceToTarget = Vector3.Distance(transform.position, Player.transform.position);

    }
    void HandleCarPath()
    {
        if (timer.timeValue < secondsOnEvent)
        {
            if (!playerNear)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
            }
            
        }
    }

    void DetectRange()
    {
        if (playerRange >= distanceToTarget)
        {
            playerNear = true;
        }
        else
        {
            playerNear = false;
        }
    }
}
