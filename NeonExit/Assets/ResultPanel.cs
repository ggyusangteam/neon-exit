using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
	public TextMesh scoreText = null;
	public TextMesh maxCombo = null;



	public Button main = null;
	public Button retry = null;
	public Button ranking = null;

	public void Initialize(string score,string maxCombo)
	{
		this.SetScore(score);
		this.SetMaxCombo(maxCombo);
	}
	private void SetScore(string score)
	{
		this.scoreText.text = score;
	}
	private void SetMaxCombo(string maxCombo)
	{
		this.maxCombo.text = maxCombo;
	}
	
	public void Active(bool active)
	{
		this.gameObject.SetActive(active);
	}


	private void Start()
	{
		this.main.onClick.AddListener(SceneManagerScript.instance.LoadSceneMain);
		this.retry.onClick.AddListener(SceneManagerScript.instance.LoadSceneMortals);
		this.ranking.onClick.AddListener( ()=>UIManager.instance.ActiveRankingPanel(true));
	}


}
