using System.Collections;
using UnityEngine;
using DG.Tweening; // DOTween ���ӽ����̽� �߰�

public class UIGameOverTransition : MonoBehaviour
{
    public RectTransform gameOverUI; // GameOver UI�� RectTransform


    public void OnEnable()
    {
        // DOTween�� ����Ͽ� GameOver UI�� ������ Ȯ���ϴ� ȿ��
        gameOverUI.localScale = Vector3.zero; // ó���� UI�� �۰� ����
        gameOverUI.gameObject.SetActive(true); // UI Ȱ��ȭ
        gameOverUI.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack); // ������ Ȯ��
    }
    /*
    // Start is called before the first frame update
    void Start()
    {
        // ���÷� Start���� GameOver UI�� �����ִ� ���
        OnGameOver();
    }*/
}