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
    float distanceFromEnd = Mathf.Infinity;
    float distanceTravelled;
    [SerializeField] float endRange;
    [SerializeField] float secondsOnEvent;
    [SerializeField] Transform sphere;
    [SerializeField] AudioSource audS;

    public bool playerNear;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        audS = GetComponent<AudioSource>();
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
        distanceFromEnd = Vector3.Distance(transform.position, sphere.position);

    }
    void HandleCarPath()
    {
        if (timer.timeValue < secondsOnEvent)
        {
            if (!playerNear)
            {
                if (endRange <= distanceFromEnd)
                {
                    distanceTravelled += speed * Time.deltaTime;
                    transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                    transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
                }
            }
            
        }
    }

    void DetectRange()
    {
        if (playerRange >= distanceToTarget)
        {
            playerNear = true;
            audS.PlayOneShot(audS.clip);
        }
        else
        {
            playerNear = false;
        }
    }
}
