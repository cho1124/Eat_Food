using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float cooltime = 10f;
    public float speed = 10f;
    public float turnSpeed = 720f; // 회전 속도

    public CharacterController playerCTRL;
    public CharacterType characterType;



    private bool isSkillReady = true;
    private Vector3 MoveDirection = Vector3.zero;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerCTRL = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
        //GRAVITY();
        //transform.position.Set(transform.position.x, 1f, transform.position.z);
    }

    public void InputHandler()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(x, 0, z);
        Vector3 movement = inputDirection.normalized * speed * Time.deltaTime;

        if (inputDirection.magnitude > 0.1f)
        {
            // 입력 방향으로 캐릭터 회전
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = targetRotation;
        }



        playerCTRL.Move(movement);
        //transform.Translate(movement, Space.Self);
        characterSkill();
    }

    private void characterSkill()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //    switch (characterType)
            //    {
            //        case CharacterType.a:
            //            Debug.Log("a skilled");
            //            break;
            //        case CharacterType.b:

            //StartCoroutine(MakeBigger_co());
            StartCoroutine(Repair_Floor_Co());
            
            //            Debug.Log("b skilled");
            //            break;
            //        case CharacterType.c:
            //            Debug.Log("c skilled");
            //            break;
            //
            //    }
        }
    }

    private IEnumerator Repair_Floor_Co()
    {
        if (isSkillReady)
        {
            isSkillReady = false;
            Debug.Log("스킬 사용");
            float repair_dinstance = 5f;
            int repair_radius = 5;

            Map_Generator map_generator = GameObject.Find("Map_Generator").GetComponent<Map_Generator>();
            Vector2 vector = map_generator.Position_To_Index(transform.position + transform.forward * repair_dinstance);
            map_generator.Repair_Floor(vector, repair_radius);
        }

        StartCoroutine(Cooltimer_co());
        yield return null;
    }

    private IEnumerator MakeBigger_co()
    {
        if (isSkillReady)
        {
            isSkillReady = false;
            Debug.Log("스킬 사용");
            float increase = 0.1f;

            while (gameObject.GetComponent<Transform>().localScale.x < 14f)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + increase
                                                                , gameObject.transform.localScale.y + increase
                                                                , gameObject.transform.localScale.z + increase);
                //크기가 바뀌는 속도
                yield return new WaitForSeconds(0.05f);
            }
            //yield return new WaitForSeconds(10f);
            yield return new WaitForSeconds(5f);

            while (gameObject.GetComponent<Transform>().localScale.x > 4.1f)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - increase
                                                                , gameObject.transform.localScale.y - increase
                                                                , gameObject.transform.localScale.z - increase);
                //크기가 바뀌는 속도
                yield return new WaitForSeconds(0.05f);
            }
            StartCoroutine(Cooltimer_co());
        }
    }

    //스킬 구현 끝에 쿨타임 넣고싶으면 이 코루틴을 쓰면 됩니다
    private IEnumerator Cooltimer_co()
    {
        yield return new WaitForSeconds(cooltime);

        isSkillReady = true;
        Debug.Log("스킬 사용 가능");
    }

    //---------------------------------------------------------------------
    // gravity for fall of this character
    //---------------------------------------------------------------------
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
    //---------------------------------------------------------------------
    // whether it is grounded
    //---------------------------------------------------------------------
    private bool CheckGrounded()
    {
        if (playerCTRL.isGrounded && playerCTRL.enabled)
        {
            return true;
        }
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        float range = 0.01f;
        return Physics.Raycast(ray, range);
    }

    //private void MOVE()
    //{
    //    // velocity
    //    if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == MoveState)
    //    {
    //        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
    //        {
    //            MOVE_Velocity(new Vector3(0, 0, -Speed), new Vector3(0, 180, 0));
    //        }
    //        else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
    //        {
    //            MOVE_Velocity(new Vector3(0, 0, Speed), new Vector3(0, 0, 0));
    //        }
    //        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
    //        {
    //            MOVE_Velocity(new Vector3(Speed, 0, 0), new Vector3(0, 90, 0));
    //        }
    //        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow))
    //        {
    //            MOVE_Velocity(new Vector3(-Speed, 0, 0), new Vector3(0, 270, 0));
    //        }
    //    }
    //    KEY_DOWN();
    //    KEY_UP();
    //}
    ////---------------------------------------------------------------------
    //// value for moving
    ////---------------------------------------------------------------------
    //private void MOVE_Velocity(Vector3 velocity, Vector3 rot)
    //{
    //    MoveDirection = new Vector3(velocity.x, MoveDirection.y, velocity.z);
    //    if (playerCTRL)
    //    {
    //        playerCTRL.Move(MoveDirection * Time.deltaTime);
    //    }
    //    MoveDirection.x = 0;
    //    MoveDirection.z = 0;
    //    this.transform.rotation = Quaternion.Euler(rot);
    //}
    ////---------------------------------------------------------------------
    //// whether arrow key is key down
    ////---------------------------------------------------------------------
    //private void KEY_DOWN()
    //{
    //    if (Input.GetKeyDown(KeyCode.UpArrow))
    //    {
    //        animator.CrossFade(MoveState, 0.1f, 0, 0);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.DownArrow))
    //    {
    //        animator.CrossFade(MoveState, 0.1f, 0, 0);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.LeftArrow))
    //    {
    //        animator.CrossFade(MoveState, 0.1f, 0, 0);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.RightArrow))
    //    {
    //        animator.CrossFade(MoveState, 0.1f, 0, 0);
    //    }
    //}
    ////---------------------------------------------------------------------
    //// whether arrow key is key up
    ////---------------------------------------------------------------------
    //private void KEY_UP()
    //{
    //    if (Input.GetKeyUp(KeyCode.UpArrow))
    //    {
    //        if (!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
    //        {
    //            Anim.CrossFade(IdleState, 0.1f, 0, 0);
    //        }
    //    }
    //    else if (Input.GetKeyUp(KeyCode.DownArrow))
    //    {
    //        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
    //        {
    //            Anim.CrossFade(IdleState, 0.1f, 0, 0);
    //        }
    //    }
    //    else if (Input.GetKeyUp(KeyCode.LeftArrow))
    //    {
    //        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
    //        {
    //            Anim.CrossFade(IdleState, 0.1f, 0, 0);
    //        }
    //    }
    //    else if (Input.GetKeyUp(KeyCode.RightArrow))
    //    {
    //        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow))
    //        {
    //            Anim.CrossFade(IdleState, 0.1f, 0, 0);
    //        }
    //    }
    //}





}
