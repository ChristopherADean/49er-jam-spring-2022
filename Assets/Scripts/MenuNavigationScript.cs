using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigationScript : MonoBehaviour
{
    [SerializeField] private GameObject hostPanel;
    [SerializeField] private GameObject targetPanel;
    [SerializeField] private string sceneToLoad;

    public void LoadNewMenu()
    {
        targetPanel.SetActive(true);
        hostPanel.SetActive(false);
    }

    public void LoadNewScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
