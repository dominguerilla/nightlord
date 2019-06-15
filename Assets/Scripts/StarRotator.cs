using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRotator : MonoBehaviour
{
    [SerializeField]
    float speed = 3.0f;

    float originalSpeed;

    private void Start() {
        originalSpeed = speed;    
    }

    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);    
    }

    public void ChangeSpeed(float scalar){
        speed *= scalar;
    }

    public void ResetSpeed(){
        speed = originalSpeed;
    }
}
