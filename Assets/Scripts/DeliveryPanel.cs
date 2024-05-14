using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryPanel : MonoBehaviour
{
    public Delivery delivery;
    public void SetDelivery(Delivery delivery, GameObject prefab) {
        this.delivery = delivery;
        foreach (var item in delivery.manifest)
        {
            RockType key = item.Key;
            int val = item.Value;
            Texture tex = key.material.GetTexture("_BaseMap");

            GameObject windowItem = Instantiate(prefab, this.transform);
            windowItem.GetComponentInChildren<TMP_Text>().SetText($"{key.typeName} - {val}x");
            windowItem.GetComponentInChildren<RawImage>().texture = tex;
        }
    }
}
