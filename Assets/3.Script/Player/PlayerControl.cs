using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float cooltime = 10f;
    public float speed = 10f;
    public CharacterController playerCTRL;
    public CharacterType characterType;

    private bool isSkillReady = true;
    private Vector3 MoveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        playerCTRL = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
        GRAVITY();
    }

    public void InputHandler()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, 0, z) * speed * Time.deltaTime;
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
            StartCoroutine(MakeBigger_co());
            //            Debug.Log("b skilled");
            //            break;
            //        case CharacterType.c:
            //            Debug.Log("c skilled");
            //            break;
            //
            //    }
        }
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




}
