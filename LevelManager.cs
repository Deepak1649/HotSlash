using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int currentLevel;
    public GameObject MainMenu;
    public GameObject Settings;


    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void exit()
    {
        
     Application.Quit();
    Debug.Log("Exit Pressed.");
    }

    public void SettingsPressed()
    {
    Settings.SetActive(true);
    MainMenu.SetActive(false);
    }

    public void BackSettingsPressed()
    {
    Settings.SetActive(false);
    MainMenu.SetActive(true);
    }

}