using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
	public Button mainButton = null;
	public Button retryButton = null;

    void Start()
    {
		this.mainButton.onClick.AddListener(SceneManagerScript.instance.LoadSceneMain);
		this.retryButton.onClick.AddListener(SceneManagerScript.instance.LoadSceneMortals);	
    }
	public void GamOverPanelActive(bool active)
	{
		this.gameObject.SetActive(active);
	}



}
