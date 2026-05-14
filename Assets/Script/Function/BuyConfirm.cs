using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;
using System.Collections.Generic;

public class BuyConfirm : SceneSingleton<BuyConfirm> {
    [SerializeField]
    private GameObject Popup;
    [SerializeField]
    private GameObject Confirm;
    [SerializeField]
    private GameObject Content;
    [SerializeField]
    private GameObject CannotBuyAnnounce;
    [SerializeField]
    private GameObject StaminaConfirmField;

    // 상자 구매
    public void ChestBuy() {

        // 등급에 따라 확률별 구매 진행
        string key = Popup.GetComponent<Chest>().Id;

        // 현재 소지 골드로 살 수 있는지 확인
        int need_gold = key switch {
            "no" => 100,
            "ad" => 500,
            "lu" => 1000,
            "li" => 3000,
            "cu" => 5000,
            _ => 1000000,
        };

        // 살 수 없으면 골드 부족 안내를 진행하고 종료
        if (datahub.User.Dot < need_gold) {
            CannotBuyAnnounce.SetActive(true);
            Popup.GetComponent<Chest>().Reset();
            Popup.SetActive(false);
            return;
        }
        // 구매 
        datahub.User.Dot -= need_gold;
        string query = UtilityHub.query_builder.Append("UPDATE user SET dot=")
                                                      .Append(datahub.User.Dot)
                                                      .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "user");

        ChestCheck();

        // header reload
        UtilityHub.PageHeaderSetting();
    }

    public void ChestCheck() {
        // 등급에 따라 확률별 구매 진행
        string key = Popup.GetComponent<Chest>().Id;
        string query;

        // 업적 가산
        // 일일 퀘스트 아무 상자 1회 오픈하기
        int quest_val = key switch {
            "no" => 1,
            "ad" => 2,
            "lu" or "adv" => 3,
            "li" => 4,
            "cu" => 5,
            _ => 0
        };
        achieve_observer.BuyChestQuestCheck(quest_val);

        // 조각 정보를 받을 준비
        int first_quarter = Popup.GetComponent<Chest>().e;
        int second_quarter = first_quarter + Popup.GetComponent<Chest>().d;
        int third_quarter = second_quarter + Popup.GetComponent<Chest>().c;
        int foured_quarter = third_quarter + Popup.GetComponent<Chest>().b;
        int fived_quarter = foured_quarter + Popup.GetComponent<Chest>().a;
        int sixed_quarter = fived_quarter + Popup.GetComponent<Chest>().s;

        int size = key switch {
            "no" => 10,
            "ad" => 20,
            "lu" or "adv" => 30,
            "li" => 50,
            "cu" => 100,
            _ => 10
        };

        datahub.InitCounter();
        // 확률에 따라 조각 수급

        // 랜덤한 조각 찾기
        for (int i = 0; i < size; i++) {
            int random_number = Random.Range(0, 100); // 0~99
            // e
            if (0 <= random_number && random_number < first_quarter) {
                // 1~3
                datahub.UnitCounter[Random.Range(E_start, E_end_1)]++;
            }
            // d
            else if (first_quarter <= random_number && random_number < second_quarter) {
                // 6 ~ 8
                datahub.UnitCounter[Random.Range(D_start, D_end_1)]++;
            }
            // c
            else if (second_quarter <= random_number && random_number < third_quarter) {
                // 9 ~ 22
                datahub.UnitCounter[Random.Range(C_start, C_end_1)]++;
            }
            // b
            else if (third_quarter <= random_number && random_number < foured_quarter) {
                // 23 ~ 33
                datahub.UnitCounter[Random.Range(B_start, B_end_1)]++;
            }
            // a
            else if (foured_quarter <= random_number && random_number < fived_quarter) {
                // 34 ~ 38
                datahub.UnitCounter[Random.Range(A_start, A_end_1)]++;
            }
            // s
            else if (fived_quarter <= random_number && random_number < sixed_quarter) {
                // 39 ~ 43
                datahub.UnitCounter[Random.Range(S_start, S_end_1)]++;
            }
        }

        // popup의 데이터 초기화
        Popup.GetComponent<Chest>().Reset();
        Popup.transform.Find("Confirm").GetComponent<Button>().onClick.RemoveAllListeners();
        Popup.SetActive(false);

        var dic_keys = new List<int>(datahub.UnitCounter.Keys);
        int keys_size = dic_keys.Count;
        for (int i = 0; i < keys_size; i++) {
            if (datahub.UnitCounter[dic_keys[i]] > 0) {
                Unit tmp = datahub.Unit_dic[dic_keys[i]] as Unit;
                // tmp_unit 에 들어온 정보를 종합하여 field의 prefab으로 등록하기
                GameObject pre_unit = Instantiate(_unit_piece_obj, Content.transform.position, Content.transform.rotation);
                pre_unit.transform.SetParent(Content.transform, false);
                pre_unit.transform.Find("Grade").GetComponent<Image>().color = tmp.Grade switch {
                    "E" => color_e,
                    "D" => color_d,
                    "C" => color_c,
                    "B" => color_b,
                    "A" => color_a,
                    "S" => color_s,
                    _ => core_color
                };
                pre_unit.transform.Find("Unit").GetComponent<Image>().sprite = UtilityHub.GetSprite(tmp.Id); // Resources.Load<Sprite>(UtilityHub.GetPath(tmp.Id));
                pre_unit.transform.Find("Counter").GetComponent<TMP_Text>().text = UtilityHub.query_builder.Append("x ")
                                                                                                           .Append(datahub.UnitCounter[dic_keys[i]].ToString())
                                                                                                           .ToString();
                UtilityHub.query_builder.Clear();

                // tmp_unit 의 조각 값 unit에 반영
                tmp.Piece += datahub.UnitCounter[dic_keys[i]];

                // unit 을 db에 modify
                query = UtilityHub.query_builder.Append("UPDATE unit SET piece=")
                                                       .Append(tmp.Piece)
                                                       .Append(" WHERE id=")
                                                       .Append(tmp.Id)
                                                       .ToString();
                modifyDB.ControllDB(query, "unit");
                UtilityHub.query_builder.Clear();
            }
        }

        // 구매 결과 창 보이기
        Confirm.transform.Find("Field").gameObject.SetActive(true);
        Confirm.transform.Find("Announce").gameObject.SetActive(false);
        Confirm.GetComponent<ChestResultAnchor>().AnchorReset();
        Confirm.SetActive(true);
    }

    public void FreeChestCheck(int reward_type) {
        string now_time = System.DateTime.Now.ToString("MMddHHmmss");

        switch (reward_type) {
            case 1:
                PlayerPrefs.SetString("Adv_UnitChest_RemainTime", now_time);
                PlayerPrefs.Save();
                ChestCheck();
                datahub.AdvUnitTimerStart();
                AdvChestChecker.Instance.UnitAdvTimerShowing();
                break;
            case 2:
                PlayerPrefs.SetString("Adv_StaChest_RemainTime", now_time);
                PlayerPrefs.Save();
                AdvStaminaBuy();
                datahub.AdvStaTimerStart();
                AdvChestChecker.Instance.StaAdvTimerShowing();
                break;
        }
    }

    private void CleanResultList() {
        var list = Content.GetComponentsInChildren<Transform>();
        foreach (var item in list) {
            if (item != Content.transform) {
                Destroy(item.gameObject);
            }
        }
    }

    // 상자 구매 취소
    public void ChestBuyCancel() {
        Popup.GetComponent<Chest>().Reset();
        Popup.transform.Find("Confirm").GetComponent<Button>().onClick.RemoveAllListeners();
        Popup.SetActive(false);
    }


    // 상자 구매 확인
    public void ConfrimCheck() {
        // 구매 결과 오브젝트 청소
        CleanResultList();

        Confirm.transform.Find("Field").gameObject.SetActive(false);
        Confirm.transform.Find("Announce").gameObject.SetActive(false);
        Confirm.SetActive(false);
    }

    // 스태미나 구매
    public void StaminaBuy() {
        // 골드가 충분한지 확인
        int need_gold = StaminaConfirmField.GetComponent<BuyStaminaData>().NeedGold;

        if (datahub.User.Dot < need_gold) {
            StaminaConfirmField.GetComponent<BuyStaminaData>().Reset();
            StaminaConfirmField.SetActive(false);
            CannotBuyAnnounce.SetActive(true);
            return;
        }

        // 구매할 스태미나 양
        int val = StaminaConfirmField.GetComponent<BuyStaminaData>().StaminaValue;

        string result_text = UtilityHub.query_builder.Append("기존 스태미나\n")
                                                     .Append(datahub.User.Stamina)
                                                     .Append("\n\n구매 이후 스태미나\n")
                                                     .Append((datahub.User.Stamina + val))
                                                     .ToString();
        Confirm.transform.Find("Announce").GetComponent<TMP_Text>().text = result_text;
        UtilityHub.query_builder.Clear();

        // 기존 창 끄기
        StaminaConfirmField.GetComponent<BuyStaminaData>().Reset();
        StaminaConfirmField.SetActive(false);

        // 구매
        datahub.User.Stamina += val;
        datahub.User.Dot -= need_gold;
        // 구매 적용
        string query = UtilityHub.query_builder.Append("UPDATE user SET stamina=")
                                               .Append(datahub.User.Stamina)
                                               .Append(", dot=")
                                               .Append(datahub.User.Dot)
                                               .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "user");


        // 구매 결과 창 보이기
        Confirm.transform.Find("Field").gameObject.SetActive(false);
        Confirm.transform.Find("Announce").gameObject.SetActive(true);
        Confirm.GetComponent<ChestResultAnchor>().AnchorReset();
        Confirm.SetActive(true);

        // header reload
        UtilityHub.PageHeaderSetting();
    }

    // 광고 스태미나 구매
    public void AdvStaminaBuy() {
        // 구매할 스태미나 양
        int val = UnityEngine.Random.Range(8, 17);

        string result_text = UtilityHub.query_builder.Append("기존 스태미나\n")
                                                     .Append(datahub.User.Stamina)
                                                     .Append("\n\n보상 이후 스태미나\n")
                                                     .Append((datahub.User.Stamina + val))
                                                     .ToString();
        Confirm.transform.Find("Announce").GetComponent<TMP_Text>().text = result_text;
        UtilityHub.query_builder.Clear();

        // 기존 창 끄기
        // popup의 데이터 초기화
        Popup.GetComponent<Chest>().Reset();
        Popup.SetActive(false);

        // 구매
        datahub.User.Stamina += val;
        // 구매 적용
        string query = UtilityHub.query_builder.Append("UPDATE user SET stamina=")
                                               .Append(datahub.User.Stamina)
                                               .Append(", dot=")
                                               .Append(datahub.User.Dot)
                                               .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "user");


        // 구매 결과 창 보이기
        Confirm.transform.Find("Field").gameObject.SetActive(false);
        Confirm.transform.Find("Announce").gameObject.SetActive(true);
        Confirm.GetComponent<ChestResultAnchor>().AnchorReset();
        Confirm.SetActive(true);
    }


    // 스태미나 구매 취소
    public void StaminaBuyCancel() {
        StaminaConfirmField.GetComponent<BuyStaminaData>().Reset();
        Popup.transform.Find("Confirm").GetComponent<Button>().onClick.RemoveAllListeners();
        StaminaConfirmField.SetActive(false);
    }

    // 골드가 충분하지 않다는 알림을 보여줄 창
    public void CantBuyAnnounceCheck() {
        CannotBuyAnnounce.SetActive(false);
    }


}