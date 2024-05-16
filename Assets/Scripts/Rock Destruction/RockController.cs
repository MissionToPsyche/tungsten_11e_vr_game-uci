using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

[System.Serializable]
public class RockPieceBrokenEvent : UnityEvent { }

public class RockController : MonoBehaviour
{
    public List<RockPieceControler> basePieces;
    public List<RockPieceControler> targetPieces;
    public RockPieceBrokenEvent RockPieceBroken;

    private void OnCollisionEnter(Collision collision)
    {
        Collider thisCollider = collision.GetContact(0).thisCollider;
        Collider otherCollider = collision.GetContact(0).otherCollider;
        if (thisCollider.transform.parent != null && otherCollider.gameObject.CompareTag("PickHead") && collision.relativeVelocity.magnitude > 2)
        {
            XRGrabInteractable gip = gameObject.GetComponent<XRGrabInteractable>();
            gip.interactionManager.UnregisterInteractable(gip.GetComponent<IXRInteractable>());
            gip.colliders.Remove(thisCollider);
            gip.interactionManager.RegisterInteractable(gip.GetComponent<IXRInteractable>());

            ParticleSystem ps = thisCollider.GetComponent<ParticleSystem>();
            ps.Play();

            AudioSource audioSource = thisCollider.GetComponent<AudioSource>();
            audioSource.Play();

            thisCollider.transform.parent = transform.parent;
            thisCollider.gameObject.AddComponent<Rigidbody>();
            thisCollider.gameObject.AddComponent<XRGrabInteractable>();

            RockPieceBroken.Invoke();
        }
    }

    public void Explode() {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform) {
            children.Add(child);
        }
        children.ForEach(delegate (Transform child)
        {
            child.parent = transform.parent;
            Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
            rb.AddExplosionForce(10.0f, transform.position, 0.0f, 3.0f);
        });
        Destroy(gameObject);
    }
}
