using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenuObject;
    public GameObject PlayGameButton;
    public GameObject OptionsButton;
    public GameObject TutorialButton;
    public GameObject QuitButton;
    public GameObject OptionsMenuObject;
    public GameObject ResumeMenuObject;
    public GameObject ResumeButton;
    public GameObject RestartButton;
    public GameObject MainMenuButton;
    public Slider slider;

    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager.PlayMusic(audioManager.menu);
        MainMenuObject.SetActive(true);
        OptionsMenuObject.SetActive(false);
        ResumeMenuObject.SetActive(false);
        slider.value = 0.5f;

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.timeScale == 0)
        {
            ResumeButton.SetActive(true);
            RestartButton.SetActive(true);
            MainMenuButton.SetActive(true);
            OptionsMenuObject.SetActive(false);
            MainMenuObject.SetActive(false);
            ResumeMenuObject.SetActive(true);



        }

        if (Time.timeScale == 1)
        {
            PlayGameButton.SetActive(true);
            OptionsButton.SetActive(true);
            TutorialButton.SetActive(true);
            QuitButton.SetActive(true);
            ResumeMenuObject.SetActive(false);

        }
    }


    public void PlayGame()
    {
        audioManager.PlaySFx(audioManager.click);
        audioManager.StopMusic();
        SceneManager.LoadScene("Level1");

    }

    public void PlayTutorial()
    {
        audioManager.PlaySFx(audioManager.click);
        audioManager.StopMusic();
        SceneManager.LoadScene("TutorialScene 1");
    }


    public void Options()
    {
        MainMenuObject.SetActive(false);
        OptionsMenuObject.SetActive(true);
        audioManager.PlaySFx(audioManager.click);
    }

    public void Exit()
    {
        audioManager.PlaySFx(audioManager.click);
        Application.Quit();
    }

    public void ResumeGame()
    {
        TutorialManager tutorialManager = FindObjectOfType<TutorialManager>();
        if (tutorialManager != null)
        {
            tutorialManager.ResumeGame();
        }

        SceneManager.UnloadSceneAsync("UIMenu");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("UIMenu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "TutorialScene 1")
        {
            SceneManager.LoadScene("TutorialScene 1");
        }

        else if (currentSceneName == "Level1" 
            || currentSceneName == "GameOverScene"
        )
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            SceneManager.LoadScene("UIMenu");
        }

        Time.timeScale = 1;
    }

    public void Back()
    {
        OptionsMenuObject.SetActive(false);
        MainMenuObject.SetActive(true);
        audioManager.PlaySFx(audioManager.click);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = slider.value;
    }
}