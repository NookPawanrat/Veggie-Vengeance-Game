using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGameBTN()
    {
        GameManager.Instance.ResetGame();
        StartCoroutine(LoadSceneWithDelay("Level1"));
    }

    public void QuitGameBTN()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

    public void RestartBTN()
    {
        GameManager.Instance.ResetGame();
        StartCoroutine(LoadSceneWithDelay("Level1"));
    }

    public void HomeBTN()
    {
        StartCoroutine(LoadSceneWithDelay("Menu"));
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(0.1f); // Small delay to ensure scene loads properly
        SceneManager.LoadScene(sceneName);
    }
}
