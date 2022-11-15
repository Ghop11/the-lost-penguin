using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    private bool isWalking;
    
    public void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusicWithVolume(6, 0.05f);
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

                if (!isWalking && charCon.isGrounded)
                {           
                    print("walking sound");
                    AudioManager.instance.PlaySFXWithPitch(14, 0.03f, 1.55f);
                    isWalking = true;
                }
                else if (!charCon.isGrounded)
                {
                    print("stop the walking sound");
                    AudioManager.instance.StopThisSFX(14);
                    isWalking = false;
                }
                if (moveAmount != Vector3.zero)
                {
                    Quaternion newRot = Quaternion.LookRotation(moveAmount);
                    transform.rotation = Quaternion.Slerp(transform.rotation, newRot, rotateSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (isWalking)
                {
                    print("stop the walking sound");
                    AudioManager.instance.StopThisSFX(14);
                    isWalking = false;
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
                    AudioManager.instance.PlaySFX(6, 0.1f);
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
