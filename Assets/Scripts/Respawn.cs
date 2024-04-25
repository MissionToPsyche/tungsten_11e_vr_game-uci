using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    //public GameObject XRRig;

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = new Vector3(0, 0, 0);   
    }
}
