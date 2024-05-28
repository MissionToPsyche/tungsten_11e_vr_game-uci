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
    public GameObject deliveryCanvas;
    public GameObject deliveryItemPrefab;
    public GameObject deliveryPanelPrefab;

    public CollectEvent collectEvent;

    private List<GameObject> touchingPieces;

    private void Awake() {
        touchingPieces = new List<GameObject>();
    }

    public void CollectPieces() {
        collectEvent.Invoke(touchingPieces);
    }

    public void AddDelivery(Delivery delivery) {
        GameObject windowPanel = Instantiate(deliveryPanelPrefab, deliveryCanvas.transform);
        windowPanel.GetComponent<DeliveryPanel>().SetDelivery(delivery, deliveryItemPrefab);
    }
    
    public void RemoveDelivery(Delivery delivery) {
        foreach (Transform child in deliveryCanvas.transform) {
            DeliveryPanel dp = child.GetComponent<DeliveryPanel>();
            if (dp && dp.delivery == delivery) {
                Destroy(child.gameObject);
                break;
            }
        }
    }

    public void ClearDeliveryArea() {
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
            //colGameObject.GetComponent<RockPieceControler>().isPersistant = true;
        }
    }

    // What happens if we destroy before exiting?
    private void OnCollisionExit(Collision collision) {
        GameObject colGameObject = collision.gameObject;
        if (touchingPieces.Contains(colGameObject)) {
            touchingPieces.Remove(colGameObject);
            //colGameObject.GetComponent<RockPieceControler>().isPersistant = false;
        }
    }
}
