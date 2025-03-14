using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    public UnityEvent OnScoreChanged;


    public void AddScore()
    {

        // Add score and update GameManager
        GameManager.Instance.score += 10;

        // Check if the target score for the current level is reached
        if (GameManager.Instance.score >= GameManager.Instance.targetScore)
        {
            Debug.Log("Win!!");
            SceneManager.LoadScene("Win");
        }

        // Notify listeners about score changes
        OnScoreChanged.Invoke();
    }

}
