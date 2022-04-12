using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] List<Material> lineList;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(Player.transform.position.x)
        {
            case float n when (n<= -1.5f):
            for(int i=1; i<lineList.Count;i++)
            {
                    lineList[i].color = Color.black;
                }
                lineList[0].color = new Color(0, 255, 219) * 3;
                break;
            case float n when (n >= -1.5f && n <-0.5f):
                for (int i = 2; i < lineList.Count; i++)
                {
                    lineList[i].color = Color.black;
                }

                lineList[0].color = new Color(0, 255, 219) * 3;
                lineList[1].color = new Color(0, 255, 219) * 3;
                break;
            case float n when (n >= -0.5f && n < 0.5f):
            
                lineList[0].color = Color.black;
                lineList[3].color = Color.black;
                lineList[1].color = new Color(0, 255, 219) * 3;
                lineList[2].color = new Color(0, 255, 219) * 3;
                break;
            case float n when (n >= 0.5f && n < 1.5f):

                lineList[0].color = Color.black;
                lineList[1].color = Color.black;
                lineList[2].color = new Color(0, 255, 219) * 3;
                lineList[3].color = new Color(0, 255, 219) * 3;
                break;
            case float n when (n >= 1.5f ):

                lineList[0].color = Color.black;
                lineList[1].color = Color.black;
                lineList[2].color = Color.black;
                lineList[3].color = new Color(0, 255, 219) * 3;
                break;

        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < lineList.Count; i++)
        {
            lineList[i].color = Color.black;
        }
    }
}
