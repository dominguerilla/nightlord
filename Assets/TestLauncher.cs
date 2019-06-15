using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLauncher : MonoBehaviour
{
    [SerializeField] CursorOrbitter cursorOrbitter;
    [SerializeField] float speed = 3f;

    GameObject cursor;
    Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        cursor = cursorOrbitter.GetCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            moveDirection = (cursor.transform.position - transform.position);
        }

        if(moveDirection != Vector3.zero){
            transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        }
    }
}
