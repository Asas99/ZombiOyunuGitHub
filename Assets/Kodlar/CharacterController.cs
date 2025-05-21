using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour, IMovement101, IMovement201
{
    [Header("Hareket ve zıplama")]
    public Rigidbody rb;
    public float MoveSpeed;
    public float MoveSpeedInAir;
    public float JumpPower;
    public float WaitForJumpTime,JumpTime;
    [SerializeField]
    private bool JumpTriggered;
    public float AdditionalGravity;
    public float CrouchMultplier;
    public float RunMultiplier;
    public bool IsRunning, IsCrouched;
    private float Multiplier;
    [Space(10)]
    public bool IsGrounded;
    public string[] GroundTags;
    [Space(10)]
    [Header("Etrafa bakınma")]
    public float Sensitivity;
    public Camera Cam;
    public GameObject Player;
    public Animator AlexAnimator;
    private Shooting shootingScript;
    [SerializeField]
    private WeaponEquipManager weaponEquipManager;
    [SerializeField]
    private bool HasAKnife;
    public GameObject Knife;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        shootingScript = FindAnyObjectByType<Shooting>();
        weaponEquipManager = FindAnyObjectByType<WeaponEquipManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(MoveSpeed);
        Jump(JumpPower);
        Run();
        Crouch();
        ADSPosAndShoot();
        GetKnifeInOut();
        AttackWithKnife();
    }

    private void AttackWithKnife()
    {
        if (HasAKnife)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AnimatorManager.SetAllAnimatorBools(AlexAnimator, "IsStabbing","Has a knife");
            }
        }
    }

    private void GetKnifeInOut()
    {
        Knife.SetActive(HasAKnife);
        if(Input.GetMouseButtonDown(1))
        {
            HasAKnife = !HasAKnife;
            AlexAnimator.SetBool("Has a knife", HasAKnife);
        }
    }

    public void Move(float speed)
    {
        rb.angularDamping = float.PositiveInfinity;

        var z = Input.GetAxis("Horizontal");
        var x = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.TransformDirection(new Vector3(z, 0, x));

        if (!IsRunning && !IsCrouched)
        {
            Multiplier = 1;
            if (x != 0 || z != 0)
            {
                AnimatorManager.SetAllAnimatorBools(AlexAnimator,"IsWalking","Has a knife");
                //AlexAnimator.SetBool("IsWalking", true);
                //AlexAnimator.SetBool("IsRunning", false);
                //AlexAnimator.SetBool("Has a pistol", false);
                //AlexAnimator.SetBool("Shoot", false);
            }
            else if (x == 0 && z == 0)
            {
                if (!HasAKnife)
                {
                    AnimatorManager.SetAllAnimatorBools(AlexAnimator);
                }
                else if(HasAKnife)
                {
                    AnimatorManager.SetAllAnimatorBools(AlexAnimator,"Has a knife");
                }
                //AlexAnimator.SetBool("IsWalking", false);
                //AlexAnimator.SetBool("IsRunning", false);
                //AlexAnimator.SetBool("Has a pistol", false);
                //AlexAnimator.SetBool("Shoot", false);
            }

        }
        else if (IsRunning)
        {
            Multiplier = RunMultiplier;
            if (x != 0 || z != 0)
            {
                AnimatorManager.SetAllAnimatorBools(AlexAnimator, "IsRunning");
                //AlexAnimator.SetBool("IsWalking", false);
                //AlexAnimator.SetBool("IsRunning", true);
                //AlexAnimator.SetBool("Has a pistol", false);
                //AlexAnimator.SetBool("Shoot", false);
            }
            else if (x == 0 && z == 0)
            {
                AnimatorManager.SetAllAnimatorBools(AlexAnimator);
                //AlexAnimator.SetBool("IsWalking", false);
                //AlexAnimator.SetBool("IsRunning", false);
                //AlexAnimator.SetBool("Has a pistol", false);
                //AlexAnimator.SetBool("Shoot", false);
            }
        }
        else if (IsCrouched)
        {
            Multiplier = CrouchMultplier;
            if (x != 0 || z != 0)
            {
                AnimatorManager.SetAllAnimatorBools(AlexAnimator,"Crawl");
                //AlexAnimator.SetBool("IsWalking", false);
                //AlexAnimator.SetBool("IsAttacking", false);
                //AlexAnimator.SetBool("IsJumping", false);
                //AlexAnimator.SetBool("Crawl", true);
                //AlexAnimator.SetBool("Has a pistol", false);
                //AlexAnimator.SetBool("Shoot", false);
            }
            else if (x == 0 && z == 0)
            {
                AnimatorManager.SetAllAnimatorBools(AlexAnimator);
                //AlexAnimator.SetBool("IsAttacking", false);
                //AlexAnimator.SetBool("IsJumping", false);
                //AlexAnimator.SetBool("Crawl", false);
                //AlexAnimator.SetBool("Has a pistol", false);
                //AlexAnimator.SetBool("Shoot", false);
            }
        }

        // Apply movement velocity
        transform.position += ((speed * Time.fixedDeltaTime * moveDirection) * Multiplier);
    }
    public void Jump(float force)
    {
        if (IsGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                AnimatorManager.SetAllAnimatorBools(AlexAnimator, "IsJumping");
                //AlexAnimator.SetBool("IsJumping", true);
                //AlexAnimator.SetBool("IsWalking", false);
                //AlexAnimator.SetBool("IsAttacking", false);
                //AlexAnimator.SetBool("Has a pistol", false);
                JumpTriggered = true;
                JumpTime = WaitForJumpTime;
            }
        }
        if (JumpTriggered)
        {
            JumpTime -= Time.deltaTime;
        }
        if (JumpTime <= 0)
        {
            JumpTriggered = false;
            JumpTime = WaitForJumpTime;
            rb.linearVelocity = new Vector3(0, force * Time.fixedDeltaTime, 0);
        }
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y - AdditionalGravity * Time.fixedDeltaTime, 0);
    }
    public void Crouch()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            IsCrouched = !IsCrouched;
        }
    }
    public void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            IsRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            IsRunning = false;
        }
        if (IsRunning)
        {
            IsCrouched = false;
        }
    }

    //Nişan alacak
    public void ADSPosAndShoot()
    {
        if(gameObject.GetComponent<PlayerInventory>() != null)
        {
            if(weaponEquipManager.Name != null)
            {
                //print(weaponEquipManager.Name);
                shootingScript.CanShoot = true;
                if (Input.GetMouseButton(1))
                {
                    if (weaponEquipManager.Name == "colt" || weaponEquipManager.Name == "revolver")
                    {
                        print("revolver var,nişan için hazır");
                        if (!AlexAnimator.GetBool("IsWalking"))
                        {
                            AnimatorManager.SetAllAnimatorBools(AlexAnimator, "Has a pistol");
                        }
                        if (AlexAnimator.GetBool("IsWalking"))
                        {
                            AnimatorManager.SetAllAnimatorBools(AlexAnimator, "Has a pistol","IsWalking");
                        }
                    }
                    else if (weaponEquipManager.Name == "ak47" || weaponEquipManager.Name == "Krag-Jergensen" || weaponEquipManager.Name == "remington" || weaponEquipManager.Name == "springfield" || weaponEquipManager.Name == "winchester1897" || weaponEquipManager.Name == "winchester1894")
                    {
                        if (!AlexAnimator.GetBool("IsWalking"))
                        {
                            AnimatorManager.SetAllAnimatorBools(AlexAnimator, "Has a rifle");
                        }
                        if (AlexAnimator.GetBool("IsWalking"))
                        {
                            AnimatorManager.SetAllAnimatorBools(AlexAnimator, "Has a rifle", "IsWalking");
                        }
                    }
                    //AlexAnimator.SetBool("Has a pistol", true);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    if (AlexAnimator.GetBool("Has a pistol"))
                    {
                    AnimatorManager.SetAllAnimatorBools(AlexAnimator, "Shoot","Has a pistol");
                    }
                    if (AlexAnimator.GetBool("Has a rifle"))
                    {
                        AnimatorManager.SetAllAnimatorBools(AlexAnimator, "Shoot", "Has a rifle");
                    }

                }
            }
            else if (gameObject.GetComponent<PlayerInventory>().ItemInfos[0].Quantity == 0)
            {
                shootingScript.CanShoot = false;
            }
        }

    }

    public void OnCollisionEnter(Collision other)
    {
        AlexAnimator.SetBool("IsJumping", false);
        AlexAnimator.SetBool("Shoot", false);
        foreach (var tag in GroundTags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                IsGrounded = true;
                Multiplier = 1;
            }
        }
    }

    public void OnCollisionExit(Collision other)
    {
        foreach (var tag in GroundTags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                IsGrounded = false;
                Multiplier = MoveSpeedInAir;
            }
        }
    }
}

