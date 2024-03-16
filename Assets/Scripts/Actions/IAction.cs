using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    /// <summary>
    /// Perform the action.
    /// </summary>
    void Act();
    /// <summary>
    /// Complete the performed action.
    /// </summary>
    void EndAct();
}
