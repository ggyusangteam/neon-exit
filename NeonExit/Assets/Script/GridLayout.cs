using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using System.Data;

public class GridLayout : MonoBehaviour
{
    [SerializeField] GameObject rankCell;

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
        //db연결
        string strConn = "Data Source=125.142.26.54,9896;Initial Catalog=unity;User ID=neon;Password=exit";
        SqlConnection mssqlconn = new SqlConnection(strConn);
        mssqlconn.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = mssqlconn;

        //insert
        cmd.CommandText = "INSERT INTO score(name,score) values('" + PlayerPrefs.GetString("NickName") + "'," + Player.instance.score + ")";
        cmd.ExecuteNonQuery();

        //select your score
        cmd.CommandText = "select * from (SELECT rank() over(order by score desc) ranking, * FROM score) sq where name='" + PlayerPrefs.GetString("NickName") + "' and score=" + Player.instance.score;
        SqlDataReader rd = cmd.ExecuteReader();

        if(rd.Read())
        {
            GameObject inst = Instantiate(rankCell, this.transform);
            GameObject t1 = inst.transform.GetChild(0).gameObject;
            t1.GetComponent<UnityEngine.UI.Text>().text = rd["ranking"].ToString();
            GameObject t2 = inst.transform.GetChild(1).gameObject;
            t2.GetComponent<UnityEngine.UI.Text>().text = rd["name"].ToString();
            GameObject t3 = inst.transform.GetChild(2).gameObject;
            t3.GetComponent<UnityEngine.UI.Text>().text = rd["score"].ToString();
        }
        rd.Close();

        //select top 10
        cmd.CommandText = "SELECT top 10 rank() over(order by score desc) ranking, * FROM score";
        rd = cmd.ExecuteReader();

        while(rd.Read())
        {
            GameObject inst = Instantiate(rankCell, this.transform);
            GameObject t1 = inst.transform.GetChild(0).gameObject;
            t1.GetComponent<UnityEngine.UI.Text>().text = rd["ranking"].ToString();
            GameObject t2 = inst.transform.GetChild(1).gameObject;
            t2.GetComponent<UnityEngine.UI.Text>().text = rd["name"].ToString();
            GameObject t3 = inst.transform.GetChild(2).gameObject;
            t3.GetComponent<UnityEngine.UI.Text>().text = rd["score"].ToString();
        }
        rd.Close();

        //db연결 해제
        mssqlconn.Close();
        

    }

}
