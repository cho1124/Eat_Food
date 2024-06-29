using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Camera_Position : MonoBehaviour
{
    private void Start()
    {
        float x = GameObject.Find("Map_Generator").GetComponent<Map_Generator>().map_width_get / 2f;
        gameObject.transform.position = new Vector3(x, x * 1.5f, -(x / 3f));
        gameObject.transform.LookAt(new Vector3(x, 0, x));
    }
}
