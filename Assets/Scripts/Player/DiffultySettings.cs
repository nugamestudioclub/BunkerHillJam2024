using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DiffultySetting",menuName ="Difficulty/NewDifficultySettings",order =1)]
public class DiffultySettings : ScriptableObject
{
    [SerializeField]
    private string settingsName = "Default";
    [SerializeField]
    private int maxKeybinds = 3;
    public string SettingName {  get { return settingsName; } }
    public int MaxKeybinds { get { return maxKeybinds; } }
    
}
