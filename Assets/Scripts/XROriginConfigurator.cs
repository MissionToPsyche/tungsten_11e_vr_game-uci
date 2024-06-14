using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class XROriginConfigurator : MonoBehaviour
{
    public Transform recenterPos;
    public Transform cameraOffset;
    private XROrigin xrOrigin;

    private void Start()
    {
        xrOrigin = GetComponent<XROrigin>();
        if (PersistentSettings.centeredPos != Vector3.zero) {
            xrOrigin.Origin.transform.position = PersistentSettings.centeredPos;
        }
        setHeight(PersistentSettings.heightOffset);
    }

    public void Recenter()
    {
        Vector3 newPos = recenterPos.position + new Vector3(0, xrOrigin.CameraInOriginSpaceHeight, 0);
        xrOrigin.MoveCameraToWorldLocation(newPos);
        PersistentSettings.centeredPos = xrOrigin.Origin.transform.position;
    }

    public void setHeight(float height) 
    {
        Vector3 newPos = new Vector3(cameraOffset.position.x, height, cameraOffset.position.z);
        cameraOffset.position = newPos;
        PersistentSettings.heightOffset = height;
    }
}
