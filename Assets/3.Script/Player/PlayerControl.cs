using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControl : MonoBehaviour
{
    private CharacterController playerCTRL;
    private Player_Skill player_skill;
    
    private Vector3 MoveDirection = Vector3.zero;
    public CharacterType characterType;
    public float speed = 10f;


    void Start()
    {
        playerCTRL = GetComponent<CharacterController>();
        player_skill = GetComponent<Player_Skill>();
    }
    void Update()
    {
        InputHandler();
        Skill_Cast();
    }

    private void Skill_Cast()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    public void InputHandler()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, 0, z) * speed * Time.deltaTime;
        playerCTRL.Move(movement);
    }
    private void GRAVITY()
    {
        if (playerCTRL.enabled)
        {
            if (CheckGrounded())
            {
                if (MoveDirection.y < -0.1f)
                {
                    MoveDirection.y = -0.1f;
                }
            }
            MoveDirection.y -= 0.1f;
            playerCTRL.Move(MoveDirection * Time.deltaTime);
        }
    }
    private bool CheckGrounded()
    {
        if (playerCTRL.isGrounded && playerCTRL.enabled)
        {
            return true;
        }
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        float range = 0.2f;
        return Physics.Raycast(ray, range);
    }
}
