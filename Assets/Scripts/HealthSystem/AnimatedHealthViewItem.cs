using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedHealthViewItem : AHealthViewItem
{
    public override void OnGain()
    {
        // todo
        Debug.Log("HP Gained.");
    }

    public override float OnLose()
    {
        // todo
        Debug.Log("HP Lost.");
        return 0f;
    }
}
