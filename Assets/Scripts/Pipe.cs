using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;

public class Pipe : MonoBehaviour
{
    public Hole hole;
    public PipeMovement pipeMovement;
    public float collectionSpeed;
    public float timeLimit;

    private bool isReady = true;
    private bool isCollecting = false;
    private List<GameObject> objects;

    private void Start()
    {
        objects = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        if (objects.Count > 0 && isCollecting) 
        {
            // Move all objects in list towards hole.
            foreach (GameObject obj in objects)
            {
                if (obj == null) continue;
                Vector3 holePos = hole.transform.position;
                Vector3 direction = (holePos - obj.transform.position).normalized;
                obj.GetComponent<Rigidbody>().velocity = direction * collectionSpeed;
            }

            // If all objects have been collected, stow the pipe away.
            if (objects.All(obj => obj == null)) StopCollection();
        }
    }

    public void CollectIntoPipe(List<GameObject> objs)
    {
        if (isReady)
        {
            isReady = false;
            // Make ungrabbable
            foreach (GameObject obj in objs)
            {
                Destroy(obj.GetComponent<XRGrabInteractable>());
            }
            objects = new List<GameObject>(objs);
            pipeMovement.Extend();
        }
    }

    public void BeginCollection()
    {
        isCollecting = true;
        hole.active = true;
        StartCoroutine(CollectTimer());
    }

    public void StopCollection()
    {
        // Guard if pipe has already been reseted
        if (isReady) return;
        isCollecting = false;
        hole.active = false;
        pipeMovement.Stow();
    }

    IEnumerator CollectTimer()
    {
        yield return new WaitForSeconds(timeLimit);
        foreach (GameObject obj in objects) {
            if (obj != null) Destroy(obj);
        }
        StopCollection();
    }

    public void ResetPipe()
    {
        objects.Clear();
        isReady = true;
    }
}
