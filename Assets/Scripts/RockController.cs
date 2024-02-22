using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    public GameObject root;

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            MeshCollider mc = child.gameObject.AddComponent<MeshCollider>();     
            mc.convex = true;
        }
    }
}
