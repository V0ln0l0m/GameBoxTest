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
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadNotebook()
    {
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadHouse()
    {
        SceneManager.LoadScene(2);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
