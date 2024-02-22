using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RockController : MonoBehaviour
{
    public GameObject root;

    void Awake()
    {
        XRGrabInteractable gi = gameObject.GetComponent<XRGrabInteractable>();
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            if (ReferenceEquals(child, root.transform)) {
                continue;
            }

            //Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
            //rb.isKinematic = true;

            MeshCollider mc = child.gameObject.AddComponent<MeshCollider>();     
            mc.convex = true;

            gi.colliders.Add(mc);

            //child.gameObject.AddComponent<RockPieceController>();
        }
        gi.interactionManager.UnregisterInteractable(gi.GetComponent<IXRInteractable>());
        gi.interactionManager.RegisterInteractable(gi.GetComponent<IXRInteractable>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider thisCollider = collision.GetContact(0).thisCollider;
        Collider otherCollider = collision.GetContact(0).otherCollider;
        if (thisCollider.transform.parent != null && otherCollider.gameObject.CompareTag("PickHead"))
        {
            Debug.Log("Hit and breaking!");
            thisCollider.transform.parent = null;
            Rigidbody rb = thisCollider.gameObject.AddComponent<Rigidbody>();
            XRGrabInteractable gi = thisCollider.gameObject.AddComponent<XRGrabInteractable>();
            MeshCollider mc = thisCollider.gameObject.GetComponent<MeshCollider>();
            mc.isTrigger = false;
        }
    }
}
