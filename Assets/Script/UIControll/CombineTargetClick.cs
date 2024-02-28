using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 조합식의 결과를 클릭했을 때 반응할 함수
/// </summary>
/// 클릭시 utilityhub의 combinechecker를 확인하여 조합을 하거나 아무일도 일어나지 않는다.
public class CombineTargetClick : MonoBehaviour, IPointerClickHandler
{
    private DataHub datahub;

    private int result_id;
    public int ResultId { get { return result_id; } set { result_id = value; } }
    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
    }

    // 클릭 되었을 떄 
    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.pointerCurrentRaycast.gameObject.name.Equals("Result")) {
            datahub.CombineTargetId = result_id;
            // pos가 있을떄, 없을 때를 확인해서 매개변수를 넘길지 확인
            if (datahub.CombineWaitingPos == 0) {
                // 현 조합식의 self id를 가지는 가장 가까운 위치의 pos를 찾아 CombineWatingPos에 넣기
                int id = transform.parent.gameObject.transform.Find("Self").GetComponent<CombineMaterialClick>().Id;
                int size = datahub.StageField.Count;
                for(int i = 1; i <= size; i++) {
                    GameObject field = datahub.StageField[i] as GameObject;
                    if (field.GetComponent<Field>().UnitId == id) {
                        datahub.CombineWaitingPos = i;
                        break;
                    }
                }
            }
            UtilityHub.CombineCheck();
        }
    }

}
