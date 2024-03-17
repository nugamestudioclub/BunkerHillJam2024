using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : MonoBehaviour, IAction
{
    
    private GameObject projectilePrefab;

    public void Act()
    {
        PlayerController.Shoot();
    }

    public void EndAct()
    {
        
    }

    public string GetActionName()
    {
        return "Shoot";
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
