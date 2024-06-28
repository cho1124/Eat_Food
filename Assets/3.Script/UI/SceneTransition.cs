using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    void Start()
    {
        // 시작 시 페이드 인 애니메이션 실행
        StartCoroutine(FadeIn());
    }

    public void SceneTrans(string nextScene)
    {
        // 버튼 클릭 시 애니메이션 및 씬 전환 실행
        StartCoroutine(FadeOutAndChangeScene(nextScene));
    }

    IEnumerator FadeIn()
    {
        // 초기 알파 값을 1로 설정하고 서서히 0으로 변경 (페이드 인)
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, 1f);
        yield return new WaitForSeconds(1f); // 애니메이션 대기
    }

    IEnumerator FadeOutAndChangeScene(string nextScene)
    {
        // 페이드 아웃 애니메이션 (알파 값을 0에서 1로)
        canvasGroup.DOFade(1, 1f);
        yield return new WaitForSeconds(1f); // 애니메이션 대기

        // 씬 전환
        SceneManager.LoadScene(nextScene); // 전환할 씬 이름으로 변경
    }
}
