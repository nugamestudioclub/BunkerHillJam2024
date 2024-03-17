using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookComponent : MonoBehaviour
{
    [Serializable]
    private struct SFKVPair
    {
        public IAction.ActionType action;
        public Sprite sprite;
    }

    [SerializeField]
    private IAction.ActionType actionType;

    [SerializeField]
    private SpriteRenderer m_iconRenderer;

    [SerializeField]
    private SFKVPair[] m_sprites;

    [SerializeField]
    private GameObject m_pickupSystem;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent<Animator>(out var animator))
        {
            animator.Play(0, -1, UnityEngine.Random.Range(0f, 1f));
        }

        foreach (SFKVPair pair in m_sprites)
        {
            if (pair.action == actionType)
            {
                m_iconRenderer.sprite = pair.sprite;
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            GameObject g = GameObject.Instantiate(m_pickupSystem, transform.position, Quaternion.identity);
            Destroy(g, 5f);

            if (actionType == IAction.ActionType.Heal)
            {
                other.GetComponent<HealthComponent>().Heal(1);
                this.gameObject.SetActive(false);

                return;
            }

            IAction action = IAction.BuildAction(actionType);
            KeybindHandler.AddKeybind(action);
            this.gameObject.SetActive(false);
        }
    }

}
