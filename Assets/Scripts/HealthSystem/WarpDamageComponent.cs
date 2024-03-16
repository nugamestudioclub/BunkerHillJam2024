using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpDamageComponent : DamageComponent
{
    [SerializeField]
    private Transform m_warpAnchor;

    protected override void DealDamageTo(HealthComponent component)
    {
        base.DealDamageTo(component);

        // make this prettier with animations and stuff

        component.transform.position = m_warpAnchor.position;
    }
}
