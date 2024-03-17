using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public class KeySequence
{
    private List<float> timeSincePress;
    private List<KeyCode> codes;
    public List<KeyCode> Codes
    {
        get { return this.codes; }
    }
    private int length;
    public int Length { get { return this.length; } }


    public KeySequence(KeyCode initCode)
    {
        this.codes = new List<KeyCode>() { initCode };
        timeSincePress = new List<float>() {0f};
        length = 1;
    }

    public bool IsPressed(System.Func<KeyCode,bool> downLambda, System.Func<KeyCode,bool> heldLambda)
    {
        bool atLeastOneKeyDown = false; // To check if at least one key is pressed down.
        bool allKeysHeld = true; // Start with true, and check if any key does not meet the criteria.

        int index = 0;
        foreach (KeyCode keyCode in codes)
        {
            bool down = downLambda(keyCode);
            if (down)
            {
                atLeastOneKeyDown = true; // At least one key is pressed down.
                timeSincePress[index] = Time.time; // Update the press time for this key.
            }
            else if (!(heldLambda(keyCode) || (Time.time - timeSincePress[index]) < 0.5f))
            {
                allKeysHeld = false; // If any key is not held down and not within the time limit, set to false.
            }
            index++;
        }

        // Return true if at least one key is pressed down and all keys are held down or were pressed recently.
        return atLeastOneKeyDown && allKeysHeld;
    }

    public bool IsReleased(System.Func<KeyCode,bool> lambda)
    {
        foreach(KeyCode code in this.codes)
        {
            if (!lambda(code))
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
        timeSincePress.Add(0);
        Debug.Log("Added keycode:" + rand);
        this.length++;
    }
}
