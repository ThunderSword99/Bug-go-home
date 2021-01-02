using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject MenuCanvas;

    private void Start()
    {
        if (MenuCanvas!=null)
        {
            MenuCanvas.gameObject.SetActive(false);
        }
    }

    public void TurnOnOffMenuCanvas()
    {
        MenuCanvas.gameObject.SetActive(!MenuCanvas.gameObject.active);
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Back()
    {
        SceneManager.LoadScene("HomeLobby");
    }

    public void Next()
    {
    
    }

    public void Retry()
    {
    
    }

    public void Menu()
    {
        SceneManager.LoadScene("HomeStage");
    }
}
