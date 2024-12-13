using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
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
    private float Multplier;
    [Space(10)]
    public bool IsGrounded;
    public string[] GroundTags;
    [Space(10)]
    [Header("Etrafa bakınma")]
    public float Sensitivity;
    public Camera Cam;
    public GameObject Player;
    [Space(10)]
    [Header("Animatör")]
    public Animator AlexAnimator;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
        CrouchAndRun();
        Jump();
    }
    public void Move()
    {
        rb.angularDamping = float.PositiveInfinity;

        var z = Input.GetAxis("Horizontal");
        var x = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.TransformDirection(new Vector3(z, 0, x));

        if (!IsRunning && !IsCrouched)
        {
            Multplier = 1;
            if (x != 0 || z != 0)
            {
                AlexAnimator.SetBool("IsWalking", true);
                AlexAnimator.SetBool("IsRunning", false);
            }
            else if (x == 0 && z == 0)
            {
                AlexAnimator.SetBool("IsWalking", false);
                AlexAnimator.SetBool("IsRunning", false);
            }

        }
        else if (IsRunning)
        {
            Multplier = RunMultiplier;
            if (x != 0 || z != 0)
            {
                AlexAnimator.SetBool("IsWalking", false);
                AlexAnimator.SetBool("IsRunning", true);
            }
            else if (x == 0 && z == 0)
            {
                AlexAnimator.SetBool("IsWalking", false);
                AlexAnimator.SetBool("IsRunning", false);
            }
        }
        else if (IsCrouched)
        {
            Multplier = CrouchMultplier;
            if (x != 0 || z != 0)
            {
                AlexAnimator.SetBool("IsWalking", false);
                AlexAnimator.SetBool("IsAttacking", false);
                AlexAnimator.SetBool("IsJumping", false);
                AlexAnimator.SetBool("IsCrouching", true);
            }
            else if (x == 0 && z == 0)
            {
                AlexAnimator.SetBool("IsAttacking", false);
                AlexAnimator.SetBool("IsJumping", false);
                AlexAnimator.SetBool("IsCrouching", false);
            }
        }

        // Apply movement velocity
        transform.position += ((moveDirection * MoveSpeed * Time.fixedDeltaTime) * Multplier);
    }
    public void Look()
    {
        var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");

        //Cam.transform.eulerAngles += new Vector3(-y * Sensitivity, 0, 0);
        Player.transform.eulerAngles += new Vector3(0, x * Sensitivity, 0);

        //Mathf.Clamp(Cam.transform.eulerAngles.x, -90, 90);
    }
    public void CrouchAndRun()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            IsCrouched = !IsCrouched;
        }
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
    public void Jump()
    {
        if (IsGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                AlexAnimator.SetBool("IsJumping", true);
                AlexAnimator.SetBool("IsWalking", false);
                AlexAnimator.SetBool("IsAttacking", false);
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
            rb.linearVelocity = new Vector3(0, JumpPower * Time.fixedDeltaTime, 0);
        }
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y - AdditionalGravity * Time.fixedDeltaTime, 0);
    }

    public void SetAnimator(string paramname, bool value)
    {
        AlexAnimator.SetBool(paramname, value);
    }

    public void OnCollisionEnter(Collision other)
    {
        AlexAnimator.SetBool("IsJumping", false);
        foreach (var tag in GroundTags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                IsGrounded = true;
                Multplier = 1;
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
                Multplier = MoveSpeedInAir;
            }
        }
    }
}
