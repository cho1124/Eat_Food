using System.Collections;
using UnityEngine;
using DG.Tweening; // DOTween 네임스페이스 추가

public class UIGameOverTransition : MonoBehaviour
{
    public RectTransform gameOverUI; // GameOver UI의 RectTransform


    public void OnEnable()
    {
        // DOTween을 사용하여 GameOver UI를 서서히 확대하는 효과
        gameOverUI.localScale = Vector3.zero; // 처음에 UI를 작게 설정
        gameOverUI.gameObject.SetActive(true); // UI 활성화
        gameOverUI.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack); // 서서히 확대
    }
    /*
    // Start is called before the first frame update
    void Start()
    {
        // 예시로 Start에서 GameOver UI를 보여주는 경우
        OnGameOver();
    }*/
}