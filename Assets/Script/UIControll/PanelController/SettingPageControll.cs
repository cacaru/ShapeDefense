using UnityEngine;
using UnityEngine.SceneManagement;
using static ShapeDefenseSpace.GameData;

public class SettingPageControll : MonoBehaviour {
    // 인게임 설정 화면 오브젝트 저장
    [SerializeField] private GameObject settingCanvas;
    [SerializeField] private GameObject ExitCheck;
    // 설정 화면 이동
    public void SettingOnClick() {
        settingCanvas.SetActive(true);
    }

    // 설정 나가기
    public void SettingExitClick() {
        settingCanvas.SetActive(false);
        ExitCheck.SetActive(false);
    }


    public void GameExitClick() {
        // 확인창 띄우기
        ExitCheck.SetActive(true);
    }

    public void GameExit() {
        // 현 게임 데이터 초기화
        datahub.GameStatInit();
        SceneManager.LoadScene("GameStartScene");
    }

}
