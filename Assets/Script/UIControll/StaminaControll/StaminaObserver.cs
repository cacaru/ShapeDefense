using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;

/// <summary>
/// stamina의 상태를 확인하고 수정할 함수
/// </summary>
/// 
public class StaminaObserver : Singleton<StaminaObserver>
{
    private bool checking = false;

    void Start()
    {
        DateTime now = DateTime.Now;

        // 종료시간과 현재 시간을 비교해서 2분마다 1의 스테미너를 회복
        DateTime end_time = Convert.ToDateTime(PlayerPrefs.GetString("End_Time") == "" ? now.ToString() : PlayerPrefs.GetString("End_Time"));
        TimeSpan dif = now - end_time;
        var min_dif = dif.TotalMinutes;

        // 이미 많이 가지고있으면 무시
        if (min_dif >= 2 && datahub.User.Stamina < datahub.User.MaxStamina) {
            datahub.User.Stamina += (int)min_dif / 2;
            if (datahub.User.Stamina >= datahub.User.MaxStamina) {
                datahub.User.Stamina = datahub.User.MaxStamina;
            }
            string query = UtilityHub.query_builder.Append("UPDATE user SET stamina=")
                                                    .Append(datahub.User.Stamina)
                                                    .ToString();
            UtilityHub.query_builder.Clear();
            modifyDB.ControllDB(query, "user");
            StaminaShow.Instance.ReShow();
        }

        // 종료 기록이 안남아있는데 스태미나 차이가 나는 경우 -> 최대로 채워줌
        else if(min_dif == 0 && datahub.User.Stamina < datahub.User.MaxStamina) {
            datahub.User.Stamina = datahub.User.MaxStamina;
            string query = UtilityHub.query_builder.Append("UPDATE user SET stamina=")
                                                    .Append(datahub.User.Stamina)
                                                    .ToString();
            UtilityHub.query_builder.Clear();
            modifyDB.ControllDB(query, "user");
            StaminaShow.Instance.ReShow();
        }

        StartCharge();
    }

    // stamina 관련 업데이트
    public void UpdateStamina() {
        // stmaina DB 업데이트
        string query = UtilityHub.query_builder.Append("UPDATE user SET stamina=")
                                              .Append(datahub.User.Stamina)
                                              .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "user");

        Scene scene = SceneManager.GetActiveScene();
        // stamina 화면 재표기
        // 메인 화면일 경우 업데이트
        if (scene.name.Equals("GameStartScene")){
            StaminaShow.Instance.ReShow();
        }
            
    }

    public void StartCharge() {
        if(datahub.User.Stamina < datahub.User.MaxStamina) {
            if (!checking) {
                StartCoroutine(StaminaCharge());
            }
        }
    }

    // stamina 회복
    // 2분당 1 씩 회복
    IEnumerator StaminaCharge() {
        checking = true;
        while (true) {
            yield return wfsr_1;
            datahub.RemainTime -= 1;
            if( datahub.RemainTime <= 0) {
                if (datahub.User.Stamina < datahub.User.MaxStamina) {
                    datahub.User.Stamina += 1;
                    UpdateStamina();
                }
                // 가득 찼다면 코루틴 종료
                else if ( datahub.User.Stamina >= datahub.User.MaxStamina) {
                    checking = false;
                    StopCoroutine(StaminaCharge());
                }
                datahub.RemainTime = 120;
            }
        }
    }

    // 게임 종료 시간을 기록하기
    public void RecoardEndTime() {
        PlayerPrefs.SetString("End_Time", DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    void OnApplicationQuit() {
        // 게임 강종/ 종료시 게임 종료시간을 저장
        RecoardEndTime();
    }

}
