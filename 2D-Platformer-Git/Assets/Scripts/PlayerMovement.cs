using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController2D characterController;
    private PlayerBash playerBash;
    private Animator playerAnimator;
    Rigidbody2D rb;
    //................
    [Header("Fields")]
    float horizontal=0;
    [SerializeField]
    float moveSpeed = 40f;
    bool jump = true;
    bool crouch = false;
    bool isBashing = false;


    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();
        playerBash = GetComponent<PlayerBash>();
    }

    // Update is called once per frame
    void Update()
    {
        
        horizontal = Input.GetAxis("Horizontal")*moveSpeed;
        print("Horizontal" + horizontal);
        ControlAnimations();
        GetInputs();
        playerBash.Bash();
        isBashing = playerBash.IsBashing;
        
        
    }

    private void FixedUpdate()
    {
        
        if (!isBashing)
        {
            characterController.Move(horizontal == 0 ? rb.velocity.x : horizontal * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
        

    }

    private void GetInputs()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            playerAnimator.SetBool("isJumping", true);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            crouch = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            crouch = false;
        }
    }
    private void ControlAnimations()
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));
          
    }

    public void OnCrouch(bool isCrouching)
    {
        playerAnimator.SetBool("isCrouching", isCrouching);
    }

    public void OnLand()
    {
        playerAnimator.SetBool("isJumping", false);
    }

}
