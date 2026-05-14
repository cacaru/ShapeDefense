using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;
using System.Collections;

public class FirstLoadingObserver : SceneSingleton<FirstLoadingObserver>
{
    [SerializeField] private Slider LoadingBar;
    [SerializeField] private GameObject LoadingUI;
    [SerializeField] private GameObject PressStartText;

    [SerializeField] private TMP_Text test;
    private bool attend_check = true;

    // Update is called once per frame
    void Update()
    {
        if (datahub.UserConnectEnd &&
            datahub.DBConnectEnd &&
            datahub.AchieveConnectEnd &&
            datahub.DailyConnectEnd &&
            datahub.WeeklyConnectEnd &&
            attend_check) {
            achieve_observer.AttendanseCheck();
            GameObject.Find("DBObject").GetComponent<AchievementObserver>().enabled = true;
            test.text = "출석 업적 체크";
            attend_check = false;
        }

        if(datahub.DBConnectEnd &&
           datahub.UserConnectEnd &&
           datahub.AchieveConnectEnd &&
           datahub.DailyConnectEnd &&
           datahub.WeeklyConnectEnd && 
           datahub.FirstQuestChecker &&
           datahub.DB_State != STATE.DB_CONNECTED) {
            
            datahub.DB_State = STATE.DB_CONNECTED;
            //재점검
            datahub.User.UserCorrectCheck();
            achieve_observer.DailyObserver();
            LoadProgress();
            StartCoroutine(Load_End());
        }

    }

    public void LoadProgress() {
        LoadingBar.value += 0.1f;
    }

   IEnumerator Load_End() {
        yield return wfs_1;
        LoadingUI.SetActive(false);
        PressStartText.SetActive(true);
        test.text = "올 컴플리트";
        GetComponent<FirstLoadingObserver>().enabled = false;
    }
}
