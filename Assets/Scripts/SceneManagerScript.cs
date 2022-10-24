using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void LoadAScene(int sceneNum)
    {
        SceneManager.LoadScene(1); // Scene1 will be the debug RandomGeneration
    }

    public void QuitGame() => Application.Quit(); // Only works if running a build // Quits the game

}