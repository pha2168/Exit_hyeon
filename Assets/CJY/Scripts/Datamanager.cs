using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;


public class PlayerData
{
    //저장할 플레이어 관련 데이터
    public string name;
    public int level;
    public int coin;
    public int item;

}

public class Datamanager : MonoBehaviour
{
    //singleton
    public static Datamanager Instance;

    PlayerData nowPlayer = new PlayerData();

    string path;
    string filename = "save";

    private void Awake()
    {
        #region singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        DontDestroyOnLoad(Instance.gameObject);
        #endregion

        path = Application.persistentDataPath + "/";
    }

    void Start()
    {
       string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path + filename, data);

        SaveData();

        LoadData();

        Debug.Log(path);
        
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + filename, data );
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + filename);
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }
}
