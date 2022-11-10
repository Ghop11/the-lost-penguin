using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed, turnSpeed;
    public Transform[] patrolPoints;
    private int currentPatrolPoint = 0;
    
    private Vector3 moveDirection, lookTarget;
    private float yStore;
    
    public Rigidbody theRB;
    private PlayerController thePlayer;
    // private Boolean inRange;

    public enum EnemyState
    {
        idle, 
        patrolling,
        chasing,
        returning
    };

    public EnemyState currentState;
    public float waitTime, waitChance, waitBeforeReturning, chaseCounter;
    private float waitCounter, returnCounter, chaseWaitCounter;
    public float chaseDistance, chaseSpeed, loseDistance;
    
    

    // Start is called before the first frame update
    void Start()
    {
        // inRange = false;
        thePlayer = FindObjectOfType<PlayerController>();
        foreach (Transform pp in patrolPoints)
        {
            pp.parent = null;
        }

        currentState = EnemyState.patrolling;
        waitCounter = waitTime;
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case EnemyState.idle:
                // waiting for something to happen
                yStore = theRB.velocity.y;
                theRB.velocity = new Vector3(0f, yStore, 0f);
                
                waitCounter -= Time.deltaTime;
                if (waitCounter <= 0)
                {
                    currentState = EnemyState.patrolling;
                    NextPatrolPoint();
                }
                break;
            
            case EnemyState.patrolling:
                yStore = theRB.velocity.y;
                moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;

                moveDirection.y = 0f;
                moveDirection.Normalize();

                theRB.velocity = moveDirection * moveSpeed;
                theRB.velocity = new Vector3(theRB.velocity.x, yStore, theRB.velocity.z);

                if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) <= .8f)
                {
                    NextPatrolPoint();
                }
                else
                {
                    lookTarget = patrolPoints[currentPatrolPoint].position;
                }
                break;
            
            case EnemyState.chasing:
                
                lookTarget = thePlayer.transform.position;
                
                if (chaseWaitCounter > 0)
                {
                    chaseWaitCounter -= Time.deltaTime;
                }
                else
                {
                    yStore = theRB.velocity.y;
                    moveDirection = thePlayer.transform.position - transform.position;

                    moveDirection.y = 0f;
                    moveDirection.Normalize();

                    theRB.velocity = moveDirection * chaseSpeed;
                    theRB.velocity = new Vector3(theRB.velocity.x, yStore, theRB.velocity.z);
                }

                if (Vector3.Distance(thePlayer.transform.position, transform.position) > loseDistance)
                {
                    currentState = EnemyState.returning;
                    returnCounter = waitBeforeReturning;
                }
                break;
            
            case EnemyState.returning:
                returnCounter -= Time.deltaTime;
                if (returnCounter <= 0)
                {
                    currentState = EnemyState.patrolling;
                }
                break;
            
        }


        if (Vector3.Distance(thePlayer.transform.position, transform.position) < chaseDistance)
        {
            currentState = EnemyState.chasing;
        }

        
        lookTarget.y = patrolPoints[currentPatrolPoint].position.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookTarget - transform.position), turnSpeed * Time.deltaTime );
        
        // transform.LookAt(lookTarget);
        // transform.LookAt(patrolPoints[currentPatrolPoint].position);
    }

    public void NextPatrolPoint()
    {
        if (Random.Range(0f, 100) < waitChance)
        {
            waitCounter = waitTime;
            currentState = EnemyState.idle;
        }
        else
        {
            if (currentPatrolPoint == 2)
            {
                currentPatrolPoint = 0;
            }
            else
            {
                currentPatrolPoint++;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth.instance.DamagePlayer();
            chaseWaitCounter = chaseCounter;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth.instance.DamagePlayer();
            chaseWaitCounter = chaseCounter;
        }
    }
    

    public void FixedUpdate()
    {

    }

}
