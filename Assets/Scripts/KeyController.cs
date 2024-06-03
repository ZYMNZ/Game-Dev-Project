using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class KeyController : MonoBehaviour
{
    private static int keyCount = 0;
    public int totalKeys;
    public TMP_Text keyText;
    public Animator animator;
    public AudioManager manager;
    public GameObject victoryCanvas;
    public float victoryDuration = 5f;
    public float restartAfterDelay = 30f;
    private bool victoryActivated = false;

    void Start()
    {
        // Ensure everything is reset when the scene starts
        ResetGame();
    }

    void Update()
    {
        if (keyCount >= totalKeys && !victoryActivated)
        {
            // Trigger victory pose animation
            DestroyAllEnemies();
            manager.PlayMusicInLoop(manager.victory, true);
            animator.SetTrigger("victory");

            // Activate victory canvas
            victoryCanvas.SetActive(true);
            victoryActivated = true;

            Invoke("DisablePanel", victoryDuration);
            Invoke("RestartGame", restartAfterDelay);
        }
        keyText.text = "Keys: " + keyCount + "/" + totalKeys;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && keyCount <= totalKeys)
        {
            keyCount++;
            Debug.Log(keyCount);
            Debug.Log("-----");
            Debug.Log(totalKeys);
            gameObject.SetActive(false);
            manager.PlaySFx(manager.collectKey);
        }
    }

    void ResetGame()
    {
        // Reset key count
        keyCount = 0;

        // Reset victory panel
        victoryCanvas.SetActive(false);
        victoryActivated = false;

        // Reinitialize animator
        animator.Rebind();

        // Reactivate enemies
        ReactivateEnemies();

        // Reset any other necessary components
    }

    void ReactivateEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }

    void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    void DisablePanel()
    {
        victoryCanvas?.SetActive(false);
    }

    public void RestartGame()
    {

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "TutorialScene 1")
        {
            SceneManager.LoadScene("TutorialScene 1");
        }

        else if (currentSceneName == "Level1")
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            SceneManager.LoadScene("UIMenu");
        }

        Time.timeScale = 1;
    }


}
