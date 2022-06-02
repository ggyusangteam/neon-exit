using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    private static string savePath => Application.persistentDataPath + "/saves/";
    public static DataManager instance=null;
    public GameObject manager;
    public  float volume;

    // Start is called before the first frame update
    private void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
           
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
            Destroy(this.gameObject);
        }

   
        Load("player_Setting");
      
    }


    void Start()
    {
       
    }

    public void Save(SaveData saveData, string saveFileName)
    {
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        string saveJson = JsonUtility.ToJson(saveData);

        string saveFilePath = savePath + saveFileName + ".json";
        File.WriteAllText(saveFilePath, saveJson);
   //     Debug.Log("Save Success: " + saveFilePath);
    }

    public SaveData Load(string saveFileName)
    {
        string saveFilePath = savePath + saveFileName + ".json";

        if (!File.Exists(saveFilePath))
        {
            Debug.LogError("No such saveFile exists");
            SaveData playerData = new SaveData(DataManager.instance.volume);
            Save(playerData, "player_Setting");

        }

        string saveFile = File.ReadAllText(saveFilePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(saveFile);
        DataManager.instance.volume = saveData.volume;
        return saveData;
    }

    // Update is called once per frame
    void Update()
    {

    }
	private void OnApplicationQuit()
	{
        SaveData playerData= new SaveData(DataManager.instance.volume);
        Save(playerData, "player_Setting");

    }
}


[System.Serializable]
public class SaveData
{
    public float volume;
    public SaveData(float _volume)
    {
        volume = _volume;
    }
}
    
