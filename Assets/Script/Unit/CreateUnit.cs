using TMPro;
using UnityEngine;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;
/// <summary>
/// 생성 버튼을 눌렀을 때 유닛을 생성할 함수
/// </summary>
public class CreateUnit : MonoBehaviour {

    [SerializeField] private TMP_Text dot;
    [SerializeField] private TMP_Text needdot;


    // 클릭 되면 강화에 필요한 dot양을 2씩 증가시킨다
    // dot양이 모자라면 클릭해도 아무일도 일어나지 않는다
    public void CreateUnitBtnClick() {
        // 빈 공간이 있는지 확인해야함 -> 빈공간이 없으면 소환 자체를 막음
        if(datahub.LeftStageField <= 0) {
            // 조합 불가
            // 안내창 열기
            return;
        } 

        if ( datahub.Dot >= datahub.NeedDot) {
            // 현재 dot량을 필요했던 양 만큼 감소시킴
            datahub.Dot -= datahub.NeedDot;
            // dot 소모 업적 증가
            achieve_observer.UseDot(datahub.NeedDot);
            // 다음 필요량을 2 증가시킴
            datahub.NeedDot += 2;
            // 유닛 생성
            UtilityHub.UnitCreateRandomField(1001, 1002, 1003, 1004, 1005, 1006);
            // 소환 횟수 업적 증가
            achieve_observer.UnitCreate();

        }
    }
}

