using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class CollectEvent : UnityEvent<List<GameObject>> { }

public class DeliveryArea : MonoBehaviour
{
    public TMP_Text screen;
    public CollectEvent collectEvent;
    private List<GameObject> touchingPieces;

    private void Awake() {
        touchingPieces = new List<GameObject>();
    }

    public void collectPieces() {
        foreach (var item in touchingPieces) {
            print(item);
        }
        collectEvent.Invoke(touchingPieces);
    }

    public void setDelivery(Dictionary<RockType, int> delivery) {
        screen.text = "";
        foreach (var item in delivery) {
            RockType key = item.Key;
            int val = item.Value;
            screen.text += $"{key.typeName} - {val}\n";
        }
    }

    public void clearDelivery() {
        screen.text = "";
        foreach (var obj in touchingPieces) {
            Destroy(obj);
        }
    }

    // TODO: Fix parent transform when we move to generic breaking system.
    private void OnCollisionEnter(Collision collision) {
        GameObject colGameObject = collision.gameObject;
        if (colGameObject.tag == "Rock Piece" && colGameObject.transform.parent == null) {
            touchingPieces.Add(colGameObject);
        }
    }

    private void OnCollisionExit(Collision collision) {
        GameObject colGameObject = collision.gameObject;
        if (touchingPieces.Contains(colGameObject)) {
            touchingPieces.Remove(colGameObject);
        }
    }
}
