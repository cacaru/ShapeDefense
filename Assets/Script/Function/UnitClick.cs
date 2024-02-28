using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �� ���� ȭ�� Ŭ�� �̺�Ʈ
/// </summary>
public class UnitClick : MonoBehaviour
{
    private RaycastHit2D hit;
    private DataHub datahub;

    [SerializeField] private GameObject CombineField;

    private CreateCombineTable creater;
    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
        creater = GameObject.Find("UnitField").GetComponent<CreateCombineTable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            //Ŭ����ġ ��ǥ
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //�ش� ��ǥ�� �ִ� ������Ʈ ã��
            hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            // hit�� �ɸ��� collider�� ������ �ش� collider�� �̸� ��������
            if (hit.collider != null) {
                // �ɸ��� ������Ʈ�� unit �̸� field�� ���� unit ������ ��������
                if (hit.collider.gameObject.name.Contains("Unit")) {
                    int cur_id = hit.collider.gameObject.GetComponent<Field>().UnitId;
                    // unit�� id �� ������� ������ �����ֱ�
                    if (cur_id != 0) {
                        // ���� ���� ���� datahub�� ����
                        datahub.CombineWaitingPos = int.Parse(hit.collider.gameObject.name.Split("_")[1]);
                        // �����ֱ� ������ �̹� �������� �ִ� ����� ������ �����
                        CleanList();
                        // ���ս� �����ֱ�
                        creater.ShowCombineFromMaterialTable(cur_id);
                    }

                }
            }
            else if (pos.x >= -1 && pos.x <= 2.82 && pos.y >= -4.37 && pos.y <= -2.2) {
                // do nothing
            }
            else {
                datahub.CombineWaitingPos = 0;
                CleanList();
            }
        }
    }

    private void CleanList() {
        // �� ������ Ŭ���ϸ� CombineField�� ����
        var list = CombineField.GetComponentsInChildren<Transform>();
        foreach (var item in list) {
            if (item != CombineField.transform) {
                Destroy(item.gameObject);
            }
        }
    }
}
