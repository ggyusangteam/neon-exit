using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
	public static UIManager instance;

	[SerializeField] private ResultPanel resultPanel = null;
	[SerializeField] private GameOverPanel gameoverPanel = null;

	[SerializeField] public GameObject backPanel = null;
	[SerializeField] private GameObject rankingPanel = null;

	private void Awake()
	{
		instance = this;
	}

	public void ActiveResultPanel(bool active, string score=null, string maxCombo = null)
	{
		if (!backPanel.activeSelf)
		{
			this.backPanel.SetActive(true);
		}
		if(active)
		{
			this.resultPanel.Active(active);
			this.resultPanel.Initialize(score, maxCombo);
		}
		this.resultPanel.Active(active);
	}

	public void ActiveRankingPanel(bool active)
	{
	    if (active)
		{
			this.rankingPanel.SetActive(true);
		    this.resultPanel.Active(false);
		}
		else
		{
			this.rankingPanel.SetActive(false);
			this.resultPanel.Active(true);
		}
	}
	public void ActiveGameOverPanel(bool active)
	{
	if(!backPanel.activeSelf)
	{
			this.backPanel.SetActive(true);
			
		}
		gameoverPanel.GamOverPanelActive(active);
	}

}
