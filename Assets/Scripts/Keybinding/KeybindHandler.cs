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
            if (key.IsPressed(Input.GetKeyDown,Input.GetKey))
            {
                actionMap[key].Act();
            }
        }
    }
    private void _CheckReleased() 
    {
        foreach(KeySequence key in actionMap.Keys)
        {
            if (key.IsReleased(Input.GetKey))
            {
                actionMap[key].EndAct();
            }
        }
    }

    private static int ManhattanDistance(Vector2 v1, Vector2 v2)
    {
        return Mathf.RoundToInt(Mathf.Abs(v2.y - v1.y) + Mathf.Abs(v2.x - v1.x));
    }

    public static List<KeyCode> EmptyKeyCodes = new List<KeyCode>();
    public static KeyCode ChooseRandom(KeyCode k, List<KeyCode> excluded)
    {
        List<KeyCode> keys = new List<KeyCode>();
        List<float> distances = new List<float>();
        List<float> weights = new List<float>();
        List<System.Tuple<float, float>> ranges = new List<System.Tuple<float, float>>();
        foreach(KeyCode key in Mapping.Mapping.Keys)
        {
            if (key != k && !excluded.Contains(key))
            {
                keys.Add(key);
                int dist = ManhattanDistance(Mapping.Mapping[k], Mapping.Mapping[key]);
                //float dist = Vector2.Distance(Mapping.Mapping[k], Mapping.Mapping[key]);
                distances.Add(dist);
            }
        }
        float maxDist = distances.Max();
        foreach(float dist in distances)
        {
            weights.Add(Mathf.Pow((maxDist - dist) + 1, 10));
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
