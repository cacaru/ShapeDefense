using ShapeDefenseSpace;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;

public class UpgradeUISetter : MonoBehaviour
{
    private int unit_id;
    private int now_level_val;
    private int next_level_val;
    private int upgrade_val;
    private Unit unit;
    [SerializeField] GameObject Main_Canvas;
    
    [SerializeField] GameObject Main_Field;
    [SerializeField] GameObject Checing_Field;
    [SerializeField] GameObject Complete_Field;
    [SerializeField] GameObject Spinning_img;
    private bool spinning = false;

    [SerializeField] Image unit_icon;
    [SerializeField] Image unit_back;
    [SerializeField] TMP_Text now_dot;
    [SerializeField] TMP_Text now_piece;

    [SerializeField] TMP_Text now_level;
    [SerializeField] TMP_Text now_attack;

    [SerializeField] TMP_Text next_level;
    [SerializeField] TMP_Text next_attack;

    [SerializeField] TMP_Text need_dot;
    [SerializeField] TMP_Text need_piece;

    private int need_piece_val;
    private int need_dot_val;

    public int SetUnit {
        set {
            unit_id = value;
            now_dot.text = datahub.User.Dot.ToString();

            unit = datahub.Unit_dic[unit_id] as Unit;

            unit_icon.sprite = UtilityHub.GetSprite(unit_id);
            unit_back.color = unit.Grade switch {
                "E" => color_e,
                "D" => color_d,
                "C" => color_c,
                "B" => color_b,
                "A" => color_a,
                "S" => color_s,
                _ => core_color
            };

            now_level_val = unit.UpgradeValue;
            next_level_val = now_level_val + 1;
            upgrade_val = next_level_val - now_level_val;

            now_level.text = now_level_val.ToString();
            now_attack.text = (unit.Attack + unit.UpgradeFigures * now_level_val).ToString();
            now_piece.text = unit.Piece.ToString();

            // 필요조각 수는 복리처럼 더해져야함
            // 목표 레벨까지 필요한 조각 수 - 현재 레벨까지 필요했던 조각 수
            int target_val = 5 * next_level_val * (next_level_val + 1);
            int now_val = 5 * now_level_val * (now_level_val + 1);
            upgrade_val = next_level_val - now_level_val;

            need_piece_val = target_val - now_val;
            need_dot_val = unit.NeedGold * upgrade_val;

            next_level.text = next_level_val.ToString();
            next_attack.text = (unit.Attack + unit.UpgradeFigures * next_level_val).ToString();

            need_dot.text = need_dot_val.ToString();
            need_piece.text = need_piece_val.ToString();
        }
    }


    public void Upgrade_Increse() {
        // 현재 소지 조각/ 골드의 최대치 만큼만 이동 가능하게 변경
        next_level_val = next_level_val + 1 >= 15 ? 15 : next_level_val + 1;
        
        // 필요조각 수는 복리처럼 더해져야함
        // 목표 레벨까지 필요한 조각 수 - 현재 레벨까지 필요했던 조각 수
        int target_val = 5 * next_level_val * (next_level_val + 1);
        int now_val = 5 * now_level_val * (now_level_val + 1);
        upgrade_val = next_level_val - now_level_val;

        if ( target_val - now_val > unit.Piece || // 조각이 부족하거나
            (unit.NeedGold * upgrade_val ) > datahub.User.Dot  // 현재 소지금이 부족하면 
            ) {
            // 다음으로 안넘어감
            next_level_val -= 1;
            target_val = 5 * next_level_val * (next_level_val + 1);
        }

        upgrade_val = next_level_val - now_level_val;

        need_piece_val = target_val - now_val;
        need_dot_val = unit.NeedGold * upgrade_val;

        next_level.text = next_level_val.ToString();
        next_attack.text = (unit.Attack + unit.UpgradeFigures * next_level_val).ToString();

        need_dot.text = need_dot_val.ToString();
        need_piece.text = need_piece_val.ToString();
    }

    public void Upgrade_Decrese() {
        next_level_val = next_level_val - 1 <= now_level_val+1 ? now_level_val+1 : next_level_val - 1;

        next_level.text = next_level_val.ToString();
        next_attack.text = (unit.Attack + unit.UpgradeFigures * next_level_val).ToString();

        int target_val = 5 * next_level_val * (next_level_val + 1);
        int now_val = 5 * now_level_val * (now_level_val + 1);

        need_piece_val = target_val - now_val;

        upgrade_val = next_level_val - now_level_val;
        need_dot_val = unit.NeedGold * upgrade_val;

        need_dot.text = need_dot_val.ToString();
        need_piece.text = need_piece_val.ToString();
    }

    // 확인중
    public void Upgrade_Achieve_Checking() {
        Main_Field.SetActive(false);
        Main_Canvas.GetComponent<Upgrade>().UpdateUnit(unit_id, need_piece_val, need_dot_val, upgrade_val);
        Checing_Field.SetActive(true);
        spinning = true;
        StartCoroutine(Checking_icon_spinning());
        
    }

    IEnumerator Checking_icon_spinning() {
        while (spinning) {
            var trans = Spinning_img.GetComponent<Transform>().rotation;
            trans.z += 25f;
            Spinning_img.GetComponent<Transform>().rotation = trans;
            yield return wff;
        }
    }

    IEnumerator Completing() {
        while (true) {
            yield return wfs_1;
            if (modifyDB.GetState() == STATE.DB_WAIT) {
                break;
            }
        }
        Checing_Field.SetActive(false);
        Complete_Field_On();
    }

    private void Complete_Field_On() {
        StopCoroutine(Checking_icon_spinning());
        spinning = false;
        Unit unit = datahub.Unit_dic[unit_id] as Unit;
        // 완료 창 생성
        Complete_Field.transform.Find("Image").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit_id);
        Complete_Field.transform.Find("ImageBack").GetComponent<Image>().color = unit.Grade switch {
            "E" => color_e,
            "D" => color_d,
            "C" => color_c,
            "B" => color_b,
            "A" => color_a,
            "S" => color_s,
            _ => core_color
        };

        Complete_Field.transform.Find("Dot").GetComponent<TMP_Text>().text = datahub.User.Dot.ToString();
        Complete_Field.transform.Find("Piece").GetComponent<TMP_Text>().text = unit.Piece.ToString();
        Complete_Field.transform.Find("NowBack").Find("Level").GetComponent<TMP_Text>().text = unit.UpgradeValue.ToString();
        Complete_Field.transform.Find("NowBack").Find("Attack").GetComponent<TMP_Text>().text = (unit.Attack + unit.UpgradeValue * unit.UpgradeFigures).ToString();

        Complete_Field.SetActive(true);
    }
    // 강화 완료
    public void Upgrade_Complete() {
        StartCoroutine(Completing());
    }

    public void Complete_Comfrim() {
        var trans = Spinning_img.GetComponent<Transform>().rotation;
        trans.z = 0;
        Spinning_img.GetComponent<Transform>().rotation = trans;
        Complete_Field.SetActive(false);
        gameObject.SetActive(false);
        Main_Field.SetActive(true);
    }
}
