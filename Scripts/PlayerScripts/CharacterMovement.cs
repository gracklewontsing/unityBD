using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterMovement : MonoBehaviour
{
    private PlayerData playerData;
    public float maxSpeed = 6.0f;
    public float moveDirection;

    public bool facingRight = true;
    
    private Rigidbody rigidbody;

    public float jumpSpeed= 600.0f;
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public float swordSpeed = 600.0f;
    public Transform swordSpawn;
    public Rigidbody swordPrefab;
    public PlayerHealth playerHealth;

    Rigidbody clone;
    public Animator anim;

    void Awake ()
    {
        groundCheck = GameObject.Find("GroundCheck").transform;
        swordSpawn = GameObject.Find("SwordSpawn").transform;
        playerHealth = GetComponent<PlayerHealth>();
        playerData = GetComponent<PlayerData>();
    }
    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection =  Input.GetAxis("Horizontal");

        if ((grounded && CrossPlatformInputManager.GetButtonDown("Jump"))||(grounded && Input.GetButtonDown ("Jump")))
        {
            anim.SetTrigger("isJumping");
            rigidbody.AddForce (new Vector2 (0, jumpSpeed));
            playerHealth.JumpAudio();
            playerData.jumpUse();
        }
        if ((CrossPlatformInputManager.GetButtonDown("Fire1"))||(Input.GetButtonDown ("Fire1")))
        {
            Attack();
            playerHealth.FireAudio();
        }
    }
    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        rigidbody.velocity = new Vector2(moveDirection * maxSpeed, rigidbody.velocity.y);
        if (moveDirection > 0.0f && !facingRight)
        {
            Flip();
        }
        else if (moveDirection < 0.0f && facingRight)
        {
            Flip();
        }
        anim.SetFloat ("Speed", Mathf.Abs (moveDirection));
    }
    
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

    void Attack()
    {
        clone = Instantiate (swordPrefab, swordSpawn.position, swordSpawn.rotation) as Rigidbody;
        clone.AddForce (swordSpawn.transform.right * swordSpeed);
        playerData.ammoUse(); //function to access or modify database test
    }
    
}
