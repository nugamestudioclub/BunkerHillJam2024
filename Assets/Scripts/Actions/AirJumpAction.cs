using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJumpAction : MonoBehaviour, IAction
{

    private bool canAirJump = false;

    public void Act()
    {
        if (canAirJump && !PlayerController.IsGrounded())
        {
            PlayerController.Jump();
            canAirJump = false;
        }
    }

    public void EndAct()
    {
        // pass
    }

    public string GetActionName()
    {
        return "Air Jump";
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.IsGrounded() && !canAirJump)
        {
            canAirJump = true;
        }
    }
}
