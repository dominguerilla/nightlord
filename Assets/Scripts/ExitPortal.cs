using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    bool isOpen = false;
    MeshRenderer render;
    ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        if(!render){
            render = GetComponentInChildren<MeshRenderer>();
        }
        render.enabled = false;
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPortal(){
        render.enabled = true;
        isOpen = true;
    }

    public bool isPortalOpen(){
        return isOpen;
    }

    /// <summary>
    /// Objects exiting the portal should call this function.
    /// 
    /// Kind of a weird pattern (caller calls portal to let them know an object is exiting,
    /// instead of the portal knowing when an object is passing through it). Normally I'd be using some UnityEvent, too.
    /// </summary>
    public void TriggerExit() {
        particleSystem.Play(); 
    }
}
