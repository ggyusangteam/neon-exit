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
        //string strConn = "Data Source=192.168.0.68,1433;Initial Catalog=unity;User ID=User1;Password=1234";
        string strConn = "Data Source=172.30.1.14,1433;Initial Catalog=unity;User ID=User2;Password=1234";
        SqlConnection mssqlconn = new SqlConnection(strConn);
        mssqlconn.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = mssqlconn;

        //select
        cmd.CommandText = "SELECT * FROM score";
        SqlDataReader rd = cmd.ExecuteReader();
        //Debug.Log("================================ 시작 ================================");
        int cnt = 0;
        while (rd.Read())
        {
            cnt++;

            GameObject inst =  Instantiate(rankCell, this.transform);
            GameObject t1 = inst.transform.GetChild(0).gameObject;
            t1.GetComponent<UnityEngine.UI.Text>().text = cnt.ToString();
            GameObject t2 = inst.transform.GetChild(1).gameObject;
            t2.GetComponent<UnityEngine.UI.Text>().text = rd["name"].ToString();
            GameObject t3 = inst.transform.GetChild(2).gameObject;
            t3.GetComponent<UnityEngine.UI.Text>().text = rd["score"].ToString();
            //Debug.Log(rd["name"].ToString() + " " + rd["score"].ToString());
        }
        //Debug.Log("================================ 끝 ================================");
        rd.Close();
		
        //insert
        //cmd.CommandText = "INSERT INTO score(name,score) values('bbb',1000)";
        //cmd.ExecuteNonQuery();

        //db연결 해제
        mssqlconn.Close();
        

    }

}
