using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Button startButton; // Start 버튼 / Start button
    public Button retryButton; // Retry 버튼 / Retry button
    public TextMeshProUGUI timerText; // 타이머 텍스트 (TextMeshPro 사용) / Timer text (using TextMeshPro)
    public GameObject player; // 플레이어 오브젝트 / Player object

    public float timeSeconds = 180f; // 타이머 시작 시간 (3분) / Timer start time (3 minutes)
    private bool gameIsRunning = false; // 게임이 실행 중인지 여부 / Whether the game is running

    void Start()
    {
        // 버튼 클릭 이벤트 리스너 추가 / Add button click event listeners
        startButton.onClick.AddListener(StartGame);
        retryButton.onClick.AddListener(RetryGame);

        // 초기 설정 / Initial setup
        retryButton.gameObject.SetActive(false); // 시작 시 리트라이 버튼 숨기기 / Hide retry button at the start
        player.SetActive(false); // 시작 전 플레이어 숨기기 / Hide player before the game starts
        timerText.text = "3:00"; // 타이머 초기 텍스트 설정 / Set initial timer text
    }

    void Update()
    {
        // 게임이 실행 중일 때만 타이머 업데이트 / Update timer only when the game is running
        if (gameIsRunning && timeSeconds > 0)
        {
            timeSeconds -= Time.deltaTime; // 경과 시간만큼 줄어듦 / Decrease time by elapsed time
            int minutes = Mathf.FloorToInt(timeSeconds / 60); // 남은 분 계산 / Calculate remaining minutes
            int seconds = Mathf.FloorToInt(timeSeconds % 60); // 남은 초 계산 / Calculate remaining seconds

            // 타이머 텍스트를 "3:00" 형식으로 표시 / Display timer in "3:00" format
            timerText.text = minutes + ":" + seconds.ToString("00");

            // 시간이 다 되었을 때 / When time runs out
            if (timeSeconds <= 0)
            {
                EndGame();
            }
        }
    }

    void StartGame()
    {
        // 게임 시작 시 필요한 설정 / Setup when the game starts
        gameIsRunning = true; // 게임 실행 중 플래그 설정 / Set game running flag
        timeSeconds = 180f; // 타이머 초기화 / Reset timer
        startButton.gameObject.SetActive(false); // 시작 버튼 숨기기 / Hide start button
        retryButton.gameObject.SetActive(false); // 리트라이 버튼 숨기기 / Hide retry button
        player.SetActive(true); // 플레이어 활성화 / Activate player
    }

    void EndGame()
    {
        // 게임 종료 시 호출 / Called when the game ends
        gameIsRunning = false; // 게임 실행 중 플래그 해제 / Unset game running flag
        retryButton.gameObject.SetActive(true); // 리트라이 버튼 보이기 / Show retry button
        player.SetActive(false); // 플레이어 비활성화 / Deactivate player
        timerText.text = "00:00"; // 타이머를 00:00으로 설정 / Set timer to 00:00
    }

    void RetryGame()
    {
        // 리트라이 버튼을 눌렀을 때 게임 재시작 / Restart game when retry button is pressed
        timeSeconds = 180f; // 타이머 초기화 / Reset timer
        gameIsRunning = true; // 게임 실행 중 플래그 설정 / Set game running flag
        retryButton.gameObject.SetActive(false); // 리트라이 버튼 숨기기 / Hide retry button
        player.SetActive(true); // 플레이어 활성화 / Activate player
    }
}
