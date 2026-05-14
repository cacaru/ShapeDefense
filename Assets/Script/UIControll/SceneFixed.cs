using UnityEngine;
using UnityEngine.UI;

public class SceneFixed : MonoBehaviour {

    private CanvasScaler thisCanvas;

    private void Start() {
        thisCanvas = GetComponent<CanvasScaler>();
        SetResolution(); // 초기에 게임 해상도 고정
        // 게임 화면이 꺼지지 않게 수정
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    /* 해상도 설정하는 함수 */
    public void SetResolution() {
        
        //Default 해상도 비율
        float fixedAspectRatio = 9f / 16f;

        //현재 해상도의 비율
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        //현재 해상도 가로 비율이 더 길 경우
        if (currentAspectRatio > fixedAspectRatio) thisCanvas.matchWidthOrHeight = 0;
        //현재 해상도의 세로 비율이 더 길 경우
        else if (currentAspectRatio < fixedAspectRatio) thisCanvas.matchWidthOrHeight = 1;

        /*
        int setWidth = 1080; // 사용자 설정 너비
        int setHeight = 2160; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장
        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);

        // 기기의 해상도 비가 더 큰 경우
        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) 
        {
            // 새로운 너비
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight);
            // 새로운 Rect 적용
            Camera.main.rect = new Rect((newWidth) / 2f, 0f, newWidth, 1f); 
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            // 새로운 높이
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight);
            // 새로운 Rect 적용
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); 
        }
        */
    }
}