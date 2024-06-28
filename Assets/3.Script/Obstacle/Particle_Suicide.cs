using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Suicide : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Particle_Suicide_Co());
    }

    private IEnumerator Particle_Suicide_Co()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
