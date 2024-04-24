using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class RockController : MonoBehaviour
{
    public List<RockPieceControler> basePieces;
    public List<RockPieceControler> targetPieces;

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

            thisCollider.transform.parent = null;
            thisCollider.gameObject.AddComponent<Rigidbody>();
            thisCollider.gameObject.AddComponent<XRGrabInteractable>();
        }
    }
}
