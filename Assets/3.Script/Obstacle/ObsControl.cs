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
    public ParticleSystem ob_particle;
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

        // 자식 객체에서 ParticleSystem을 찾습니다.
        ob_particle = GetComponentInChildren<ParticleSystem>();
        if (ob_particle == null)
        {
            Debug.LogError("ParticleSystem not found in children of " + gameObject.name);
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            Debug.Log("작동잘됨");
            StartCoroutine(On_Collapse());
        }
    }

    private IEnumerator On_Collapse()
    {
        // 물리적 상호작용을 멈추고 콜라이더를 비활성화
        ob_r.isKinematic = true;
        ob_msRenderer.enabled = false;

        if (ob_particle != null)
        {
            ob_particle.transform.position = transform.position;
            ob_particle.Play();
            Debug.Log("Particle played");
        }
        else
        {
            Debug.LogError("ParticleSystem reference is missing.");
        }

        yield return new WaitForSeconds(3f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x * 2f);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Floor"))
            {
                Cube_Control cubeControl = colliders[i].gameObject.GetComponent<Cube_Control>();
                if (cubeControl != null)
                {
                    cubeControl.Cube_Collapse(1);
                }
            }
        }

        obSpawner.instance.List_Active_False(gameObject);
    }
}
