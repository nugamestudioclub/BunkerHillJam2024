public interface IHealthViewItem
{
    void OnGain();
    float OnLose(); // returns time till deletion
}
