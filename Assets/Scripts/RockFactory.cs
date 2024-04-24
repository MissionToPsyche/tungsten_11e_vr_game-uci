using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct TargetChance {
    [SerializeField]
    public RockType type;
    [SerializeField]
    public int chance;
}

public class RockFactory : MonoBehaviour
{
    public Vector3 spawnPosition;
    public List<TargetChance> targetTypes;
    public GameObject rockPrefab;
    private GameObject rockInstance;

    public void GenerateRock() {
        rockInstance = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        
        RockController rc = rockInstance.GetComponent<RockController>();
        double total = targetTypes.Sum(tc => tc.chance);
        foreach (RockPieceControler rpc in rc.targetPieces) {
            // Modified from https://stackoverflow.com/questions/46563490/c-sharp-weighted-random-numbers
            // Sums then subtracts from each value until we reach 0, then we've made our choice.
            double totalCopy = total;
            double numericValue = Random.value * totalCopy;

            foreach (var item in targetTypes)
            {
                numericValue -= item.chance;

                if (!(numericValue <= 0))
                    continue;

                rpc.SetRockType(item.type);
                break;
            }
        }
    }
}
