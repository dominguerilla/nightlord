using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantPoser : MonoBehaviour
{
    [SerializeField] CursorOrbitter orbitter;
    public GameObject playerMesh;

    GameObject cursor;

    private void Start() {
        cursor = orbitter.GetCursor();
    }

    // Update is called once per frame
    void Update()
    {
        Point(cursor.transform.position);
    }

    void Point(Vector3 referencePoint){
        Vector3 direction = referencePoint - playerMesh.transform.position;        
        playerMesh.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

}
