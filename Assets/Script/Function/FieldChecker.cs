using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldChecker : MonoBehaviour
{
    [SerializeField]
    private GameObject unit_field;

    private DataHub datahub;

    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();

        // unit ��ü ���
        int count = unit_field.transform.childCount;
        datahub.StageFieldNumber = count;

        Transform[] all_children = transform.GetComponentsInChildren<Transform>();
        ArrayList list = new();
        // 0���� ä��� ���� �� �� �ϳ��� ����
        GameObject tmp = new();
        list.Add(tmp);
        foreach (var child in all_children) {
            // �ڱ� �ڽ��� �н�
            if (child.name == transform.name) {
                continue;
            }
            list.Add(child.gameObject);
        }
        datahub.StageField = list;
    }

}
