using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIEnterYourNameTransition : MonoBehaviour
{
    public RectTransform EnterYourNameUI;
    //public TextMeshProUGUI leaderboard;

    // 게임 오버 시 조건을 추가하는 부분
    public void OnEnable()
    {
        // DOTween을 사용하여 GameOver UI를 서서히 확대하는 효과
        EnterYourNameUI.localScale = Vector3.zero;
        EnterYourNameUI.gameObject.SetActive(true);
        EnterYourNameUI.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack);
        //DisplayLeaderboard();
    }

    // Start is called before the first frame update
   // void Start()
   // {
   //     //leaderboard = GetComponent<TextMeshProUGUI>();
   //     //GameManager.instance.LoadPlayerDataFromJson();
   // }
   //
   // void DisplayLeaderboard()
   // {
   //     //leaderboard.text = ""; // 초기화
   //     //List<PlayerInfo> playerInfos = GameManager.instance.GetPlayerInfos();
   //
   //     //foreach (PlayerInfo playerInfo in playerInfos)
   //     //{
   //     //    leaderboard.text += $"{playerInfo.playerName}: {playerInfo.score}\n";
   //     //}
   // }
}