using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    // Singleton Instance
    public static GameManager Instance { get; private set; }

    [Header("Player Settings")]
    public int score;
    public int targetScore;
    public ScoreBarUI currentScoreUI;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip gameplayMusic;
    private AudioSource audioSource;
    [Range(0f, 1f)] public float musicVolume = 0.5f;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip killSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    private void Awake()
    {
        // Ensure a single GameManager instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = musicVolume;
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene load events
        currentScoreUI = GetComponent<ScoreBarUI>();

    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        // Destroy the player instance to ensure it doesn't persist into the main menu
        ResetGame();
        UpdateScoreUI();
        //AssignCameraTarget();
    }

    public void ResetGame()
    {
        score = 0;
        targetScore = 100; 
        UpdateScoreUI();
 

    }
    public void UpdateScoreUI()
    {
        ScoreBarUI currentScoreUI = FindObjectOfType<ScoreBarUI>();
        if (currentScoreUI != null)
        {
            currentScoreUI.UpdateScoreBar();
        }
    }


    // Sound function
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        //if (audioSource.clip == clip) return; // Avoid restarting same song


        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void PlayShootSound() => PlaySound(shootSound);
    public void PlayKillSound() => PlaySound(killSound);
    public void PlayWinSound() 
    {
        StopMusic();
        PlaySound(winSound);
    }
    public void PlayLoseSound()
    {
        StopMusic();
        PlaySound(loseSound);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            PlayMusic(mainMenuMusic, true); 
        }
        if (scene.name == "Level1")
        {
            PlayMusic(gameplayMusic, true);
            UpdateScoreUI();
        }
        if (scene.name == "Fail")
        {            
            PlayLoseSound();          
        }
        if (scene.name == "Win")
        {             
            PlayWinSound();          
        }
    }

}
