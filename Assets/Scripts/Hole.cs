using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public bool active = false;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!active) return;
        GameObject colGameObject = collision.gameObject;
        Transform parent = colGameObject.transform.parent;
        if (colGameObject.tag == "Rock Piece" && parent && parent.GetComponent<RockController>() == null)
        {
            Destroy(collision.gameObject);
        }
    }
}
