using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 위 유닛 화면 클릭 이벤트
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
            //클릭위치 좌표
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //해당 좌표에 있는 오브젝트 찾기
            hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            // hit에 걸리는 collider가 있으면 해당 collider의 이름 가져오기
            if (hit.collider != null) {
                // 걸리는 오브젝트가 unit 이면 field를 통해 unit 정보를 가져오기
                if (hit.collider.gameObject.name.Contains("Unit")) {
                    int cur_id = hit.collider.gameObject.GetComponent<Field>().UnitId;
                    // unit에 id 가 비어있지 않으면 보여주기
                    if (cur_id != 0) {
                        // 현재 선택 값을 datahub에 저장
                        datahub.CombineWaitingPos = int.Parse(hit.collider.gameObject.name.Split("_")[1]);
                        // 보여주기 이전에 이미 보여지고 있는 목록이 있으면 지우기
                        CleanList();
                        // 조합식 보여주기
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
        // 빈 공간을 클릭하면 CombineField를 비우기
        var list = CombineField.GetComponentsInChildren<Transform>();
        foreach (var item in list) {
            if (item != CombineField.transform) {
                Destroy(item.gameObject);
            }
        }
    }
}
