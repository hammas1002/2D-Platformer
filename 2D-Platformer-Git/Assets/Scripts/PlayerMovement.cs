using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController2D characterController;
    private Animator playerAnimator;

    


    //................
    [Header("Fields")]
    float horizontal=0;
    [SerializeField]
    float moveSpeed = 40f;
    bool jump = true;
    bool crouch = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        characterController = FindObjectOfType<CharacterController2D>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal")*moveSpeed;
        ControlAnimations();
        GetInputs();
        
    }

    private void FixedUpdate()
    {
        characterController.Move(horizontal*Time.deltaTime, crouch, jump); 
        jump = false;
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
