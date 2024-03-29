using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : PersistentSingleton<SceneController>
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ChangeSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
