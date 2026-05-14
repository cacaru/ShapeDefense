
using UnityEngine;
using static ShapeDefenseSpace.GameData;

/// <summary>
/// 아이템 유닛의 변환 이벤트
/// </summary>
public class ItemReroll : SceneSingleton<ItemReroll>
{
    // 변환에 필요한 금액
    private readonly int COST = 50;

    public int unit_id;

    private int tmp_waitting_pos = 0;
    public void RollChecking() {
        // 게임 옵션이 켜져있으면 
        // - 현 아이템이 별/ 달 일 떄 변환 확인
        bool option_check = datahub.RollCheckOption;
        if (option_check && (
            unit_id == 304 || 
            unit_id == 305 || 
            unit_id == 306 || 
            unit_id == 404 || 
            unit_id == 405 || 
            unit_id == 406
            ) ) {
            //open cofirm field
            AnnounceControll.Instance.AnnounceOn(4);
            tmp_waitting_pos = datahub.CombineWaitingPos;
        }
        else {
            // 아니면 바로 진행
            datahub.ItemRollActive = true;
            Reroll();
        }
        
    }

    public void Reroll() {
        if (!datahub.ItemRollActive) return;

        // 50원이 있는지 , unit_id가 정상적인지 확인
        if (datahub.Dot >= COST && unit_id >= 300 && unit_id <= 599) {
            int ran = Random.Range(1, 991);
            // 5개의 변수 지정
            int circle, triangle, square, star, moon, sun;
            if(unit_id < 400) {
                circle = 301;
                triangle = 302;
                square = 303;
                star = 304;
                moon = 305;
                sun = 306;
            }
            else {
                circle = 401;
                triangle = 402;
                square = 403;
                star = 404;
                moon = 405;
                sun = 406;
            }

            // 아이템 등급에 따라 해당 등급의 랜덤한 한개로 변경
            int next_unit_id = ran switch {
                (>= 1) and (< 261) => circle,
                (>= 261) and (< 521) => triangle,
                (>= 521) and (< 781) => square,
                (>= 781) and (< 851) => star,
                (>= 851) and (< 921) => moon,
                (>= 921) and (<= 991) => sun,
                _ => circle
            };

            // dot 차감
            datahub.Dot -= COST;

            // 해당 위치의 유닛 변경
            if(datahub.CombineWaitingPos == 0) {
                datahub.CombineWaitingPos = tmp_waitting_pos;
            }
            var target = datahub.StageField[datahub.CombineWaitingPos] as GameObject;
            datahub.UnitCounter[target.GetComponent<Field>().UnitId]--;
            target.GetComponent<Field>().UnitId = next_unit_id;
            datahub.UnitCounter[next_unit_id]++;

            // 필드를 변경된 유닛으로 변경
            //UnitClickObserver.Instance.CleanField();
            UnitClickObserver.Instance.UnitCombineFieldSetting(next_unit_id, datahub.CombineWaitingPos, Vector3.zero);
            InfoPanelControll.Instance.InfoPanelModify(next_unit_id, 7000);
        }
        else {
            // do nothing
        }

        datahub.ItemRollActive = false;
    }
}
