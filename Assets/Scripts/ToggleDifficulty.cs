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

    bool isEasy = false;

    void Start()
    {
        DifficultySettingsManager.SelectedSetting = easy;
        isEasy = !isEasy;

        text.text = "Toggle to " + (isEasy ? "hard" : "easy") + " mode";
    }

    public void Toggle()
    {
        DifficultySettingsManager.SelectedSetting = isEasy ? easy : hard;
        isEasy = !isEasy;

        text.text = "Toggle to " +( isEasy ? "hard" : "easy") + " mode";
    }
}
