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

        if(characterName == "0")
        {
            this.characterName = "마술사 유령";
        }
        else if(characterName == "1")
        {
            this.characterName = "바이킹 유령";
        }
        else if(characterName == "2")
        {
            this.characterName = "빌더 유령\t";
        }


        //this.characterName = characterName;
        this.score = score;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int selectedCharacter = 0;

    private string filePath;
    public int playerScore;
    public List<PlayerInfo> playerinfo = new List<PlayerInfo>();
    public AudioSource ShootaudioSource;
    public AudioSource ImpactaudioSource;
    public AudioClip ShootClip;
    public AudioClip ImpactClip;
    [Range(0, 1)]
    public float globalVolume = 0.5f;  // 전역 볼륨 조절 변수

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
        ShootaudioSource = gameObject.AddComponent<AudioSource>();
        ShootaudioSource.volume = globalVolume;

        ImpactaudioSource = gameObject.AddComponent<AudioSource>();
        ImpactaudioSource.volume = globalVolume;

    }

    


    public void PlaySound(AudioSource SelectedSource, AudioClip clip)
    {
        if (clip != null)
        {
            // 현재 재생 중인 오디오 클립을 중지합니다.
            if (SelectedSource.isPlaying)
            {
                SelectedSource.Stop();
            }

            // 새로운 오디오 클립을 재생합니다.
            SelectedSource.PlayOneShot(clip);
        }
    }

    public void SavePlayerData(string playerName, string characterType)
    {
        playerinfo.Add(new PlayerInfo(playerName, characterType, playerScore));
    }


    public void SavePlayerDataToJson()
    {
        PlayerDataListWrapper wrapper = new PlayerDataListWrapper(playerinfo);
        string jsonData = JsonUtility.ToJson(wrapper,true);
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

    public List<PlayerInfo> GetPlayerInfos()
    {
        return playerinfo;
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
