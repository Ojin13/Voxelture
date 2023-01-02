using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestoyer : MonoBehaviour
{
    public GameObject additionalDestruction;
    void Start()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        float totalDuration = particleSystem.duration + particleSystem.startLifetime;

        if (additionalDestruction != null)
        {
            Destroy(additionalDestruction, 0.1f);
        }
        
        Destroy(gameObject, totalDuration);
    }
}
