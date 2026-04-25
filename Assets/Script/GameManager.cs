using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject readyText;
    public GameObject startText;
    public GameObject gameOverText;

    // 플레이어가 움직일 수 있는지 체크하는 변수
    public bool isGameStart = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(GameFlowRoutine());
    }

    IEnumerator GameFlowRoutine()
    {
        // 1. 준비 상태: 텍스트 띄우고 플레이어 정지
        isGameStart = false;
        readyText.SetActive(true);
        yield return new WaitForSeconds(2.0f); // 2초 대기
        readyText.SetActive(false);

        // 2. 시작 상태: 텍스트 띄우고 플레이어 이동 허용
        startText.SetActive(true);
        isGameStart = true; // 여기서부터 움직임 가능!

        // 3. 1초 뒤에 시작 텍스트 숨기기
        yield return new WaitForSeconds(1.0f);
        startText.SetActive(false);
    }

    public void OnGameOver()
    {
        isGameStart = false; // 죽었을 때도 못 움직이게 막음
        gameOverText.SetActive(true);
        // Time.timeScale = 0; // 필요하다면 전체 시간 정지
    }
}