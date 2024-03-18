using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToggleDifficulty : MonoBehaviour
{
    [SerializeField]
    private DiffultySettings easy;
    [SerializeField]
    private DiffultySettings hard;

    [SerializeField]
    private TMP_Text text;

    bool isEasy = true;

    void Start()
    {
        DifficultySettingsManager.SelectedSetting = easy;
        isEasy = !isEasy;

        text.text = "Toggle to " + (isEasy ? "easy" : "hard") + " mode";
        Debug.Log(DifficultySettingsManager.SelectedSetting.name);
    }

    public void Toggle()
    {
        DifficultySettingsManager.SelectedSetting = isEasy ? easy : hard;
        isEasy = !isEasy;

        text.text = "Toggle to " +( isEasy ? "easy" : "hard") + " mode";
        Debug.Log(DifficultySettingsManager.SelectedSetting.name);
    }
}
