using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Star))]
public class StarParticleEmitter : MonoBehaviour
{
    Star star;
    ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        star = GetComponent<Star>();
        star.onEnterOrbit.AddListener(EmitOrbitParticles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EmitOrbitParticles(){
        particles.Play();
    }
}
