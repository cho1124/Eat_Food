using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Suicide : MonoBehaviour
{
    [SerializeField] private float timer;
    
    private void OnEnable()
    {
        StartCoroutine(Particle_Suicide_Co());
    }

    private IEnumerator Particle_Suicide_Co()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
