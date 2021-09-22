using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panelPause;

    public void pauseControl()
    {
        if (Time.timeScale == 1){
            panelPause.SetActive (true);
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
            panelPause.SetActive (false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene() .buildIndex);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Back()
    {
        SceneManager.LoadScene(1);
    }

    public void Next()
    {
        SceneManager.LoadScene(2);
    }
}

