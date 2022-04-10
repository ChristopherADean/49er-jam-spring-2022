using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PMovement : MonoBehaviour
{
    //Private Variables
    private Vector2 m_Look;
    private GameObject cam;
    private Rigidbody rb;
    private GameObject playerCollider;

    //Misc vars
    [SerializeField] private LayerMask groundLayers;

    //MouseLook Variables
    public float mSens = 1.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float targetCamTilt = 0.0f;
    public float camTiltVel = 1.0f;

    //Movement Variables
    public float moveSpeed = 200f;
    public float airMoveSpeed = 200f;
    public float crouchSpeed = 4f;
    public float crouchBoost = 10f;
    public float crouchBoostCooldown = 2f;
    private float nextBoost = 0f;
    private bool cBoostNow = false;
    public float sprintSpeed = 8f;
    public float maxMoveSpeed = 5f;
    public float maxAirSpeed = 0.6f;
    public float maxCrouchSpeed = 7f;
    public float maxSprintSpeed = 16f;
    public float friction = 8f;
    public float crouchingFriction = 4f;
    public float standingFriction = 8f;  //Friction to apply if no inputs are detected while on ground
    public float jumpForce = 5f;
    private Vector2 moveInputVector;
    private Vector3 mInput;
    private bool queueJump = false;
    private bool sprinting = false;
    [SerializeField] private bool sprintingEnabled = false;  //Basically whether sprinting is a mechanic in the game or not

    //Crouching Variables
    public AnimationCurve crouchCurve = new AnimationCurve();
    public float crouchTime = 0.3f;
    private float crouchingEndTime = 0.0f;
    private float unCrouchingEndTime = 0.0f;
    private bool crouching = false;

    private float speedMult = 1f;


    // Start is called before the first frame update
    void Start()
    {


        playerCollider = transform.Find("PlayerCollider").gameObject;
        rb = transform.GetComponent<Rigidbody>();
        cam = transform.Find("Main Camera").gameObject;

        //lock and hide cursor when player spawns
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

 
        yaw += mSens * m_Look.x;
        pitch -= mSens * m_Look.y;

        cam.transform.rotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(Mathf.Clamp(pitch, -90, 90), yaw, cam.transform.eulerAngles.z)), Quaternion.Euler(new Vector3(Mathf.Clamp(pitch, -90, 90), yaw, targetCamTilt)), camTiltVel);
        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);

        //Normalized movement input vector
        mInput = Vector3.Normalize(transform.right * moveInputVector.x + transform.forward * moveInputVector.y);
    }

    private void FixedUpdate()
    {

        //Reduce the player's speed if they are on the ground by friction and add movement if they are moving.
        Vector3 pVelocity = rb.velocity;
        pVelocity = CalcFriction(pVelocity) + CalcVelocity(mInput);
        
            rb.velocity = pVelocity;
        //Add velocity for crouch boosts
        if (cBoostNow)
        {
            rb.AddForce(transform.forward * crouchBoost);
            cBoostNow = false;
        }
        

        //Add jump velocity
        if (queueJump)
        {
            rb.velocity = rb.velocity + new Vector3(0f, jumpForce, 0f);
            queueJump = false;
        }

        //Code for crouching
        if(crouching && Time.time < crouchingEndTime)
        {
            //Based on the length of time from starting the crouch change the scale of the player capsule.
            playerCollider.transform.localScale = new Vector3(1f, crouchCurve.Evaluate((crouchingEndTime - Time.time) / crouchTime), 1f);
        }else if(crouching && Time.time >= crouchingEndTime)
        {
            playerCollider.transform.localScale = new Vector3(1f, 0.4f, 1f);
        }

        //Don't uncrouch if there is an object above
        if(!crouching && Time.time < unCrouchingEndTime && !CheckAbove())
        {
            //Based on the length of time from starting the crouch change the scale of the player capsule.
            playerCollider.transform.localScale = new Vector3(1f, crouchCurve.Evaluate(1-((unCrouchingEndTime - Time.time) / crouchTime)), 1f);
        }

        //If there is an object set the player's size to the minimum and reset the unCrouch countdown
        if (!crouching && Time.time < unCrouchingEndTime && CheckAbove())
        {
            
            unCrouchingEndTime = Time.time + crouchTime;
            playerCollider.transform.localScale = new Vector3(1f, crouchCurve.Evaluate(0f), 1f);
        }

    }

    private Vector3 CalcVelocity(Vector3 input)
    {
        
        //Set acceleration value based on if in air or not
        float curAccel = moveSpeed;
        bool grounded = CheckGround();
        if (grounded && sprinting && sprintingEnabled)
            curAccel = sprintSpeed;

        if (grounded && crouching)
            curAccel = crouchSpeed;

        if (!grounded)
            curAccel = airMoveSpeed;

        //Set maxSpeed
        float mSpeed = maxMoveSpeed * speedMult;
        if (grounded && sprinting && sprintingEnabled)
            mSpeed = maxSprintSpeed;

        if (grounded && crouching)
            mSpeed = maxCrouchSpeed;

        if (!grounded)
            mSpeed = maxAirSpeed;

        Vector3 inputVelocity = input * curAccel;

        //Being honest I don't understand this and found it online

        //Get current velocity
        Vector3 currentVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //How close the current speed to max velocity is (1 = not moving, 0 = at/over max speed)
        float max = Mathf.Max(0f, 1 - (currentVelocity.magnitude / mSpeed));

        //How perpendicular the input to the current velocity is (0 = 90°)
        float velocityDot = Vector3.Dot(currentVelocity, inputVelocity);

        //Scale the input to the max speed
        Vector3 modifiedVelocity = inputVelocity * max;

        //The more perpendicular the input is, the more the input velocity will be applied
        Vector3 correctVelocity = Vector3.Lerp(inputVelocity, modifiedVelocity, velocityDot);


        //Return
        return correctVelocity;

    }

    private Vector3 CalcFriction(Vector3 currentVelocity)
    {
        if (CheckGround())
        {
            //reduce friction if sliding
             float realFrict = friction;
             if (crouching)
                realFrict = crouchingFriction;

             float speedToReduceBy = currentVelocity.magnitude * realFrict * Time.deltaTime;
            if(currentVelocity.magnitude != 0f)
            {
                return currentVelocity * (Mathf.Max(currentVelocity.magnitude - speedToReduceBy, 0f) / currentVelocity.magnitude);
            }
            else
            {
                return currentVelocity;
            }
             
        }
        else
        {
            return currentVelocity;
        }
    }
    
    private bool CheckGround()
    {

        RaycastHit hit;
        if(Physics.SphereCast(transform.position, 0.45f, -transform.up, out hit, 1f, groundLayers))
        {
            
            return true;
        }
        return false;
    }

    private bool CheckAbove()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.45f, transform.up, out hit, 0.8f, groundLayers))
        {
            return true;
        }

        return false;
    }

    public void OnMovement(InputValue i)
    {
        Vector2 input = i.Get<Vector2>();
        moveInputVector = input;
    }

    public void OnMouseLook(InputValue i)
    {
        Vector2 input = i.Get<Vector2>();
        m_Look = input;
    }

    public void CrouchInput(float f)
    {
        if(f == 1f)
        {
            //button is pressed
            crouching = true;
            crouchingEndTime = Time.time + crouchTime;

            //See if a boost can be applied
            if (Time.time > nextBoost)
            {
                cBoostNow = true;
                nextBoost = Time.time + crouchBoostCooldown;
            }
        }
        else
        {
            crouching = false;
            unCrouchingEndTime = Time.time + crouchTime;
        }

        
    }

    public void OnJump()
    {

        if (CheckGround() && queueJump != true)
        {
            queueJump = true;
        }
    }

    public void SprintInput(float f)
    {
        if(f == 1f)
            sprinting = true;
        else
            sprinting = false;
    }

    public void SpeedMultEvent(float f)
    {
        speedMult = speedMult * f;
    }
}
