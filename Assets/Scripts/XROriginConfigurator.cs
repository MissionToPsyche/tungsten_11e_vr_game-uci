using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class XROriginConfigurator : MonoBehaviour
{
    public Transform recenterPos;
    public Transform cameraOffset;
    private XROrigin xrOrigin;

    // TODO: Load settings from static class
    private void Start()
    {
        xrOrigin = GetComponent<XROrigin>();
    }

    public void Recenter()
    {
        Vector3 newPos = recenterPos.position + new Vector3(0, xrOrigin.CameraInOriginSpaceHeight, 0);
        xrOrigin.MoveCameraToWorldLocation(newPos);
    }

    public void setHeight(float height) 
    {
        Vector3 newPos = new Vector3(cameraOffset.position.x, height, cameraOffset.position.z);
        cameraOffset.position = newPos;
    }
}
