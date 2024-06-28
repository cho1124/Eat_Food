using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class ButtonControl : MonoBehaviour
{
    public GameObject[] player;
    public Scene currentScene;
    public TextMeshProUGUI info;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "SelectChar")
        {
            player = new GameObject[3];

            // 비활성화된 오브젝트 포함하여 모든 오브젝트 찾기
            var allObjects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.hideFlags == HideFlags.None).ToArray();
            info = GameObject.FindObjectOfType<TextMeshProUGUI>();

            for (int i = 1; i < 4; i++)
            {
                player[i - 1] = allObjects.FirstOrDefault(obj => obj.name == "Player" + i.ToString());
                if (player[i - 1] == null)
                {
                    Debug.LogError("Player" + i.ToString() + " not found!");
                }
            }
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("SelectChar");
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }



    //-----------------------------------------SceneChar--------------------------------------//

    public void ShowCharInfo()
    {
        if(GameManager.instance.selectedCharacter==0)
        {
            info.text = "플레이어 1\n\n플레이어1입니다\n설명을 추가해주세요";

        }
        else if (GameManager.instance.selectedCharacter == 1)
        {
            info.text = "플레이어 2\n\n플레이어2입니다\n설명을 추가해주세요";
        }
        else
        {
            info.text = "플레이어 3\n\n플레이어3입니다\n설명을 추가해주세요";
        }
    }




    public void LeftButton()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is not set!");
            return;
        }

        Debug.Log("Current selected character: " + GameManager.instance.selectedCharacter);

        if (GameManager.instance.selectedCharacter == 0)
        {
            player[GameManager.instance.selectedCharacter]?.SetActive(false);
            GameManager.instance.selectedCharacter = 2;
            player[GameManager.instance.selectedCharacter]?.SetActive(true);
            ShowCharInfo();
        }
        else
        {
            player[GameManager.instance.selectedCharacter]?.SetActive(false);
            GameManager.instance.selectedCharacter--;
            player[GameManager.instance.selectedCharacter]?.SetActive(true);
            ShowCharInfo();
        }
    }






    public void RightButton()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is not set!");
            return;
        }

        Debug.Log("Current selected character: " + GameManager.instance.selectedCharacter);

        if (GameManager.instance.selectedCharacter == 2)
        {
            player[GameManager.instance.selectedCharacter]?.SetActive(false);
            GameManager.instance.selectedCharacter = 0;
            player[GameManager.instance.selectedCharacter]?.SetActive(true);
            ShowCharInfo();
        }
        else
        {
            player[GameManager.instance.selectedCharacter]?.SetActive(false);
            GameManager.instance.selectedCharacter++;
            player[GameManager.instance.selectedCharacter]?.SetActive(true);
            ShowCharInfo();
        }
    }

    public void ConfirmButton()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is not set!");
            return;
        }

        Debug.Log("Confirming character: " + GameManager.instance.selectedCharacter);
        SceneManager.LoadScene("SampleScene");
    }
    //-----------------------------------------SceneCharEnd--------------------------------------//

}