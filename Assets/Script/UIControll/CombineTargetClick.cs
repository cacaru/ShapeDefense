using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ���ս��� ����� Ŭ������ �� ������ �Լ�
/// </summary>
/// Ŭ���� utilityhub�� combinechecker�� Ȯ���Ͽ� ������ �ϰų� �ƹ��ϵ� �Ͼ�� �ʴ´�.
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

    // Ŭ�� �Ǿ��� �� 
    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.pointerCurrentRaycast.gameObject.name.Equals("Result")) {
            datahub.CombineTargetId = result_id;
            // pos�� ������, ���� ���� Ȯ���ؼ� �Ű������� �ѱ��� Ȯ��
            if (datahub.CombineWaitingPos == 0) {
                // �� ���ս��� self id�� ������ ���� ����� ��ġ�� pos�� ã�� CombineWatingPos�� �ֱ�
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
