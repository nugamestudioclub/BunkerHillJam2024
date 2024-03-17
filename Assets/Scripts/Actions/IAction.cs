using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    public enum ActionType { Jump, Crouch, AirJump, DashHorizontal, FireFlower, Heal }
    /// <summary>
    /// Perform the action.
    /// </summary>
    void Act();
    /// <summary>
    /// Complete the performed action.
    /// </summary>
    void EndAct();

    string GetActionName();

    public static IAction BuildAction(ActionType type)
    {
        IAction a;
        GameObject player = PlayerController.Instance.gameObject;
        switch (type)
        {
            case ActionType.Jump:
                {
                    a = player.GetComponent<JumpAction>();
                    if (a == null)
                    {
                        a = player.AddComponent<JumpAction>();
                        
                    }
                    break;
                }
            case ActionType.Crouch:
                {
                    a = player.GetComponent<CrouchAction>();
                    if (a == null)
                    {
                        a = player.AddComponent<CrouchAction>();
                        
                    }
                    break;
                }
            case ActionType.AirJump:
            case ActionType.DashHorizontal:
                a = null;
                break;
            case ActionType.FireFlower:
                {
                    a = player.GetComponent<ShootAction>();
                    if (a == null)
                    {
                        a= player.AddComponent<ShootAction>();
                    }
                    break;
                }
            case ActionType.Heal:
            default:
                a = null;
                return a;
                
        }
        return a;
    }
}
