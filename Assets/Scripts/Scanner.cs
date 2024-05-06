using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scanner : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public TMP_Text screen;
    public RawImage image;
    public float maxDistance = 10;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }
    void FixedUpdate()
    {
        Vector3[] vertexPos = new Vector3[2];
        vertexPos[0] = transform.position;
        vertexPos[1] = vertexPos[0] + (maxDistance * transform.parent.TransformDirection(Vector3.forward));
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.parent.TransformDirection(Vector3.forward), out hit, 10))
        {
            vertexPos[1] = hit.point;
            RockPieceControler rpc = hit.transform.GetComponent<RockPieceControler>();
            if (rpc && rpc.rockType) setRock(rpc.rockType);
            else setInvalid();
        }
        else {
            setEmpty();
        }

        lineRenderer.SetPositions(vertexPos);
    }

    void setEmpty() {
        screen.SetText("Scanned Nothing");
        image.color = Color.clear;
    }

    void setRock(RockType rt) {
        screen.SetText(rt.typeName);
        image.texture = rt.material.GetTexture("_BaseMap");
        image.color = Color.white;
    }

    void setInvalid() {
        screen.SetText("Invalid Target");
        image.color = Color.clear;
    }
}
