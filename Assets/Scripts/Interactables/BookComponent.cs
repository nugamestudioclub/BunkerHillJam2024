using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookComponent : MonoBehaviour
{
    
    [SerializeField]
    private IAction.ActionType actionType;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            IAction action = IAction.BuildAction(actionType);
            KeybindHandler.AddKeybind(action);
            this.gameObject.SetActive(false);
        }
    }

}
