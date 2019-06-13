using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CursorOrbitter))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float glideSpeed = 10f;

    CursorOrbitter orbitter;
    GameObject cursor;
    Vector3 launchDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        orbitter = GetComponent<CursorOrbitter>();
        cursor = orbitter.GetCursor();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(x, y, 0);

        // Player Movement
        if(Input.GetKeyDown(KeyCode.Space)){
            Launch(cursor.transform.position);
        }

        Vector3 direction = (launchDirection - transform.position);
        if(launchDirection != Vector3.zero){
            transform.Translate(direction * glideSpeed * Time.deltaTime,Space.World);
        }
    }

    void Launch(Vector3 direction){
        launchDirection = direction;
    }

}
