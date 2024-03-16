using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
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

    public bool IsPressed(System.Func<KeyCode,bool> downLambda, System.Func<KeyCode,bool> heldLambda)
    {
        bool isPressed = false;
        // Check if one is pressed down.
        foreach(KeyCode keyCode in codes)
        {
            isPressed = isPressed||downLambda(keyCode);
        }

        //Check if all are pressed.
        foreach(KeyCode code in this.codes)
        {
            isPressed = isPressed && heldLambda(code);
        }
        return isPressed;
    }

    public bool IsReleased(System.Func<KeyCode,bool> lambda)
    {
        foreach(KeyCode code in this.codes)
        {
            if (lambda(code))
            {
                return true;
            }
        }
        return false;
    }
    

    /// <summary>
    /// Adds a random key based on keyboard region.
    /// </summary>
    public void AddRandom()
    {
        KeyCode rand = KeybindHandler.ChooseRandom(codes[0], codes);
        codes.Add(rand);
        Debug.Log("Added keycode:" + rand);
    }
}
