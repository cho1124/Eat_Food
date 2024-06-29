/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // DOTween ���ӽ����̽� �߰�
using TMPro;
public class UIGameOverTransition : MonoBehaviour
{
    public RectTransform gameOverUI; // GameOver UI�� RectTransform
    //public GameObject leaderboard_obj;
    public TextMeshProUGUI leaderboard;
    private List<PlayerInfo> playerinfo;

    public void OnEnable()
    {


        // DOTween�� ����Ͽ� GameOver UI�� ������ Ȯ���ϴ� ȿ��
        gameOverUI.localScale = Vector3.zero; // ó���� UI�� �۰� ����
        gameOverUI.gameObject.SetActive(true); // UI Ȱ��ȭ
        gameOverUI.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack); // ������ Ȯ��


        //leaderboard_obj = GameObject.Find("Leaderboard");
        leaderboard = GameObject.Find("BestScore").GetComponentInChildren<TextMeshProUGUI>();
        leaderboard.text = $"<align=center>���\t\tĳ����\t\t�̸�\t\t����</align>\n";
        playerinfo = GameManager.instance.GetPlayerInfos();
        playerinfo.Sort((x,y) => y.score.CompareTo(x.score));

        //int count = playerinfo.Count < 3 ? playerinfo.Count : 3;
        

        for(int i = 0; i < playerinfo.Count; i++)
        {
            leaderboard.text += $"<align=center>{i+1}��\t\t{playerinfo[i].characterName}\t\t{playerinfo[i].playerName}\t\t{playerinfo[i].score}\n</align>";
            //leaderboard.text += ("<align=center><tr><td>{0}��</td><td>{1}</td><td>{2}</td><td>{3}</td></tr></align>",i+1, playerinfo[i].characterName, playerinfo[i].playerName, playerinfo[i].score);
        }
    }
    /*
    // Start is called before the first frame update
    void Start()
    {
        // ���÷� Start���� GameOver UI�� �����ִ� ���
        OnGameOver();
    }
}*/

using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIGameOverTransition : MonoBehaviour
{
    public RectTransform gameOverUI; // GameOver UI�� RectTransform
    public TextMeshProUGUI leaderboard;
    private List<PlayerInfo> playerinfo;

    public void OnEnable()
    {
        // DOTween�� ����Ͽ� GameOver UI�� ������ Ȯ���ϴ� ȿ��
        gameOverUI.localScale = Vector3.zero; // ó���� UI�� �۰� ����
        gameOverUI.gameObject.SetActive(true); // UI Ȱ��ȭ
        gameOverUI.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack); // ������ Ȯ��

        leaderboard = GameObject.Find("BestScore").GetComponentInChildren<TextMeshProUGUI>();
        leaderboard.text = $"<align=center>���\t\tĳ����\t\t�̸�\t\t����</align>\n";
        playerinfo = GameManager.instance.GetPlayerInfos();
        playerinfo.Sort((x, y) => y.score.CompareTo(x.score));

        // �� ���� �ʺ� �����ϰ� ���߱� ���� ���� ���ڿ�
        string format = "{0,-6} {1,-10} {2,-20} {3,10}\n";

        if (playerinfo.Count < 12)
        {
            for (int i = 0; i < playerinfo.Count; i++)
            {
                leaderboard.text += string.Format(format, "\t" + (i + 1) + "��", "\t" + playerinfo[i].characterName, "\t" + playerinfo[i].playerName, "\t" + playerinfo[i].score);
            }
        }
        else
        {
            for (int i = 0; i < 12; i++)
            {
                leaderboard.text += string.Format(format, "\t" + (i + 1) + "��", "\t" + playerinfo[i].characterName, "\t" + playerinfo[i].playerName, "\t" + playerinfo[i].score);
            }
        }

    }
}