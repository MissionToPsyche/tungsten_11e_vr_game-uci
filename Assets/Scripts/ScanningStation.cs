using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScanningStation : MonoBehaviour
{
    private GameObject currentObj = null;
    public TMP_Text screen;
    public RawImage image;

    private void Start() {
        screen.SetText("Scanned Nothing");
        image.enabled = false;
    }

    private void InvalidState() {
        image.enabled = false;
        screen.SetText("Invalid Target");
    }

    private void SetState(RockPieceControler rpc) {
        RockType rt = rpc.rockType;
        if (!rt) InvalidState();
        screen.SetText(rt.typeName);
        image.enabled = true;
        image.texture = rt.material.GetTexture("_BaseMap");
        image.color = Color.white;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (currentObj) InvalidState();
        else {
            currentObj = other.gameObject;
            RockPieceControler rpc = other.GetComponent<RockPieceControler>();
            if (rpc && rpc.rockType) SetState(rpc);
            else InvalidState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentObj) currentObj = null;
    }
}
