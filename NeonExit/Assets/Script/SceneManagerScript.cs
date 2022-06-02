using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{

     void Start()
    {
        //���� �� �̺�Ʈ�� ����� �ݴϴ�.
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }
    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
     
    }
    public void LoadSceneMortals()
    {
      if(SceneManager.GetActiveScene().name=="SceneMortals") 
      {
            PauseManager.instance.UnPause();
        }
      
        SceneManager.LoadScene("SceneMortals");
    }

    public void LoadSceneMain()
    {
        PauseManager.instance.UnPause();
        SceneManager.LoadScene("Main");
    }
    
    public void Quit()
    {
        Application.Quit();


    }
}
