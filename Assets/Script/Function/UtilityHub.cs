using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityHub
{
    /// <summary>
    /// ������ �ʵ��� ���� ��ġ�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="a">30�� Ȯ�� 1</param>
    /// <param name="b">30�� Ȯ�� 2</param>
    /// <param name="c">30�� Ȯ�� 3</param>
    /// <param name="d">10�� Ȯ�� 4</param>
    public static void UnitCreateRandomField(int a, int b, int c, int d) {

        DataHub datahub = GameObject.Find("GameObject").GetComponent<DataHub>();

        // ������ ����
        // ������ ��ġ�� �������� �����ؾ���
        while (true) {
            int pos = Random.Range(1, datahub.StageFieldNumber + 1);
            GameObject tmp_field = datahub.StageField[pos] as GameObject;
            // 0 �� �ƴϸ� ���⿡ �ֱ�
            if (tmp_field.GetComponent<Field>().UnitId == 0) {
                // ���ֵ� 1~4 �� �ϳ� �����ϰ� �ֱ�
                // ���� �������ִ� ��� ���� ���� �� �� �־�� �� -> ������ ���� > ���� ������ ������
                int tmp_unit_id = Random.Range(1, 1001); // 1~4 �� 1�� ���� -> Ȯ�� ���� �� 100 �� 30 30 30 10 �� Ȯ��

                if (tmp_unit_id >= 1 && tmp_unit_id < 301) {
                    // a
                    datahub.UnitCounter[a]++;
                    tmp_field.GetComponent<Field>().UnitId = a;
                    tmp_field.GetComponent<UnitAttack>().Id = a;
                }
                else if (tmp_unit_id >= 301 && tmp_unit_id < 601) {
                    // b
                    datahub.UnitCounter[b]++;
                    tmp_field.GetComponent<Field>().UnitId = b;
                    tmp_field.GetComponent<UnitAttack>().Id = b;
                }
                else if (tmp_unit_id >= 601 && tmp_unit_id < 901) {
                    // c
                    datahub.UnitCounter[c]++;
                    tmp_field.GetComponent<Field>().UnitId = c;
                    tmp_field.GetComponent<UnitAttack>().Id = c;
                }
                else if (tmp_unit_id >= 901 && tmp_unit_id < 1001) {
                    // d
                    datahub.UnitCounter[d]++;
                    tmp_field.GetComponent<Field>().UnitId = d;
                    tmp_field.GetComponent<UnitAttack>().Id = d;
                }
                // �ְ� ���ѷ��� ����
                datahub.StageField[pos] = tmp_field;
                break;
            }
        }
    }


    /// <summary>
    /// id�� ���� resoruces ���� �ε��� sprite�� path�� ����
    /// </summary>
    /// <param name="id">unit id for sprite image</param>
    public static string GetPath(int id) {
        string path = id switch {
            1 => "Sprite/Unit/normal_circle",
            2 => "Sprite/Unit/normal_triangle",
            3 => "Sprite/Unit/normal_square",
            4 => "Sprite/Unit/normal_star",
            5 => "Sprite/Unit/ad_circle_1",
            6 => "Sprite/Unit/ad_circle_2",
            7 => "Sprite/Unit/ad_circle_3",
            8 => "Sprite/Unit/ad_triangle_1",
            9 => "Sprite/Unit/ad_triangle_2",
            10 => "Sprite/Unit/ad_triangle_3",
            11 => "Sprite/Unit/ad_square_1",
            12 => "Sprite/Unit/ad_square_2",
            13 => "Sprite/Unit/ad_square_3",
            14 => "Sprite/Unit/lu_circle_1",
            15 => "Sprite/Unit/lu_circle_2",
            16 => "Sprite/Unit/lu_circle_3",
            17 => "Sprite/Unit/lu_triangle_1",
            18 => "Sprite/Unit/lu_triangle_2",
            19 => "Sprite/Unit/lu_triangle_3",
            20 => "Sprite/Unit/lu_square_1",
            21 => "Sprite/Unit/lu_square_2",
            22 => "Sprite/Unit/lu_square_3",
            23 => "Sprite/Unit/lu_star",
            24 => "Sprite/Unit/li_circle_1",
            25 => "Sprite/Unit/li_circle_2",
            26 => "Sprite/Unit/li_circle_3",
            27 => "Sprite/Unit/li_triangle_1",
            28 => "Sprite/Unit/li_triangle_2",
            29 => "Sprite/Unit/li_triangle_3",
            30 => "Sprite/Unit/li_square_1",
            31 => "Sprite/Unit/li_square_2",
            32 => "Sprite/Unit/li_square_3",
            33 => "Sprite/Unit/li_star_1",
            34 => "Sprite/Unit/li_star_2",
            35 => "Sprite/Unit/li_star_3",
            36 => "Sprite/Unit/c_circle",
            37 => "Sprite/Unit/c_triangle",
            38 => "Sprite/Unit/c_square",
            39 => "Sprite/Unit/c_star",
            // ���ϴ� ������ 
            40 => "Sprite/Unit/lu_circle_0",
            41 => "Sprite/Unit/lu_triangle_0",
            42 => "Sprite/Unit/lu_square_0",
            43 => "Sprite/Unit/lu_star_0",
            44 => "Sprite/Unit/li_circle_0",
            45 => "Sprite/Unit/li_triangle_0",
            46 => "Sprite/Unit/li_square_0",
            47 => "Sprite/Unit/li_star_0",
            
            _ => "",
        };
        return path;
    }


    /// <summary>
    /// ���� �������� Ȯ���ϰ� ������ �����ϴ� �Լ�
    /// </summary>
    /// datahub�� ����� �����͸� ������� ����Ǳ� ������ param ����
    public static void CombineCheck() {
        
        DataHub datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
        // ��� ����
        // base id
        // target result
        // datahub �� unitcounter
        // stage field

        // base id instance
        int base_id = ((GameObject)datahub.StageField[datahub.CombineWaitingPos]).GetComponent<Field>().UnitId;
        Unit base_unit = datahub.Unit[base_id] as Unit;
        int target_id = datahub.CombineTargetId;

        //base_id�� target �� �ش��ϴ� ���ս� ã��
        CombineFunction tmp = new();
        int size = base_unit.CombFucntion.Count;
        for (int i = 0; i < size; i++) {
            if (base_unit.CombFucntion[i].Result == target_id) {
                tmp = base_unit.CombFucntion[i];
                break;
            }
        }
        bool can = false;
        // ���� ������ �� Ȯ��
        switch (tmp.NeedCount) {
            case 1:
                // ���� baseid �� ���ٸ� 2���� , �ٸ��ٸ� 1���� �ִ��� Ȯ��
                if (base_unit.Id == tmp.A) {
                    if (datahub.UnitCounter[tmp.A] >= 2)
                        can = true;
                    else
                        can = false;
                }
                else {
                    if (datahub.UnitCounter[tmp.A] >= 1)
                        can = true;
                    else
                        can = false;
                }
                break;
            // ��� 2�� �̻��� �ߺ��� �����Ƿ� 1 �̻��̸� ����
            case 2:
                if (datahub.UnitCounter[tmp.A] >= 1 && datahub.UnitCounter[tmp.B] >= 1)
                    can = true;
                else
                    can = false;
                break;
            case 3:
                if (datahub.UnitCounter[tmp.A] >= 1 && datahub.UnitCounter[tmp.B] >= 1 && datahub.UnitCounter[tmp.C] >= 1)
                    can = true;
                else
                    can = false;
                break;
            default:
                break;
        }


        // ���� �����ϸ� ����
        if (can) {
            size = datahub.StageField.Count;
            // ������ �� �ʿ��� ������ ���� �� unit field���� ����

            // ���̽��� unitcounter�� ����
            datahub.UnitCounter[base_id]--;

            // �ʿ��� ��� A�� ���� �ʵ��� ���� ��ȣ�� ���� �� ���� ã��
            // A�� ��� ���� �ݵ�� �����ϹǷ� �ٷ� ����
            // ���� ��ȣ && Ŭ�� ��ġ�� �ѱ��
            if (base_id == tmp.A) {

                for (int i = 1; i < size; i++) {
                    GameObject search_field = datahub.StageField[i] as GameObject;
                    if (i != datahub.CombineWaitingPos && tmp.A == search_field.GetComponent<Field>().UnitId) {
                        // unit counter���� ����
                        datahub.UnitCounter[tmp.A] -= 1;
                        // �ʿ� ����̹Ƿ� 0���� ����
                        search_field.GetComponent<Field>().UnitId = 0;
                        search_field.GetComponent<UnitAttack>().Id = 0;
                        search_field.GetComponent<UnitAttack>().StopShot();
                        //search_field.GetComponent<UnitAttack>().enabled = false;
                        // unitcounter ����]
                        break;
                    }
                }
            }
            // ���� �ٸ� ����� �ٷ� ã��
            else {
                for (int i = 1; i < size; i++) {
                    GameObject search_field = datahub.StageField[i] as GameObject;
                    if (search_field.GetComponent<Field>().UnitId == tmp.A) {
                        // unit counter���� ����
                        datahub.UnitCounter[tmp.A] -= 1;
                        search_field.GetComponent<Field>().UnitId = 0;
                        search_field.GetComponent<UnitAttack>().Id = 0;
                        search_field.GetComponent<UnitAttack>().StopShot();
                        //search_field.GetComponent<UnitAttack>().enabled = false;
                        break;
                    }
                }
            }

            // �ʿ���ᰡ 2�� �̻��̶�� B���� ��������
            if (tmp.NeedCount >= 2) {
                // B ã�� �����
                for (int i = 1; i < size; i++) {
                    GameObject search_field = datahub.StageField[i] as GameObject;
                    int field_id = search_field.GetComponent<Field>().UnitId;
                    if (field_id == tmp.B) {
                        // unit counter���� ����
                        datahub.UnitCounter[tmp.B] -= 1;
                        search_field.GetComponent<Field>().UnitId = 0;
                        search_field.GetComponent<UnitAttack>().Id = 0;
                        search_field.GetComponent<UnitAttack>().StopShot();
                        //search_field.GetComponent<UnitAttack>().enabled = false;
                        break;
                    }
                }
            }

            //�ʿ� ��ᰡ 3�� �̻��̶�� C ���� ��������
            if (tmp.NeedCount >= 3) {
                // C ã�� �����
                for (int i = 1; i < size; i++) {
                    GameObject search_field = datahub.StageField[i] as GameObject;
                    int field_id = search_field.GetComponent<Field>().UnitId;
                    if (field_id == tmp.C) {
                        // unit counter���� ����
                        datahub.UnitCounter[tmp.C] -= 1;
                        search_field.GetComponent<Field>().UnitId = 0;
                        search_field.GetComponent<UnitAttack>().Id = 0;
                        search_field.GetComponent<UnitAttack>().StopShot();
                        //search_field.GetComponent<UnitAttack>().enabled = false;
                        break;
                    }
                }
            }
            
            // ��� ���Ű� �Ϸ�Ǿ����Ƿ� Ŭ�� ��ġ�� ������ ��� �������� ����
            GameObject now_select_field = datahub.StageField[datahub.CombineWaitingPos] as GameObject;
            now_select_field.GetComponent<Field>().UnitId = tmp.Result;
            now_select_field.GetComponent<UnitAttack>().Id = tmp.Result;

            // ��� ���� ī��Ʈ ����
            datahub.UnitCounter[tmp.Result]++;
        }

        // ���� �Ұ����ϸ� ī�带 ���� ����Ʈ�� �Ѱ� ����
        else {
            Debug.Log("Combine cannot possible");
        }
    }

    /// <summary>
    /// ���� ���Ḧ Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="stage_number">���� �������� ��ȣ</param>
    /// <param name="now_enemy_round">����� ���� ��ȣ</param>
    /// <returns>
    /// 0 : Enemy ���� ����
    /// 1 : Enemy ����
    /// 2 : ���� �Ϸ�
    /// </returns>
    public static int EndRoundChecker(int stage_number, int now_enemy_round) {
        // �������� ������ �������� ����
        switch (stage_number) {
            case 1:
                // 65���� ���� ���� -> ������ ��µ� 5���� �߰� �ð��� �ֹǷ� ���� 65����
                if (now_enemy_round >= 60) {
                    // spawn�� �����ؾ���
                    return 0;
                }
                else if (now_enemy_round == 65) {
                    return 2;
                }
                break;
            case 2:
                if (now_enemy_round >= 70) {
                    return 0;
                }
                else if ( now_enemy_round == 75) {
                    return 2;
                }
                // 75
                break;
            case 3:
                if (now_enemy_round >= 80) {
                    return 0;
                }
                else if (now_enemy_round == 85) {
                    return 2;
                }
                // 85
                break;
        }

        // �⺻�����δ� ���带 �����ؾ��ϹǷ� true ��ȯ
        return 1;
    }
}
