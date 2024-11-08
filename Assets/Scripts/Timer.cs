using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeSeconds = 180f; // 시작 시간을 180초로 설정 / this is a 3-min timer so it's 180 sec
    private TextMeshProUGUI timerText; // 텍스트매쉬 프로 UI 컴포넌트 / Gonna use Text mesh pro cuz I really wanna import pixelart-like font 

    void Start()
    {
        // TextMeshProUGUI 컴포넌트를 가져오기 / this code brings Text mesh pro UI component so that 
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // 시간이 0보다 크면 초마다 줄어들게 설정 / if the remaining time is bigger than 0, the timer is going on
        if (timeSeconds > 0)
        {
            timeSeconds -= Time.deltaTime; // 경과 시간만큼 줄어듦 / the numer on the timer is keep reduded
            //This code is changing int to float 
            int minutes = Mathf.FloorToInt(timeSeconds / 60); // 분 계산 / counting minutes
            int seconds = Mathf.FloorToInt(timeSeconds % 60); // 초 계산 / counting seconds

            // 텍스트 업데이트 (mm:ss 형식으로 표시) / this code updates remaining time in min:sec form
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            // 시간이 다 됐을 때 00:00 표시 / if the time's up, the timer shows 00:00
            timerText.text = "00:00";
        }
    }
}
