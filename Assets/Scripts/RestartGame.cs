using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private int firstScene;

    private void Start()
    {
        firstScene = SceneManager.sceneCountInBuildSettings - SceneManager.sceneCountInBuildSettings;
    }
    public void Restart()
    {
        SceneManager.LoadScene(firstScene);
    }
}
