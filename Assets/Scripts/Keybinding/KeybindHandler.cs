using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class KeybindHandler : MonoBehaviour
{
    private Dictionary<KeySequence, IAction> actionMap = new Dictionary<KeySequence, IAction>();
    private List<IAction> loadedActions = new List<IAction>();

    [SerializeField]
    private KeyMapping mapping;
    public static KeyMapping Mapping;
    private static KeybindHandler instance;

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

    private static int _ManhattanDistance(Vector2 v1, Vector2 v2)
    {
        return Mathf.RoundToInt(Mathf.Abs(v2.y - v1.y) + Mathf.Abs(v2.x - v1.x));
    }
    
    public static List<KeyCode> EmptyKeyCodes = new List<KeyCode>();
    /// <summary>
    /// Chooses randomly from keys near the selected key, excluding specified keys.
    /// </summary>
    /// <param name="k">Key being selected (excluded from random selection)</param>
    /// <param name="excluded">Keys excluded</param>
    /// <returns>Keycode</returns>
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
                int dist = _ManhattanDistance(Mapping.Mapping[k], Mapping.Mapping[key]);
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
    public static KeyCode ChooseRandom()
    {
        List<KeyCode> keys = Mapping.Mapping.Keys.ToList();
        int range = keys.Count;
        int sel = Random.Range(0, range);
        return keys[sel];
    }
    /// <summary>
    /// Appends a keybind to all keybind sequences.
    /// </summary>
    public void AppendKeybindSeq()
    {
        foreach (KeySequence ks in actionMap.Keys)
        {
            ks.AddRandom();
        }
    }
    /// <summary>
    /// Given an action, appends a keybind to it.
    /// </summary>
    /// <param name="a"></param>
    public void AppendKeybind(IAction a)
    {
        instance.actionMap.FirstOrDefault(x => x.Value == a).Key.AddRandom();
    }
    
    public static void AddKeybind(IAction action)
    {
        if (!instance.loadedActions.Contains(action))
        {
            KeyCode k = ChooseRandom();
            instance.AppendKeybindSeq();
            KeySequence ks = new KeySequence(k);
            instance.actionMap.Add(ks, action);
            print("Added keybind:" + action + " with keybind " + k);
            instance.loadedActions.Add(action);
        }
        else
        {
            instance.AppendKeybind(action);
        }

        foreach(KeySequence ks in instance.actionMap.Keys)
        {
            string name = instance.actionMap[ks].GetActionName();
            UIActionDisplay.UpdateAction(name, ks.Codes);
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        actionMap = new Dictionary<KeySequence, IAction>();
        Mapping = mapping;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        _CheckPressed();
        _CheckReleased();
    }
}
