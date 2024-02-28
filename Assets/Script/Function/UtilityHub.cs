using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityHub
{
    /// <summary>
    /// 유닛을 필드의 랜덤 위치에 생성하는 함수
    /// </summary>
    /// <param name="a">30퍼 확률 1</param>
    /// <param name="b">30퍼 확률 2</param>
    /// <param name="c">30퍼 확률 3</param>
    /// <param name="d">10퍼 확률 4</param>
    public static void UnitCreateRandomField(int a, int b, int c, int d) {

        DataHub datahub = GameObject.Find("GameObject").GetComponent<DataHub>();

        // 유닛을 생성
        // 생성될 위치를 랜덤으로 형성해야함
        while (true) {
            int pos = Random.Range(1, datahub.StageFieldNumber + 1);
            GameObject tmp_field = datahub.StageField[pos] as GameObject;
            // 0 이 아니면 여기에 넣기
            if (tmp_field.GetComponent<Field>().UnitId == 0) {
                // 유닛도 1~4 중 하나 랜덤하게 넣기
                // 현재 가지고있는 모든 유닛 수를 알 수 있어야 함 -> 조합을 위해 > 유닛 갯수를 세놓기
                int tmp_unit_id = Random.Range(1, 1001); // 1~4 중 1개 나옴 -> 확률 지정 총 100 중 30 30 30 10 퍼 확률

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
                // 넣고 무한루프 종료
                datahub.StageField[pos] = tmp_field;
                break;
            }
        }
    }


    /// <summary>
    /// id를 통해 resoruces 에서 로드할 sprite의 path를 구함
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
            // 이하는 아이템 
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
    /// 조합 가능한지 확인하고 조합을 진행하는 함수
    /// </summary>
    /// datahub에 저장된 데이터를 기반으로 수행되기 떄문에 param 없음
    public static void CombineCheck() {
        
        DataHub datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
        // 사용 정보
        // base id
        // target result
        // datahub 의 unitcounter
        // stage field

        // base id instance
        int base_id = ((GameObject)datahub.StageField[datahub.CombineWaitingPos]).GetComponent<Field>().UnitId;
        Unit base_unit = datahub.Unit[base_id] as Unit;
        int target_id = datahub.CombineTargetId;

        //base_id중 target 에 해당하는 조합식 찾기
        CombineFunction tmp = new();
        int size = base_unit.CombFucntion.Count;
        for (int i = 0; i < size; i++) {
            if (base_unit.CombFucntion[i].Result == target_id) {
                tmp = base_unit.CombFucntion[i];
                break;
            }
        }
        bool can = false;
        // 조합 가능한 지 확인
        switch (tmp.NeedCount) {
            case 1:
                // 재료와 baseid 가 같다면 2개가 , 다르다면 1개가 있는지 확인
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
            // 재료 2개 이상은 중복이 없으므로 1 이상이면 진행
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


        // 조합 가능하면 조합
        if (can) {
            size = datahub.StageField.Count;
            // 조합할 떄 필요한 재료들을 소진 및 unit field에서 제거

            // 베이스의 unitcounter를 제거
            datahub.UnitCounter[base_id]--;

            // 필요한 재료 A를 유닛 필드중 가장 번호가 작은 순 부터 찾기
            // A는 어떠한 경우라도 반드시 존재하므로 바로 실행
            // 동일 번호 && 클릭 위치면 넘기기
            if (base_id == tmp.A) {

                for (int i = 1; i < size; i++) {
                    GameObject search_field = datahub.StageField[i] as GameObject;
                    if (i != datahub.CombineWaitingPos && tmp.A == search_field.GetComponent<Field>().UnitId) {
                        // unit counter에서 제거
                        datahub.UnitCounter[tmp.A] -= 1;
                        // 필요 재료이므로 0으로 제거
                        search_field.GetComponent<Field>().UnitId = 0;
                        search_field.GetComponent<UnitAttack>().Id = 0;
                        search_field.GetComponent<UnitAttack>().StopShot();
                        //search_field.GetComponent<UnitAttack>().enabled = false;
                        // unitcounter 제거]
                        break;
                    }
                }
            }
            // 서로 다른 재료라면 바로 찾기
            else {
                for (int i = 1; i < size; i++) {
                    GameObject search_field = datahub.StageField[i] as GameObject;
                    if (search_field.GetComponent<Field>().UnitId == tmp.A) {
                        // unit counter에서 제거
                        datahub.UnitCounter[tmp.A] -= 1;
                        search_field.GetComponent<Field>().UnitId = 0;
                        search_field.GetComponent<UnitAttack>().Id = 0;
                        search_field.GetComponent<UnitAttack>().StopShot();
                        //search_field.GetComponent<UnitAttack>().enabled = false;
                        break;
                    }
                }
            }

            // 필요재료가 2개 이상이라면 B까지 지워야함
            if (tmp.NeedCount >= 2) {
                // B 찾아 지우기
                for (int i = 1; i < size; i++) {
                    GameObject search_field = datahub.StageField[i] as GameObject;
                    int field_id = search_field.GetComponent<Field>().UnitId;
                    if (field_id == tmp.B) {
                        // unit counter에서 제거
                        datahub.UnitCounter[tmp.B] -= 1;
                        search_field.GetComponent<Field>().UnitId = 0;
                        search_field.GetComponent<UnitAttack>().Id = 0;
                        search_field.GetComponent<UnitAttack>().StopShot();
                        //search_field.GetComponent<UnitAttack>().enabled = false;
                        break;
                    }
                }
            }

            //필요 재료가 3개 이상이라면 C 까지 지워야함
            if (tmp.NeedCount >= 3) {
                // C 찾아 지우기
                for (int i = 1; i < size; i++) {
                    GameObject search_field = datahub.StageField[i] as GameObject;
                    int field_id = search_field.GetComponent<Field>().UnitId;
                    if (field_id == tmp.C) {
                        // unit counter에서 제거
                        datahub.UnitCounter[tmp.C] -= 1;
                        search_field.GetComponent<Field>().UnitId = 0;
                        search_field.GetComponent<UnitAttack>().Id = 0;
                        search_field.GetComponent<UnitAttack>().StopShot();
                        //search_field.GetComponent<UnitAttack>().enabled = false;
                        break;
                    }
                }
            }
            
            // 재료 제거가 완료되었으므로 클릭 위치의 유닛을 결과 유닛으로 변경
            GameObject now_select_field = datahub.StageField[datahub.CombineWaitingPos] as GameObject;
            now_select_field.GetComponent<Field>().UnitId = tmp.Result;
            now_select_field.GetComponent<UnitAttack>().Id = tmp.Result;

            // 결과 유닛 카운트 증가
            datahub.UnitCounter[tmp.Result]++;
        }

        // 조합 불가능하면 카드를 흔드는 이펙트를 켜고 종료
        else {
            Debug.Log("Combine cannot possible");
        }
    }

    /// <summary>
    /// 라운드 종료를 확인하는 함수
    /// </summary>
    /// <param name="stage_number">현재 스테이지 번호</param>
    /// <param name="now_enemy_round">진행될 라운드 번호</param>
    /// <returns>
    /// 0 : Enemy 생성 종료
    /// 1 : Enemy 생성
    /// 2 : 게임 완료
    /// </returns>
    public static int EndRoundChecker(int stage_number, int now_enemy_round) {
        // 스테이지 종료라면 진행하지 않음
        switch (stage_number) {
            case 1:
                // 65라운드 까지 진행 -> 보스를 잡는데 5라운드 추가 시간을 주므로 최종 65라운드
                if (now_enemy_round >= 60) {
                    // spawn을 종료해야함
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

        // 기본적으로는 라운드를 진행해야하므로 true 반환
        return 1;
    }
}
