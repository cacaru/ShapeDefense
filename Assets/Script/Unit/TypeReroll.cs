
using UnityEngine;
using UnityEngine.XR;
using static ShapeDefenseSpace.GameData;

/// <summary>
/// 아이템 유닛의 변환 이벤트
/// </summary>
public class TypeReroll : SceneSingleton<TypeReroll>
{
    public int unit_id;

    private int tmp_waitting_pos = 0;
    public void RollChecking() {
        
        // 게임 옵션이 켜져있으면 
        // c 급 이상이고 아이템이 아닐 때
        bool option_check = datahub.RollCheckOption;
        if (option_check &&
            unit_id >= 9 &&
            unit_id < 44) { 
            //open cofirm field
            AnnounceControll.Instance.AnnounceOn(5);
            tmp_waitting_pos = datahub.CombineWaitingPos;
        }
        else {
            // 아니면 바로 진행
            Reroll();
        }
        
    }

    public void Reroll() {

        // 300원이 있는지 , unit_id가 정상적인지 확인
        if(datahub.Dot >= datahub.TypeRollValue && unit_id > 1000 && unit_id < 6999) {
            // dot 차감
            datahub.Dot -= datahub.TypeRollValue;

            // 해당 위치의 유닛 타입 가져오기
            if (datahub.CombineWaitingPos == 0) {
                datahub.CombineWaitingPos = tmp_waitting_pos;
            }
            else {
                tmp_waitting_pos = datahub.CombineWaitingPos;
            }
            var target = datahub.StageField[datahub.CombineWaitingPos] as GameObject;

            int tmp_target_type = target.GetComponent<Field>().Type;
            //Debug.Log(tmp_target_type);
            int ran = Random.Range(1, 1001);
            // 3개의 변수 지정 
            int normal = 7000, poision = 7001, explosion = 7002, paralysis = 7003;

            // 아이템 등급에 따라 해당 등급의 랜덤한 한개로 변경
            // 현 등급을 뺀 나머지 하나를 선택해야함
            int next_unit_type;
            while (true) {
                next_unit_type = ran switch {
                    (>= 1) and (< 251) => normal,
                    (>= 301) and (< 501) => poision,
                    (>= 601) and (< 751) => explosion,
                    (>= 751) and (< 1001) => paralysis,
                    _ => normal
                };
                if(next_unit_type != tmp_target_type) {
                    break;
                }
                ran = Random.Range(1, 1001);
            }

            // 필드를 변경된 유닛 타입으로 변경
            target.GetComponent<Field>().Type = next_unit_type;
            // 공격 재계산
            target.GetComponent<UnitAttack>().SetDamage();
        }
        else {
            // do nothing
        }
    }
}
