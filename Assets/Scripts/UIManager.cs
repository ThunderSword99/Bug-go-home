using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject MenuCanvas;

    private void Start()
    {
        MenuCanvas.gameObject.SetActive(false);
    }

    public void TurnOnOffMenuCanvas()
    {
        MenuCanvas.gameObject.SetActive(!MenuCanvas.gameObject.active);
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
