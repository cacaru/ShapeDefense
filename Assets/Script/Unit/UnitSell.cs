using ShapeDefenseSpace;
using UnityEngine;
using static ShapeDefenseSpace.GameData;

/// <summary>
/// 유닛 판매 함수
/// </summary>
/// 
public class UnitSell : SceneSingleton<UnitSell>
{
    [SerializeField] private GameObject SellBtn;
    [SerializeField] private GameObject AttackArea;
    private int selling_price = 0;
    private int unit_id;

    public int UnitId { 
        set { 
            unit_id = value;
            // 등급에 따라 구성
            // E = 15
            // D = 30
            // C = 75
            // B = 280
            // A = 785 
            // S = 1565
            // IC = 100
            // IB = 150
            // IA = 500
            if ( unit_id >= 300 && unit_id <= 6999) {
                var tmp_unit_data = datahub.Unit_dic[unit_id] as Unit;
                selling_price = tmp_unit_data.Grade switch {
                    "E" => 15,
                    "D" => 30,
                    "C" => 75,
                    "B" => 280,
                    "A" => 785,
                    "S" => 1565,
                    "IC" => 100,
                    "ID" => 150,
                    "IA" => 500,
                    _ => 15
                };
            }
            else { selling_price = 0; }
        } 
    }

    // 유닛 판매하기
    public void SellUnitBtnClick() {
        //Debug.Log(selling_price);
        var sell_target = datahub.StageField[datahub.CombineWaitingPos] as GameObject;

        // target의 유닛 번호에 맞는 유닛 수 줄이기
        datahub.UnitCounter[sell_target.GetComponent<Field>().UnitId] -= 1;

        // 유닛 위치를 0으로 변경하며 유닛 지우기
        sell_target.GetComponent<Field>().UnitId = 0;
        sell_target.GetComponent<UnitAttack>().Id = 0;

        // 선택한 위치 삭제
        datahub.CombineWaitingPos = 0;

        // 조합법 목록 삭제
        UnitClickObserver.Instance.CleanList();
        
        // 판매 가격 만큼 dot를 상승시킴
        datahub.Dot += selling_price;
        datahub.NeedDot -= 2;

        // 범위 제거
        AttackArea.SetActive(false);

        // 판매 정보 초기화
        selling_price = 0;
        unit_id = 0;

        datahub.LeftStageField++;
        UtilityHub.UnitFieldChange(sell_target.transform.position, 0);

        // 판매하기 버튼 내리기
        SellBtn.SetActive(false);

        // 필드 내리기
        UnitClickObserver.Instance.UnitClickFieldOff();
    }
}
