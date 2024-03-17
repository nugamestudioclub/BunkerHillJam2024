using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainSystem : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_system;

    private void Awake()
    {
        m_system = GetComponent<ParticleSystem>();
    }

    public void OnDamage(int amount, int _)
    {
        if (amount >= 0) return;
        m_system.Play();
    }
}
