using UnityEngine;

public abstract class AHealthViewItem : MonoBehaviour, IHealthViewItem
{
    public virtual void OnGain() { }
    public virtual float OnLose() { return 0f; }
}
