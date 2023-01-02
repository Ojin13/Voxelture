
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float MovementSpeed;
    public float InputForce; //0 - 1
    public float InputX;
    public float InputZ;

    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed;
    public float allowPlayerRotation;


    public Animator anim;
    public Camera cam;

    public bool isRolling;
    public bool canJump = true;
    public float jumpForce;
    public float jumpDelayTime;
    public bool canControllJumpDirection = true;
    private bool jumpDelayOnlyOnceAtTime = true;

    public bool canMove = true;
    public bool crounched;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;



    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }


    void FixedUpdate()
    {
        InputMagnitude();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIController.UIControl.DialoguePanelIsActive)
        {
            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && canJump && canMove && !isRolling && anim.GetBool("Grounded") && anim.GetBool("Standing") && !GetComponent<Attack>().isAttacking)
            {
                JumpPls();
            }

            if (!canJump)
            {
                if (anim.GetBool("Grounded") && jumpDelayOnlyOnceAtTime)
                {
                    jumpDelayOnlyOnceAtTime = false;
                    Invoke("jumpDelay", jumpDelayTime);
                }
            }


            //Crounch
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                CrounchPls();
            }
        }
        
        //Roll
        if(Input.GetKeyDown(KeyCode.X) && anim.GetBool("Grounded") && !GetComponent<Attack>().isAttacking && !isRolling)
        {
            RollPls();
        }
    }

    public void startRoll()
    {
        GetComponent<InvisibleController>().setVisible();
        blockRotationPlayer = true;
        canMove = false;
    }

    public void stopRoll()
    {
        isRolling = false;
        canJump = true;
        blockRotationPlayer = false;
        canMove = true;
    }


    public void RollPls()
    {
        isRolling = true;
        crounched = false;
        anim.SetTrigger("Roll");
        anim.SetBool("Crounched", false);
        canJump = false;
    }
    
    
    public void CrounchPls()
    {
        if (anim.GetBool("Grounded"))
        {
            if (crounched)
            {
                crounched = false;
                canJump = true;
                anim.SetBool("Crounched", false);

                GetComponent<InvisibleController>().setVisible();
            }
            else
            {
                if (!GetComponent<Attack>().isAttacking)
                {
                    crounched = true;
                    canJump = false;
                    anim.SetBool("Crounched", true);

                    if (GetComponent<Equipment>().isEquipped(7, "The one ring"))
                    {
                        GetComponent<InvisibleController>().SetInvisible();
                    }

                }
            }
        }
        else
        {
            Debug.Log("yo what up Nigga");
        }
    }

    public void JumpPls(int normalJump = 1)
    {
        if (crounched)
        {
            crounched = false;
            anim.SetBool("Crounched", false);
        }
        else
        {
            var force = jumpForce * (InputForce * 1.1f);

            if (force < jumpForce)
            {
                force = jumpForce;
            }

            GetComponent<Rigidbody>().velocity += new Vector3(0, force, 0);
            canJump = false;

            if (normalJump == 1)
            {
                anim.SetTrigger("Jump");
            }
        }
    }

    public void jumpDelay()
    {
        canJump = true;
        jumpDelayOnlyOnceAtTime = true;
    }


    void PlayerMoveAndRotation()
    {
        if (blockRotationPlayer == false && canMove)
        {
            if(crounched)
            {
                transform.Translate(Vector3.forward.normalized * Time.deltaTime * (InputForce * MovementSpeed/3));
            }
            else
            {
                if(PlayerController.MovementControl.anim.GetBool("WeaponDrawn"))
                {
                    transform.Translate(Vector3.forward.normalized * Time.deltaTime * (InputForce * MovementSpeed/1.25f));
                }
                else
                {
                    transform.Translate(Vector3.forward.normalized * Time.deltaTime * (InputForce * MovementSpeed));
                }
            }

            if (GetComponent<GroundedChecker>().CollisionCount > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed/2);
            }
        }
    }

    void InputMagnitude()
    {
        //Calculate Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        if(anim != null)
        {
            var forward = cam.transform.forward;
            var right = cam.transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            
            if (canControllJumpDirection)
            {
                desiredMoveDirection = forward * InputZ + right * InputX;
            }

            if (anim.GetBool("Grounded"))
            {
                anim.SetFloat("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
                anim.SetFloat("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);
                
                //Calculate the Input Magnitude
                InputForce = new Vector2(InputX, InputZ).sqrMagnitude;

                //run with Left Shift
                if (!Input.GetKey(KeyCode.LeftShift) && !crounched)
                {
                    if (InputForce > 0.5f) { InputForce = 0.5f; }
                }
                else
                {
                    if (GetComponent<PlayerController>().Hunger < 20)
                    {
                        if (InputForce > 0.5f) { InputForce = 0.5f; }
                    }
                }   
            }
        }

        if (UIController.UIControl.DialoguePanelIsActive)
        {
            InputForce = 0;
        }

        //if player press two buttons at the same time
        if (InputForce > 1) { InputForce = 1; }
        
        //Physically move player
        if (InputForce > allowPlayerRotation)
        {
            if(anim != null)
            {
                anim.SetFloat("InputMagnitude", InputForce, StartAnimTime, Time.deltaTime);
                
                if (!UIController.UIControl.DialoguePanelIsActive)
                {
                    PlayerMoveAndRotation();
                }
            }
        }
        else if (InputForce < allowPlayerRotation)
        {
            if (anim != null)
            {
                anim.SetFloat("InputMagnitude", InputForce, StopAnimTime, Time.deltaTime);
            }
        }
    }

    public void setCanMove()
    {
        anim.SetBool("CanMoveLegs",true);
        canMove = true;
    }

    public void setCantMove()
    {
        anim.SetBool("CanMoveLegs", false);
        canMove = false;
    }

    public void decreaseMovementSpeed()
    {
        MovementSpeed /= 2;
        desiredRotationSpeed /= 100;
    }

    public void increaseMovementSpeed()
    {
        MovementSpeed *= 2;
        desiredRotationSpeed *= 100;
    }

    public void SetIsStanding()
    {
        anim.SetBool("Standing", true);
        GetComponent<TakeDamage>().canPlayGetHit();
    }

    public void SetIsNotStanding()
    {
        anim.SetBool("Standing", false);
    }

    public void SetCanControllJumpDirection()
    {
        canControllJumpDirection = true;
    }
    
    public void SetCantControllJumpDirection()
    {
        canControllJumpDirection = false;
    }
    
}
