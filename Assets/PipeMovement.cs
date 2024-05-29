using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PipeMovement : MonoBehaviour
{
    public Pipe pipe;
    
    private Vector3 targetPos;
    public Vector3 stowedPos = new Vector3(0, 5, 0);
    public Vector3 extendedPos = new Vector3(0, -2.85f, 0);

    public float speed = 0.5f;

    private bool isIdle = true;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = stowedPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle) return;
        Vector3 currentPos = transform.localPosition;
        // If our position is close to target, set pos and set bool to false.
        if ((currentPos - targetPos).magnitude <= 0.01f)
        {
            transform.localPosition = targetPos;
            if (targetPos == stowedPos)
            {
                pipe.ResetPipe();
            }
            else 
            {
                pipe.BeginCollection();
            }
            isIdle = true;
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);
        }
    }

    public void Extend()
    {
        if (targetPos == extendedPos) return;
        targetPos = extendedPos;
        isIdle = false;
    }

    public void Stow()
    {
        if (targetPos == stowedPos) return;
        targetPos = stowedPos;
        isIdle = false;
    }
}
