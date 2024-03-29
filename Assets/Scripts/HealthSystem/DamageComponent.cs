using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField]
    private int m_damageAmount = 1;

    private int m_prevColliderUID;
    private HealthComponent m_cachedComponent;

    public void OnTriggerEnter(Collider collision)
    {
        int id = collision.gameObject.GetInstanceID();

        HealthComponent component = (id == m_prevColliderUID) ? m_cachedComponent : null;

        if (component || collision.gameObject.TryGetComponent<HealthComponent>(out component))
        {
            DealDamageTo(component);

            m_cachedComponent = component;
            m_prevColliderUID = id;
        }
    }

    protected virtual void DealDamageTo(HealthComponent component)
    {
        component.Damage(m_damageAmount);
    }
}
