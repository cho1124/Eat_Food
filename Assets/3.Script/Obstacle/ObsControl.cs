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
