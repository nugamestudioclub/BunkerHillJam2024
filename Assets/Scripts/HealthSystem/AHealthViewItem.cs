using UnityEngine;

public abstract class AHealthViewItem : MonoBehaviour, IHealthViewItem
{
    public abstract void OnGain();
    public abstract float OnLose();
}
