using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance;
//	public static ScoreManager instance;
	public TextMesh score;
	
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
	}

}
