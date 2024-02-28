using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 조합 재료를 클릭 했을 때 해당 재료를 조합할 수 있는 식을 보여주기 위한 함수
/// </summary>
/// result를 기반으로 하는 검색 구현
public class CombineMaterialClick : MonoBehaviour , IPointerClickHandler
{
    private DataHub datahub;
    private CreateCombineTable creater;

    private int id;
    public int Id { get { return id; } set { id = value; } }

    // Start is called before the first frame update
    void Start() {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
        creater = GameObject.Find("UnitField").GetComponent<CreateCombineTable>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.pointerCurrentRaycast.gameObject.name.Contains("Material") ||
            eventData.pointerCurrentRaycast.gameObject.name.Equals("Self")         ) {
            // 선택한 유닛의 위치가 변경되므로 pos 값을 초기화
            datahub.CombineWaitingPos = 0;
            creater.ShowCombineToResultTable(id);
        }
    }

}
