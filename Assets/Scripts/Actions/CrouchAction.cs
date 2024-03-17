using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchAction : MonoBehaviour, IAction
{
    public void Act()
    {
        if (!PlayerController.IsCrouching())
        {
            PlayerController.Crouch();
        }
    }

    public void EndAct()
    {
        if (PlayerController.IsCrouching())
        {
            PlayerController.Uncrouch();
        }
    }
    public string GetActionName()
    {
        return "Crouch";
    }
}
