using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class CollectEvent : UnityEvent<List<GameObject>> { }

public class DeliveryArea : MonoBehaviour
{
    public GameObject deliveryPanel;
    public GameObject deliveryItemPrefab;
    public CollectEvent collectEvent;
    private List<GameObject> touchingPieces;

    private void Awake() {
        touchingPieces = new List<GameObject>();
    }

    public void collectPieces() {
        collectEvent.Invoke(touchingPieces);
    }

    public void setDelivery(Delivery delivery) {
        clearDelivery();
        foreach (var item in delivery.manifest) {
            RockType key = item.Key;
            int val = item.Value;
            Texture tex = key.material.GetTexture("_BaseMap");

            GameObject windowItem = Instantiate(deliveryItemPrefab, deliveryPanel.transform);
            windowItem.GetComponentInChildren<TMP_Text>().SetText($"{key.typeName} - {val}x");
            windowItem.GetComponentInChildren<RawImage>().texture = tex;
        }
    }

    public void clearDelivery() {
        foreach (Transform child in deliveryPanel.transform) {
            Destroy(child.gameObject);
        }
        foreach (GameObject obj in touchingPieces) {
            Destroy(obj);
        }
        touchingPieces.Clear();
    }

    private void OnCollisionEnter(Collision collision) {
        GameObject colGameObject = collision.gameObject;
        Transform parent = colGameObject.transform.parent;
        if (colGameObject.tag == "Rock Piece" && parent && parent.GetComponent<RockController>() == null) {
            touchingPieces.Add(colGameObject);
            colGameObject.GetComponent<RockPieceControler>().isPersistant = true;
        }
    }

    // What happens if we destroy before exiting?
    private void OnCollisionExit(Collision collision) {
        GameObject colGameObject = collision.gameObject;
        if (touchingPieces.Contains(colGameObject)) {
            touchingPieces.Remove(colGameObject);
            colGameObject.GetComponent<RockPieceControler>().isPersistant = false;
        }
    }
}
