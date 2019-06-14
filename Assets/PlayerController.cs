using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Servant has two movement states: one for being in flight, another for orbitting around a star.
/// (I have no time to learn & do physics for this)
/// 
/// The Servant is in flight when launching/being launched.
/// The Servant is in orbit when it gets close enough to a star.
/// </summary>
[RequireComponent(typeof(CursorOrbitter))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float glideSpeed = 10f;

    CursorOrbitter orbitter;
    GameObject cursor;
    Vector3 launchDirection = Vector3.zero;
    
    // True when in flight; false otherwise
    bool isInFlight = true;

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
        isInFlight = true;
        launchDirection = direction;
    }

    void Orbit(){
        isInFlight = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<Star>()){
            Orbit();
            Debug.Log("Hit star!");
        }
    }

}
