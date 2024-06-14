using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct TargetChance {
    public RockType type;
    [Min(0)]
    public int chance;
}

public class RockFactory : MonoBehaviour
{
    public Vector3 spawnPosition;
    public RockType baseType;
    public List<TargetChance> targetTypes;
    public GameObject rockPrefab;
    private GameObject rockInstance;
    private bool cleaningUp = false;

    public void GenerateRock() {
        CleanUp(true);
        if (cleaningUp) return;
        rockInstance = Instantiate(rockPrefab, spawnPosition, Quaternion.identity, transform);
        
        RockController rc = rockInstance.GetComponent<RockController>();
        double total = targetTypes.Sum(tc => tc.chance);
        foreach (RockPieceControler rpc in rc.targetPieces) {
            // Modified from https://stackoverflow.com/questions/46563490/c-sharp-weighted-random-numbers
            // Sums then subtracts from each value until we reach 0, then we've made our choice.
            double numericValue = Random.value * total;

            foreach (var item in targetTypes)
            {
                numericValue -= item.chance;

                if (!(numericValue <= 0))
                    continue;

                rpc.SetRockType(item.type);
                break;
            }
        }

        foreach (RockPieceControler rpc in rc.basePieces) rpc.SetRockType(baseType);
    }

    public void CleanUp(bool spawnNew) {
        if (rockInstance && !cleaningUp) {
            cleaningUp = true;
            rockInstance.GetComponent<RockController>().Explode();
            StartCoroutine(Decay(spawnNew));
        }
    }

    IEnumerator Decay(bool spawnNew) {
        for (int i = 0; i < 10; i++) {
            foreach (Transform child in transform) {
                RockPieceControler rpc = child.GetComponent<RockPieceControler>();
                if (rpc && rpc.isPersistant) continue;
                child.localScale *= 0.7f;
            }
            yield return new WaitForSeconds(.1f);
        }
        foreach (Transform child in transform) {
            RockPieceControler rpc = child.GetComponent<RockPieceControler>();
            if (rpc && rpc.isPersistant) continue;
            Destroy(child.gameObject);
        }
        cleaningUp = false;
        if (spawnNew) GenerateRock();
    }
}
