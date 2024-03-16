using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeySequence
{

    private List<KeyCode> codes;
    public List<KeyCode> Codes
    {
        get { return this.codes; }
    }

    public KeySequence(KeyCode initCode)
    {
        this.codes = new List<KeyCode>() { initCode };
    }

    public bool IsPressed(System.Func<KeyCode,bool> lambda)
    {
        bool isPressed = true;
        foreach(KeyCode code in this.codes)
        {
            isPressed = isPressed && lambda(code);
        }
        return false;
    }

    /// <summary>
    /// Adds a random key based on keyboard region.
    /// </summary>
    public void AddRandom()
    {
        codes.Add(KeybindHandler.ChooseRandom(codes[0], codes));
    }
}
