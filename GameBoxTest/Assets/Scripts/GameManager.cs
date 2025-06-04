using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void LoadMap()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNotebook()
    {
        SceneManager.LoadScene(1);
    }
}
