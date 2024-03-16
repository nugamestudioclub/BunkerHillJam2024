using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyMapping", menuName = "KeyMapping/NewKeyMapping", order = 1)]
public class KeyMapping : ScriptableObject
{
    [SerializeField]
    private KeyMap[] m_KeyMap;
    private Dictionary<KeyCode, Vector2> mapping;
    public Dictionary<KeyCode,Vector2> Mapping { get
        {
            if (mapping==null)
            {
                foreach(KeyMap k in m_KeyMap)
                {
                    mapping.Add(k.KeyCode, k.position);
                }
            }
            
            return mapping;
            
        } 
    }

    [System.Serializable]
    private class KeyMap
    {
        public KeyCode KeyCode;
        public Vector2 position;
    }
}
