using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class CompleteDeliveryEvent : UnityEvent<int> { }

[System.Serializable]
public class GenerateDeliveryEvent : UnityEvent<Delivery> { }

public class Delivery {
    public Dictionary<RockType, int> manifest;
    public int points;

    public Delivery(Dictionary<RockType, int> manifest, int points) {
        this.manifest = manifest;
        this.points = points;
    }

    public bool CanComplete(Dictionary<RockType, int> collectedPieces) {
        var deliveryCopy = manifest.ToDictionary(entry => entry.Key, entry => entry.Value);
        foreach (var item in collectedPieces) {
            if (deliveryCopy.TryGetValue(item.Key, out var val)) {
                deliveryCopy[item.Key] = val - item.Value;
            }
        }
        return deliveryCopy.All(entry => entry.Value <= 0);
    }
}

[System.Serializable]
public struct DeliveryAttribute
{
    public RockType type;
    [Min(0)]
    public int chance;
    [Min(0)]
    public int pointValue;
}

public class DeliverySystem : MonoBehaviour
{
    [Header("Delivery Configuration")]
    public List<DeliveryAttribute> deliveryAttributes;
    
    [Space(20)]

    public CompleteDeliveryEvent CompleteDeliveryEvent;
    public GenerateDeliveryEvent GenerateDeliveryEvent;
    public List<Delivery> deliveries;


    private void Awake()
    {
        deliveries = new List<Delivery>();
    }
    private void Start()
    {
        GenerateDelivery();
    }

    /// <summary>
    /// Generates a delivery with the amount of items specified.
    /// </summary>
    /// <param name="numItems">The number of items in the delivery</param>
    public void GenerateDelivery(int numItems = 0) {
        Dictionary<RockType, int> manifest = new Dictionary<RockType, int>();

        if (numItems <= 0) numItems = Random.Range(1, 6);

        int pointTotal = 0;
        for (int i = 0; i < numItems; i++) {
            DeliveryAttribute attr = PickRandomDeliveryAttr();
            if (!manifest.TryAdd(attr.type, 1)) manifest[attr.type] += 1;
            pointTotal += attr.pointValue;
        }

        var delivery = new Delivery(manifest, pointTotal);

        deliveries.Add(delivery);
        GenerateDeliveryEvent.Invoke(delivery);
    }

    public void VerifyDelivery(List<GameObject> objs) {
        var collectedObjs = objs
            .Select(obj => obj.GetComponent<RockPieceControler>().rockType)
            .GroupBy(obj => obj)
            .ToDictionary(obj => obj.Key, obj => obj.Count());
        var sortedDeliveries = deliveries.OrderByDescending(delivery => delivery.points);
        var delivery = sortedDeliveries.FirstOrDefault(delivery => delivery.CanComplete(collectedObjs));
        if (delivery != null) {
            CompleteDeliveryEvent.Invoke(delivery.points);
            deliveries.Remove(delivery);
        }
    }

    private DeliveryAttribute PickRandomDeliveryAttr(int min = 0, int max = int.MaxValue) {
        double total = deliveryAttributes.Sum(tc => tc.chance);
        // Modified from https://stackoverflow.com/questions/46563490/c-sharp-weighted-random-numbers
        // Sums then subtracts from each value until we reach 0, then we've made our choice.
        double numericValue = Random.value * total;

        DeliveryAttribute res = new DeliveryAttribute();
        foreach (var item in deliveryAttributes)
        {
            numericValue -= item.chance;

            if (!(numericValue <= 0))
                continue;

            res = item;
            break;
        }
        return res;
    }
}
