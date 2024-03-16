using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : MonoBehaviour, IAction
{
    private PlayerController player;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }
    public void Act()
    {
        if (PlayerController.IsGrounded())
        {
            PlayerController.Jump();
        }
    }

    public void EndAct()
    {
        // pass
    }
}
