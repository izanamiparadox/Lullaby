using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;


public enum BearState {Idle, Patrol, Chase, AttackClose}
public class BearAI : MonoBehaviour
{

    [Header("AI State")]
    [SerializeField] BearState state;


    [Header("Timer Functions")]
    [SerializeField] float attackTimer = 0f; // time(seconds) it will take for the AI action to reach its end
    [SerializeField] float attackTimerMultiplier = 1f;
    [SerializeField] float timer;
    [SerializeField] float cdTimer;
    [SerializeField] bool timerStarted;
    [SerializeField] bool cdTimerStarted;
    [SerializeField] bool attackStarted;
    [SerializeField] bool canChangeState;   // when true its allowed to change state from current state to another
    float waitTime = 5f;

    [Header("Captures")]
    [SerializeField] Transform target;

    [Header("Functions")]
    //public bool isFarAttack;
    //public bool isCloseAttack;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float attackCloseRange = 3f;
    float distanceToTarget = Mathf.Infinity;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] bool isChase;
    [SerializeField] float attackCD;
    [SerializeField] bool canInitiate;
    public float multiplier = 1f;
    [SerializeField] bool isMoving;

    [Header("Nav mesh")]
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;
    public LayerMask whatIsGround;
    [SerializeField] float normalSpeed;



    [Header("Connections")]
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent agent;
    public PlayerStatus playerStatus;
    public Transform sphere;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>(); 
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerStatus = target.GetComponent<PlayerStatus>();
        audioSource = GetComponent<AudioSource>();
        canChangeState = true;
        normalSpeed = agent.speed;
    }


    public void HandleTimer()
    {
        if (attackTimer > 0f)
        {
            attackTimer -= attackTimerMultiplier * Time.deltaTime;
        }

        if (cdTimer > 0f)
        {
            cdTimer -= attackTimerMultiplier * Time.deltaTime;
        }
    }

    void Update()
    {
        
        HandleTimer();
        HandleState();
        HandleAI();
        HandleCheckDistance();
    }


    void HandleState()
    {
        if (distanceToTarget >= chaseRange && canChangeState)
        {
            SetStatePatrol();
            Debug.Log("Now patroling");
            canChangeState = false;
            
        }


        if (!cdTimerStarted)
        {
            cdTimerStarted = true;
            cdTimer = attackCD;
        }


        if (cdTimer <= 0.2f)
        {

            if (distanceToTarget <= attackCloseRange && canChangeState)
            {
                cdTimerStarted = false;
                SetStateAttackClose(); // Set Close Attack
            }

        }

        if (distanceToTarget <= chaseRange && distanceToTarget >= attackCloseRange && canChangeState)
        {
            SetStateChase(); // Set Chase
        }
    }

    void HandleAI()
    {

        if (state == BearState.AttackClose)
        {
            canChangeState = false;

            if (!attackStarted && attackTimer <= 0.2f)
            {
                attackStarted = true;
                attackTimer = 1.5f;
            }

            LookAtPlayer();
            animator.Play("Arm_Bear|attack_1");

            if (attackTimer <= 0.2)
            {
                attackStarted = false;
                canChangeState = true;
            }

        }
        else if (state == BearState.Chase)
        {

            if (isChase)
            {
                animator.Play("Arm_Bear|Run_forward_IP");

                LookAtPlayer();
                agent.speed = agent.speed * 2f;
                audioSource.PlayOneShot(audioSource.clip);
                agent.SetDestination(target.position);
            }

            if (distanceToTarget <= attackCloseRange)
            {
                isChase = false;
                agent.speed = normalSpeed;
                SetStateAttackClose();
            }
            else
            {
                isChase = true;

            }


        }
        else if (state == BearState.Patrol)
        {

            if (!walkPointSet)
            {
                SearchWalkPoint();
            }

            if (walkPointSet)
            {
                agent.SetDestination(walkPoint);
            }


            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            if (distanceToWalkPoint.magnitude > 1f)
            {
                animator.Play("Arm_Bear|Walk_forward_IP");
                isMoving = true;

                if (distanceToTarget <= chaseRange)
                {
                    SetStateChase(); // Set Chase
                    return;
                }

            }

            
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        isMoving = false;
                        walkPointSet = false;
                        canChangeState = true;
                    }

                }
            }
        }                                                                 

        else if (state == BearState.Idle)
        {
            animator.Play("Arm_Bear|Idle_1");

            if (timer >= 0f)
            {
                timer += Time.deltaTime;
            }
            if (timer >= waitTime)
            {
                timer = 0f;
                canChangeState = true;
            }
        }
    }

    
    void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            Debug.Log("Finished searching point.");
        }
            
    }


    


    void HandleCheckDistance()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

    }

    void LookAtPlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void SetStateIdle()
    {
        state = BearState.Idle;
    }

   
    void SetStateChase()
    {
        state = BearState.Chase;
    }


    void SetStateAttackClose()
    {
        state = BearState.AttackClose;
    }

    void SetStatePatrol()
    {
        state = BearState.Patrol;
    }

}
