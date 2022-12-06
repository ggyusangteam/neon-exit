using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance;
//	public static ScoreManager instance;
	public TextMesh score;
	public TextMesh combo;

	public int maxCombo = 0;
	
	// Start is called before the first frame update
	void Start()
    {
		instance = this;
	}

    // Update is called once per frame
    void Update()
    {
	
	}
	public void UpdateScore()
	{
		score.text = Player.instance.score.ToString();
	


		///ÄÞº¸
		if (Player.instance.comboCnt>100)
		{
			combo.color = Color.red;
		}
		else
		{
			combo.color = Color.white;
		}
		combo.text = $"{Player.instance.comboCnt.ToString()}";

		if (maxCombo<Player.instance.comboCnt)
		{
			maxCombo = Player.instance.comboCnt;
		}
	}

}
