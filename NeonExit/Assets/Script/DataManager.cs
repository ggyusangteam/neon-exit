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
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;

            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
            //gameObject�����ε� �� ��ũ��Ʈ�� ������Ʈ�μ� �پ��ִ� Hierarchy���� ���ӿ�����Ʈ��� ��������, 
            //���� �򰥸� ������ ���� this�� �ٿ��ֱ⵵ �Ѵ�.
           
        }
        else
        {
            //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� GameMgr�� ������ ���� �ִ�.
            //�׷� ��쿣 ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ���� �� ����.
            //�׷��� �̹� ���������� instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� GameMgr)�� �������ش�.
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
    
