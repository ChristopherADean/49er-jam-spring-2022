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
        FindObjectOfType<AudioManagerScript>().Play("sfx_bell");
        yield return new WaitForSeconds(3);
        FindObjectOfType<AudioManagerScript>().Play("bgm_level");
        SceneManager.LoadScene(sceneToLoad);
    }
}
