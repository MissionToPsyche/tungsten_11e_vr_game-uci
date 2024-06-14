using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightFrame : Frame
{
    public Outline objectOutline;

    public override void Enter() {
        if (objectOutline != null) {
            objectOutline.enabled = true;
        }
        base.Enter();
    }

    public override void Exit() {
        if (objectOutline != null) {
            objectOutline.enabled = false;
        }
        base.Exit();
    }
}
