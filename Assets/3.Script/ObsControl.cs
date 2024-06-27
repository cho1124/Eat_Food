using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsControl : MonoBehaviour
{
    public ObstacleSpawner obSpawner;
    public Rigidbody ob_r;
    public float rotateSpeed;
    public float up;


    private void Awake()
    {
        ob_r = GetComponent<Rigidbody>();
    }
    private void Update()
    {

        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Floor"))
        {
            Debug.Log("¿€µø¿ﬂµ ");
            obSpawner.instance.InsertQueue(gameObject);
        }
    }
}
