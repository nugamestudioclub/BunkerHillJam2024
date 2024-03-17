using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;

    private string m_loadingSceneName = "LoadingScene";

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this.gameObject); 

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(string scene_name)
    {
        StartCoroutine(_IEChangeScene(scene_name));
    }

    private IEnumerator _IEChangeScene(string scene_name)
    {
        SceneManager.LoadScene(m_loadingSceneName, LoadSceneMode.Single);

        var operation = SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Single);

        yield return new WaitUntil(() => operation.isDone);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene_name));
    }
}
