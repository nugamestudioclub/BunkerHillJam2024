using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : MonoBehaviour, IAction
{
    private CharacterController c;

    void Start()
    {
        this.c = GetComponent<CharacterController>();
    }
    public void Act()
    {
        if (this.c.isGrounded)
        {
            // jump
        }
    }

    public void EndAct()
    {
        // pass
    }
}
