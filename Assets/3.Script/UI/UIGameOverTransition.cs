/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // DOTween 네임스페이스 추가
using TMPro;
public class UIGameOverTransition : MonoBehaviour
{
    public RectTransform gameOverUI; // GameOver UI의 RectTransform
    //public GameObject leaderboard_obj;
    public TextMeshProUGUI leaderboard;
    private List<PlayerInfo> playerinfo;

    public void OnEnable()
    {


        // DOTween을 사용하여 GameOver UI를 서서히 확대하는 효과
        gameOverUI.localScale = Vector3.zero; // 처음에 UI를 작게 설정
        gameOverUI.gameObject.SetActive(true); // UI 활성화
        gameOverUI.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack); // 서서히 확대


        //leaderboard_obj = GameObject.Find("Leaderboard");
        leaderboard = GameObject.Find("BestScore").GetComponentInChildren<TextMeshProUGUI>();
        leaderboard.text = $"<align=center>등수\t\t캐릭터\t\t이름\t\t점수</align>\n";
        playerinfo = GameManager.instance.GetPlayerInfos();
        playerinfo.Sort((x,y) => y.score.CompareTo(x.score));

        //int count = playerinfo.Count < 3 ? playerinfo.Count : 3;
        

        for(int i = 0; i < playerinfo.Count; i++)
        {
            leaderboard.text += $"<align=center>{i+1}등\t\t{playerinfo[i].characterName}\t\t{playerinfo[i].playerName}\t\t{playerinfo[i].score}\n</align>";
            //leaderboard.text += ("<align=center><tr><td>{0}등</td><td>{1}</td><td>{2}</td><td>{3}</td></tr></align>",i+1, playerinfo[i].characterName, playerinfo[i].playerName, playerinfo[i].score);
        }
    }
    /*
    // Start is called before the first frame update
    void Start()
    {
        // 예시로 Start에서 GameOver UI를 보여주는 경우
        OnGameOver();
    }
}*/

using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIGameOverTransition : MonoBehaviour
{
    public RectTransform gameOverUI; // GameOver UI의 RectTransform
    public TextMeshProUGUI leaderboard;
    private List<PlayerInfo> playerinfo;

    public void OnEnable()
    {
        // DOTween을 사용하여 GameOver UI를 서서히 확대하는 효과
        gameOverUI.localScale = Vector3.zero; // 처음에 UI를 작게 설정
        gameOverUI.gameObject.SetActive(true); // UI 활성화
        gameOverUI.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack); // 서서히 확대

        leaderboard = GameObject.Find("BestScore").GetComponentInChildren<TextMeshProUGUI>();
        leaderboard.text = $"<align=center>등수\t\t캐릭터\t\t이름\t\t점수</align>\n";
        playerinfo = GameManager.instance.GetPlayerInfos();
        playerinfo.Sort((x, y) => y.score.CompareTo(x.score));

        // 각 열의 너비를 일정하게 맞추기 위한 포맷 문자열
        string format = "{0,-6} {1,-10} {2,-20} {3,10}\n";

        if (playerinfo.Count < 12)
        {
            for (int i = 0; i < playerinfo.Count; i++)
            {
                leaderboard.text += string.Format(format, "\t" + (i + 1) + "등", "\t" + playerinfo[i].characterName, "\t" + playerinfo[i].playerName, "\t" + playerinfo[i].score);
            }
        }
        else
        {
            for (int i = 0; i < 12; i++)
            {
                leaderboard.text += string.Format(format, "\t" + (i + 1) + "등", "\t" + playerinfo[i].characterName, "\t" + playerinfo[i].playerName, "\t" + playerinfo[i].score);
            }
        }

    }
}