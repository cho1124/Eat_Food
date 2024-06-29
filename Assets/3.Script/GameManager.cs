using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerInfo
{
    public string playerName;
    public string characterName;
    public int score;
    public GameObject GameOver_Obj;

    public PlayerInfo(string playerName, string characterName, int score)
    {
        this.playerName = playerName;
        this.characterName = characterName;
        this.score = score;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int selectedCharacter = 0;

    private string filePath;
    public int playerScore;
    private List<PlayerInfo> playerinfo = new List<PlayerInfo>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
            Debug.Log(Application.persistentDataPath);

            LoadPlayerDataFromJson();

        }
        else
        {
            Destroy(gameObject);

        }

    }

    private void Start()
    {
        Debug.Log("Scene Start");
    }


    private void Update()
    {
        //테스트용 키
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerinfo.Add(new PlayerInfo("Player1", "a", 100)); // 예시 데이터 추가
            SavePlayerDataToJson();
            Debug.Log("Player data saved.");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadPlayerDataFromJson();
            Debug.Log("Player data loaded.");
        }
    }

    private void LoadAll()
    {


    }

    public void SavePlayerData(string playerName, string characterType)
    {
        playerinfo.Add(new PlayerInfo(playerName, characterType, playerScore));
    }


    public void SavePlayerDataToJson()
    {
        PlayerDataListWrapper wrapper = new PlayerDataListWrapper(playerinfo);
        string jsonData = JsonUtility.ToJson(wrapper);
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");
        File.WriteAllText(path, jsonData);

    }


    public void LoadPlayerDataFromJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            PlayerDataListWrapper wrapper = JsonUtility.FromJson<PlayerDataListWrapper>(jsonData);
            playerinfo = wrapper.playerDataList;
        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
    }

}

[System.Serializable]
public class PlayerDataListWrapper
{
    public List<PlayerInfo> playerDataList;

    public PlayerDataListWrapper(List<PlayerInfo> playerDataList)
    {
        this.playerDataList = playerDataList;
    }
}
