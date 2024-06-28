using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Skill : MonoBehaviour
{
    [SerializeField] private float cooltime;
    private bool is_skill_ready;

    public event EventHandler on_skill_casted;

    

    private IEnumerator Repair_Floor_Co()
    {
        if (is_skill_ready)
        {
            is_skill_ready = false;
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
        if (is_skill_ready)
        {
            is_skill_ready = false;
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

        is_skill_ready = true;
        Debug.Log("��ų ��� ����");
    }
}
