using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ShapeDefenseSpace.GameData;

public class StatInit : MonoBehaviour
{
    [SerializeField] private GameObject Announce;

    private int need_gold = 0;
    /// <summary>
    /// 스탯 초기화 버튼 클릭
    /// </summary>
    ///  처음이라면 공짜
    ///  2회부터 500골드 필요
    public void StatInitClick() {

        int init_time = PlayerPrefs.GetInt("Stat_Init_Time");
        if(init_time == 0) {
            need_gold = 0;
            Announce.transform.Find("InitField").Find("Free").gameObject.SetActive(true);
        }
        else {
            need_gold = 500;
            Announce.transform.Find("InitField").Find("Gold").gameObject.SetActive(true);
        }

        Announce.SetActive(true);
    }

    public void StatInitCancel() {
        Announce.transform.Find("InitField").Find("Free").gameObject.SetActive(false);
        Announce.transform.Find("InitField").Find("Gold").gameObject.SetActive(false);
        Announce.transform.Find("InitField").gameObject.SetActive(true);
        Announce.transform.Find("Impossible").gameObject.SetActive(false);
        need_gold = 0;

        Announce.SetActive(false);
    }

    public void StatInitActive() {
        Announce.transform.Find("InitField").gameObject.SetActive(false);
        if (datahub.User.Dot < need_gold) {
            // 불가
            Announce.transform.Find("Impossible").gameObject.SetActive(true);
        }

        // 초기화
        datahub.User.SkillInit();
        PlayerPrefs.SetInt("Stat_Init_Time", 1);
        PlayerPrefs.Save();
        StatEffectObserver.Instance.EffectObserve();
        StatInitCancel();
    }
}
