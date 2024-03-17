using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySettingsManager : MonoBehaviour
{
    public static DiffultySettings SelectedSetting;
    [SerializeField]
    private DiffultySettings _settings;
    // Start is called before the first frame update
    void Start()
    {
        SelectedSetting = _settings;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
