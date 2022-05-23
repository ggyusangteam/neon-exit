using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{


    public void LoadSceneMortals()
    {
        PauseManager.instance.UnPause();
        SceneManager.LoadScene("SceneMortals");
    }

    public void Quit()
    {
        Application.Quit();


    }
}
