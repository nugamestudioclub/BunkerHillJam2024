using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private int m_maxHP = 3;
    private int m_currentHP;

    [SerializeField]
    private UnityEvent<int, int> OnHealthChanged; // arg1: change amount, arg2: new hp

    [SerializeField]
    private UnityEvent OnDeath;

    private void Start()
    {
        m_currentHP = m_maxHP;

        OnHealthChanged.Invoke(m_maxHP, m_currentHP);
    }

    private void _ChangeHP(int amount)
    {
        if (m_currentHP <= 0 || amount == 0) return;

        m_currentHP += amount;

        OnHealthChanged.Invoke(amount, m_currentHP);

        if (m_currentHP <= 0) OnDeath.Invoke();
    }

    public void Heal(int amount) => _ChangeHP(amount);
    public void Damage(int amount) => _ChangeHP(-amount);
}
