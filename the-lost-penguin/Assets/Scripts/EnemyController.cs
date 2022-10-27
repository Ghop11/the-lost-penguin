using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed;
    public Transform[] patrolPoints;
    private int currentPatrolPoint = 0;
    
    private Vector3 moveDirection;
    private float yStore;
    
    public Rigidbody theRB;
    
    

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform pp in patrolPoints)
        {
            pp.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        yStore = theRB.velocity.y;
        moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;

        moveDirection.y = 0f;
        moveDirection.Normalize();

        theRB.velocity = moveDirection * moveSpeed;
        theRB.velocity = new Vector3(theRB.velocity.x, yStore, theRB.velocity.z);

        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) <= .5f)
        {
            NextPatrolPoint();
        }
        
        transform.LookAt(patrolPoints[currentPatrolPoint].position);
    }

    public void NextPatrolPoint()
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth.instance.DamagePlayer();
        }
    }

    public void FixedUpdate()
    {

    }

}
