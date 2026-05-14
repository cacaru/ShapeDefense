using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShapeDefenseSpace.GameData;

public class AdvChestChecker : SceneSingleton<AdvChestChecker>
{
    [SerializeField] private Image Unit_Blind;
    [SerializeField] private Image Stamina_Blind;
    [SerializeField] private TMP_Text unit_adv_remain;
    [SerializeField] private TMP_Text sta_adv_remain;


    private Color blind_on = new(92 / 255f, 92 / 255f, 92 / 255f, 200 / 255f);
    private Color blind_off = new(92 / 255f, 92 / 255f, 92 / 255f, 0);

    // Start is called before the first frame update
    void Start()
    {
        if(datahub.AdvUnitTime > 0) {
            Unit_Blind.color = blind_on;
            unit_adv_remain.gameObject.SetActive(true);
            StartCoroutine(Unit_adv_timer_routine());
        }
        else {
            unit_adv_remain.gameObject.SetActive(false);
            Unit_Blind.color = blind_off;
        }

        if (datahub.AdvStaTime > 0) {
            Stamina_Blind.color = blind_on;
            sta_adv_remain.gameObject.SetActive(true);
            StartCoroutine(Sta_adv_timer_routine());
        }
        else {
            sta_adv_remain.gameObject.SetActive(false);
            Stamina_Blind.color = blind_off;
        }
    }

    public void UnitAdvTimerShowing() {
        Unit_Blind.color = blind_on;
        unit_adv_remain.gameObject.SetActive(true);
        StartCoroutine(Unit_adv_timer_routine());
    }

    IEnumerator Unit_adv_timer_routine() {
        int min = datahub.AdvUnitTime / 60;
        int sec = datahub.AdvUnitTime % 60;
        unit_adv_remain.text = string.Format("{00}:{1:D2}", min, sec);
        while (datahub.AdvUnitTime > 0) {
            yield return wfs_1;
            min = datahub.AdvUnitTime / 60;
            sec = datahub.AdvUnitTime % 60;
            unit_adv_remain.text = string.Format("{00}:{1:D2}", min, sec);
        }
    }

    public void StaAdvTimerShowing() {
        Stamina_Blind.color = blind_on;
        sta_adv_remain.gameObject.SetActive(true);
        StartCoroutine(Sta_adv_timer_routine());
    }

    IEnumerator Sta_adv_timer_routine() {
        int min = datahub.AdvStaTime / 60;
        int sec = datahub.AdvStaTime % 60;
        sta_adv_remain.text = string.Format("{00}:{1:D2}", min, sec);
        while (datahub.AdvStaTime > 0) {
            yield return wfs_1;
            min = datahub.AdvStaTime / 60;
            sec = datahub.AdvStaTime % 60;
            sta_adv_remain.text = string.Format("{00}:{1:D2}", min, sec);
        }
    }
}
