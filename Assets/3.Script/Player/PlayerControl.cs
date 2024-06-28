using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float cooltime = 10f;
    public float speed = 10f;
    public float turnSpeed = 720f; // ȸ�� �ӵ�

    public CharacterController playerCTRL;
    public CharacterType characterType;

    private bool isSkillReady = true;
    private Vector3 MoveDirection = Vector3.zero;
    private Animator animator;
    private bool is_on_floor = true;
    public bool is_dead = false;

    void Start()
    {
        playerCTRL = GetComponent<CharacterController>();
    }

    void Update()
    {
        InputHandler();
        GRAVITY();
        if (transform.position.y <= -20f) is_dead = true;
    }

    public void InputHandler()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(x, 0, z);
        Vector3 movement = inputDirection.normalized * speed * Time.deltaTime;

        if (inputDirection.magnitude > 0.1f)
        {
            // �Է� �������� ĳ���� ȸ��
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = targetRotation;
        }

        playerCTRL.Move(movement);
        characterSkill();
    }

    private void characterSkill()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            switch (characterType)
            {
                case CharacterType.������:
                    Debug.Log("a skilled");
                    break;
                case CharacterType.����ŷ:

                    StartCoroutine(MakeBigger_co());
                    break;
                case CharacterType.����:
                    
                    StartCoroutine(Repair_Floor_Co());
                    break;

            }




        }
    }

    private IEnumerator Repair_Floor_Co()
    {
        if (isSkillReady)
        {
            isSkillReady = false;
            Debug.Log("��ų ���");
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
            Debug.Log("��ų ���");
            float increase = 0.1f;

            while (gameObject.GetComponent<Transform>().localScale.x < 14f)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + increase
                                                                , gameObject.transform.localScale.y + increase
                                                                , gameObject.transform.localScale.z + increase);
                //ũ�Ⱑ �ٲ�� �ӵ�
                yield return new WaitForSeconds(0.05f);
            }
            //yield return new WaitForSeconds(10f);
            yield return new WaitForSeconds(5f);

            while (gameObject.GetComponent<Transform>().localScale.x > 4.1f)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - increase
                                                                , gameObject.transform.localScale.y - increase
                                                                , gameObject.transform.localScale.z - increase);
                //ũ�Ⱑ �ٲ�� �ӵ�
                yield return new WaitForSeconds(0.05f);
            }
            StartCoroutine(Cooltimer_co());
        }
    }

    //��ų ���� ���� ��Ÿ�� �ְ������ �� �ڷ�ƾ�� ���� �˴ϴ�
    private IEnumerator Cooltimer_co()
    {
        yield return new WaitForSeconds(cooltime);

        isSkillReady = true;
        Debug.Log("��ų ��� ����");
    }

    private void GRAVITY()
    {
        if (playerCTRL.enabled)
        {
            if (is_on_floor) MoveDirection = Vector3.zero;
            else MoveDirection.y -= 0.1f;
            playerCTRL.Move(MoveDirection * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            is_on_floor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            is_on_floor = false;
        }
    }

}
