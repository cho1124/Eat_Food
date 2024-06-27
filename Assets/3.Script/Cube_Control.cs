using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Control : MonoBehaviour
{
    [SerializeField] private int durability_max;
    [SerializeField] private int durability_current;
    Renderer cube_renderer;

    private void OnEnable()
    {
        durability_current = durability_max;
        TryGetComponent(out cube_renderer);
        Cube_Set_Color();
    }

    private void Cube_Set_Color()
    {
        cube_renderer.material.color = new Color((float)durability_current / (float)durability_max, (float)durability_current / (float)durability_max, (float)durability_current / (float)durability_max, 1);
    }

    public void Cube_Collapse(int damage)
    {
        durability_current -= damage;
        Cube_Set_Color();

        if(durability_current <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
