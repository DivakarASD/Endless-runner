using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform targetTransform;

    private Vector3 startOffset;
    private Vector3 moveVector;
    private Vector3 animationOffset = new Vector3(0, 5, 5);

    private float transition = 0.0f;
    
    public float animationDuration = 2.0f;    

    void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //targetTransform = GameObject.FindGameObjectWithTag("Player").transform; This works same as the previous line where the "tranform" refers to the gameObject with that tag.
        startOffset = transform.position - targetTransform.position;
    }

    void Update()
    {
        moveVector = targetTransform.position + startOffset;
        moveVector.x = 0;

        if (transition > 1.0f)
        {
            transform.position = moveVector;
        }
        else
        {
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(targetTransform.position+Vector3.up);
        }                
    }
}
