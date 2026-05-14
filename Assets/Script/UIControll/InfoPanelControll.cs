using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ShapeDefenseSpace;

using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;

/// <summary>
/// 정보창 컨트롤
/// </summary>
public class InfoPanelControll : SceneSingleton<InfoPanelControll> {

    [SerializeField] private GameObject InfoPanel;
    [SerializeField] private Image image;
    [SerializeField] private Image grade;
    [SerializeField] private TMP_Text nick_name;
    [SerializeField] private TMP_Text attack;
    [SerializeField] private TMP_Text ori_attack;
    [SerializeField] private TMP_Text add_attack;
    [SerializeField] private TMP_Text speed;
    [SerializeField] private TMP_Text attack_figures;
    [SerializeField] private TMP_Text attack_type;
    
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = InfoPanel.GetComponent<Animator>();
    }

    // type number  1 > 기본 정보창
    // type number  2 > 도감에서의 정보창 
    public void InfoPanelActivate(int unit_id, int unit_type ,int effect_type_number) {
        var unit = datahub.Unit_dic[unit_id] as Unit;
        image.sprite = UtilityHub.GetSprite(unit_id);
        nick_name.text = unit.NickName;
        int add_attack_value = unit.UpgradeFigures;
        int ori_attack_value = unit.Attack + (unit.UpgradeValue * unit.UpgradeFigures);
        switch (unit.Grade) {
            case "E":
                add_attack_value *= datahub.UpgradeValueE;
                grade.color = color_e;
                break;
            case "D":
                add_attack_value *= datahub.UpgradeValueD;
                grade.color = color_d;
                break;
            case "C":
                add_attack_value *= datahub.UpgradeValueC;
                grade.color = color_c;
                break;
            case "B":
                add_attack_value *= datahub.UpgradeValueB;
                grade.color = color_b;
                break;
            case "A":
                add_attack_value *= datahub.UpgradeValueA;
                grade.color = color_a;
                break;
            case "S":
                add_attack_value *= datahub.UpgradeValueS;
                grade.color = color_s;
                break;
            case "IC":
                add_attack_value *= 0;
                grade.color = core_color;
                break;
            case "IB":
                add_attack_value *= 0;
                grade.color = unicore_color;
                break;
            case "IA":
                add_attack_value *= 0;
                grade.color = crystal_color;
                break;
            default:
                add_attack_value *= 0;
                break;
        }

        // 공격 타입 설정
        // 공격타입에 따라 공격력 분할
        switch (unit_type) {
            case -1:
                attack_type.text = "랜덤으로 결정";
                break;
            case 7000:
                attack_type.text = "기본형";
                break;
            case 7001:
                attack_type.text = "출혈형";
                ori_attack_value /= 2;
                break;
            case 7002:
                attack_type.text = "폭발형";
                ori_attack_value /= 3;
                break;
            case 7003:
                attack_type.text = "마비형";
                ori_attack_value /= 2;
                break;
        }

        int attack_value = ori_attack_value + add_attack_value;

        attack.text = attack_value.ToString();
        ori_attack.text = ori_attack_value.ToString();
        add_attack.text = add_attack_value.ToString();

        attack_figures.text = unit.UpgradeFigures.ToString();

        speed.text = (unit.AttackSpeed + datahub.User.StatusAttackSpeedLevel * 0.05f).ToString();

        if (datahub.Gaming) {
            if (effect_type_number == 1) {
                animator.SetBool(ANI_ACTIVATE, true);
            }
            else if (effect_type_number == 2) {
                animator.SetBool("Activate2", true);
            }
        }
        else {
            animator.SetBool("StartPageActivate", true);
        }
        
    }

    public void InfoPanelModify(int unit_id, int unit_type) {
        var unit = datahub.Unit_dic[unit_id] as Unit;
        image.sprite = UtilityHub.GetSprite(unit_id);
        nick_name.text = unit.NickName;
        int add_attack_value = unit.UpgradeFigures;
        int ori_attack_value = unit.Attack + (unit.UpgradeValue * unit.UpgradeFigures);
        switch (unit.Grade) {
            case "E":
                add_attack_value *= datahub.UpgradeValueE;
                grade.color = color_e;
                break;
            case "D":
                add_attack_value *= datahub.UpgradeValueD;
                grade.color = color_d;
                break;
            case "C":
                add_attack_value *= datahub.UpgradeValueC;
                grade.color = color_c;
                break;
            case "B":
                add_attack_value *= datahub.UpgradeValueB;
                grade.color = color_b;
                break;
            case "A":
                add_attack_value *= datahub.UpgradeValueA;
                grade.color = color_a;
                break;
            case "S":
                add_attack_value *= datahub.UpgradeValueS;
                grade.color = color_s;
                break;
            case "IC":
                add_attack_value *= 0;
                grade.color = core_color;
                break;
            case "IB":
                add_attack_value *= 0;
                grade.color = unicore_color;
                break;
            case "IA":
                add_attack_value *= 0;
                grade.color = crystal_color;
                break;
            default:
                add_attack_value *= 0;
                break;
        }

        // 공격 타입 설정
        // 공격타입에 따라 공격력 분할
        switch (unit_type) {
            case -1:
                attack_type.text = "랜덤으로 결정";
                break;
            case 7000:
                attack_type.text = "기본형";
                break;
            case 7001:
                attack_type.text = "출혈형";
                ori_attack_value /= 2;
                break;
            case 7002:
                attack_type.text = "폭발형";
                ori_attack_value /= 3;
                break;
            case 7003:
                attack_type.text = "마비형";
                ori_attack_value /= 2;
                break;
        }

        int attack_value = ori_attack_value + add_attack_value;

        attack.text = attack_value.ToString();
        ori_attack.text = ori_attack_value.ToString();
        add_attack.text = add_attack_value.ToString();

        attack_figures.text = unit.UpgradeFigures.ToString();

        speed.text = unit.AttackSpeed.ToString();
    }

    public void InfoPanelDown(int type_number) {
        nick_name.text = "";
        attack.text = "";
        speed.text = "";

        if (datahub.Gaming) {
            if (type_number == 1) {
                animator.SetBool(ANI_ACTIVATE, false);
            }
            else if (type_number == 2) {
                animator.SetBool("Activate2", false);
            }
        }
        else {
            animator.SetBool("StartPageActivate", false);
        }
    }
}
