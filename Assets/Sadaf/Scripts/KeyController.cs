using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class KeyController : MonoBehaviour
{
    private static int keyCount = 0;
    public int totalKeys = 5;
    public TMP_Text keyText;
    public Animator animator;

    public AudioManager manager;
    public GameObject victoryCanvas;
    public float victoryDuration = 5f;
    private bool victoryActivated = false;

    void Start()
    {
        victoryCanvas.SetActive(false);

        //manager = GetComponent<AudioManager>();


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

            // Deactivate victory canvas after specified duration
            Invoke("DeactivateVictoryCanvas", victoryDuration);
        }
        keyText.text = "Keys: " + keyCount + "/" + totalKeys;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && keyCount < totalKeys)
        {
            keyCount++;
            //manager.PlaySFx(manager.collectKey);
            gameObject.SetActive(false);
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

    void DeactivateVictoryCanvas()
    {
        victoryCanvas.SetActive(false);
    }

}
