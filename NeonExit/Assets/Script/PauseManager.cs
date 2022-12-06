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
		Debug.Log(paused);
        if (paused == false)
        {
            Time.timeScale = 0;
			UIManager.instance.backPanel.SetActive(true);
            PauseUi.SetActive(true);
            audio.Pause();
            paused = true;
           
        }
        else { UnPause(); }
    }
	public void DeadPause(  )
	{
		Time.timeScale = 0;
		audio.Pause();
		UIManager.instance.ActiveGameOverPanel(true);
	}
	public void EndPause(int score,int combo)
	{
		Time.timeScale = 0;
		audio.Pause();
		UIManager.instance.ActiveResultPanel(true, score.ToString(), combo.ToString());
	}
    public void UnPause()
    {
     
        Time.timeScale = 1f;
		UIManager.instance.backPanel.SetActive(false);
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
