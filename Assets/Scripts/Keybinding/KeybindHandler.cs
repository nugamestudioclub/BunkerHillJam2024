using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeybindHandler : MonoBehaviour
{
    private Dictionary<KeySequence, IAction> actionMap = new Dictionary<KeySequence, IAction>();
    [SerializeField]
    private KeyMapping mapping;
    public static KeyMapping Mapping;

    private void _CheckPressed()
    {
        foreach(KeySequence key in actionMap.Keys)
        {
            if (key.IsPressed(Input.GetKeyDown))
            {
                actionMap[key].Act();
            }
        }
    }
    private void _CheckReleased() 
    {
        foreach(KeySequence key in actionMap.Keys)
        {
            if (key.IsPressed(Input.GetKeyUp))
            {
                actionMap[key].EndAct();
            }
        }
    }

    public static KeyCode ChooseRandom(KeyCode k)
    {
        List<KeyCode> keys = new List<KeyCode>();
        List<float> distances = new List<float>();
        List<float> weights = new List<float>();
        List<System.Tuple<float, float>> ranges = new List<System.Tuple<float, float>>();
        
        foreach(KeyCode key in Mapping.Mapping.Keys)
        {
            if (key != k)
            {
                keys.Add(key);
                float dist = Vector2.Distance(Mapping.Mapping[k], Mapping.Mapping[key]);
                distances.Add(dist);
            }
        }
        float maxDist = distances.Max();
        foreach(float dist in distances)
        {
            weights.Add(Mathf.Pow((maxDist - dist) + 1, 2));
        }
        float maxRange = 0;
        foreach(float weight in weights)
        {
            float start = maxRange;
            float end = start + weight;
            ranges.Add(System.Tuple.Create(start, end));
            maxRange = end;
        }
        float val = Random.Range(0, maxRange + 1);
        int i = 0;
        foreach(System.Tuple<float, float> range in ranges)
        {
            if (val > range.Item1 && val <= range.Item2)
            {
                return keys[i];
            }
            i++;
        }
        return KeyCode.None;

    }

    // Start is called before the first frame update
    void Start()
    {
        actionMap = new Dictionary<KeySequence, IAction>();
        Mapping = mapping;
    }

    // Update is called once per frame
    void Update()
    {
        _CheckPressed();
        _CheckReleased();
    }
}
