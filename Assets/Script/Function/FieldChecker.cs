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

        // unit 객체 담기
        int count = unit_field.transform.childCount;
        datahub.StageFieldNumber = count;

        Transform[] all_children = transform.GetComponentsInChildren<Transform>();
        ArrayList list = new();
        // 0번을 채우기 위해 빈 값 하나를 넣음
        GameObject tmp = new();
        list.Add(tmp);
        foreach (var child in all_children) {
            // 자기 자신은 패스
            if (child.name == transform.name) {
                continue;
            }
            list.Add(child.gameObject);
        }
        datahub.StageField = list;
    }

}
