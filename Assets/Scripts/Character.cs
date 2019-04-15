using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private const int acceleration = 23;
    CapsuleCollider capsuleCollider;
    public LayerMask layerMask;
    private const float skinWidth = 0.003f;
    private const float gravityConstant = 8f;
    private const float groundCheckDistance = 0.3f;
    GeneralFunctions generalFunctions;
    private Vector3 velocity;
    private const int maxSpeed = 10;
    private const float deceleration = 5;
    private Vector2 airResistance;
    private const float staticFriction = 0.55f;
    [SerializeField] private float dynamicFriction = 0.35f;
    private const float airCoeff = 0.4f;
    private Vector3 normal;
    private const float jumpHeight = 7;

    private void Awake()
    {
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
        generalFunctions = gameObject.GetComponent<GeneralFunctions>();
    }
    void Update()
    {
        #region Input
        Vector3 input = GetDirectionInput();

        if (input.magnitude <= 0)
        {
            Decelerate();
        }
        else
        {
            Accelerate(input);
        }
        #endregion
        #region Gravity
        Vector3 gravity = Vector3.down * gravityConstant * Time.deltaTime;
        velocity += gravity;
        #endregion
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            velocity += new Vector3(0, jumpHeight, 0);

        CollisionCheck();
        /*
         airResistance = velocity * airCoeff;
         //velocity *= Mathf.Pow(airResistance, Time.deltaTime);
         */
        transform.position += velocity * Time.deltaTime;
    }

    void Friction(float normalMag)
    {
        if (velocity.magnitude < (staticFriction * normalMag))
        {
            velocity = new Vector3(0, 0, 0);
        }
        else
        {
            velocity += (dynamicFriction * normalMag) * -velocity.normalized;
        }
    }
    void Accelerate(Vector3 direction)
    {

        if (direction.magnitude > 1)
        {

            direction = direction.normalized;
        }
        velocity += direction * acceleration * Time.deltaTime;

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

    }
    void Decelerate()
    {
        Vector3 tempVel = new Vector3(velocity.x, 0, velocity.z);
        velocity -= tempVel.normalized * deceleration * Time.deltaTime;
    }



    Vector3 GetDirectionInput()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        // Move in camera's direction
        input = Camera.main.transform.rotation * input;
        input = Vector3.ProjectOnPlane(input, GroundCast().normal).normalized;
        return input;


    }

    RaycastHit GroundCast()
    {
        RaycastHit raycastHit;
        bool capsuleCast = Physics.CapsuleCast(transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius), transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius),
            capsuleCollider.radius, Vector3.down, out raycastHit, groundCheckDistance + skinWidth, layerMask);
        
        return raycastHit;
    }

    bool IsGrounded()
    {

        return GroundCast().distance <= groundCheckDistance;
    }
    void CollisionCheck()
    {

        RaycastHit raycastHit;
        bool capsulecast = Physics.CapsuleCast(transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius), transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius),
            capsuleCollider.radius, velocity, out raycastHit, velocity.magnitude * Time.deltaTime + skinWidth, layerMask);


        if (raycastHit.collider == null)
            return;
        else
        {
            #region Apply Normal Force
            normal = generalFunctions.Normal3D(velocity, raycastHit.normal);
            velocity += normal;
            #endregion
            Friction(normal.magnitude);

            // Flytta närmre kollidern
            // CollisionNormal(raycastHit2D);

            // Exit Recursion
            if (velocity.magnitude < skinWidth)
            {
                velocity = Vector3.zero;
                return;
            }

            CollisionCheck();
        }
    }

}