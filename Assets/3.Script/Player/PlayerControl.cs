using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public CharacterType characterType;
    public float moveSpeed;
    public float cooltime;
    public bool is_dead = false;
    


    private CharacterController playerCTRL;
    private bool isSkillReady = true;
    private bool is_casting = false;
    private Vector3 MoveDirection = Vector3.zero;
    private Animator animator;
   

    void Start()
    {
        playerCTRL = GetComponent<CharacterController>();
        is_dead = false;
        animator = GetComponent<Animator>();


    }

    void Update()
    {
        if(!is_casting) InputHandler();
        if (transform.position.y <= -20f)
        {
            if(!is_dead)
            {
                is_dead = true;
                //GameManager.instance.playerinfo.Add(new PlayerInfo("Player1", "a", 100)); // 예시 데이터 추가
                //GameManager.instance.SavePlayerData("asd", name);
                //GameManager.instance.SavePlayerDataToJson();

               
                Debug.Log("Player is Dead!");
                //ButtonControl.instance.ShowGameOverPanel();
                ButtonControl.instance.ShowEnterNamePanel();
                GameManager.instance.LoadPlayerDataFromJson();
            }


            
            //Time.timeScale = 0f;
            
        }
        
    }

    public void InputHandler()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(x, 0, z);
        Vector3 movement = inputDirection.normalized * this.moveSpeed * Time.deltaTime;

        float speed = Mathf.Sqrt(x * x + z * z);

        // 애니메이터의 Speed 파라미터에 적용
        animator.SetFloat("Speed", speed);


        if (inputDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = targetRotation;
        }

        MoveDirection = Vector3.zero;
        RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up * transform.localScale.y, Vector3.down, 100f);
        if (hits.Length < 3) MoveDirection.y = -1f;

        playerCTRL.Move(movement + MoveDirection);
        characterSkill();
    }

    private void characterSkill()
    {
        if (isSkillReady && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Skill");
            switch (characterType)
            {
                case CharacterType.매지션:
                    StartCoroutine(Teleport_Co());
                    StartCoroutine(Cooltimer_co());
                    break;
                case CharacterType.바이킹:
                    StartCoroutine(MakeBigger_co());
                    StartCoroutine(Cooltimer_co());
                    break;
                case CharacterType.빌더:
                    StartCoroutine(Repair_Floor_Co());
                    StartCoroutine(Cooltimer_co());
                    break;
            }
        }
    }

    private IEnumerator Teleport_Co()
    {
        if (isSkillReady)
        {
            Debug.Log("스킬 사용");
            isSkillReady = false;
            is_casting = true;
            float distance = 10f;
            float distance_clamped = distance;
            Vector3 destination;
            Vector3 destination_output = Vector3.zero;
            Map_Generator map_generator = GameObject.Find("Map_Generator").GetComponent<Map_Generator>();
            while (distance_clamped > 0)
            {
                destination = transform.position + transform.forward * distance_clamped;
                if (map_generator.Index_To_Position(map_generator.Position_To_Index(destination), out destination_output)) break;
                else
                {
                    distance_clamped -= 0.1f;
                    destination_output = transform.position;
                }
            }
            transform.position = destination_output;
            yield return new WaitForSeconds(0.2f);
            is_casting = false;
        }
        yield return null;
    }

    private IEnumerator Repair_Floor_Co()
    {
        if (isSkillReady)
        {
            Debug.Log("스킬 사용");
            isSkillReady = false;
            is_casting = true;
            float repair_dinstance = 5f;
            int repair_radius = 5;

            Map_Generator map_generator = GameObject.Find("Map_Generator").GetComponent<Map_Generator>();
            Vector2Int index = map_generator.Position_To_Index(transform.position + transform.forward * repair_dinstance);
            map_generator.Repair_Floor(index, repair_radius);
            yield return new WaitForSeconds(0.5f);
            is_casting = false;
        }
        yield return null;
    }

    private IEnumerator MakeBigger_co()
    {
        if (isSkillReady)
        {
            Debug.Log("스킬 사용");
            isSkillReady = false;
            float increase = 0.1f;

            while (gameObject.GetComponent<Transform>().localScale.x < 14f)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + increase
                                                                , gameObject.transform.localScale.y + increase
                                                                , gameObject.transform.localScale.z + increase);
                //크기가 바뀌는 속도
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(5f);

            while (gameObject.GetComponent<Transform>().localScale.x > 4.1f)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - increase
                                                                , gameObject.transform.localScale.y - increase
                                                                , gameObject.transform.localScale.z - increase);
                //크기가 바뀌는 속도
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    //스킬 구현 끝에 쿨타임 넣고싶으면 이 코루틴을 쓰면 됩니다
    private IEnumerator Cooltimer_co()
    {
        yield return new WaitForSeconds(cooltime);
        isSkillReady = true;
        Debug.Log("스킬 사용 가능");
    }
}
