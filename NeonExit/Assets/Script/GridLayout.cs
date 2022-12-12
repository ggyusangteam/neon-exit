using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using System.Data;

public class GridLayout : MonoBehaviour
{
    [SerializeField] GameObject rankCell;
    bool flag = false;
    bool getFlag = false;
    private int[] bestScore = new int[100];
    private string[] bestName = new string[100];


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        //PlayerPrefs.SetInt(0 + "BestScore", 101484);
        //PlayerPrefs.SetString("0BestName", "lukmu");
        //PlayerPrefs.SetInt(1 + "BestScore", 100203);
        //PlayerPrefs.SetString("1BestName", "heegyoung");
        //PlayerPrefs.SetInt(2 + "BestScore", 99436);
        //PlayerPrefs.SetString("2BestName", "hi");

        if (getFlag == false)
        {
            getFlag = true;

            if (flag == false)
            {
                flag = true;

                int currentScore = Player.instance.score;
                string currentName = PlayerPrefs.GetString("NickName");

                PlayerPrefs.SetString("CurrentPlayerName", currentName);
                PlayerPrefs.SetInt("CurrentPlayerScore", currentScore);

                int tempScore = 0;
                string tempName = "";

                for (int x = 0; x < 100; x++)
                {
                    bestScore[x] = PlayerPrefs.GetInt(x + "BestScore");
                    bestName[x] = PlayerPrefs.GetString(x + "BestName");

                    while (bestScore[x] < currentScore)
                    {
                        tempScore = bestScore[x];
                        tempName = bestName[x];
                        bestScore[x] = currentScore;
                        bestName[x] = currentName;

                        PlayerPrefs.SetInt(x + "BestScore", currentScore);
                        PlayerPrefs.SetString(x.ToString() + "BestName", currentName);

                        currentScore = tempScore;
                        currentName = tempName;
                    }
                }

                for (int x = 0; x < 100; x++)
                {
                    PlayerPrefs.SetInt(x + "BestScore", bestScore[x]);
                    PlayerPrefs.SetString(x.ToString() + "BestName", bestName[x]);
                }
            }
            if (flag == true)
            {
                GameObject inst = Instantiate(rankCell, this.transform);
                GameObject t1 = inst.transform.GetChild(0).gameObject;
                t1.GetComponent<UnityEngine.UI.Text>().text = "Me";
                GameObject t2 = inst.transform.GetChild(1).gameObject;
                t2.GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetString("CurrentPlayerName");
                GameObject t3 = inst.transform.GetChild(2).gameObject;
                t3.GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetInt("CurrentPlayerScore").ToString();

                for (int x = 0; x < 100; x++)
                {
                    inst = Instantiate(rankCell, this.transform);
                    t1 = inst.transform.GetChild(0).gameObject;
                    t1.GetComponent<UnityEngine.UI.Text>().text = (x + 1).ToString();
                    t2 = inst.transform.GetChild(1).gameObject;
                    t2.GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetString(x.ToString() + "BestName");
                    t3 = inst.transform.GetChild(2).gameObject;
                    t3.GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetInt(x + "BestScore").ToString();
                }
            }
        }




        ////db연결
        //string strConn = "Data Source=;Initial Catalog=unity;User ID=neon;Password=exit";
        //SqlConnection mssqlconn = new SqlConnection(strConn);
        //mssqlconn.Open();
        //SqlCommand cmd = new SqlCommand();
        //cmd.Connection = mssqlconn;

        ////insert
        //cmd.CommandText = "INSERT INTO score(name,score) values('" + PlayerPrefs.GetString("NickName") + "'," + Player.instance.score + ")";
        //cmd.ExecuteNonQuery();

        ////select your score
        //cmd.CommandText = "select * from (SELECT rank() over(order by score desc) ranking, * FROM score) sq where name='" + PlayerPrefs.GetString("NickName") + "' and score=" + Player.instance.score;
        //SqlDataReader rd = cmd.ExecuteReader();

        //if(rd.Read())
        //{
        //    GameObject inst = Instantiate(rankCell, this.transform);
        //    GameObject t1 = inst.transform.GetChild(0).gameObject;
        //    t1.GetComponent<UnityEngine.UI.Text>().text = rd["ranking"].ToString();
        //    GameObject t2 = inst.transform.GetChild(1).gameObject;
        //    t2.GetComponent<UnityEngine.UI.Text>().text = rd["name"].ToString();
        //    GameObject t3 = inst.transform.GetChild(2).gameObject;
        //    t3.GetComponent<UnityEngine.UI.Text>().text = rd["score"].ToString();
        //}
        //rd.Close();

        ////select top 10
        //cmd.CommandText = "SELECT top 10 rank() over(order by score desc) ranking, * FROM score";
        //rd = cmd.ExecuteReader();

        //while(rd.Read())
        //{
        //    GameObject inst = Instantiate(rankCell, this.transform);
        //    GameObject t1 = inst.transform.GetChild(0).gameObject;
        //    t1.GetComponent<UnityEngine.UI.Text>().text = rd["ranking"].ToString();
        //    GameObject t2 = inst.transform.GetChild(1).gameObject;
        //    t2.GetComponent<UnityEngine.UI.Text>().text = rd["name"].ToString();
        //    GameObject t3 = inst.transform.GetChild(2).gameObject;
        //    t3.GetComponent<UnityEngine.UI.Text>().text = rd["score"].ToString();
        //}
        //rd.Close();

        ////db연결 해제
        //mssqlconn.Close();


    }

}
