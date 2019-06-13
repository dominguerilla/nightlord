using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    [SerializeField]
    float cursorDistance = 2.0f;

    [SerializeField]
    Vector3 cursorCenter;

    [SerializeField]
    GameObject cursor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(x, y, 0);

        // Player Movement
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        // Cursor Placement
        if(cursor){
            Vector3 cursorLocation = transform.position + cursorCenter + Vector3.Normalize(moveDirection) * cursorDistance;
            cursor.transform.position = cursorLocation;
        }
    }

}
