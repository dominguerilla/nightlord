using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRotator : MonoBehaviour
{
    [SerializeField]
    float speed = 3.0f;

    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);    
    }
}
