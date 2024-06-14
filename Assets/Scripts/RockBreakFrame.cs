using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBreakFrame : HighlightFrame
{
    public string rockName;
    public override void Enter() {
        GameObject rock = GameObject.Find(rockName);
        objectOutline = rock.GetComponent<Outline>();
        rock.GetComponent<RockController>().RockPieceBroken.AddListener(Next);
        base.Enter();
    }
}
