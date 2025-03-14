using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarUI : MonoBehaviour
{
    [SerializeField] private Image scoreBarUI;

    private void Awake()
    {
        // Check if scoreBarUI is assigned
        if (scoreBarUI == null)
        {
            Debug.LogError("ScoreBarUI: The Image for the score bar is not assigned in the Inspector.");
            return;
        }

        UpdateScoreBar();
    }

    public void UpdateScoreBar()
    {
        // Ensure GameManager.Instance is valid and values are non-zero
        if (GameManager.Instance == null || GameManager.Instance.targetScore <= 0)
        {
            Debug.LogError("ScoreBarUI: GameManager instance or target score is invalid.");
            return;
        }

        // Calculate the percentage (float division)
        float remainingHealthPercentage = (float)GameManager.Instance.score / GameManager.Instance.targetScore;

        // Ensure the value is between 0 and 1
        remainingHealthPercentage = Mathf.Clamp01(remainingHealthPercentage);

        // Update the UI fill amount
        scoreBarUI.fillAmount = remainingHealthPercentage;

        // Debug the percentage value
        Debug.Log($"Score Percentage: {remainingHealthPercentage * 100}%");
    }
}
