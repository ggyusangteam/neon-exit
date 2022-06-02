using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    public GameObject PauseUi;
    public AudioSource audio;
    public static bool paused;
   
    // Start is called before the first frame update
    private void Awake()
    {

   
        instance = this;
    }
    void Start()
    {
        PauseUi.SetActive(false);
     
    }

    public void Pause()
    {
        if (paused == false)
        {
            Time.timeScale = 0;
            PauseUi.SetActive(true);
            audio.Pause();
            paused = true;
           
        }
        else { UnPause(); }



    }
    public void UnPause()
    {
     
        Time.timeScale = 1f;
        PauseUi.SetActive(false);
        audio.UnPause();
        paused = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Pause();
            }
        }
    }
}
