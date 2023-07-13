using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int currentLevel;
    public GameObject MainMenu;
    public GameObject Settings;

    public GameObject FadeInCanvas;


    public void NextLevel()
    {
        FadeInCanvas.gameObject.SetActive(true);
        StartCoroutine(LoadNew());
    }

    public void ReloadLevel()
    {
        FadeInCanvas.gameObject.SetActive(true);
        StartCoroutine(Reload());
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public IEnumerator LoadNew()
    {
        yield return new WaitForSeconds(2f);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    
    }

     public IEnumerator Reload()
    {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    yield return new WaitForSeconds(2f);
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