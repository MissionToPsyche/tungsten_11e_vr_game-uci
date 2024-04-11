using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPieceControler : MonoBehaviour
{
    public RockType rockType;

    public void SetRockType(RockType rockType) {
        this.rockType = rockType;
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.SetMaterials(new List<Material>() { this.rockType.material });
    }
}
