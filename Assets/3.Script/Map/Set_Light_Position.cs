using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Light_Position : MonoBehaviour
{
    private void Start()
    {
        float x = GameObject.Find("Map_Generator").GetComponent<Map_Generator>().map_width_get / 2f;
        gameObject.transform.position = new Vector3(x, x * 10f, x);
        gameObject.transform.LookAt(new Vector3(x, 0, x));
    }
}
