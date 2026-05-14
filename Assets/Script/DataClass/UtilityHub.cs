using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;

namespace ShapeDefenseSpace {
    public class UtilityHub {
        // query string을 생성하기 위한 string builder
        public static StringBuilder query_builder = new(100);

        public static void PageHeaderSetting() {
            if (!GameObject.Find("Header").activeSelf) GameObject.Find("Header").SetActive(true);
            HeaderSetting.Instance.HeaderSet();
        }


        /// <summary>
        /// 유닛을 필드의 랜덤 위치에 생성하는 함수
        /// </summary>
        /// <param name="a">30퍼 확률 1</param>
        /// <param name="b">30퍼 확률 2</param>
        /// <param name="c">30퍼 확률 3</param>
        /// <param name="d">10/3퍼 확률 4</param>
        /// <param name="e">10/3퍼 확률 5</param>
        /// <param name="f">10/3퍼 확률 5</param>
        public static void UnitCreateRandomField(int a, int b, int c, int d, int e, int f) {

            // 유닛을 생성
            // 생성될 위치를 랜덤으로 형성해야함
            while (datahub.LeftStageField > 0 ) {
                int pos = Random.Range(1, datahub.StageFieldNumber+1); // ((int)Time.time % datahub.StageFieldNumber) + 1;
                GameObject tmp_field = datahub.StageField[pos] as GameObject;
                // 0 이 아니면 여기에 넣기
                if (tmp_field.GetComponent<Field>().UnitId == 0) {
                    // 유닛도 1~5 중 하나 랜덤하게 넣기
                    // 현재 가지고있는 모든 유닛 수를 알 수 있어야 함 -> 조합을 위해 > 유닛 갯수를 세놓기
                    int tmp_unit_id = Random.Range(1, 991);

                    if (tmp_unit_id >= 1 && tmp_unit_id < 301) {
                        // a
                        datahub.UnitCounter[a]++;
                        tmp_field.GetComponent<Field>().UnitId = a;
                    }
                    else if (tmp_unit_id >= 301 && tmp_unit_id < 601) {
                        // b
                        datahub.UnitCounter[b]++;
                        tmp_field.GetComponent<Field>().UnitId = b;
                    }
                    else if (tmp_unit_id >= 601 && tmp_unit_id < 901) {
                        // c
                        datahub.UnitCounter[c]++;
                        tmp_field.GetComponent<Field>().UnitId = c;
                    }

                    // 10 을 3등분해 사용
                    else if (tmp_unit_id >= 901 && tmp_unit_id < 1001) {
                        int hidden_piece = Random.Range(1, 1000);
                        if (hidden_piece >= 1 && hidden_piece < 334) {
                            // d
                            datahub.UnitCounter[d]++;
                            tmp_field.GetComponent<Field>().UnitId = d;
                        }
                        else if (hidden_piece >= 334 && hidden_piece < 667) {
                            // e
                            datahub.UnitCounter[e]++;
                            tmp_field.GetComponent<Field>().UnitId = e;
                        }
                        else if (hidden_piece >= 667 && hidden_piece < 1000) {
                            // e
                            datahub.UnitCounter[f]++;
                            tmp_field.GetComponent<Field>().UnitId = f;
                        }
                    }

                    // 넣고 무한루프 종료
                    datahub.StageField[pos] = tmp_field;
                    datahub.LeftStageField--;
                    // 발판 이미지 변화
                    UnitFieldChange((datahub.StageField[pos] as GameObject).transform.position, 1);

                    // 유닛 갯수가 보여지고있다면 재활성
                    if (datahub.IsShowUnitCount) {
                        UnitCounterPool.Instance.ShowUnitCount();
                    }
                    break;
                }
            }
        }


        public static void UnitFieldChange(Vector3 pos, int type) {
            switch (type) {
                case 0:
                    datahub.UnitMap.SetTile(datahub.UnitMap.WorldToCell(pos), empty_base);
                    break;
                case 1:
                    datahub.UnitMap.SetTile(datahub.UnitMap.WorldToCell(pos), unit_ani_tile);
                    break;
            }
            
        }

        /// <summary>
        /// id를 통해 resoruces 에서 로드할 sprite의 path를 구함
        /// </summary>
        /// <param name="id">unit id for sprite image</param>
        public static Sprite GetSprite(int id) {
            Sprite path = id switch {
                1001 => unit_e_circle,
                1002 => unit_e_triangle,
                1003 => unit_e_square,
                1004 => unit_e_star,
                1005 => unit_e_moon,
                1006 => unit_e_sun,

                2001 => unit_d_circle,
                2002 => unit_d_triangle,
                2003 => unit_d_square,

                3001 => unit_c_circle_1,
                3002 => unit_c_circle_2,
                3003 => unit_c_circle_3,
                3004 => unit_c_triangle_1,
                3005 => unit_c_triangle_2,
                3006 => unit_c_triangle_3,
                3007 => unit_c_square_1,
                3008 => unit_c_square_2,
                3009 => unit_c_square_3,
                3010 => unit_c_amalgation_1,
                3011 => unit_c_amalgation_2,
                3012 => unit_c_amalgation_3,
                3013 => unit_c_star,
                3014 => unit_c_moon,
                3015 => unit_c_sun,

                4001 => unit_b_circle_1,
                4002 => unit_b_circle_2,
                4003 => unit_b_circle_3,
                4004 => unit_b_triangle_1,
                4005 => unit_b_triangle_2,
                4006 => unit_b_triangle_3,
                4007 => unit_b_square_1,
                4008 => unit_b_square_2,
                4009 => unit_b_square_3,
                4010 => unit_b_star,
                4011 => unit_b_moon,
                4012 => unit_b_sun,

                5001 => unit_a_circle,
                5002 => unit_a_triangle,
                5003 => unit_a_square,
                5004 => unit_a_star,
                5005 => unit_a_moon,
                5006 => unit_a_sun,

                6001 => unit_s_circle,
                6002 => unit_s_triangle,
                6003 => unit_s_square,
                6004 => unit_s_star,
                6005 => unit_s_moon,
                6006 => unit_s_sun,

                // 아이템
                301 => unit_c_circle_0,
                302 => unit_c_triangle_0,
                303 => unit_c_square_0,
                304 => unit_c_star_0,
                305 => unit_c_moon_0,
                306 => unit_c_sun_0,

                401 => unit_b_circle_0,
                402 => unit_b_triangle_0,
                403 => unit_b_square_0,
                404 => unit_b_star_0,
                405 => unit_b_moon_0,
                406 => unit_b_sun_0,

                501 => unit_a_harmony_of_star_moon,
                502 => unit_a_harmony_of_sun_moon,
                503 => unit_a_harmony_of_sun_star,

                7001 => poison_border,
                7002 => blust_border,
                7003 => paralysis_border,

                _ => null,
            };

            return path;
        }

        /// <summary>
        /// 조합 가능한지 확인하고 조합을 진행하는 함수
        /// </summary>
        /// datahub에 저장된 데이터를 기반으로 수행되기 떄문에 param 없음
        private static CombineFunction tmp_combinefuntion = new();
        public static void CombineCheck(int combine_id) {

            // 사용 정보
            // base id
            // target result
            // datahub 의 unitcounter
            // stage field

            // base id instance
            if(datahub.CombineWaitingPos <= 0) {
                AnnounceControll.Instance.AnnounceOn(1);
                return;
            }
            GameObject tmp_field = datahub.StageField[datahub.CombineWaitingPos] as GameObject;
            int base_id = tmp_field.GetComponent<Field>().UnitId;
            Unit base_unit = datahub.Unit_dic[base_id] as Unit;

            // 조합식의 고유 아이디에 따라 확인할 조합식을 설정            
            int size = base_unit.CombFucntion.Count;
            for (int i = 0; i < size; i++) {
                if (base_unit.CombFucntion[i].Id == combine_id) {
                    tmp_combinefuntion = base_unit.CombFucntion[i];
                    break;
                }
            }
            bool can = false;
            // 조합 가능한 지 확인
            // 최대 중복재료는 3개
            int a = datahub.UnitCounter[tmp_combinefuntion.A];
            int b = tmp_combinefuntion.B > 0 ? datahub.UnitCounter[tmp_combinefuntion.B] : -1;
            int c = tmp_combinefuntion.C > 0 ? datahub.UnitCounter[tmp_combinefuntion.C] : -1;
            int d = tmp_combinefuntion.D > 0 ? datahub.UnitCounter[tmp_combinefuntion.D] : -1;

            switch (tmp_combinefuntion.NeedCount) {
                case 1: can = tmp_combinefuntion.A == base_unit.Id ? a >= 2 : a >= 1; break;
                case 2: can =  a >= 1 && b >= 1 && (tmp_combinefuntion.A != tmp_combinefuntion.B || a >= 2); break;
                case 3: can =  a >= 1 && b >= 1 && c >= 1 && (tmp_combinefuntion.A != base_unit.Id || a >= 2); break;
                case 4: can = a >= 1 && b >= 1 && c >= 1 && d >= 1 && (tmp_combinefuntion.A != base_unit.Id || a >= 2);break;
            }

            // 조합 가능하면 조합
            if (can) {
                size = datahub.StageField.Count;
                datahub.UnitCounter[base_id]--;

                // 각 재료 ID
                a = tmp_combinefuntion.A;
                b = tmp_combinefuntion.B;
                c = tmp_combinefuntion.C;
                d = tmp_combinefuntion.D;

                // 각 제거 대상 수 초기화
                int aCount = 0, bCount = 0, cCount = 0, dCount = 0;

                if (a > 0) aCount++;
                if (b > 0) bCount++;
                if (c > 0) cCount++;
                if (d > 0) dCount++;

                // base_id는 CombineWaitingPos에서 이미 제거되므로 하나 빼기
                if (base_id == a) aCount--;
                else if (base_id == b) bCount--;
                else if (base_id == c) cCount--;
                else if (base_id == d) dCount--;

                for (int i = 1; i < size; i++) {
                    if (i == datahub.CombineWaitingPos) continue;
                    GameObject field = datahub.StageField[i] as GameObject;
                    int fieldId = field.GetComponent<Field>().UnitId;

                    if (fieldId == a && aCount > 0) {
                        field.GetComponent<Field>().UnitId = 0;
                        field.GetComponent<UnitAttack>().Id = 0;
                        field.GetComponent<UnitAttack>().StopShot();
                        datahub.LeftStageField++;
                        datahub.UnitCounter[a]--;
                        UnitFieldChange(field.transform.position, 0);
                        aCount--;
                    }
                    else if (fieldId == b && bCount > 0) {
                        field.GetComponent<Field>().UnitId = 0;
                        field.GetComponent<UnitAttack>().Id = 0;
                        field.GetComponent<UnitAttack>().StopShot();
                        datahub.LeftStageField++;
                        datahub.UnitCounter[b]--;
                        UnitFieldChange(field.transform.position, 0);
                        bCount--;
                    }
                    else if (fieldId == c && cCount > 0) {
                        field.GetComponent<Field>().UnitId = 0;
                        field.GetComponent<UnitAttack>().Id = 0;
                        field.GetComponent<UnitAttack>().StopShot();
                        datahub.LeftStageField++;
                        datahub.UnitCounter[c]--;
                        UnitFieldChange(field.transform.position, 0);
                        cCount--;
                    }
                    else if (fieldId == d && dCount > 0) {
                        field.GetComponent<Field>().UnitId = 0;
                        field.GetComponent<UnitAttack>().Id = 0;
                        field.GetComponent<UnitAttack>().StopShot();
                        datahub.LeftStageField++;
                        datahub.UnitCounter[d]--;
                        UnitFieldChange(field.transform.position, 0);
                        dCount--;
                    }

                    if (aCount + bCount + cCount + dCount == 0)
                        break;
                }

                // 결과 유닛 적용
                GameObject now_select_field = datahub.StageField[datahub.CombineWaitingPos] as GameObject;
                now_select_field.GetComponent<Field>().UnitId = tmp_combinefuntion.Result;
                now_select_field.GetComponent<UnitAttack>().Id = tmp_combinefuntion.Result;
                datahub.UnitCounter[tmp_combinefuntion.Result]++;

                // 업적 체크
                var resultUnit = datahub.Unit_dic[tmp_combinefuntion.Result] as Unit;
                if (resultUnit.Grade == "A" || resultUnit.Grade == "S") {
                    achieve_observer.CombineQuestCheck(resultUnit.Grade);
                }

                // 필드 정리 및 UI
                UnitClickObserver.Instance.CleanField();
                if (datahub.IsShowUnitCount) {
                    UnitCounterPool.Instance.ShowUnitCount();
                }
            }
            // 조합 불가 시
            else {
                AnnounceControll.Instance.AnnounceOn(1);
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
        public static int EndRoundChecker(int now_enemy_round) {

            int end_round = datahub.EndRound;
            int end_boss_round = end_round - 1;
            //Debug.Log(end_round + " ?? " + end_boss_round);

            if (now_enemy_round >= end_round) {
                return 2;
            }

            if (now_enemy_round >= end_boss_round) {
                // spawn을 종료해야함
                // enemy를 검사해서 아무것도 남아있지 않다면 게임 종료
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemies.Length <= 0 && datahub.KillLastBoss) {
                    return 2;
                }
                
                return 0;
            }

            // 5의 배수 라운드에 enemies에 보스가 살아있으면 게임 종료
            if (now_enemy_round % 5 == 0 && now_enemy_round % 10 != 0) {
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach(var enemy in enemies) {
                    if (enemy.name.Equals("enemy_boss")) {
                        return 2;
                    }
                }
            }

            // 기본적으로는 라운드를 진행해야하므로 1 반환
            return 1;
        }

        /// <summary>
        /// 조각 갯수 만큼 랜덤한 유닛을 뽑아 주는 함수
        /// </summary>
        /// <param name="rank"> 유닛 등급</param>
        /// <param name="max"> 조각 갯수</param>
        /// <returns></returns>
        public static void SelectRecivePiece(int rank, int max) {

            int piece_name, a, b;

            switch (rank) {
                // E
                case 1:
                    a = E_start; b = E_end_1;
                    break;

                // D
                case 2:
                    a = D_start; b = D_end_1;
                    break;

                // C
                case 3:
                    a = C_start; b = C_end_1;
                    break;

                // B
                case 4:
                    a = B_start; b = B_end_1;
                    break;

                // A
                case 5:
                    a = A_start; b = A_end_1;
                    break;

                // S
                case 6:
                    a = S_start; b = S_end_1;
                    break;
                default:
                    a = 0; b = 1;
                    break;
            }

            for (int i = 0; i < max; i++) {
                //int dif = b - a;
                piece_name = Random.Range(a, b);
                datahub.UnitCounter[piece_name]++;
            }

        }

        /// <summary>
        /// 유닛의 현재 공격력을 계산하여 return 하는 함수
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        public static int GetResultAttack(int unit_id) {

            Unit unit = datahub.Unit_dic[unit_id] as Unit;

            return unit.Grade switch {
                "E" => unit.Attack + (unit.UpgradeValue * unit.UpgradeFigures) + datahub.UpgradeValueE * 2,
                "D" => unit.Attack + (unit.UpgradeValue * unit.UpgradeFigures) + datahub.UpgradeValueD * 2,
                "C" => unit.Attack + (unit.UpgradeValue * unit.UpgradeFigures) + datahub.UpgradeValueC * 2,
                "B" => unit.Attack + (unit.UpgradeValue * unit.UpgradeFigures) + datahub.UpgradeValueB * 2,
                "A" => unit.Attack + (unit.UpgradeValue * unit.UpgradeFigures) + datahub.UpgradeValueA * 2,
                "S" => unit.Attack + (unit.UpgradeValue * unit.UpgradeFigures) + datahub.UpgradeValueS * 2,
                _ => 0
            };
        }

        /// <summary>
        /// 유닛의 등급을 받아오는 함수
        /// </summary>
        /// <param name="id">unit id</param>
        /// <returns>E ~ S</returns>
        public static string GetUnitGrade(int id) {

            return (datahub.Unit_dic[id]).Grade;
        }

        public static void MergeSort(int[] arr, int start, int end) {

            if (start >= end)
                return;

            int mid = (start + end) / 2;
            MergeSort(arr, start, mid);
            MergeSort(arr, mid + 1, end);
            Merge(arr, start, mid, end);
        }

        private static void Merge(int[] arr, int start, int mid, int end) {
            int a = start;
            int b = mid + 1;
            int c = start;

            int[] tmp = new int[arr.Length];
            // 두지점부터 시작해 마지막까지 비교하며 작은것부터 정렬
            while (a <= mid && b <= end) {
                tmp[c++] = arr[a] <= arr[b] ? arr[a++] : arr[b++];
            }

            // 남아있는 경우 순서대로 다시 넣기
            // 후열부터 확인 -> 전열이 더 작은 경우를 먼저 넣었으므로 
            // 전열이 남은것은 후열보다 큰것 -> 반드시 남은 후열이 남은 전열보다 작음
            while (b <= end) {
                tmp[c++] = arr[b++];
            }
            while (a <= mid) {
                tmp[c++] = arr[a++];
            }

            // a를 정렬상태로 옮기기
            // 시작점은 start와 end까지만
            for (int i = start; i <= end; i++) {
                arr[i] = tmp[i];
            }

        }

    }
}