using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPieceControler : MonoBehaviour
{
    public RockType rockType;
    public List<GameObject> neighbors;

    private void Start()
    {
        GetNeighbors();
    }

    public void SetRockType(RockType rockType) {
        this.rockType = rockType;
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.SetMaterials(new List<Material>() { this.rockType.material });
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shape = ps.shape;
        shape.texture = (this.rockType.material.mainTexture as Texture2D);
    }

    public void GetNeighbors()
    {
        Vector3 startScale = transform.localScale;
        transform.localScale *= 1.02f;
        var collider = GetComponent<MeshCollider>();
        foreach (Transform child in transform.parent)
        {
            if (child == this.transform) continue;
            var otherCollider = child.GetComponent<MeshCollider>();

            Vector3 otherPosition = otherCollider.transform.position;
            Quaternion otherRotation = otherCollider.transform.rotation;

            if (Physics.ComputePenetration(
                collider, transform.position, transform.rotation,
                otherCollider, otherPosition, otherRotation,
                out _, out _
                )) neighbors.Add(child.gameObject);
        }
        transform.localScale = startScale;
    }
}
