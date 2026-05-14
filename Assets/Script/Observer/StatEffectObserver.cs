using System.Collections;
using TMPro;
using UnityEngine;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;


public class StatEffectObserver : SceneSingleton<StatEffectObserver>
{
    // 시작 화면 이펙트
    [SerializeField] GameObject stat_btn_border;
    [SerializeField] Animator stat_btn_animator;

    // stat modal control
    [SerializeField] GameObject attack_upgrade;
    [SerializeField] GameObject attackspeed_upgrade;
    [SerializeField] GameObject gain_dot_upgrade;
    [SerializeField] GameObject start_dot_upgrade;
    [SerializeField] GameObject result_dot_upgrade;
    [SerializeField] GameObject increase_area;

    [SerializeField] GameObject Upgrade_Check_Pannel;
    [SerializeField] TMP_Text Stat_info;
    [SerializeField] TMP_Text Stat_now;
    [SerializeField] TMP_Text Stat_next;

    private int stat_type = 0;

    GameObject attack_upgrade_btn;
    GameObject attackspeed_upgrade_btn;
    GameObject gain_dot_upgrade_btn;
    GameObject start_dot_upgrade_btn;
    GameObject result_dot_upgrade_btn;

    TMP_Text attack_level;
    TMP_Text attackspeed_level;
    TMP_Text start_level;
    TMP_Text gain_level;
    TMP_Text result_level;

    TMP_Text attack_ability;
    TMP_Text attackspeed_ability;
    TMP_Text start_ability;
    TMP_Text gain_ability;
    TMP_Text result_ability;

    [SerializeField] TMP_Text left_skill_point;
    [SerializeField] TMP_Text max_skill_point;

    delegate void StatObserver();
    StatObserver statObserver;
    // Start is called before the first frame update
    void Start()
    {
        statObserver += UpgradeLevelSetting;
        statObserver += BtnEffectObserve;

        attack_upgrade_btn = attack_upgrade.transform.Find("UpgradePossible").gameObject;
        attackspeed_upgrade_btn = attackspeed_upgrade.transform.Find("UpgradePossible").gameObject;
        gain_dot_upgrade_btn = gain_dot_upgrade.transform.Find("UpgradePossible").gameObject;
        start_dot_upgrade_btn = start_dot_upgrade.transform.Find("UpgradePossible").gameObject;
        result_dot_upgrade_btn = result_dot_upgrade.transform.Find("UpgradePossible").gameObject;

        attack_level = attack_upgrade.transform.Find("Level").gameObject.GetComponent<TMP_Text>();
        attackspeed_level = attackspeed_upgrade.transform.Find("Level").gameObject.GetComponent<TMP_Text>();
        start_level = start_dot_upgrade.transform.Find("Level").gameObject.GetComponent<TMP_Text>();
        gain_level = gain_dot_upgrade.transform.Find("Level").gameObject.GetComponent<TMP_Text>();
        result_level = result_dot_upgrade.transform.Find("Level").gameObject.GetComponent<TMP_Text>();

        attack_ability = increase_area.transform.Find("attack_increase").gameObject.GetComponent<TMP_Text>();
        attackspeed_ability = increase_area.transform.Find("attackspeed_increase").gameObject.GetComponent<TMP_Text>();
        start_ability = increase_area.transform.Find("start_increase").gameObject.GetComponent<TMP_Text>();
        gain_ability = increase_area.transform.Find("gain_increase").gameObject.GetComponent<TMP_Text>();
        result_ability = increase_area.transform.Find("clear_increase").gameObject.GetComponent<TMP_Text>();
    }

    public void UpgradeLevelSetting() {
        left_skill_point.text = datahub.User.SkillPoint.ToString();
        max_skill_point.text = datahub.User.MaxSkillPoint.ToString();

        attack_level.text = datahub.User.StatusAttackLevel.ToString();
        attack_ability.text = (datahub.User.StatusAttackLevel * 5).ToString();

        attackspeed_level.text = datahub.User.StatusAttackSpeedLevel.ToString();
        attackspeed_ability.text = (datahub.User.StatusAttackSpeedLevel * 5).ToString();

        start_level.text = datahub.User.StatusStartDotLevel.ToString();
        start_ability.text = (datahub.User.StatusStartDotLevel * 5).ToString();

        gain_level.text = datahub.User.StatusGainDotLevel.ToString();
        gain_ability.text = (datahub.User.StatusGainDotLevel * 5).ToString();

        result_level.text = datahub.User.StatusClearDotLevel.ToString();
        result_ability.text = (datahub.User.StatusClearDotLevel * 5).ToString();
    }

    public void BtnEffectObserve() {
        UpgradeLevelSetting();
        // start page에서 skillpoint가 남아있을 때 스탯 버튼의 이펙트 활성화
        if (datahub.User.SkillPoint > 0) {
            BtnEffectOn();
            UpgradePossible();
        }
        else {
            BtnEffectOff();
            UpgradeImpossible();
        }
    }

    public void EffectObserve() {
        statObserver();
    }

    private void BtnEffectOn() {
        stat_btn_animator.SetBool(ANI_ACTIVATE, true);
        stat_btn_border.SetActive(true);
    }

    private void BtnEffectOff() {
        stat_btn_animator.SetBool(ANI_ACTIVATE, false);
        stat_btn_border.SetActive(false);
    }

    private void UpgradePossible() {
        attack_upgrade_btn.SetActive(true);
        attackspeed_upgrade_btn.SetActive(true);
        gain_dot_upgrade_btn.SetActive(true);
        start_dot_upgrade_btn.SetActive(true);
        result_dot_upgrade_btn.SetActive(true);
    }

    private void UpgradeImpossible() {
        attack_upgrade_btn.SetActive(false);
        attackspeed_upgrade_btn.SetActive(false);
        gain_dot_upgrade_btn.SetActive(false);
        start_dot_upgrade_btn.SetActive(false);
        result_dot_upgrade_btn.SetActive(false);
    }


    // stat upgrade check pannel open
    public void Stat_Upgrade_Pannel_Open(int type) {
        stat_type = type;
        Stat_info.text = stat_type switch {
            1 => "공격력",
            2 => "공격속도",
            3 => "시작 재화",
            4 => "처치 재화",
            5 => "보상 골드",
            _ => "",
        };

        Stat_now.text = stat_type switch {
            1 => (datahub.User.StatusAttackLevel * 5).ToString(),
            2 => (datahub.User.StatusAttackSpeedLevel * 5).ToString(),
            3 => (datahub.User.StatusStartDotLevel * 5).ToString(),
            4 => (datahub.User.StatusGainDotLevel * 5).ToString(),
            5 => (datahub.User.StatusClearDotLevel * 5).ToString(),
            _ => "0"
        };

        Stat_next.text = stat_type switch {
            1 => ((datahub.User.StatusAttackLevel + 1) * 5).ToString(),
            2 => ((datahub.User.StatusAttackSpeedLevel + 1) * 5).ToString(),
            3 => ((datahub.User.StatusStartDotLevel + 1) * 5).ToString(),
            4 => ((datahub.User.StatusGainDotLevel + 1) * 5).ToString(),
            5 => ((datahub.User.StatusClearDotLevel + 1) * 5).ToString(),
            _ => "0"
        };

        Upgrade_Check_Pannel.SetActive(true);
    }

    // 스탯 상승 버튼 누름
    public void UpgradeBtnClick() {
        switch (stat_type) {
            case 1:
                datahub.User.StatusAttackLevel += 1;
                break;
            case 2:
                datahub.User.StatusAttackSpeedLevel += 1;
                break;
            case 3:
                datahub.User.StatusStartDotLevel += 1;
                break;
            case 4:
                datahub.User.StatusGainDotLevel += 1;
                break;
            case 5:
                datahub.User.StatusClearDotLevel += 1;
                break;
        }

        datahub.User.SkillPoint -= 1;

        datahub.User.AllSkillUpdate();
        UpgradeLevelSetting();
        BtnEffectObserve();

        Upgrade_Check_Pannel.SetActive(false);
    }

    public void UpgradeCancel() {
        Upgrade_Check_Pannel.SetActive(false);
    }
}
