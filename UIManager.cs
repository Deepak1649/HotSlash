using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject failMenu;

    [SerializeField]
    private GameObject winMenu;

    private LevelManager levelManager;

   

    private void Start()
    {
        levelManager = Object.FindObjectOfType<LevelManager>();
        failMenu.SetActive(false);
        winMenu.SetActive(false);
    }


    public void Fail()
    {
        failMenu.SetActive(true);  
    }

    public void Win()
    {
        winMenu.SetActive(true);
        
    }

    public void RestartPressed()
    {
        levelManager.ReloadLevel();
    }

    public void ContinuePressed()
    {
        levelManager.NextLevel();
    }
}
