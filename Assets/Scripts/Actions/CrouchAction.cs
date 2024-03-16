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
            player.transform.localScale = player.transform.localScale + Vector3.down * 0.25f;
            player.GetComponent<CharacterController>().height /= 2;
            player.GetComponent<CharacterController>().radius /= 2;
        }
    }

    public void EndAct()
    {
        if (isCrouching)
        {
            isCrouching = false;
            player.transform.localScale = originalHitboxSize;
            player.GetComponent<CharacterController>().height *= 2f;
            player.GetComponent<CharacterController>().radius *= 2f;
        }
    }
    public string GetActionName()
    {
        return "Crouch";
    }

    void Start()
    {
        player = GetComponent<PlayerController>();
    }
}
