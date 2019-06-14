using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantPoser : MonoBehaviour
{
    [SerializeField] CursorOrbitter orbitter;
    [SerializeField] float speed = 100f;

    GameObject cursor;

    private void Start() {
        cursor = orbitter.GetCursor();
    }

    // Update is called once per frame
    void Update()
    {
        Glide1Init(cursor.transform.position);
        //Glide1(cursor.transform.position);

        if(Input.GetKeyDown(KeyCode.C)){
            Debug.Log(cursor.transform.position - transform.position);
        }
        if(Input.GetKeyDown(KeyCode.R)){
            Debug.Log(transform.up);
        }
    }

    void Glide1Init(Vector3 referencePoint){
        transform.up = referencePoint - transform.position;
    }

    void Glide1(Vector3 referencePoint){
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);
    }
}
