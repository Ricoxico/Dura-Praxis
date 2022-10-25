using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtonManager : MonoBehaviour
{
    public int gameStartScene = 1;

    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Aplication.Quit();
#endif
    }
}
