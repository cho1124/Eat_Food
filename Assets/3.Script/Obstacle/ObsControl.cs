/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsControl : MonoBehaviour
{
    private ObstacleSpawner obSpawner;
    private Rigidbody ob_r;
    //public ParticleSystem ob_particle;
    [SerializeField] private float rotateSpeed;
    private float up;

    private void OnEnable()
    {
        obSpawner = FindObjectOfType<ObstacleSpawner>();
        ob_r = GetComponent<Rigidbody>();
        ob_r.velocity = Vector3.zero;
        //ob_particle = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        //ob_r.AddTorque(0, 10f, 0);

        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        if (transform.position.y < -30) obSpawner.instance.List_Active_False(gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Floor"))
    //    {
    //        Debug.Log("작동잘됨");
    //        On_Collapse();
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            Debug.Log("작동잘됨");
            On_Collapse();
        }
    }

    private void On_Collapse()
    {
        //StartCoroutine(Particle_co());
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x * 2f);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Floor"))
            {
                colliders[i].gameObject.GetComponent<Cube_Control>().Cube_Collapse(1);
            }
        }
        obSpawner.instance.List_Active_False(gameObject);
    }
    /*
    private IEnumerator Particle_co()
    {
        ob_particle.Play();
        yield return new WaitForSeconds(3f);
    }
}
*/


using System.Collections;
using UnityEngine;

public class ObsControl : MonoBehaviour
{
    private ObstacleSpawner obSpawner;
    private Rigidbody ob_r;
    
    [SerializeField] private float rotateSpeed;
    [SerializeField] private MeshRenderer ob_msRenderer;

    private void OnEnable()
    {
        obSpawner = FindObjectOfType<ObstacleSpawner>();
        ob_r = GetComponent<Rigidbody>();
        ob_r.isKinematic = false;
        ob_r.velocity = Vector3.zero;
        ob_msRenderer = GetComponentInChildren<MeshRenderer>();
        ob_msRenderer.enabled = true;

    }

    private void Update()
    {
        // 오브젝트 회전
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        // 특정 위치에 도달하면 오브젝트 비활성화
        if (transform.position.y < -30)
        {
            obSpawner.instance.List_Active_False(gameObject);
        }
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Floor"))
        {
            Debug.Log("작동잘됨");
            On_Collapse();
        }

        //음식이 플레이어랑 충돌했을때
        if(collider.CompareTag("Player"))
        {
            obSpawner.instance.List_Active_False_ToPlayer(gameObject);
            GameManager.instance.AddScore(10);
            Debug.Log("scored!");

            //점수추가
        }
    }

    private void On_Collapse()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x * 2f);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Floor"))
            {
                Cube_Control cubeControl = colliders[i].gameObject.GetComponent<Cube_Control>();
                if (cubeControl != null) cubeControl.Cube_Collapse(1);
            }
        }
        obSpawner.instance.List_Active_False(gameObject);
    }
}
