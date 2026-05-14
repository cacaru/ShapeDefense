using UnityEngine;
using UnityEngine.EventSystems;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.CombineTableShowObserver;

/// <summary>
/// 조합 재료를 클릭 했을 때 해당 재료를 조합할 수 있는 식을 보여주기 위한 함수
/// </summary>
/// result를 기반으로 하는 검색 구현
public class CombineMaterialClick : MonoBehaviour , IPointerClickHandler
{

    private int id;
    private bool in_lib = false;

    public int Id { get { return id; } set { id = value; } }
    public bool InLib { get { return in_lib; } set { in_lib = value; } }


    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.pointerCurrentRaycast.gameObject.name.Contains("Material") ||
            eventData.pointerCurrentRaycast.gameObject.name.Equals("Self")         ) {
            if (datahub.Gaming) {
                // 트리거 설정
                if (in_lib) {
                    // 도감에서는 결과물로 조합할 수 있는 녀석을 띄워줘야함
                    LibraryClickObserver.Instance.CombineWithDetailShow(id);
                }
                else {
                    // 선택한 유닛의 위치가 변경되므로 pos 값을 초기화
                    datahub.CombineWaitingPos = 0;
                    ShowCombineTable(id, false, transform.parent.parent.gameObject, 2);
                }
            }
            else {
                LibraryClickObserver.Instance.CombineWithDetailShow(id);
            }
        }
    }

}
