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
    [SerializeField] float orbitSpeed = 10f;
    [SerializeField] float orbitRadius = 1f;

    CursorOrbitter cursorOrbitter;
    GameObject cursor;

    Vector3 launchDirection = Vector3.zero;

    GameObject orbittedObject;
    
    // True when in flight; false otherwise
    bool isInFlight = true;

    // Start is called before the first frame update
    void Start()
    {
        cursorOrbitter = GetComponent<CursorOrbitter>();
        cursor = cursorOrbitter.GetCursor();
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

        // Updating movement
        // Flight mode
        if(isInFlight && launchDirection != Vector3.zero){
            Vector3 direction = (launchDirection - transform.position);
            transform.Translate(direction * glideSpeed * Time.deltaTime,Space.World);
        }

        //Orbit mode
        if(!isInFlight){
            transform.RotateAround(orbittedObject.transform.position, Vector3.forward, orbitSpeed * Time.deltaTime);
            Vector3 desiredPosition = (transform.position - orbittedObject.transform.position).normalized * orbitRadius + orbittedObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, orbitSpeed * Time.deltaTime);
        }
    }

    void Launch(Vector3 direction){
        isInFlight = true;
        launchDirection = direction;
        this.orbittedObject = null;
    }

    void Orbit(GameObject orbittedObject){
        this.orbittedObject = orbittedObject;
        isInFlight = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(isInFlight && other.gameObject.GetComponent<Star>()){
            Orbit(other.gameObject);
        }
    }

}
