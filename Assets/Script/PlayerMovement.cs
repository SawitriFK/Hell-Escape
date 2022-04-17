using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public PlayerBattle fight;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float lastDashed = -2f;
    public float dashCooldown = 3f;
    bool jump = false;
    bool dash = false;
    public bool invinsible = false;
    public float dashInvinsibleMultplier = 0.7f;
    [SerializeField]private float cooldown;
    private bool attack;
    private Animator anim;

    public static Button buttonRun;

    private bool moveLeft, moveRight;

    void Awake()
    {
        GameObject ButtonLeft = GameObject.Find("UICanvas/ButtonRight/Run");
        buttonRun = ButtonLeft.GetComponentInChildren<Button>();
        cooldown = dashCooldown;
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(moveLeft){
            horizontalMove = -1 * runSpeed;
        }
        if(moveRight){
            horizontalMove = 1 * runSpeed;
        }
        // if(clickAttack){
        //     attack = true;
        // }
        
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) 
        {
            jump = true;
        }
        // Ganti Jum FOrece = 470
        // if(Input.GetButtonDown("Jump")) 
        // {
        //     jump = true;
        // }

        // if(Input.GetButtonDown("Fire2") && cooldown <= 0)
        // {
        //     lastDashed = Time.time;
        //     dash = true;
        //     cooldown = dashCooldown;
        //     StartCoroutine(Invinsible());
        // }

        //  if (Input.GetButtonDown("Fire1"))
        // {
        //     attack = true;
        // }

        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if(attack && controller.isGrounded())
        {
            controller.Move(0f, false, false);
            fight.Attack();
            
        }else
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
        }
        
        attack = false;
        jump = false;
        dash = false;
        
    }

    IEnumerator Invinsible()
    {
        invinsible = true;
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(dashCooldown * dashInvinsibleMultplier);
        invinsible = false;
        buttonRun.interactable = true;
    }

    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    {
        switch(e.skillType)
        {
            case PlayerSkills.SkillType.MoveSpeed :
                runSpeed *= 1.5f;
                break;
        }
    }

    public void Jump(){
        jump = true;
    }
    public void Run(){
        if(cooldown <= 0.5){ // setelah menunggu cooldown, respose agak lama untuk di klik lagi (Awalnya cooldown <= 0)
            lastDashed = Time.time;
            dash = true;
            cooldown = dashCooldown;
            StartCoroutine(Invinsible());
        }
    }

    public void ClickAttack(){
        attack = true;
    }
    // public void StopAttack(){
        
    //     attack = false;
    // }

    public void WalkRight(){
        moveRight = true;
    }
    public void WalkLeft(){
        moveLeft = true;
    }
    public void StopMove(){
        moveLeft = false;
        moveRight = false;
    }

}
