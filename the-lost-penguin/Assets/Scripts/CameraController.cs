using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    private Vector3 offsetPosition;
    private Quaternion offsetRoatation;
    
    
    // 1 = normal straight ahead view
    // 2 = turn camera right 90 degrees
    // 3 = turn camera behind (total 180 degrees from start view)
    // 4 = turn camera left (this will be left of normal view)
    // Camera is only rotating 90 degrees right then resets 1 when a full 360 is completed
    private int cameraAngleView; 

    public float moveSpeed = 15f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        offsetPosition = transform.position;
        offsetRoatation = transform.rotation;
        cameraAngleView = 1;

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        // move camera angle right
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (cameraAngleView == 4)
            {
                cameraAngleView = 0;
            }
            cameraAngleView++;
           MoveCameraAngle();
        }

        // move camera angle left
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (cameraAngleView == 1)
            {
                cameraAngleView = 5;
            }
            cameraAngleView--;
            MoveCameraAngle();
        }
        
        transform.position = Vector3.Lerp(transform.position, target.position + offsetPosition, moveSpeed * Time.deltaTime);

        if (transform.position.y < offsetPosition.y)
        {
            transform.position = new Vector3(transform.position.x, offsetPosition.y, transform.position.z);
        }



    }

    private void MoveCameraAngle()
    {
        switch (cameraAngleView)
        {
            case 1:
                offsetPosition = new Vector3(0f, 5.93f, -10.98f);
                transform.rotation = Quaternion.Euler(25f, 0f, 0);
                break;
            case 2:
                offsetPosition = new Vector3(-10.98f, 5.93f, 0f);
                transform.rotation = Quaternion.Euler(25f, 90f, 0);
                break;
            case 3:
                offsetPosition = new Vector3(0f, 5.93f, 10.98f);
                transform.rotation = Quaternion.Euler(25f, 180f, 0);
                break;
            case 4:
                offsetPosition = new Vector3(10.98f, 5.93f, 0f);
                transform.rotation = Quaternion.Euler(25f, -90f, 0);
                break;
        }
    }
}
