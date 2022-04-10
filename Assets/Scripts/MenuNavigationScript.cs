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
        FindObjectOfType<AudioManagerScript>().Play("sfx_select");
        targetPanel.SetActive(true);
        hostPanel.SetActive(false);
    }

    public void LoadNewScene()
    {
        StartCoroutine(NextScene());
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    IEnumerator NextScene() {
        FindObjectOfType<AudioManagerScript>().StopAll();
        if (sceneToLoad == "MainGame") {
            FindObjectOfType<AudioManagerScript>().Play("sfx_bell");
            yield return new WaitForSeconds(2);
            FindObjectOfType<AudioManagerScript>().Play("bgm_level");
        }
        if (sceneToLoad == "MainMenu") {
            FindObjectOfType<AudioManagerScript>().Play("sfx_select");
            yield return new WaitForSeconds(1);
            FindObjectOfType<AudioManagerScript>().Play("bgm_main");
        }
        SceneManager.LoadScene(sceneToLoad);
    }
}
