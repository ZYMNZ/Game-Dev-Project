using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{

    public GameObject PlayAgainButton;
    public GameObject MainMenuButton;
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager.PlayMusic(audioManager.gameOver);
    }
    public void MainMenu()
    {
        audioManager.PlaySFx(audioManager.click);
        SceneManager.LoadScene("UIMenu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }

   public void RestartGame()
    {
        audioManager.PlaySFx(audioManager.click);

        string lastSceneName = PlayerPrefs.GetString("LastScene", "UIMenu");
        SceneManager.LoadScene(lastSceneName);

        Time.timeScale = 1;
    }
}
