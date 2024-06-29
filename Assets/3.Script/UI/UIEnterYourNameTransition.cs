using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIEnterYourNameTransition : MonoBehaviour
{
    public RectTransform EnterYourNameUI;
    //public TextMeshProUGUI leaderboard;

    // ���� ���� �� ������ �߰��ϴ� �κ�
    public void OnEnable()
    {
        // DOTween�� ����Ͽ� GameOver UI�� ������ Ȯ���ϴ� ȿ��
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
   //     //leaderboard.text = ""; // �ʱ�ȭ
   //     //List<PlayerInfo> playerInfos = GameManager.instance.GetPlayerInfos();
   //
   //     //foreach (PlayerInfo playerInfo in playerInfos)
   //     //{
   //     //    leaderboard.text += $"{playerInfo.playerName}: {playerInfo.score}\n";
   //     //}
   // }
}