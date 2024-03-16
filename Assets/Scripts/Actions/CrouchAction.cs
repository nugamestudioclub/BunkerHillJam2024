using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchAction : MonoBehaviour, IAction
{
    private PlayerController player;

    private Vector3 originalHitboxSize;

    private bool isCrouching = false;

    public void Act()
    {
        if (!isCrouching)
        {
            isCrouching = true;
            originalHitboxSize = player.transform.localScale;
            player.transform.localScale = player.transform.localScale + Vector3.down * 0.5f;
        }
    }

    public void EndAct()
    {
        if (isCrouching)
        {
            isCrouching = false;
            player.transform.localScale = originalHitboxSize;
        }
    }

    void Start()
    {
        player = GetComponent<PlayerController>();
    }
}
