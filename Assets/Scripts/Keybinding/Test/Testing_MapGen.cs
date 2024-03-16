using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_MapGen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                KeyCode code1 = KeybindHandler.ChooseRandom(kcode, KeybindHandler.EmptyKeyCodes);
                KeyCode code2 = KeybindHandler.ChooseRandom(kcode, new List<KeyCode> { code1 });
                print(code1+","+code2);
            }
                
        }
    }
}
