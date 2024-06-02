using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public AudioManager audioManager;
    private AudioListener mainAudioListener;

    private bool isMusicPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        audioManager.PlayMusic(audioManager.level1);
        mainAudioListener = FindObjectOfType<AudioListener>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale > 0)
            {
                // Pause the game
                Time.timeScale = 0;
                audioManager.PauseMusic(audioManager.level1);
  
                if (mainAudioListener != null)
                {
                    mainAudioListener.enabled = false;
                }
                SceneManager.LoadScene("UIMenu", LoadSceneMode.Additive);
                isMusicPaused = true;
            }
            else
            {
                // Resume the game
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (isMusicPaused)
        {
            audioManager.ResumeMusic();
            isMusicPaused = false;
        }

        if (mainAudioListener != null)
        {
            mainAudioListener.enabled = true;
        }
        SceneManager.UnloadSceneAsync("UIMenu");
    }
}