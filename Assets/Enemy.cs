using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    ScreenLoader loader;
    ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        loader = GameObject.FindObjectOfType<ScreenLoader>();
        particles = GetComponent<ParticleSystem>();    
    }

    /// <summary>
    /// Emits particles and triggers a screen reload
    /// </summary>
    public void TriggerPlayerDeath(){
        if(particles) particles.Play();
        StartCoroutine(DelayedScreenReload());
    }

    IEnumerator DelayedScreenReload(){
        yield return new WaitForSeconds(2f);
        loader.ResetScreen();
    }
}
