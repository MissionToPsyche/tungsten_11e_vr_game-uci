using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PointEvent : UnityEvent<int> { }

public class DestroyObject : MonoBehaviour
{

    public PointEvent pointEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rock Piece" && collision.GetContact(0).thisCollider.name == "Floor")
        {
            RockPieceControler rpc = collision.gameObject.GetComponent<RockPieceControler>();
            Destroy(collision.gameObject);
        }
    }
}
