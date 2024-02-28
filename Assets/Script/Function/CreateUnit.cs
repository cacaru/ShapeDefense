using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 생성 버튼을 눌렀을 때 유닛을 생성할 함수
/// </summary>
public class CreateUnit : MonoBehaviour {

    private DataHub datahub;
    [SerializeField] private TMP_Text dot;
    [SerializeField] private TMP_Text needdot;

    void Start() {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();        
    }

    // 클릭 되면 강화에 필요한 dot양을 2씩 증가시킨다
    // dot양이 모자라면 클릭해도 아무일도 일어나지 않는다
    public void CreateUnitBtnClick() {
        int need_dot = int.Parse(needdot.text);
        //int all_dot = datahub.Dot;
        int all_dot = int.Parse(dot.text);
        if ( all_dot >= need_dot) {
            // 현재 dot량을 필요했던 양 만큼 감소시킴
            all_dot -= need_dot;
            datahub.Dot -= need_dot;
            dot.text = all_dot.ToString();
            // 다음 필요량을 2 증가시킴
            needdot.text = (need_dot+2).ToString();
            UtilityHub.UnitCreateRandomField(1, 2, 3, 4);
        }
        else {
            // 재화가 모자라다는 announce
        }
    }
}

