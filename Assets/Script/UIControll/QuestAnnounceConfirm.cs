using System.Collections;
using TMPro;
using UnityEngine;
using ShapeDefenseSpace;
/// <summary>
/// 업적 받기 결과 받아가기
/// </summary>
public class QuestAnnounceConfirm : MonoBehaviour
{
    [SerializeField] private GameObject QuestController;

    [SerializeField] private TMP_Text ConfirmAnnounceText;
    [SerializeField] private GameObject ConfirmBtn;

    private readonly string confirmText = "확인중";

    // 확인창 확인
    public void RecieveConfirm() {
        // text 리셋
        ConfirmAnnounceText.text = confirmText;

        // 모든 창을 리로드
        QuestController.GetComponent<DailyQuestControll>().Show();
        QuestController.GetComponent<AchievementControll>().Show();
        QuestController.GetComponent<WeeklyQuestControll>().Show();

        // ani reload
        QuestReciveObserver.Instance.SetAni();

        // unit upgrade check reload
        UnitUpgradeObserver.Instance.SetAni();
        UIUnitPageSetting.Instance.CheckUnitLevel();

        // 골드 보상을 위한 header reload
        UtilityHub.PageHeaderSetting();

        ConfirmBtn.SetActive(false);
        gameObject.SetActive(false);
    }

}