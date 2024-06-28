using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsControl : MonoBehaviour
{
    public ObstacleSpawner obSpawner;
    public Rigidbody ob_r;
    public float rotateSpeed;
    public float up;

    private void OnEnable()
    {
        obSpawner = FindObjectOfType<ObstacleSpawner>();
        ob_r = GetComponent<Rigidbody>();
        ob_r.velocity = Vector3.zero;
        rotateSpeed = obSpawner.ObjRotateSpeed;
        
    }
    private void Update()
    {
        //ob_r.AddTorque(0, 10f, 0);

        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            Debug.Log("¿€µø¿ﬂµ ");
            On_Collapse();
        }
    }
    private void On_Collapse()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3.0f);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Floor"))
            {
                colliders[i].gameObject.GetComponent<Cube_Control>().Cube_Collapse(1);
            }
        }
        obSpawner.instance.InsertQueue(gameObject);
    }
}
