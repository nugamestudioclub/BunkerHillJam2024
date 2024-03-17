using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthView : MonoBehaviour
{
    [SerializeField]
    private GameObject m_healthViewPrefab;

    [SerializeField]
    private Transform m_healthContainer;

    private List<AHealthViewItem> m_healthViewItems;

    private void Awake()
    {
        m_healthViewItems = new();
    }

    public void OnHealthChanged(int amount, int new_hp)
    {
        if (m_healthViewItems.Count == new_hp) return;

        int abs_amount = Mathf.Abs(amount);
        bool was_damage = amount < 0;

        for (int i = 0; i < abs_amount; ++i)
        {
            if (was_damage) LoseHealth();
            else AddHealth();
        }
    }

    private void AddHealth()
    {
        GameObject item = GameObject.Instantiate(m_healthViewPrefab, m_healthContainer);
        var health_component = item.GetComponent<AHealthViewItem>();

        health_component.OnGain();
        m_healthViewItems.Add(health_component);
    }

    private void LoseHealth()
    {
        int end = m_healthViewItems.Count - 1;
        var item = m_healthViewItems[end];

        m_healthViewItems.RemoveAt(end);

        // there can technically be a bit of a visual bug here if you take damage then immediately gain HP:
        // the breaking HP will still occupy a space while the new HP is added. The new HP will slide over
        // once the breaking HP vanishes instead of occupying that space originally.
        Destroy(
            item.gameObject, 
            item.OnLose());
    }
}
