using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMainMenuManager : MonoBehaviour
{
    public void ChangeScene(int _sceneIndex) {

    }
    public void ChangeScene(string _sceneName) {
        SceneManager.LoadScene(_sceneName);
    }
}