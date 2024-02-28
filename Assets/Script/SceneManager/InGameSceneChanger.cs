using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameSceneChanger : MonoBehaviour
{
    // 인게임 설정 화면 오브젝트 저장
    [SerializeField]
    public GameObject settingCanvas;

    /// <summary>
    /// 내부 정보 초기화를 위한 datahub
    /// </summary>
    private DataHub datahub;


    void Start() {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
    }
    // 설정 화면 이동
    public void SettingOnClick() {
        settingCanvas.SetActive(true);
    }

    // 설정 나가기
    public void SettingExitClick() {
        settingCanvas.SetActive(false);
    }
    // 게임 나가기 == 로비로 이동 == gameStartPage로 이동
    public void GameExitClick() {
        // 현 게임 데이터 초기화
        ResetData();
        SceneManager.LoadScene("GameStartScene");
    }

    private void ResetData() {
        Debug.Log("data reset");
        
        // 강화도 초기화
        datahub.UpgradeNoValue = 0;
        datahub.UpgradeAdValue = 0;
        datahub.UpgradeLuValue = 0;
        datahub.UpgradeLiValue = 0;
        datahub.UpgradeCuValue = 0;

        // 게임 내 재화 초기화
        datahub.Dot = 80;

        // 라운드 초기화
        datahub.RoundNumber = 0;
        // 스테이지 초기화
        datahub.StageNumber = 1;
        // 현재 유닛 수 초기화
        for (int i = 0; i < datahub.UnitCounter.Count(); i++) {
            datahub.UnitCounter[i] = 0;
        }
        // 조합 초기화 
        datahub.CombineWaitingPos = 0;
        datahub.CombineTargetId = 0;
        // 난이도 초기화
        datahub.Difficulty = 1;
    }
}
