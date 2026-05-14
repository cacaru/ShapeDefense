using System.Collections;
using TMPro;
using UnityEngine;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;
using UnityEngine.Tilemaps;

public class FieldChecker : MonoBehaviour
{
    [SerializeField] private GameObject unit_field;
    [SerializeField] private Tilemap unit_map;


    // Start is called before the first frame update
    void Start()
    {
        //var all_child = GameObject.FindGameObjectsWithTag("Unit_Wait");
        Transform[] all_children = transform.GetComponentsInChildren<Transform>();
        ArrayList list = new();
        // 0번을 채우기 위해 빈 값 하나를 넣음
        GameObject tmp = new();
        list.Add(tmp);
        foreach (var child in all_children) {
            // 자기 자신은 패스
            if (child.name == transform.name || child.name.Contains("Border")) {
                continue;
            }
            
            list.Add(child.gameObject);
        }

        datahub.StageField = list;
        datahub.StageFieldNumber = list.Count - 1;
        datahub.LeftStageField = list.Count-1;

        // 이름 순서대로 정렬하기
        for (int i = 1; i <= datahub.StageFieldNumber; i++) {

            GameObject obj = datahub.StageField[i] as GameObject;
            UtilityHub.query_builder.Append(obj.GetComponent<Field>().name + ",");
        }
        //GameObject.Find("Canvas").transform.Find("test2").gameObject.GetComponent<TMP_Text>().text = UtilityHub.query_builder.ToString();
        UtilityHub.query_builder.Clear();

        // unit tile 저장
        datahub.UnitMap = unit_map;
    }

}
