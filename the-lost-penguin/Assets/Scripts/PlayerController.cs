using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    public float moveSpeed, jumpForce, gravityScale, moveVel;
    public CharacterController charCon;
    

    private CameraController cam;
    private Vector3 moveAmount;
    private float yStore;
    
    public float rotateSpeed = 10f;

    public GameObject jumpParticle, landingParticle;
    private bool lastGrounded;

    public Animator amin;

    public bool playerKnockedOut;

    public void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CameraController>();
        charCon.Move(new Vector3(0f, Physics.gravity.y * gravityScale * Time.deltaTime, 0f));
        playerKnockedOut = false;
    }

    private void FixedUpdate()
    {
        if (!charCon.isGrounded)
        {
            moveAmount.y += + (Physics.gravity.y * gravityScale * Time.fixedDeltaTime);    
        }
        else
        {
            moveAmount.y = Physics.gravity.y * gravityScale * Time.deltaTime;
        }
        
    }

    public void updateKnockedOutStatus(bool status)
    {
        playerKnockedOut = status;
    }
    

    // Update is called once per frame
    void Update()
    {
        
        // Check if player is knocked out, if not then allow key input to work
        if (!playerKnockedOut)
        {
            yStore = moveAmount.y;

            moveAmount = (cam.transform.forward * Input.GetAxisRaw("Vertical")) + 
                         (cam.transform.right * Input.GetAxisRaw("Horizontal"));
            moveAmount.y = 0f;
            moveAmount = moveAmount.normalized;

            if (moveAmount.magnitude > .1f)
            {
                if (moveAmount != Vector3.zero)
                {
                    Quaternion newRot = Quaternion.LookRotation(moveAmount);
                    transform.rotation = Quaternion.Slerp(transform.rotation, newRot, rotateSpeed * Time.deltaTime);
                }
            }
            
            moveAmount.y = yStore;

            // for actiosn: 
            if (charCon.isGrounded)
            {
                jumpParticle.SetActive(false);

                if (!lastGrounded)
                {
                    landingParticle.SetActive(true);    
                }
            
            
                if (Input.GetButtonDown("Jump"))
                {
                    moveAmount.y = jumpForce;
                    jumpParticle.SetActive(true);
                }
            }
        
            lastGrounded = charCon.isGrounded;
        
            charCon.Move(new Vector3(moveAmount.x * moveSpeed, moveAmount.y, moveAmount.z * moveSpeed) * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (PlayerStats.instance.pebbleCount > 0 && !PlayerHealth.instance.isTakingDamage)
                {
                    amin.SetTrigger("shoot");
                    // FIRE Pebble
                    PlayerShot.instance.FirePebble();
                    PlayerStats.instance.PebbleThrown();
                }

            }
        
            // get speed for animation:
            float moveVel = new Vector3(moveAmount.x, 0f, moveAmount.z).magnitude * moveSpeed;
            amin.SetFloat("speed", moveVel);
            amin.SetBool("isGrounded", charCon.isGrounded);
            amin.SetFloat("yVel", moveAmount.y);
        }
       

    }
}
