using System.Collections;
using UnityEngine;
using static ShapeDefenseSpace.GameData;

public class FirstSceneObserver : MonoBehaviour
{
    private bool is_active = false;
    private void Update() {
        if (datahub.QuestResetChecker) {
            datahub.QuestResetChecker = false;
            achieve_observer.AttendanseCheck();
           
            gameObject.GetComponent<DailyQuestControll>().PageReset(); 
            gameObject.GetComponent<WeeklyQuestControll>().PageReset();
            gameObject.GetComponent<DailyQuestControll>().Show();
            gameObject.GetComponent<WeeklyQuestControll>().Show();
        }

        if (datahub.DBConnectEnd && !is_active) {

            StatEffectObserver.Instance.EffectObserve();
            UIUnitPageSetting.Instance.CheckUnitLevel();
            UnitUpgradeObserver.Instance.SetAni();
            QuestReciveObserver.Instance.SetAni();
            StaminaShow.Instance.enabled = true;
            stamina_observer.enabled = true;

            // 정보 로딩 이후 이 스크립트가 필요 없으므로 바로 제거
            gameObject.GetComponent<FirstSceneObserver>().enabled = false;
        }
    }
}