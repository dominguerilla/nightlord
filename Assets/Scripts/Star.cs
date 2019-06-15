using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Star : MonoBehaviour
{
    [SerializeField] float translateSpeed = 5f;

    public UnityEvent onEnterOrbit { get; private set; }
    public UnityEvent onReachDestination { get; private set; }
    bool wasOrbitted = false;

    Vector3 targetDestination = Vector3.zero;
    bool isAtDestination = true;

    private void Awake() {
        onEnterOrbit = new UnityEvent();
        onReachDestination = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTowards(Vector3 target) {
        targetDestination = target;
        isAtDestination = false;
        StartCoroutine(TranslateStar());
    }

    public void OnEnterOrbit(){
        if(!wasOrbitted){
            wasOrbitted = true;
            onEnterOrbit.Invoke();
        }
    }

    public bool hasBeenOrbitted(){
        return this.wasOrbitted;
    }

    IEnumerator TranslateStar() {
        Vector3 direction = targetDestination - transform.position;
        while(Vector3.Distance(transform.position, targetDestination) > 0.5f) {
            transform.position = Vector3.MoveTowards(transform.position, targetDestination, translateSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        
        ReachDestination();
        yield return null;
    }

    void ReachDestination() {
        onReachDestination.Invoke();
        isAtDestination = true;
        targetDestination = Vector3.zero;
    }
}
