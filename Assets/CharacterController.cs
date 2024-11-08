using System.Collections;
using System.Collections.Generic;
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
    public float AdditionalGravity;
    public float CrouchMultplier;
    public float RunMultiplier;
    public bool IsRunning, IsCrouched;
    private float Multplier;
    [Space(10)]
    public bool IsGrounded;
    public string[] GroundTags;
    [Header("Etrafa bakınma")]
    [Space(10)]
    public float Sensitivity;
    public Camera Cam;
    public GameObject Player;

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
    }
    public void Move()
    {
        rb.angularDrag = float.PositiveInfinity;

        var z = Input.GetAxis("Horizontal");
        var x = Input.GetAxis("Vertical");
        var y = Input.GetAxis("Jump");

        Vector3 moveDirection = transform.TransformDirection(new Vector3(z, 0, x));
        Vector3 jumpDirection = transform.TransformDirection(new Vector3(0, y, 0));

        if (!IsRunning && !IsCrouched)
        {
            Multplier = 1;
        }
        else if (IsRunning)
        {
            Multplier = RunMultiplier;
        }
        else if (IsCrouched)
        {
            Multplier = CrouchMultplier;
        }

        // Apply movement velocity
        transform.position += ((moveDirection * MoveSpeed * Time.fixedDeltaTime) * Multplier);
        if (IsGrounded)
        {
          rb.velocity = jumpDirection * JumpPower * Time.fixedDeltaTime;
        }

        rb.velocity = new Vector3(0, rb.velocity.y - AdditionalGravity * Time.fixedDeltaTime, 0);
    }
    public void Look()
    {
        var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");

        Cam.transform.eulerAngles += new Vector3(-y * Sensitivity, 0, 0);
        Player.transform.eulerAngles += new Vector3(0, x * Sensitivity, 0);

        Mathf.Clamp(Cam.transform.eulerAngles.x, -90, 90);
    }
    public void CrouchAndRun()
    {
        if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
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

    public void OnCollisionEnter(Collision other)
    {
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
