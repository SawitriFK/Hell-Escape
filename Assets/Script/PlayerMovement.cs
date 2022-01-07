using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public PlayerBattle fight;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float lastDashed = -2f;
    public float dashCooldown = .6f;
    bool jump = false;
    bool dash = false;
    public bool invinsible = false;
    public float dashInvinsibleMultplier = 0.7f;
    [SerializeField]private float cooldown;
    private bool attack;
    private Animator anim;

    void Awake()
    {
        cooldown = dashCooldown;
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
        if(Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            jump = true;
        }

        if(Input.GetKeyDown(KeyCode.Space) && cooldown <= 0)
        {
            lastDashed = Time.time;
            dash = true;
            cooldown = dashCooldown;
            StartCoroutine(Invinsible());
        }

         if (Input.GetKeyDown(KeyCode.Z))
        {
            attack = true;
        }

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

}
