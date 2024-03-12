using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rock Piece" && collision.GetContact(0).thisCollider.name == "Floor")
        {
            Destroy(collision.gameObject);
        }
    }
}
