using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;

namespace ShapeDefenseSpace {
    public class CombineTableShowObserver : MonoBehaviour {

        private readonly static string CombineFromMateiralTitle = "만들 수 있는 조합";
        private readonly static string CombineToMateiralTitle = "만드는 방법";
        private readonly static string CombineDefaultTitle = "소환을 통해 랜덤으로 획득할 수 있습니다.";
        private readonly static string CombineItemTitle = "보스를 잡아 획득 할 수 있습니다.";

        private readonly static Vector3 ori_size = new(1f, 1f, 1f);

        private static Vector3 Content_Top;
        private static Dictionary<string, int> key_checker = new();

        private static Dictionary<int, int> duple = new();
        private static List<int> keys = new(datahub.Unit_dic.Keys);
        private static bool[] comb_val_has_check_arr = new bool[6] { false, false, false, false, false, false };

        /// <summary>
        /// 조합식 생성함수
        /// </summary>
        /// <param name="cur_id">타겟 혹은 눌린 유닛의 id</param>
        /// <param name="is_library">도감에서 보여주는 것인지 여부</param>
        /// <param name="Content">조합식이 생성될 위치</param>
        /// <param name="type">1 : From (id로 만들 수 있는) // 2 : to (id를 만들 수 있는) </param>
        public static void ShowCombineTable(int cur_id, bool is_library, GameObject Content, int type) {
            // content 위치 초기화
            Content_Top = Content.transform.position;
            Content_Top.y = 0;
            Content.transform.position = Content_Top;
            CleanContent(Content);
            switch (type) {
                case 0:
                    ShowCombineTableTo(cur_id, is_library, Content);
                    ShowCombineTableFrom(cur_id, is_library, Content);
                    break;
                case 1:
                    ShowCombineTableFrom(cur_id, is_library, Content);
                    break;
                case 2:
                    ShowCombineTableTo(cur_id, is_library, Content);
                    break;
            }
        }

        private static void ClearChecker() {
            key_checker.Clear();
        }

        private static void ClearDuple() {
            int size = keys.Count;
            for(int i = 0; i < size; i++) {
                duple[keys[i]] = 0;
            }
        }

        private static void ClearCombCheckArr() {
            for (int i = 0; i < 6; i++) {
                comb_val_has_check_arr[i] = false;
            }
        }

        /// <summary>
        /// Content안의 내용을 청소하는 함수
        /// </summary>
        /// <param name="content">하위 자식을 모두 지울 부모</param>
        public static void CleanContent(GameObject content) {
            var list = content.GetComponentsInChildren<Transform>();
            foreach (var item in list) {
                if (item != content.transform && item.CompareTag("CombineTable")) {
                    // type 분류
                    int type = item.gameObject.name.Split("_")[1] switch {
                        "title" => 1,
                        "2" => 2,
                        "3" => 3,
                        "4" => 4,
                        "5" => 5,
                        _ => 0
                    };
                    CombinePool.Instance.ReturnFunction(item.gameObject, type);
                }
            }
        }

        /// <summary>
        /// id로 만들 수 있는 조합식 생성
        /// </summary>
        /// <param name="cur_id">타겟</param>>
        /// <param name="is_library">조합 도감인지 여부</param>
        /// <param name="Content">조합식이 생성될 위치</param>
        private static void ShowCombineTableFrom(int cur_id, bool is_library, GameObject Content) {
            // id에 따라 self 이미지 path 저장
            //string path = UtilityHub.GetPath(cur_id);
            Sprite sprite = UtilityHub.GetSprite(cur_id);
            // id에 따라 조합할 수 있는 선택지 보여주기
            
            Unit unit = datahub.Unit_dic[cur_id] as Unit;
            
            // 조합할 수 있는 선택자기 없으면 없다는 표시를 해줘야 함
            if (unit.CombFucntion == null) { return; }
            int count = unit.CombFucntion.Count;

            if (count > 0) {
                if (is_library) {
                    // title 생성
                    GameObject title = CombinePool.Instance.GetFuction(1);
                    title.transform.SetParent(Content.transform);
                    title.transform.Find("Title").gameObject.GetComponent<TMP_Text>().text = CombineFromMateiralTitle;
                }

                // 현재 출력된 조합의 순서를 id순으로 정렬하여 동일한 녀석이 있으면 출력하지 않게 해야함
                // id 사이는 -로 연결해 key값을 형성하기
                // dictionary key로 id 연결값을, value로 1을 주어 haskey로 검색 없으면 추가
                
                int[] order_by_id = new int[6] { 0, 0, 0, 0, 0, 0 };
                string key;

                for (int i = 0; i < count; i++) {
                    //unit.CombFucntion[i].Print();
                    ClearDuple();
                    ClearCombCheckArr();
                    // 현재 조합을 id 순으로 연결한 string key 생성
                    order_by_id[0] = unit.Id;
                    order_by_id[1] = unit.CombFucntion[i].A;
                    order_by_id[2] = unit.CombFucntion[i].B;
                    order_by_id[3] = unit.CombFucntion[i].C;
                    order_by_id[4] = unit.CombFucntion[i].D;
                    order_by_id[5] = unit.CombFucntion[i].Result;
                    //Debug.Log("정렬전 " + order_by_id[0] + " " + order_by_id[1] + " " + order_by_id[2] + " " + order_by_id[3] + " " + order_by_id[4] + " " + order_by_id[5]);
                    UtilityHub.MergeSort(order_by_id, 0, order_by_id.Length - 1);
                    //Debug.Log("정렬 후 >> " + order_by_id[0] + " " + order_by_id[1] + " " + order_by_id[2] + " " + order_by_id[3] + " " + order_by_id[4] + " " + order_by_id[5]);
                    // string으로 나열하기
                    key = UtilityHub.query_builder.Append(order_by_id[0])
                                                  .Append("/")
                                                  .Append(order_by_id[1])
                                                  .Append("/")
                                                  .Append(order_by_id[2])
                                                  .Append("/")
                                                  .Append(order_by_id[3])
                                                  .Append("/")
                                                  .Append(order_by_id[4])
                                                  .Append("/")
                                                  .Append(order_by_id[5]).ToString();
                    UtilityHub.query_builder.Clear();
                    // key check
                    if (key_checker.ContainsKey(key)) {
                        // 다음으로 넘기기
                        continue;
                    }

                    // 없으니 새로 진행
                    key_checker.Add(key, 1);
                    // 조합식 보여주기
                    //Sprite m_path_1, m_path_2, m_path_3, m_path_4, result_path;
                    switch (unit.CombFucntion[i].NeedCount) {
                        case 1:

                            GameObject _combine_function_2 = CombinePool.Instance.GetFuction(2);
                            _combine_function_2.transform.SetParent(Content.transform);
                            _combine_function_2.transform.Find("Self").GetComponent<Image>().sprite = sprite; // Resources.Load<Sprite>(path);
                            _combine_function_2.transform.Find("Material").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].A);
                            _combine_function_2.transform.Find("Result").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].Result);
                            _combine_function_2.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = unit.CombFucntion[i].Result;
                            _combine_function_2.transform.Find("Result").GetComponent<CombineTargetClick>().CombineId = unit.CombFucntion[i].Id;
                            // material에 id 부여
                            _combine_function_2.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = unit.Id;
                            _combine_function_2.transform.Find("Material").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[i].A;

                            _combine_function_2.transform.Find("Result").GetComponent<CombineTargetClick>().InLib = is_library;
                            _combine_function_2.transform.Find("Self").GetComponent<CombineMaterialClick>().InLib = is_library;
                            _combine_function_2.transform.Find("Material").GetComponent<CombineMaterialClick>().InLib = is_library;
                            
                            // 배경에 등급색 설정
                            _combine_function_2.transform.Find("SelfBorder").GetComponent<Image>().color = GetGradeColor(unit.Grade);
                            _combine_function_2.transform.Find("MaterialBorder").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[i].A] as Unit).Grade);
                            _combine_function_2.transform.Find("ResultBorder").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[i].Result] as Unit).Grade);

                            // 결과물의 공격력 및 공격속도 설정
                            _combine_function_2.transform.Find("Attack").GetComponent<TMP_Text>().text = UtilityHub.GetResultAttack(unit.CombFucntion[i].Result).ToString();
                            _combine_function_2.transform.Find("Speed").GetComponent<TMP_Text>().text = (datahub.Unit_dic[unit.CombFucntion[i].Result] as Unit).AttackSpeed.ToString();

                            if (!is_library) {

                                CheckHas(unit.Id, unit.CombFucntion[i]);
                                // 각 unit들을 현재 소지하고 있다면 해당 위치의 back을 활성화
                                // 모든 back이 활성화 되면 result의 back을 활성화 하여 조합 가능함을 알리기
                                _combine_function_2.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, unit.Grade);
                                if (comb_val_has_check_arr[1]) { 
                                    _combine_function_2.transform.Find("MaterialBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[i].A] as Unit).Grade);
                                    _combine_function_2.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[i].Result] as Unit).Grade);
                                }

                            }

                            _combine_function_2.transform.localScale = ori_size;
                            break;
                        case 2:

                            GameObject _combine_function_3 = CombinePool.Instance.GetFuction(3);
                            _combine_function_3.transform.SetParent(Content.transform);
                            _combine_function_3.transform.Find("Self").GetComponent<Image>().sprite = sprite; // Resources.Load<Sprite>(path);
                            _combine_function_3.transform.Find("Material_1").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].A);
                            _combine_function_3.transform.Find("Material_2").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].B);
                            _combine_function_3.transform.Find("Result").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].Result);
                            // cur_id 설정
                            _combine_function_3.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = unit.CombFucntion[i].Result;
                            _combine_function_3.transform.Find("Result").GetComponent<CombineTargetClick>().CombineId = unit.CombFucntion[i].Id;
                            // material에 id 부여
                            _combine_function_3.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = unit.Id;
                            _combine_function_3.transform.Find("Material_1").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[i].A;
                            _combine_function_3.transform.Find("Material_2").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[i].B;

                            _combine_function_3.transform.Find("Result").GetComponent<CombineTargetClick>().InLib = is_library;
                            _combine_function_3.transform.Find("Self").GetComponent<CombineMaterialClick>().InLib = is_library;
                            _combine_function_3.transform.Find("Material_1").GetComponent<CombineMaterialClick>().InLib = is_library;
                            _combine_function_3.transform.Find("Material_2").GetComponent<CombineMaterialClick>().InLib = is_library;

                            // 배경에 등급색 설정
                            _combine_function_3.transform.Find("SelfBorder").GetComponent<Image>().color = GetGradeColor(unit.Grade);
                            _combine_function_3.transform.Find("Material_1_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[i].A] as Unit).Grade);
                            _combine_function_3.transform.Find("Material_2_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[i].B] as Unit).Grade);
                            _combine_function_3.transform.Find("ResultBorder").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[i].Result] as Unit).Grade);

                            // 결과물의 공격력 및 공격속도 설정
                            _combine_function_3.transform.Find("Attack").GetComponent<TMP_Text>().text = UtilityHub.GetResultAttack(unit.CombFucntion[i].Result).ToString();
                            _combine_function_3.transform.Find("Speed").GetComponent<TMP_Text>().text = (datahub.Unit_dic[unit.CombFucntion[i].Result] as Unit).AttackSpeed.ToString();

                            if (!is_library) {
                                CheckHas(unit.Id, unit.CombFucntion[i]);
                                // 각 unit들을 현재 소지하고 있다면 해당 위치의 back을 활성화
                                // 모든 back이 활성화 되면 result의 back을 활성화 하여 조합 가능함을 알리기
                                _combine_function_3.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, unit.Grade);

                                if (comb_val_has_check_arr[1]) {
                                    _combine_function_3.transform.Find("MaterialBack_1").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[i].A] as Unit).Grade);
                                }
                                if (comb_val_has_check_arr[2]) {
                                    _combine_function_3.transform.Find("MaterialBack_2").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[i].B] as Unit).Grade);
                                }
                                if (comb_val_has_check_arr[1] && comb_val_has_check_arr[2]) {
                                    _combine_function_3.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[i].Result] as Unit).Grade);
                                }
                            }

                            _combine_function_3.transform.localScale = ori_size;
                            break;
                        case 3:

                            GameObject _combine_function_4 = CombinePool.Instance.GetFuction(4);
                            _combine_function_4.transform.SetParent(Content.transform);
                            _combine_function_4.transform.Find("Self").GetComponent<Image>().sprite = sprite; // Resources.Load<Sprite>(path);
                            _combine_function_4.transform.Find("Material_1").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].A);
                            _combine_function_4.transform.Find("Material_2").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].B);
                            _combine_function_4.transform.Find("Material_3").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].C);
                            _combine_function_4.transform.Find("Result").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].Result);
                            // cur_id 설정
                            _combine_function_4.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = unit.CombFucntion[i].Result;
                            _combine_function_4.transform.Find("Result").GetComponent<CombineTargetClick>().CombineId = unit.CombFucntion[i].Id;
                            // material에 id 부여
                            _combine_function_4.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = unit.Id;
                            _combine_function_4.transform.Find("Material_1").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[i].A;
                            _combine_function_4.transform.Find("Material_2").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[i].B;
                            _combine_function_4.transform.Find("Material_3").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[i].C;

                            _combine_function_4.transform.Find("Result").GetComponent<CombineTargetClick>().InLib = is_library;
                            _combine_function_4.transform.Find("Self").GetComponent<CombineMaterialClick>().InLib = is_library;
                            _combine_function_4.transform.Find("Material_1").GetComponent<CombineMaterialClick>().InLib = is_library;
                            _combine_function_4.transform.Find("Material_2").GetComponent<CombineMaterialClick>().InLib = is_library;
                            _combine_function_4.transform.Find("Material_3").GetComponent<CombineMaterialClick>().InLib = is_library;

                            // 배경에 등급색 설정
                            _combine_function_4.transform.Find("SelfBorder").GetComponent<Image>().color = GetGradeColor(unit.Grade);
                            _combine_function_4.transform.Find("Material_1_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[i].A] as Unit).Grade);
                            _combine_function_4.transform.Find("Material_2_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[i].B] as Unit).Grade);
                            _combine_function_4.transform.Find("Material_3_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[i].C] as Unit).Grade);
                            _combine_function_4.transform.Find("ResultBorder").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[i].Result] as Unit).Grade);

                            // 결과물의 공격력 및 공격속도 설정
                            _combine_function_4.transform.Find("Attack").GetComponent<TMP_Text>().text = UtilityHub.GetResultAttack(unit.CombFucntion[i].Result).ToString();
                            _combine_function_4.transform.Find("Speed").GetComponent<TMP_Text>().text = (datahub.Unit_dic[unit.CombFucntion[i].Result] as Unit).AttackSpeed.ToString();

                            if (!is_library) {
                                CheckHas(unit.Id, unit.CombFucntion[i]);
                                // 각 unit들을 현재 소지하고 있다면 해당 위치의 back을 활성화
                                // 모든 back이 활성화 되면 result의 back을 활성화 하여 조합 가능함을 알리기
                                _combine_function_4.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, unit.Grade);
                                
                                if (comb_val_has_check_arr[1]) {
                                    _combine_function_4.transform.Find("MaterialBack_1").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[i].A] as Unit).Grade);
                                }
                                if (comb_val_has_check_arr[2]) {
                                    _combine_function_4.transform.Find("MaterialBack_2").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[i].B] as Unit).Grade);
                                }
                                if (comb_val_has_check_arr[3]) {
                                    _combine_function_4.transform.Find("MaterialBack_3").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[i].C] as Unit).Grade);
                                }

                                if (comb_val_has_check_arr[1] && comb_val_has_check_arr[2] && comb_val_has_check_arr[3]) {
                                    _combine_function_4.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[i].Result] as Unit).Grade);
                                }
                                
                            }

                            _combine_function_4.transform.localScale = ori_size;
                            break;
                        case 4:
                            
                            GameObject _combine_function_5 = CombinePool.Instance.GetFuction(5);
                            _combine_function_5.transform.SetParent(Content.transform);
                            _combine_function_5.transform.Find("Self").GetComponent<Image>().sprite = sprite; // Resources.Load<Sprite>(path);
                            _combine_function_5.transform.Find("Material_1").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].A);
                            _combine_function_5.transform.Find("Material_2").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].B);
                            _combine_function_5.transform.Find("Material_3").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].C);
                            _combine_function_5.transform.Find("Material_4").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].D);
                            _combine_function_5.transform.Find("Result").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].Result);
                            // cur_id 설정
                            _combine_function_5.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = unit.CombFucntion[i].Result;
                            _combine_function_5.transform.Find("Result").GetComponent<CombineTargetClick>().CombineId = unit.CombFucntion[i].Id;
                            // material에 id 부여
                            _combine_function_5.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = unit.Id;
                            _combine_function_5.transform.Find("Material_1").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[i].A;
                            _combine_function_5.transform.Find("Material_2").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[i].B;
                            _combine_function_5.transform.Find("Material_3").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[i].C;
                            _combine_function_5.transform.Find("Material_4").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[i].D;

                            
                            _combine_function_5.transform.Find("Result").GetComponent<CombineTargetClick>().InLib = is_library;
                            _combine_function_5.transform.Find("Self").GetComponent<CombineMaterialClick>().InLib = is_library;
                            _combine_function_5.transform.Find("Material_1").GetComponent<CombineMaterialClick>().InLib = is_library;
                            _combine_function_5.transform.Find("Material_2").GetComponent<CombineMaterialClick>().InLib = is_library;
                            _combine_function_5.transform.Find("Material_3").GetComponent<CombineMaterialClick>().InLib = is_library;
                            _combine_function_5.transform.Find("Material_4").GetComponent<CombineMaterialClick>().InLib = is_library;
                            

                            // 배경에 등급색 설정
                            _combine_function_5.transform.Find("SelfBorder").GetComponent<Image>().color = GetGradeColor(unit.Grade);
                            _combine_function_5.transform.Find("Material_1_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[i].A] as Unit).Grade);
                            _combine_function_5.transform.Find("Material_2_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[i].B] as Unit).Grade);
                            _combine_function_5.transform.Find("Material_3_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[i].C] as Unit).Grade);
                            _combine_function_5.transform.Find("Material_4_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[i].D] as Unit).Grade);
                            _combine_function_5.transform.Find("ResultBorder").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[i].Result] as Unit).Grade);

                            // 결과물의 공격력 및 공격속도 설정
                            _combine_function_5.transform.Find("Attack").GetComponent<TMP_Text>().text = UtilityHub.GetResultAttack(unit.CombFucntion[i].Result).ToString();
                            _combine_function_5.transform.Find("Speed").GetComponent<TMP_Text>().text = (datahub.Unit_dic[unit.CombFucntion[i].Result] as Unit).AttackSpeed.ToString();

                            if (!is_library) {
                                CheckHas(unit.Id, unit.CombFucntion[i]);
                                // 각 unit들을 현재 소지하고 있다면 해당 위치의 back을 활성화
                                // 모든 back이 활성화 되면 result의 back을 활성화 하여 조합 가능함을 알리기
                                _combine_function_5.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, unit.Grade);

                                if (comb_val_has_check_arr[1]) {
                                    _combine_function_5.transform.Find("MaterialBack_1").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[i].A] as Unit).Grade);
                                }
                                if (comb_val_has_check_arr[2]) {
                                    _combine_function_5.transform.Find("MaterialBack_2").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[i].B] as Unit).Grade);

                                }
                                if (comb_val_has_check_arr[3]) {
                                    _combine_function_5.transform.Find("MaterialBack_3").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[i].C] as Unit).Grade);

                                }
                                if (comb_val_has_check_arr[4]) {
                                    _combine_function_5.transform.Find("MaterialBack_4").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[i].D] as Unit).Grade);

                                }

                                if (comb_val_has_check_arr[1] && comb_val_has_check_arr[2] && comb_val_has_check_arr[3] && comb_val_has_check_arr[4]) {
                                    _combine_function_5.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[i].Result] as Unit).Grade);
                                }

                            }

                            _combine_function_5.transform.localScale = ori_size;
                            break;
                    }
                }
                /*
                // 모든 키를 보기
                foreach(var item in key_checker.Keys) {
                    UtilityHub.query_builder.Append(item).Append("\n");    
                }
                Debug.Log(UtilityHub.query_builder.ToString());
                UtilityHub.query_builder.Clear();
                */
                // 키 초기화
                ClearChecker();
            }

        }

        /// <summary>
        /// id를 만들 수 있는 조합식 생성
        /// </summary>
        /// <param name="cur_id">타겟</param>
        /// <param name="is_librayr">도감 여부</param>
        /// <param name="Content">조합식이 생성될 위치</param>
        private static void ShowCombineTableTo(int cur_id, bool is_library, GameObject Content) {

            int size; // 2중 for문 안의 순회횟수
            //string m_path_1, m_path_2, m_path_3, m_path_4, m_path_5, result_path = UtilityHub.GetPath(cur_id);


            if (cur_id >= 1000 && cur_id <= 1999 && is_library) {
                // 결과가 될 수 없거나, 재료가 될 수 없으므로 예외처리
                // title 추가
                GameObject title = CombinePool.Instance.GetFuction(1);
                title.transform.SetParent(Content.transform);
                title.transform.Find("Title").gameObject.GetComponent<TMP_Text>().text = CombineDefaultTitle;
            }

            // 등급으로 구분해서 타깃을 설정
            // 조각으로 인해 min은 항상 1
            int min = 0, max = 0;
            switch ((datahub.Unit_dic[cur_id] as Unit).Grade) {
                case "D":
                    min = E_start;
                    max = E_end;
                    break;
                case "C":
                    min = E_start;
                    max = D_end;
                    break;
                case "B":
                    min = E_start;
                    max = C_end;
                    break;
                case "A":
                    min = E_start;
                    max = B_end;
                    break;
                case "S":
                    min = E_start;
                    max = B_end;
                    break;
                // 수정은 결정과 조화로만 조합하므로 아이템만 살펴보면 됨
                case "IA":
                    min = 301;
                    max = 406;
                    break;

                // 아이템은 아이템을 획득하는 방법을 안내하기
                case "IB":
                case "IC":
                    // title 추가
                    GameObject title = CombinePool.Instance.GetFuction(1);
                    title.transform.SetParent(Content.transform);
                    title.transform.Find("Title").gameObject.GetComponent<TMP_Text>().text = CombineItemTitle;
                    max = 0;
                    break;
                default:
                    min = 0;
                    max = 0;
                    break;
            }

            // 조합식을 검색할 친구들
            if (max > 0) {

                if (is_library) {
                    // title 추가
                    GameObject title = CombinePool.Instance.GetFuction(1);
                    title.transform.SetParent(Content.transform);
                    title.transform.Find("Title").gameObject.GetComponent<TMP_Text>().text = CombineToMateiralTitle;
                }

                // 현재 출력된 조합의 순서를 id순으로 정렬하여 동일한 녀석이 있으면 출력하지 않게 해야함
                // id 사이는 -로 연결해 key값을 형성하기
                // dictionary key로 id 연결값을, value로 1을 주어 haskey로 검색 없으면 추가
                int[] order_by_id = new int[6] { 0, 0, 0, 0, 0, 0 };
                string key;

                // 탐색할 유닛 선정
                int start_point = -1, end_point = -1;
                for(int z = 0; z < datahub.Unit_Number; z++) {
                    if (datahub.Unit_Ids[z] == min) start_point = z;
                    if (datahub.Unit_Ids[z] == max) end_point = z;

                    if( start_point >= 0 && end_point >= 0 ) break;
                } 

                // 탐색 시작
                for (int i = start_point; i <= end_point; i++) {
                    Unit unit = datahub.Unit_dic[datahub.Unit_Ids[i]] as Unit;
                    size = unit.CombFucntion.Count;
                    // 가능한 조합 탐색
                    for (int j = 0; j < size; j++) {
                        // 출력해야할 것 확인
                        if (unit.CombFucntion[j].Result == cur_id) {
                            ClearDuple();
                            ClearCombCheckArr();
                            // 현재 조합을 id 순으로 연결한 string key 생성
                            order_by_id[0] = unit.Id;
                            order_by_id[1] = unit.CombFucntion[j].A;
                            order_by_id[2] = unit.CombFucntion[j].B;
                            order_by_id[3] = unit.CombFucntion[j].C;
                            order_by_id[4] = unit.CombFucntion[j].D;
                            order_by_id[5] = unit.CombFucntion[j].Result;
                            //string test = "정렬전 " + order_by_id[0] + " " + order_by_id[1] + " " + order_by_id[2] + " " + order_by_id[3] + " " + order_by_id[4] + " " + order_by_id[5];
                            UtilityHub.MergeSort(order_by_id, 0, order_by_id.Length - 1);
                            //Debug.Log("정렬 후 >> " + order_by_id[0] + " " + order_by_id[1] + " " + order_by_id[2] + " " + order_by_id[3] + " " + order_by_id[4] + " " + order_by_id[5]);
                            // string으로 나열하기
                            key = UtilityHub.query_builder.Append(order_by_id[0])
                                                          .Append("/")
                                                          .Append(order_by_id[1])
                                                          .Append("/")
                                                          .Append(order_by_id[2])
                                                          .Append("/")
                                                          .Append(order_by_id[3])
                                                          .Append("/")
                                                          .Append(order_by_id[4])
                                                          .Append("/")
                                                          .Append(order_by_id[5]).ToString();
                            UtilityHub.query_builder.Clear();
                            //Debug.Log(test + " \n" + key);
                            // key check
                            if (key_checker.ContainsKey(key)) {
                                // 다음으로 넘기기
                                //Debug.Log("check same");
                                continue;
                            }

                            // 없으니 새로 진행
                            key_checker.Add(key, 1);

                            // 몇개의 재료를 필요로 하는지에따라 다른 prefab 사용
                            switch (unit.CombFucntion[j].NeedCount) {
                                case 1:
                                    
                                    GameObject _combine_function_2 = CombinePool.Instance.GetFuction(2);
                                    _combine_function_2.transform.SetParent(Content.transform);
                                    _combine_function_2.transform.Find("Self").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.Id);
                                    _combine_function_2.transform.Find("Material").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[j].A);
                                    _combine_function_2.transform.Find("Result").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[j].Result);
                                    // cur_id 설정
                                    _combine_function_2.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = cur_id;
                                    _combine_function_2.transform.Find("Result").GetComponent<CombineTargetClick>().CombineId = unit.CombFucntion[j].Id;
                                    // material id 부여
                                    _combine_function_2.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = unit.Id;
                                    _combine_function_2.transform.Find("Material").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].A;

                                    
                                    _combine_function_2.transform.Find("Result").GetComponent<CombineTargetClick>().InLib = is_library;
                                    _combine_function_2.transform.Find("Self").GetComponent<CombineMaterialClick>().InLib = is_library;
                                    _combine_function_2.transform.Find("Material").GetComponent<CombineMaterialClick>().InLib = is_library;
                                

                                    // 배경에 등급색 설정
                                    _combine_function_2.transform.Find("SelfBorder").GetComponent<Image>().color = GetGradeColor(unit.Grade);
                                    _combine_function_2.transform.Find("MaterialBorder").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[j].A] as Unit).Grade);
                                    _combine_function_2.transform.Find("ResultBorder").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[j].Result] as Unit).Grade);

                                    // 결과물의 공격력 및 공격속도 설정
                                    _combine_function_2.transform.Find("Attack").GetComponent<TMP_Text>().text = UtilityHub.GetResultAttack(unit.CombFucntion[j].Result).ToString();
                                    _combine_function_2.transform.Find("Speed").GetComponent<TMP_Text>().text = (datahub.Unit_dic[unit.CombFucntion[j].Result] as Unit).AttackSpeed.ToString();

                                    if (!is_library) {
                                        CheckHas(unit.Id, unit.CombFucntion[j]);
                                        
                                        if (comb_val_has_check_arr[0]) {
                                            _combine_function_2.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, unit.Grade);
                                        }

                                        if (comb_val_has_check_arr[1]) {
                                            _combine_function_2.transform.Find("MaterialBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[j].A] as Unit).Grade);

                                        }

                                        if (comb_val_has_check_arr[0] && comb_val_has_check_arr[1]) {
                                            _combine_function_2.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[j].Result] as Unit).Grade);
                                        }
                                    }

                                    _combine_function_2.transform.localScale = ori_size;
                                    break;
                                case 2:
                                    
                                    GameObject _combine_function_3 = CombinePool.Instance.GetFuction(3);
                                    _combine_function_3.transform.SetParent(Content.transform);
                                    _combine_function_3.transform.Find("Self").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.Id);
                                    _combine_function_3.transform.Find("Material_1").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[j].A);
                                    _combine_function_3.transform.Find("Material_2").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[j].B);
                                    _combine_function_3.transform.Find("Result").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[j].Result);
                                    // cur_id 설정
                                    _combine_function_3.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = cur_id;
                                    _combine_function_3.transform.Find("Result").GetComponent<CombineTargetClick>().CombineId = unit.CombFucntion[j].Id;

                                    // material id 부여
                                    _combine_function_3.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = unit.Id;
                                    _combine_function_3.transform.Find("Material_1").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].A;
                                    _combine_function_3.transform.Find("Material_2").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].B;

                                    
                                    _combine_function_3.transform.Find("Result").GetComponent<CombineTargetClick>().InLib = is_library;
                                    _combine_function_3.transform.Find("Self").GetComponent<CombineMaterialClick>().InLib = is_library;
                                    _combine_function_3.transform.Find("Material_1").GetComponent<CombineMaterialClick>().InLib = is_library;
                                    _combine_function_3.transform.Find("Material_2").GetComponent<CombineMaterialClick>().InLib = is_library;
                                    

                                    // 배경에 등급색 설정
                                    _combine_function_3.transform.Find("SelfBorder").GetComponent<Image>().color = GetGradeColor(unit.Grade);
                                    _combine_function_3.transform.Find("Material_1_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[j].A] as Unit).Grade);
                                    _combine_function_3.transform.Find("Material_2_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[j].B] as Unit).Grade);
                                    _combine_function_3.transform.Find("ResultBorder").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[j].Result] as Unit).Grade);

                                    // 결과물의 공격력 및 공격속도 설정
                                    _combine_function_3.transform.Find("Attack").GetComponent<TMP_Text>().text = UtilityHub.GetResultAttack(unit.CombFucntion[j].Result).ToString();
                                    _combine_function_3.transform.Find("Speed").GetComponent<TMP_Text>().text = (datahub.Unit_dic[unit.CombFucntion[j].Result] as Unit).AttackSpeed.ToString();

                                    if (!is_library) {
                                        CheckHas(unit.Id, unit.CombFucntion[j]);

                                        if (comb_val_has_check_arr[0]) {
                                            _combine_function_3.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, unit.Grade);
                                        }

                                        if (comb_val_has_check_arr[1]) {
                                            _combine_function_3.transform.Find("MaterialBack_1").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[j].A] as Unit).Grade);
                                        }

                                        if (comb_val_has_check_arr[2]) {
                                            _combine_function_3.transform.Find("MaterialBack_2").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[j].B] as Unit).Grade);
                                        }

                                        if (comb_val_has_check_arr[0] && comb_val_has_check_arr[1] && comb_val_has_check_arr[2]) {
                                            _combine_function_3.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[j].Result] as Unit).Grade);
                                        }
                                    }

                                    _combine_function_3.transform.localScale = ori_size;
                                    break;
                                case 3:
                                    
                                    GameObject _combine_function_4 = CombinePool.Instance.GetFuction(4);
                                    _combine_function_4.transform.SetParent(Content.transform);
                                    _combine_function_4.transform.Find("Self").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.Id);
                                    _combine_function_4.transform.Find("Material_1").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[j].A);
                                    _combine_function_4.transform.Find("Material_2").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[j].B);
                                    _combine_function_4.transform.Find("Material_3").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[j].C);
                                    _combine_function_4.transform.Find("Result").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[j].Result);
                                    // cur_id 설정
                                    _combine_function_4.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = cur_id;
                                    _combine_function_4.transform.Find("Result").GetComponent<CombineTargetClick>().CombineId = unit.CombFucntion[j].Id;
                                    // material id 부여
                                    _combine_function_4.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = unit.Id;
                                    _combine_function_4.transform.Find("Material_1").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].A;
                                    _combine_function_4.transform.Find("Material_2").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].B;
                                    _combine_function_4.transform.Find("Material_3").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].C;

                                    
                                    _combine_function_4.transform.Find("Result").GetComponent<CombineTargetClick>().InLib = is_library;
                                    _combine_function_4.transform.Find("Self").GetComponent<CombineMaterialClick>().InLib = is_library;
                                    _combine_function_4.transform.Find("Material_1").GetComponent<CombineMaterialClick>().InLib = is_library;
                                    _combine_function_4.transform.Find("Material_2").GetComponent<CombineMaterialClick>().InLib = is_library;
                                    _combine_function_4.transform.Find("Material_3").GetComponent<CombineMaterialClick>().InLib = is_library;
                                    

                                    // 배경에 등급색 설정
                                    _combine_function_4.transform.Find("SelfBorder").GetComponent<Image>().color = GetGradeColor(unit.Grade);
                                    _combine_function_4.transform.Find("Material_1_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[j].A] as Unit).Grade);
                                    _combine_function_4.transform.Find("Material_2_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[j].B] as Unit).Grade);
                                    _combine_function_4.transform.Find("Material_3_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[j].C] as Unit).Grade);
                                    _combine_function_4.transform.Find("ResultBorder").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[j].Result] as Unit).Grade);

                                    // 결과물의 공격력 및 공격속도 설정
                                    _combine_function_4.transform.Find("Attack").GetComponent<TMP_Text>().text = UtilityHub.GetResultAttack(unit.CombFucntion[j].Result).ToString();
                                    _combine_function_4.transform.Find("Speed").GetComponent<TMP_Text>().text = (datahub.Unit_dic[unit.CombFucntion[j].Result] as Unit).AttackSpeed.ToString();

                                    if (!is_library) {
                                        CheckHas(unit.Id, unit.CombFucntion[j]);

                                        if (comb_val_has_check_arr[0]) {
                                            _combine_function_4.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, unit.Grade);
                                        }

                                        if (comb_val_has_check_arr[1]) {
                                            _combine_function_4.transform.Find("MaterialBack_1").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[j].A] as Unit).Grade);
                                        }

                                        if (comb_val_has_check_arr[2]) {
                                            _combine_function_4.transform.Find("MaterialBack_2").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[j].B] as Unit).Grade);
                                        }

                                        if (comb_val_has_check_arr[3]) {
                                            _combine_function_4.transform.Find("MaterialBack_3").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[j].C] as Unit).Grade);
                                        }

                                        if (comb_val_has_check_arr[0] && comb_val_has_check_arr[1] && comb_val_has_check_arr[2] && comb_val_has_check_arr[3]) {
                                            _combine_function_4.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[j].Result] as Unit).Grade);
                                        }
                                    }

                                    _combine_function_4.transform.localScale = ori_size;
                                    break;
                                case 4:
                                
                                    GameObject _combine_function_5 = CombinePool.Instance.GetFuction(5);
                                    _combine_function_5.transform.SetParent(Content.transform);
                                    _combine_function_5.transform.Find("Self").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.Id);
                                    _combine_function_5.transform.Find("Material_1").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[j].A);
                                    _combine_function_5.transform.Find("Material_2").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[j].B);
                                    _combine_function_5.transform.Find("Material_3").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[j].C);
                                    _combine_function_5.transform.Find("Material_4").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[j].D);
                                    _combine_function_5.transform.Find("Result").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.CombFucntion[i].Result);
                                    // cur_id 설정
                                    _combine_function_5.transform.Find("Result").GetComponent<CombineTargetClick>().ResultId = unit.CombFucntion[j].Result;
                                    _combine_function_5.transform.Find("Result").GetComponent<CombineTargetClick>().CombineId = unit.CombFucntion[j].Id;

                                    // material에 id 부여
                                    _combine_function_5.transform.Find("Self").GetComponent<CombineMaterialClick>().Id = unit.Id;
                                    _combine_function_5.transform.Find("Material_1").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].A;
                                    _combine_function_5.transform.Find("Material_2").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].B;
                                    _combine_function_5.transform.Find("Material_3").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].C;
                                    _combine_function_5.transform.Find("Material_4").GetComponent<CombineMaterialClick>().Id = unit.CombFucntion[j].D;

                                    
                                    _combine_function_5.transform.Find("Result").GetComponent<CombineTargetClick>().InLib = is_library;
                                    _combine_function_5.transform.Find("Self").GetComponent<CombineMaterialClick>().InLib = is_library;
                                    _combine_function_5.transform.Find("Material_1").GetComponent<CombineMaterialClick>().InLib = is_library;
                                    _combine_function_5.transform.Find("Material_2").GetComponent<CombineMaterialClick>().InLib = is_library;
                                    _combine_function_5.transform.Find("Material_3").GetComponent<CombineMaterialClick>().InLib = is_library;
                                    _combine_function_5.transform.Find("Material_4").GetComponent<CombineMaterialClick>().InLib = is_library;
                                    
                                    // 배경에 등급색 설정
                                    _combine_function_5.transform.Find("SelfBorder").GetComponent<Image>().color = GetGradeColor(unit.Grade);
                                    _combine_function_5.transform.Find("Material_1_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[j].A] as Unit).Grade);
                                    _combine_function_5.transform.Find("Material_2_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[j].B] as Unit).Grade);
                                    _combine_function_5.transform.Find("Material_3_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[j].C] as Unit).Grade);
                                    _combine_function_5.transform.Find("Material_4_Border").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[j].D] as Unit).Grade);
                                    _combine_function_5.transform.Find("ResultBorder").GetComponent<Image>().color = GetGradeColor((datahub.Unit_dic[unit.CombFucntion[j].Result] as Unit).Grade);

                                    // 결과물의 공격력 및 공격속도 설정
                                    _combine_function_5.transform.Find("Attack").GetComponent<TMP_Text>().text = UtilityHub.GetResultAttack(unit.CombFucntion[j].Result).ToString();
                                    _combine_function_5.transform.Find("Speed").GetComponent<TMP_Text>().text = (datahub.Unit_dic[unit.CombFucntion[j].Result] as Unit).AttackSpeed.ToString();

                                    if (!is_library) {
                                        CheckHas(unit.Id, unit.CombFucntion[j]);

                                        if (comb_val_has_check_arr[0]) {
                                            _combine_function_5.transform.Find("SelfBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, unit.Grade);
                                        }

                                        if (comb_val_has_check_arr[1]) {
                                            _combine_function_5.transform.Find("MaterialBack_1").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[j].A] as Unit).Grade);
                                        }

                                        if (comb_val_has_check_arr[2]) {
                                            _combine_function_5.transform.Find("MaterialBack_2").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[j].B] as Unit).Grade);
                                        }

                                        if (comb_val_has_check_arr[3]) {
                                            _combine_function_5.transform.Find("MaterialBack_3").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[j].C] as Unit).Grade);
                                        }

                                        if (comb_val_has_check_arr[4]) {
                                            _combine_function_5.transform.Find("MaterialBack_4").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[j].D] as Unit).Grade);
                                        }

                                        if (comb_val_has_check_arr[0] && comb_val_has_check_arr[1] && comb_val_has_check_arr[2] && comb_val_has_check_arr[3] && comb_val_has_check_arr[4]) {
                                            _combine_function_5.transform.Find("ResultBack").GetComponent<CombinePossibleEffect>().OnImageSetting(true, (datahub.Unit_dic[unit.CombFucntion[j].Result] as Unit).Grade);
                                        }
                                    }

                                    _combine_function_5.transform.localScale = ori_size;
                                    break;
                            }

                        }
                    }
                }

                //키 초기화
                ClearChecker();
            }
        }

        private static Color GetGradeColor(string grade) {
            return grade switch {
                "E" => color_e,
                "D" => color_d,
                "C" => color_c,
                "B" => color_b,
                "A" => color_a,
                "S" => color_s,
                "IC" => core_color,
                "IB" => unicore_color,
                "IA" => crystal_color,
                _ => core_color
            };
        }

        private static void CheckHas(int id, CombineFunction comb) {
            ClearDuple();
            ClearCombCheckArr();
            // comb를 돌며 duple check
            /*
            if (id > 0) duple[id]++;
            if (comb.A > 0) duple[comb.A]++;
            if (comb.B > 0) duple[comb.B]++;
            if (comb.C > 0) duple[comb.C]++;
            if (comb.D > 0) duple[comb.D]++;
            */
            // 가지고 있는지 체크
            if (id > 0) {
                if (datahub.UnitCounter[id] > duple[id]) {
                    duple[id]++;
                    comb_val_has_check_arr[0] = true;
                }
            }
            
            if (comb.A > 0) {
                if (datahub.UnitCounter[comb.A] > duple[comb.A]) {
                    duple[comb.A]++;
                    comb_val_has_check_arr[1] = true;
                }
            }
            if (comb.B > 0) {
                if (datahub.UnitCounter[comb.B] > duple[comb.B]) {
                    duple[comb.B]++;
                    comb_val_has_check_arr[2] = true;
                }
            }
            
            if (comb.C > 0) {
                if (datahub.UnitCounter[comb.C] > duple[comb.C]) {
                    duple[comb.C]++;
                    comb_val_has_check_arr[3] = true;
                }
            }
            
            if (comb.D > 0) {
                if (datahub.UnitCounter[comb.D] > duple[comb.D]) {
                    duple[comb.D]++;
                    comb_val_has_check_arr[4] = true;
                }
            }
            
        }
    }

}

