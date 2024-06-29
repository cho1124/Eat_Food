using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using TMPro;

public class ButtonControl : MonoBehaviour
{
    public GameObject[] player;
    public Scene currentScene;
    public TextMeshProUGUI info;
    public SceneTransition sct;

    // 플레이어 이름 넣는 칸
    public TMP_InputField playerNameInput;
    public string playerName;



    
    // 씬 트렌지션할때 잠깐 대기시키는 코루틴
    private IEnumerator Scene_Transition_co()
    {


        yield return new WaitForSeconds(1f);

    }
  
    private void Awake()
    {
        sct = GameObject.Find("TransitionImage").GetComponent<SceneTransition>() ;
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
        ShowCharInfo();
    }
    //----------------------------title--------------------------------------//

    public void PlayButton()
    {
        sct.SceneTrans("SelectChar");
        //SceneManager.LoadScene("SelectChar");
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    //-----------------------------titleEnd---------------------------------------//


    //-----------------------------------------SceneChar--------------------------------------//

    public void ShowCharInfo()
    {
        if(GameManager.instance.selectedCharacter==0)
        {
            info.text = "마술사 유령\n\n마술사 유령입니다\n슬라이딩을 할 수 있습니다";

        }
        else if (GameManager.instance.selectedCharacter == 1)
        {
            info.text = "바이킹 유령\n\n바이킹 유령입니다\n몸의 크기를 키울 수 있습니다";
        }
        else
        {
            info.text = "빌더 유령\n\n빌더 유령입니다\n파괴된 블록을 재생할 수 있습니다";
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
        sct.SceneTrans("SampleScene");
        //SceneManager.LoadScene("SampleScene");
    }
    //-----------------------------------------SceneCharEnd--------------------------------------//


    //------------------------------------GameOverUIButton----------------------------------//

    public void RetryButton()
    {
        sct.SceneTrans("SampleScene");
        //SceneManager.LoadScene("SampleScene");
    }

    public void ReturnToTitleButton()
    {
        sct.SceneTrans("Title");
        SceneManager.LoadScene("Title");
    }



    public void EnterNameButton()
    {
        playerName = playerNameInput.GetComponent<TMP_InputField>().text;
        
        Debug.Log(playerName);

        GameObject gameover = GameObject.Find("Canvas").transform.Find("GameOverPanel").gameObject;//.gameObject.SetActive(true);//GameObject.Find("GameOverPanel");
        GameObject enterYourName = GameObject.Find("EnterYourName");
        gameover.SetActive(true);
        enterYourName.SetActive(false);
        
    }

}


