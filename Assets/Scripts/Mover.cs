using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Vector3 moveDirection;

    int currentMovementDirection = 1;
    Vector3 originalPosition;
    Coroutine pingPongRoutine = null;

    void Start()
    {
        originalPosition = transform.position;
        StartPingPong();
    }

    public void StopPingPong(){
        if(pingPongRoutine != null){
            StopCoroutine(pingPongRoutine);
        }
    }

    void StartPingPong(){
        pingPongRoutine = StartCoroutine(PingPong());
    }

    IEnumerator PingPong(){
        Vector3 destination = originalPosition + (moveDirection * currentMovementDirection);
        while(true){
            transform.Translate(currentMovementDirection * moveDirection * moveSpeed * Time.deltaTime, Space.Self);
            yield return new WaitForEndOfFrame();
            if (Vector3.Distance(transform.position, destination) <= 0.5f){
                currentMovementDirection *= -1;
                destination = originalPosition + (moveDirection * currentMovementDirection);
            }
        }
    }
}
