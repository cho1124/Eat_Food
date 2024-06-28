using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    void Start()
    {
        // ���� �� ���̵� �� �ִϸ��̼� ����
        StartCoroutine(FadeIn());
    }

    public void SceneTrans(string nextScene)
    {
        // ��ư Ŭ�� �� �ִϸ��̼� �� �� ��ȯ ����
        StartCoroutine(FadeOutAndChangeScene(nextScene));
    }

    IEnumerator FadeIn()
    {
        // �ʱ� ���� ���� 1�� �����ϰ� ������ 0���� ���� (���̵� ��)
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, 1f);
        yield return new WaitForSeconds(1f); // �ִϸ��̼� ���
    }

    IEnumerator FadeOutAndChangeScene(string nextScene)
    {
        // ���̵� �ƿ� �ִϸ��̼� (���� ���� 0���� 1��)
        canvasGroup.DOFade(1, 1f);
        yield return new WaitForSeconds(1f); // �ִϸ��̼� ���

        // �� ��ȯ
        SceneManager.LoadScene(nextScene); // ��ȯ�� �� �̸����� ����
    }
}
