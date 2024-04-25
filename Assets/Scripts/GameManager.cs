using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class CompleteDeliveryEvent : UnityEvent<int> { }

[System.Serializable]
public class GenerateDeliveryEvent : UnityEvent<Dictionary<RockType, int>> { }
public class GameManager : MonoBehaviour
{
    public List<RockType> targetTypes;
    public List<Dictionary<RockType, int>> deliveries;
    public CompleteDeliveryEvent cdEvent;
    public GenerateDeliveryEvent gdEvent;

    private void Awake()
    {
        deliveries = new List<Dictionary<RockType, int>>();
    }
    private void Start()
    {
        GenerateDelivery();
    }

    // Returns a delivery with key as rock type and value as the number required to complete.
    public void GenerateDelivery() {
        // TODO: Replace with varying logic, maybe create a delivery class
        Dictionary<RockType, int> delivery = new Dictionary<RockType, int>();

        foreach (var type in targetTypes) {
            int randInd = Random.Range(0, 3);
            if (randInd == 0) continue;
            delivery.Add(type, randInd);
        }

        deliveries.Add(delivery);
        gdEvent.Invoke(delivery);
    }

    // TODO: Refactor to support multiple deliveries
    public void VerifyDelivery(List<GameObject> objs) {
        // Copy delivery
        foreach (var item in objs)
        {
            print(item);
        }
        var delivery = deliveries[0].ToDictionary(entry => entry.Key, entry => entry.Value);
        foreach (GameObject obj in objs) {
            RockType rt = obj.GetComponent<RockPieceControler>().rockType;
            if (!rt) continue;
            if (delivery.ContainsKey(rt)) delivery[rt] -= 1;
        }
        foreach (var item in delivery) {
            print($"{item.Key} - {item.Value}");
        }
        if (delivery.All(item => item.Value <= 0)) {
            deliveries.RemoveAt(0);
            cdEvent.Invoke(10);
        }
    }
}
