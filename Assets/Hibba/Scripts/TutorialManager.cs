using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {
        audioManager.PlayMusic(audioManager.level1);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
                SceneManager.LoadScene("UIMenu", LoadSceneMode.Additive);
            }
        }
    }

}