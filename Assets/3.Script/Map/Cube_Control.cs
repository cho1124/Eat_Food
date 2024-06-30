using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Control : MonoBehaviour
{
    [SerializeField] private int durability_max;
    [SerializeField] private int durability_current;
    Renderer cube_renderer;
    private Color initial_color;

    private void OnEnable()
    {
        durability_current = durability_max;
        TryGetComponent(out cube_renderer);

        initial_color = cube_renderer.material.color;

        Cube_Set_Color();
    }

    private void Cube_Set_Color()
    {
        float color_ratio = (float)durability_current / (float)durability_max;


        cube_renderer.material.color = initial_color * color_ratio;
    }

    public void Cube_Collapse(int damage)
    {
        durability_current -= damage;
        Cube_Set_Color();
        GameManager.instance.PlaySound(GameManager.instance.ImpactaudioSource ,GameManager.instance.ImpactClip);

        if(durability_current <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
