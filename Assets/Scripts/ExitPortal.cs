using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    bool isOpen = false;
    MeshRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        render.enabled = false;
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

}
