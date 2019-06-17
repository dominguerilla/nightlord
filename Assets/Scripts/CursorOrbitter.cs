using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles orbitting the cursor around this GameObject based on player input.
/// </summary>
public class CursorOrbitter : MonoBehaviour
{
    /// <summary>
    /// `cursor` should be a child GameObject that will be moved around based on input.
    /// Make sure it's the right size and shape.
    /// </summary>
    [SerializeField] GameObject cursor;
    [SerializeField] float cursorDistance = 6f;
    [SerializeField] Vector3 cursorOffset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(x, y, 0);

        // Cursor Placement
        if(inputDirection != Vector3.zero){
            Vector3 cursorLocation = transform.position + cursorOffset + Vector3.Normalize(inputDirection) * cursorDistance;
            this.cursor.transform.position = cursorLocation;
        }

    }

    public GameObject GetCursor(){
        return this.cursor;
    }
}
