using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using TMPro;
using UnityEngine.SceneManagement;


public class KeyController : MonoBehaviour
{
    private static float key_count = 0;
    // Start is called before the first frame update
    public TMP_Text keyText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(key_count >= 5){
            //end game/new level?
        }
        keyText.text = "Keys: " + key_count+"/5";
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player" && key_count<5)
        {
            key_count+=1;
            //Debug.Log("Key # "+key_count+ " Collected!");
            Destroy(this.gameObject);
        }
    }
}
