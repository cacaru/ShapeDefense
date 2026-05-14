using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;
using System.Drawing;

/// <summary>
/// 조합식의 결과를 클릭했을 때 반응할 함수
/// </summary>
/// 클릭시 utilityhub의 combinechecker를 확인하여 조합을 하거나 아무일도 일어나지 않는다.
public class CombineTargetClick : MonoBehaviour, IPointerClickHandler
{

    private int result_id;
    private bool in_lib = false;
    private int combine_id = -1;
    private int grade;

    public int ResultId { 
        get { return result_id; } 
        set { 
            result_id = value;
            grade = result_id switch {
                >= 1000 and <= 1999 => 6,
                >= 2000 and <= 2999 => 5,
                >= 3000 and <= 3999 => 4,
                >= 4000 and <= 4999 => 3,
                >= 5000 and <= 5999 => 2,
                >= 6000 and <= 6999 => 1,
                _ => 0
            };
        } 
    }
    public bool InLib { get { return in_lib; } set {  in_lib = value; } }
    public int CombineId { get { return combine_id; } set { combine_id = value; } }

    // 클릭 되었을 떄 
    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.pointerCurrentRaycast.gameObject.name.Equals("Result")) {
            if (in_lib) {
                LibraryClickObserver.Instance.CombineWithDetailShow(result_id);
                return;
            }
            if (datahub.Gaming && result_id > 0 && combine_id >= 0) {
                datahub.CombineTargetId = result_id;
                // pos가 있을떄, 없을 때를 확인해서 매개변수를 넘길지 확인
                if (datahub.CombineWaitingPos == 0) {
                    // 현 조합식의 self id를 가지는 가장 가까운 위치의 pos를 찾아 CombineWatingPos에 넣기
                    int id = transform.parent.gameObject.transform.Find("Self").GetComponent<CombineMaterialClick>().Id;
                    int size = datahub.StageField.Count;
                    for (int i = 1; i < size; i++) {
                        GameObject field = datahub.StageField[i] as GameObject;
                        if (field.GetComponent<Field>().UnitId == id) {
                            datahub.CombineWaitingPos = i;
                            break;
                        }
                    }
                }                
                // 조합 확인 - E급 D급은 확인안함
                if (datahub.CombineCheckOption && grade < 4) {
                    AnnounceControll.Instance.WaitingPos = datahub.CombineWaitingPos;
                    AnnounceControll.Instance.CombineID = combine_id;
                    AnnounceControll.Instance.AnnounceOn(2);
                }
                else {
                    UtilityHub.CombineCheck(combine_id);
                }
            }
        }  
        
    }

}
