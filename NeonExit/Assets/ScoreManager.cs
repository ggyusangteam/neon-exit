using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
//	public static ScoreManager instance;
	public TextMesh score;
	
	// Start is called before the first frame update
	void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
		score.text = Player.instance.score.ToString();
	}
}
