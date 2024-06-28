using System.Collections;
using UnityEngine;
using DG.Tweening; // DOTween ���ӽ����̽� �߰�

public class UIEnterYourNameTransition : MonoBehaviour
{
    public RectTransform EnterYourNameUI; // GameOver UI�� RectTransform

    // ���� ���� �� ������ �߰��ϴ� �κ�
    public void UIEnterYourName()
    {
        // DOTween�� ����Ͽ� GameOver UI�� ������ Ȯ���ϴ� ȿ��
        EnterYourNameUI.localScale = Vector3.zero; // ó���� UI�� �۰� ����
        EnterYourNameUI.gameObject.SetActive(true); // UI Ȱ��ȭ
        EnterYourNameUI.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack); // ������ Ȯ��
    }

    // Start is called before the first frame update
    void Start()
    {
        // ���÷� Start���� GameOver UI�� �����ִ� ���
        UIEnterYourName();
    }
}