using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class CreateCombineTable : MonoBehaviour
{
    private DataHub datahub;

    [SerializeField] private GameObject CombineField;

    private GameObject _function_2;
    private GameObject _function_3;
    private GameObject _function_4;

    private string path;

    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
        _function_2 = Resources.Load<GameObject>("Prefabs/CombineFunctionLineObject_2");
        _function_3 = Resources.Load<GameObject>("Prefabs/CombineFunctionLineObject_3");
        _function_4 = Resources.Load<GameObject>("Prefabs/CombineFunctionLineObject_4");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 클릭된 유닛으로 어떠한 조합이 가능한지 보기
    public void ShowCombineFromMaterialTable(int cur_id) {
        // id에 따라 self 이미지 path 저장
        path = UtilityHub.GetPath(cur_id);
        // id에 따라 조합할 수 있는 선택지 보여주기
        Unit now = datahub.Unit[cur_id] as Unit;
        int count = now.CombFucntion.Count;
        for (int i = 0; i < count; i++) {
            //now.CombFucntion[i].Print();
            // 조합식 보여주기
            string m_path_1, m_path_2, m_path_3, result_path;
            bool has_1, has_2, has_3;
            switch (now.CombFucntion[i].NeedCount) {
                case 1:
                    m_path_1 = UtilityHub.GetPath(now.CombFucntion[i].A);
                    result_path = UtilityHub.GetPath(now.CombFucntion[i].Result);
                    GameObject _combine_function_2 = Instantiate(_function_2, CombineField.transform.position, CombineField.transform.rotation);
                    _combine_function_2.transform.SetParent(CombineField.transform);
                    _combine_function_2.transform.Find("Self").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
                    _combine_function_2.transform.Find("Material").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_1);
                    _combine_function_2.transform.Find("Result").GetComponent<Image>().sprite = Resources.Load<Sprite>(result_path);
                    // result_id 설정
                    _combine_function_2.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = now.CombFucntion[i].Result;
                    // material에 id 부여
                    _combine_function_2.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = now.Id;
                    _combine_function_2.transform.Find("Material").GetComponent<CombineMaterialClick>().Id = now.CombFucntion[i].A;
                    // 각 unit들을 현재 소지하고 있다면 해당 위치의 back을 활성화
                    // 모든 back이 활성화 되면 result의 back을 활성화 하여 조합 가능함을 알리기
                    _combine_function_2.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                    has_1 = false;
                    if (datahub.UnitCounter[now.CombFucntion[i].A] >= 1) {
                        // 자기 자신은 빼고 계산해야함 - 기본 조합만 자기자신을 사용하므로 2 3 case 에서는 할 필요 없음
                        if (cur_id != now.CombFucntion[i].A) {
                            _combine_function_2.transform.Find("MaterialBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                            has_1 = true;
                        }
                        else if (cur_id == now.CombFucntion[i].A && datahub.UnitCounter[now.CombFucntion[i].A] >= 2) {
                            _combine_function_2.transform.Find("MaterialBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                            has_1 = true;
                        }
                    }
                    if (has_1) {
                        _combine_function_2.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                    }
                    break;
                case 2:
                    m_path_1 = UtilityHub.GetPath(now.CombFucntion[i].A);
                    m_path_2 = UtilityHub.GetPath(now.CombFucntion[i].B);
                    result_path = UtilityHub.GetPath(now.CombFucntion[i].Result);
                    GameObject _combine_function_3 = Instantiate(_function_3, CombineField.transform.position, CombineField.transform.rotation);
                    _combine_function_3.transform.SetParent(CombineField.transform);
                    _combine_function_3.transform.Find("Self").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
                    _combine_function_3.transform.Find("Material_1").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_1);
                    _combine_function_3.transform.Find("Material_2").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_2);
                    _combine_function_3.transform.Find("Result").GetComponent<Image>().sprite = Resources.Load<Sprite>(result_path);
                    // result_id 설정
                    _combine_function_3.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = now.CombFucntion[i].Result;
                    // material에 id 부여
                    _combine_function_3.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = now.Id;
                    _combine_function_3.transform.Find("Material_1").GetComponent<CombineMaterialClick>().Id = now.CombFucntion[i].A;
                    _combine_function_3.transform.Find("Material_2").GetComponent<CombineMaterialClick>().Id = now.CombFucntion[i].B;
                    // 각 unit들을 현재 소지하고 있다면 해당 위치의 back을 활성화
                    // 모든 back이 활성화 되면 result의 back을 활성화 하여 조합 가능함을 알리기
                    _combine_function_3.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                    has_1 = false; has_2 = false;
                    if (datahub.UnitCounter[now.CombFucntion[i].A] >= 1) {
                        _combine_function_3.transform.Find("MaterialBack_1").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                        has_1 = true;
                    }
                    if (datahub.UnitCounter[now.CombFucntion[i].B] >= 1) {
                        _combine_function_3.transform.Find("MaterialBack_2").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                        has_2 = true;
                    }

                    if (has_1 && has_2) {
                        _combine_function_3.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                    }
                    break;
                case 3:
                    m_path_1 = UtilityHub.GetPath(now.CombFucntion[i].A);
                    m_path_2 = UtilityHub.GetPath(now.CombFucntion[i].B);
                    m_path_3 = UtilityHub.GetPath(now.CombFucntion[i].C);
                    result_path = UtilityHub.GetPath(now.CombFucntion[i].Result);
                    GameObject _combine_function_4 = Instantiate(_function_4, CombineField.transform.position, CombineField.transform.rotation);
                    _combine_function_4.transform.SetParent(CombineField.transform);
                    _combine_function_4.transform.Find("Self").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
                    _combine_function_4.transform.Find("Material_1").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_1);
                    _combine_function_4.transform.Find("Material_2").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_2);
                    _combine_function_4.transform.Find("Material_3").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_3);
                    _combine_function_4.transform.Find("Result").GetComponent<Image>().sprite = Resources.Load<Sprite>(result_path);
                    // result_id 설정
                    _combine_function_4.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = now.CombFucntion[i].Result;
                    // material에 id 부여
                    _combine_function_4.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = now.Id;
                    _combine_function_4.transform.Find("Material_1").GetComponent<CombineMaterialClick>().Id = now.CombFucntion[i].A;
                    _combine_function_4.transform.Find("Material_2").GetComponent<CombineMaterialClick>().Id = now.CombFucntion[i].B;
                    _combine_function_4.transform.Find("Material_3").GetComponent<CombineMaterialClick>().Id = now.CombFucntion[i].C;
                    // 각 unit들을 현재 소지하고 있다면 해당 위치의 back을 활성화
                    // 모든 back이 활성화 되면 result의 back을 활성화 하여 조합 가능함을 알리기
                    _combine_function_4.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                    has_1 = false; has_2 = false; has_3 = false;
                    if (datahub.UnitCounter[now.CombFucntion[i].A] >= 1) {
                        _combine_function_4.transform.Find("MaterialBack_1").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                        has_1 = true;
                    }
                    if (datahub.UnitCounter[now.CombFucntion[i].B] >= 1) {
                        _combine_function_4.transform.Find("MaterialBack_2").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                        has_2 = true;
                    }
                    if (datahub.UnitCounter[now.CombFucntion[i].C] >= 1) {
                        _combine_function_4.transform.Find("MaterialBack_3").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                        has_3 = true;
                    }

                    if (has_1 && has_2 && has_3) {
                        _combine_function_4.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                    }
                    break;
            }
        }
    }

    // 클릭된 유닛이 결과로 나오는 조합들을 보기
    public void ShowCombineToResultTable(int result_id) {
        // 번호를 기반으로함
        // 등급을 나누면 편할듯
        // 유닛의 번호에 따라 검색할 구간을 지정
        // 1~4 : 미동 없음
        // 5~13 : advenced
        // 14~23 : luxury
        // 24~35 : limit
        // 36~39 : custom
        int size = 0; // 2중 for문 안의 순회횟수
        string m_path_1, m_path_2, m_path_3, m_path_4, result_path = UtilityHub.GetPath(result_id);
        bool has_1, has_2, has_3, has_4;
        if ((result_id >= 1 && result_id <= 4)||(result_id>=36 && result_id <= 39)) {
            // 결과가 될 수 없거나, 재료가 될 수 없으므로 예외처리
        }
        // advenced 등급이므로 1 2 3의 unit 목록을 돌아 결과가 result_id와 같은 combinefunction을 표시함
        else if (result_id >= 5 && result_id <= 13) {
            CleanList();
            for (int i = 1; i <= 3; i++) {
                // 1 2 3 만 순회하므로 조합식의 needCount는 항상 1 따라서 A 값만 확인하면 됨
                Unit unit = datahub.Unit[i] as Unit;
                size = unit.CombFucntion.Count;
                for (int j = 0; j < size; j++) {
                    // 결과가 같은 것 찾기
                    // 같으면 만들어 출력하기
                    if (unit.CombFucntion[j].Result == result_id) {
                        // 재료 2개 path설정
                        m_path_1 = UtilityHub.GetPath(unit.Id);
                        m_path_2 = UtilityHub.GetPath(unit.CombFucntion[j].A);
                        GameObject _combine_function_2 = Instantiate(_function_2, CombineField.transform.position, CombineField.transform.rotation);
                        _combine_function_2.transform.SetParent(CombineField.transform);
                        _combine_function_2.transform.Find("Self").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_1);
                        _combine_function_2.transform.Find("Material").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_2);
                        _combine_function_2.transform.Find("Result").GetComponent<Image>().sprite = Resources.Load<Sprite>(result_path);
                        // result_id 설정
                        _combine_function_2.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = result_id;
                        // material id 부여
                        _combine_function_2.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = unit.Id;
                        _combine_function_2.transform.Find("Material").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].A;
                        // 각 unit들을 현재 소지하고 있다면 해당 위치의 back을 활성화
                        // 모든 back이 활성화 되면 result의 back을 활성화 하여 조합 가능함을 알리기
                        has_1 = false; has_2 = false;
                        if (datahub.UnitCounter[unit.Id] >= 1) {
                            _combine_function_2.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                            has_1 = true;
                        }
                        
                        if (datahub.UnitCounter[unit.CombFucntion[j].A] >= 1) {
                            // 자기 자신은 빼고 계산해야함 - 기본 조합만 자기자신을 사용하므로 2 3 case 에서는 할 필요 없음
                            if (unit.Id != unit.CombFucntion[j].A) {
                                _combine_function_2.transform.Find("MaterialBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                                has_2 = true;
                            }
                            else if (unit.Id == unit.CombFucntion[j].A && datahub.UnitCounter[unit.CombFucntion[j].A] >= 2) {
                                _combine_function_2.transform.Find("MaterialBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                                has_2 = true;
                            }
                        }
                        if (has_1 && has_2) {
                            _combine_function_2.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                        }
                    }
                }
            }
        }

        // luxury 등급이므로 5~13을 검색
        else if (result_id >= 14 && result_id <= 23) {
            CleanList();
            for (int i = 5; i <= 13; i++) {
                // 5~13 만 순회하므로 조합식의 needCount는 항상 3 따라서 A B C 값 모두 확인하면 됨
                Unit unit = datahub.Unit[i] as Unit;
                size = unit.CombFucntion.Count;
                for (int j = 0; j < size; j++) {
                    // 결과가 같은 것 찾기
                    // 같으면 만들어 출력하기
                    if (unit.CombFucntion[j].Result == result_id) {
                        // 재료 2개 path설정
                        m_path_1 = UtilityHub.GetPath(unit.Id);
                        m_path_2 = UtilityHub.GetPath(unit.CombFucntion[j].A);
                        m_path_3 = UtilityHub.GetPath(unit.CombFucntion[j].B);
                        m_path_4 = UtilityHub.GetPath(unit.CombFucntion[j].C);
                        GameObject _combine_function_4 = Instantiate(_function_4, CombineField.transform.position, CombineField.transform.rotation);
                        _combine_function_4.transform.SetParent(CombineField.transform);
                        _combine_function_4.transform.Find("Self").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_1);
                        _combine_function_4.transform.Find("Material_1").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_2);
                        _combine_function_4.transform.Find("Material_2").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_3);
                        _combine_function_4.transform.Find("Material_3").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_4);
                        _combine_function_4.transform.Find("Result").GetComponent<Image>().sprite = Resources.Load<Sprite>(result_path);
                        // result_id 설정
                        _combine_function_4.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = result_id;
                        // material id 부여
                        _combine_function_4.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = unit.Id;
                        _combine_function_4.transform.Find("Material_1").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].A;
                        _combine_function_4.transform.Find("Material_2").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].B;
                        _combine_function_4.transform.Find("Material_3").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].C;
                        // 각 unit들을 현재 소지하고 있다면 해당 위치의 back을 활성화
                        // 모든 back이 활성화 되면 result의 back을 활성화 하여 조합 가능함을 알리기
                        has_1 = false; has_2 = false; has_3 = false; has_4 = false;
                        if (datahub.UnitCounter[unit.Id] >= 1) {
                            _combine_function_4.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                            has_1 = true;
                        }
                        if (datahub.UnitCounter[unit.CombFucntion[j].A] >= 1) {
                            _combine_function_4.transform.Find("MaterialBack_1").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                            has_2 = true;
                        }
                        if (datahub.UnitCounter[unit.CombFucntion[j].B] >= 1) {
                            _combine_function_4.transform.Find("MaterialBack_2").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                            has_3 = true;
                        }
                        if (datahub.UnitCounter[unit.CombFucntion[j].C] >= 1) {
                            _combine_function_4.transform.Find("MaterialBack_3").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                            has_4 = true;
                        }
                        if (has_1 && has_2 && has_3 && has_4) {
                            _combine_function_4.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                        }
                    }
                }
            }
        }

        // limit 등급이므로 14~23을 검색
        else if (result_id >= 24 && result_id <= 35) {
            CleanList();
            for (int i = 5; i <= 13; i++) {
                // 5~13 만 순회하므로 조합식의 needCount는 항상 3 따라서 A B C 값 모두 확인하면 됨
                Unit unit = datahub.Unit[i] as Unit;
                size = unit.CombFucntion.Count;
                for (int j = 0; j < size; j++) {
                    // 결과가 같은 것 찾기
                    // 같으면 만들어 출력하기
                    if (unit.CombFucntion[j].Result == result_id) {
                        // 재료 2개 path설정
                        m_path_1 = UtilityHub.GetPath(unit.Id);
                        m_path_2 = UtilityHub.GetPath(unit.CombFucntion[j].A);
                        m_path_3 = UtilityHub.GetPath(unit.CombFucntion[j].B);
                        GameObject _combine_function_3 = Instantiate(_function_4, CombineField.transform.position, CombineField.transform.rotation);
                        _combine_function_3.transform.SetParent(CombineField.transform);
                        _combine_function_3.transform.Find("Self").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_1);
                        _combine_function_3.transform.Find("Material_1").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_2);
                        _combine_function_3.transform.Find("Material_2").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_3);
                        _combine_function_3.transform.Find("Result").GetComponent<Image>().sprite = Resources.Load<Sprite>(result_path);
                        // result_id 설정
                        _combine_function_3.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = result_id;
                        // material id 부여
                        _combine_function_3.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = unit.Id;
                        _combine_function_3.transform.Find("Material_1").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].A;
                        _combine_function_3.transform.Find("Material_2").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].B;
                        // 각 unit들을 현재 소지하고 있다면 해당 위치의 back을 활성화
                        // 모든 back이 활성화 되면 result의 back을 활성화 하여 조합 가능함을 알리기
                        has_1 = false; has_2 = false; has_3 = false; has_4 = false;
                        if (datahub.UnitCounter[unit.Id] >= 1) {
                            _combine_function_3.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                            has_1 = true;
                        }
                        if (datahub.UnitCounter[unit.CombFucntion[j].A] >= 1) {
                            _combine_function_3.transform.Find("MaterialBack_1").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                            has_2 = true;
                        }
                        if (datahub.UnitCounter[unit.CombFucntion[j].B] >= 1) {
                            _combine_function_3.transform.Find("MaterialBack_2").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                            has_3 = true;
                        }
                        if (has_1 && has_2 && has_3) {
                            _combine_function_3.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                        }
                    }
                }
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
