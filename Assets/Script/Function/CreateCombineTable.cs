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

    // Ŭ���� �������� ��� ������ �������� ����
    public void ShowCombineFromMaterialTable(int cur_id) {
        // id�� ���� self �̹��� path ����
        path = UtilityHub.GetPath(cur_id);
        // id�� ���� ������ �� �ִ� ������ �����ֱ�
        Unit now = datahub.Unit[cur_id] as Unit;
        int count = now.CombFucntion.Count;
        for (int i = 0; i < count; i++) {
            //now.CombFucntion[i].Print();
            // ���ս� �����ֱ�
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
                    // result_id ����
                    _combine_function_2.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = now.CombFucntion[i].Result;
                    // material�� id �ο�
                    _combine_function_2.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = now.Id;
                    _combine_function_2.transform.Find("Material").GetComponent<CombineMaterialClick>().Id = now.CombFucntion[i].A;
                    // �� unit���� ���� �����ϰ� �ִٸ� �ش� ��ġ�� back�� Ȱ��ȭ
                    // ��� back�� Ȱ��ȭ �Ǹ� result�� back�� Ȱ��ȭ �Ͽ� ���� �������� �˸���
                    _combine_function_2.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                    has_1 = false;
                    if (datahub.UnitCounter[now.CombFucntion[i].A] >= 1) {
                        // �ڱ� �ڽ��� ���� ����ؾ��� - �⺻ ���ո� �ڱ��ڽ��� ����ϹǷ� 2 3 case ������ �� �ʿ� ����
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
                    // result_id ����
                    _combine_function_3.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = now.CombFucntion[i].Result;
                    // material�� id �ο�
                    _combine_function_3.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = now.Id;
                    _combine_function_3.transform.Find("Material_1").GetComponent<CombineMaterialClick>().Id = now.CombFucntion[i].A;
                    _combine_function_3.transform.Find("Material_2").GetComponent<CombineMaterialClick>().Id = now.CombFucntion[i].B;
                    // �� unit���� ���� �����ϰ� �ִٸ� �ش� ��ġ�� back�� Ȱ��ȭ
                    // ��� back�� Ȱ��ȭ �Ǹ� result�� back�� Ȱ��ȭ �Ͽ� ���� �������� �˸���
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
                    // result_id ����
                    _combine_function_4.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = now.CombFucntion[i].Result;
                    // material�� id �ο�
                    _combine_function_4.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = now.Id;
                    _combine_function_4.transform.Find("Material_1").GetComponent<CombineMaterialClick>().Id = now.CombFucntion[i].A;
                    _combine_function_4.transform.Find("Material_2").GetComponent<CombineMaterialClick>().Id = now.CombFucntion[i].B;
                    _combine_function_4.transform.Find("Material_3").GetComponent<CombineMaterialClick>().Id = now.CombFucntion[i].C;
                    // �� unit���� ���� �����ϰ� �ִٸ� �ش� ��ġ�� back�� Ȱ��ȭ
                    // ��� back�� Ȱ��ȭ �Ǹ� result�� back�� Ȱ��ȭ �Ͽ� ���� �������� �˸���
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

    // Ŭ���� ������ ����� ������ ���յ��� ����
    public void ShowCombineToResultTable(int result_id) {
        // ��ȣ�� ���������
        // ����� ������ ���ҵ�
        // ������ ��ȣ�� ���� �˻��� ������ ����
        // 1~4 : �̵� ����
        // 5~13 : advenced
        // 14~23 : luxury
        // 24~35 : limit
        // 36~39 : custom
        int size = 0; // 2�� for�� ���� ��ȸȽ��
        string m_path_1, m_path_2, m_path_3, m_path_4, result_path = UtilityHub.GetPath(result_id);
        bool has_1, has_2, has_3, has_4;
        if ((result_id >= 1 && result_id <= 4)||(result_id>=36 && result_id <= 39)) {
            // ����� �� �� ���ų�, ��ᰡ �� �� �����Ƿ� ����ó��
        }
        // advenced ����̹Ƿ� 1 2 3�� unit ����� ���� ����� result_id�� ���� combinefunction�� ǥ����
        else if (result_id >= 5 && result_id <= 13) {
            CleanList();
            for (int i = 1; i <= 3; i++) {
                // 1 2 3 �� ��ȸ�ϹǷ� ���ս��� needCount�� �׻� 1 ���� A ���� Ȯ���ϸ� ��
                Unit unit = datahub.Unit[i] as Unit;
                size = unit.CombFucntion.Count;
                for (int j = 0; j < size; j++) {
                    // ����� ���� �� ã��
                    // ������ ����� ����ϱ�
                    if (unit.CombFucntion[j].Result == result_id) {
                        // ��� 2�� path����
                        m_path_1 = UtilityHub.GetPath(unit.Id);
                        m_path_2 = UtilityHub.GetPath(unit.CombFucntion[j].A);
                        GameObject _combine_function_2 = Instantiate(_function_2, CombineField.transform.position, CombineField.transform.rotation);
                        _combine_function_2.transform.SetParent(CombineField.transform);
                        _combine_function_2.transform.Find("Self").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_1);
                        _combine_function_2.transform.Find("Material").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_2);
                        _combine_function_2.transform.Find("Result").GetComponent<Image>().sprite = Resources.Load<Sprite>(result_path);
                        // result_id ����
                        _combine_function_2.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = result_id;
                        // material id �ο�
                        _combine_function_2.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = unit.Id;
                        _combine_function_2.transform.Find("Material").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].A;
                        // �� unit���� ���� �����ϰ� �ִٸ� �ش� ��ġ�� back�� Ȱ��ȭ
                        // ��� back�� Ȱ��ȭ �Ǹ� result�� back�� Ȱ��ȭ �Ͽ� ���� �������� �˸���
                        has_1 = false; has_2 = false;
                        if (datahub.UnitCounter[unit.Id] >= 1) {
                            _combine_function_2.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true);
                            has_1 = true;
                        }
                        
                        if (datahub.UnitCounter[unit.CombFucntion[j].A] >= 1) {
                            // �ڱ� �ڽ��� ���� ����ؾ��� - �⺻ ���ո� �ڱ��ڽ��� ����ϹǷ� 2 3 case ������ �� �ʿ� ����
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

        // luxury ����̹Ƿ� 5~13�� �˻�
        else if (result_id >= 14 && result_id <= 23) {
            CleanList();
            for (int i = 5; i <= 13; i++) {
                // 5~13 �� ��ȸ�ϹǷ� ���ս��� needCount�� �׻� 3 ���� A B C �� ��� Ȯ���ϸ� ��
                Unit unit = datahub.Unit[i] as Unit;
                size = unit.CombFucntion.Count;
                for (int j = 0; j < size; j++) {
                    // ����� ���� �� ã��
                    // ������ ����� ����ϱ�
                    if (unit.CombFucntion[j].Result == result_id) {
                        // ��� 2�� path����
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
                        // result_id ����
                        _combine_function_4.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = result_id;
                        // material id �ο�
                        _combine_function_4.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = unit.Id;
                        _combine_function_4.transform.Find("Material_1").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].A;
                        _combine_function_4.transform.Find("Material_2").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].B;
                        _combine_function_4.transform.Find("Material_3").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].C;
                        // �� unit���� ���� �����ϰ� �ִٸ� �ش� ��ġ�� back�� Ȱ��ȭ
                        // ��� back�� Ȱ��ȭ �Ǹ� result�� back�� Ȱ��ȭ �Ͽ� ���� �������� �˸���
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

        // limit ����̹Ƿ� 14~23�� �˻�
        else if (result_id >= 24 && result_id <= 35) {
            CleanList();
            for (int i = 5; i <= 13; i++) {
                // 5~13 �� ��ȸ�ϹǷ� ���ս��� needCount�� �׻� 3 ���� A B C �� ��� Ȯ���ϸ� ��
                Unit unit = datahub.Unit[i] as Unit;
                size = unit.CombFucntion.Count;
                for (int j = 0; j < size; j++) {
                    // ����� ���� �� ã��
                    // ������ ����� ����ϱ�
                    if (unit.CombFucntion[j].Result == result_id) {
                        // ��� 2�� path����
                        m_path_1 = UtilityHub.GetPath(unit.Id);
                        m_path_2 = UtilityHub.GetPath(unit.CombFucntion[j].A);
                        m_path_3 = UtilityHub.GetPath(unit.CombFucntion[j].B);
                        GameObject _combine_function_3 = Instantiate(_function_4, CombineField.transform.position, CombineField.transform.rotation);
                        _combine_function_3.transform.SetParent(CombineField.transform);
                        _combine_function_3.transform.Find("Self").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_1);
                        _combine_function_3.transform.Find("Material_1").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_2);
                        _combine_function_3.transform.Find("Material_2").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_path_3);
                        _combine_function_3.transform.Find("Result").GetComponent<Image>().sprite = Resources.Load<Sprite>(result_path);
                        // result_id ����
                        _combine_function_3.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = result_id;
                        // material id �ο�
                        _combine_function_3.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = unit.Id;
                        _combine_function_3.transform.Find("Material_1").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].A;
                        _combine_function_3.transform.Find("Material_2").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].B;
                        // �� unit���� ���� �����ϰ� �ִٸ� �ش� ��ġ�� back�� Ȱ��ȭ
                        // ��� back�� Ȱ��ȭ �Ǹ� result�� back�� Ȱ��ȭ �Ͽ� ���� �������� �˸���
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
        // �� ������ Ŭ���ϸ� CombineField�� ����
        var list = CombineField.GetComponentsInChildren<Transform>();
        foreach (var item in list) {
            if (item != CombineField.transform) {
                Destroy(item.gameObject);
            }
        }
    }
}
