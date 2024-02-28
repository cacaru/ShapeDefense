using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ���� ��Ḧ Ŭ�� ���� �� �ش� ��Ḧ ������ �� �ִ� ���� �����ֱ� ���� �Լ�
/// </summary>
/// result�� ������� �ϴ� �˻� ����
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
            // ������ ������ ��ġ�� ����ǹǷ� pos ���� �ʱ�ȭ
            datahub.CombineWaitingPos = 0;
            creater.ShowCombineToResultTable(id);
        }
    }

}
