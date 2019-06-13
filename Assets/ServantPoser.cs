using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CursorOrbitter))]
public class ServantPoser : MonoBehaviour
{
    [SerializeField] float speed = 100f;

    CursorOrbitter orbitter;
    GameObject cursor;

    private void Start() {
        orbitter = GetComponent<CursorOrbitter>();
        cursor = orbitter.GetCursor();
    }

    // Update is called once per frame
    void Update()
    {
        Glide1Init(cursor.transform.position);
        Glide1(cursor.transform.position);

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
