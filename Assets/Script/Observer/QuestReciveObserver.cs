using UnityEngine;
using static ShapeDefenseSpace.PublicData;
using static ShapeDefenseSpace.GameData;

public class QuestReciveObserver : SceneSingleton<QuestReciveObserver>
{
    [SerializeField] private GameObject Ani;
    [SerializeField] private Animator daily;
    [SerializeField] private Animator weekly;
    [SerializeField] private Animator achieve;

    private bool can_recive_quest = false;
    // Start is called before the first frame update
    void Start()
    {
        SetAni();
    }

    public void SetAni() {
        can_recive_quest = false;
        // 모든 퀘스트들을 돌아보며 canrecive가 있는지 확인
        bool is_exist = false;
        foreach(Achievement item in datahub.Achievement) {
            if (item.CanRecive) {
                is_exist = true;
                can_recive_quest = true;
                achieve.SetBool(ANI_ACTIVATE, true);
                break;
            }
        }
        if (!is_exist) {
            achieve.SetBool(ANI_ACTIVATE, false);
        }
        is_exist = false;
        foreach(DailyQuest item in datahub.DailyQuest) {
            if (item.CanRecive) {
                is_exist = true;
                can_recive_quest = true;
                daily.SetBool(ANI_ACTIVATE, true);
                break;
            }
        }
        if (!is_exist) {
            daily.SetBool(ANI_ACTIVATE, false);
        }
        is_exist = false;
        foreach (WeeklyQuest item in datahub.WeeklyQuest) {  
            if (item.CanRecive) {
                is_exist = true;
                can_recive_quest = true;
                weekly.SetBool(ANI_ACTIVATE, true);
                break;
            }
        }
        if (!is_exist) {
            weekly.SetBool(ANI_ACTIVATE, false);
        }

        if (can_recive_quest) {
            Ani.SetActive(true);
        }
        else {
            Ani.SetActive(false);
        }
    }
}
