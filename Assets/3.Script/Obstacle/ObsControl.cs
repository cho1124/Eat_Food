using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsControl : MonoBehaviour
{
    private ObstacleSpawner obSpawner;
    private Rigidbody ob_r;
    public ParticleSystem ob_particle;
    [SerializeField] private float rotateSpeed;
    private float up;

    private void OnEnable()
    {
        obSpawner = FindObjectOfType<ObstacleSpawner>();
        ob_r = GetComponent<Rigidbody>();
        ob_r.velocity = Vector3.zero;
        ob_particle = GetComponentInChildren<ParticleSystem>();
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
    //        Debug.Log("ÀÛµ¿ÀßµÊ");
    //        On_Collapse();
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            Debug.Log("ÀÛµ¿ÀßµÊ");
            On_Collapse();
        }
    }

    private void On_Collapse()
    {
        StartCoroutine(Particle_co());
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

    private IEnumerator Particle_co()
    {
        ob_particle.Play();
        yield return new WaitForSeconds(3f);
    }
}
