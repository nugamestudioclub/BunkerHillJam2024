using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadComponent : MonoBehaviour
{
    [SerializeField]
    private string m_sceneName;

    public void LoadScene()
    {
        LevelLoader.Instance.ChangeScene(m_sceneName);
    }
}
