using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject MainGUI;

    // Update is called once per frame
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
     public void getPing()
    {
       
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
    }  
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        MainGUI.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        MainGUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void GoToMainMenu()
    {
        //SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }    
}
