using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ShapeDefenseSpace;

using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;
using System.Text;

/// <summary>
/// 업적을 생성하는 함수
/// </summary>
/// 
public class AchievementControll : QuestController
{
    [SerializeField] private GameObject A_Content;
    [SerializeField] private GameObject A_AllReciveBtn;

    private readonly StringBuilder builder = new();

    // 받은 보상을 저장해둘 변수

    private readonly int announce_size = datahub.UnitCounter.Count;
    private int reward_gold_summary = 0;

    void Start()
    {
        Content = A_Content;
        AllReciveBtn = A_AllReciveBtn;

        Show();
    }

    public override void Show() {
        PageReset();
        // 갱신된 정보를 바탕으로 업적 목록 생성
        // list를 재정렬
        ArrayList ori_list = datahub.Achievement;
        int size = ori_list.Count;

        for (int i = 1; i < size; i++) {
            Achievement achieve = ori_list[i] as Achievement;
            // checker가 1이고 repeat이 false인 것들 모아두기 == 완료된것
            if (achieve.Checker == 1 && achieve.Repeat == false) {
                already_do.Add(achieve);
            }
            // 받기가 가능한 업적 모아두기
            else if (achieve.CanRecive) {
                can_recive.Add(achieve);
            }
            // 위 두 모은것 이외의 나머지 저장하기
            else {
                normal.Add(achieve);
            }
        }

        /*
         * 프리팹 설정항목
         * Background - Color
         * Name - name(text)
         * Slider - value 조절
         * Counter - text
         * Recive Color Controll
         * -> 최상위에 BtnStateControl설정
         *  -> SlideChecker 로 value 조절
         *  -> Counter에 값 작성
         */
        // 받기가능한 업적부터 출력
        size = can_recive.Count;
        // 받기 가능하면 모두 받기 버튼을 출력
        if (size > 0) {
            //AllReciveBtn.SetActive(true);
            AllReciveBtn.GetComponent<Image>().color = BTN_ACTIVATE_STATE;
            AllReciveBtn.transform.Find("Text").gameObject.GetComponent<TMP_Text>().color = ACTIVE_TEXT;
            AllReciveBtn.GetComponent<Button>().interactable = true;
        }
        else {
            //AllReciveBtn.SetActive(false);
            AllReciveBtn.GetComponent<Image>().color = BTN_DISABLE_STATE;
            AllReciveBtn.transform.Find("Text").gameObject.GetComponent<TMP_Text>().color = DISABLE_TEXT;
            AllReciveBtn.GetComponent<Button>().interactable = false;
        }

        for (int i = 0; i < size; i++) {
            Achievement now = can_recive[i] as Achievement;
            GameObject prefab = Instantiate(_quest_obj, Content.transform.position, Content.transform.rotation);
            // value 설정
            prefab.transform.SetParent(Content.transform, false);
            prefab.transform.Find("BackGround").gameObject.GetComponent<Image>().color = DEFAULT_BACKGROUND;
            prefab.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text = now.Name;

            if (now.Repeat) {
                // 무한루프 업적은 repeatreward가 없다
                int tmp_val = now.EndlessValue * (now.Checker + 1);
                prefab.GetComponent<AchievementSliderChecker>().NeedVal = tmp_val;

                prefab.transform.Find("Counter").gameObject.GetComponent<TMP_Text>().text = builder.Append(now.Counter.ToString())
                                                                                                    .Append(" / ")
                                                                                                    .Append(tmp_val)
                                                                                                    .ToString();
                builder.Clear();
            }
            else {
                prefab.GetComponent<AchievementSliderChecker>().NeedVal = 1;
                prefab.transform.Find("Counter").gameObject.GetComponent<TMP_Text>().text = builder.Append(now.Counter.ToString())
                                                                                                                    .Append(" / 1")
                                                                                                                    .ToString();
                builder.Clear();
            }
            prefab.GetComponent<AchievementSliderChecker>().NowVal = now.Counter;
            prefab.GetComponent<AchievementReciveBtnState>().ColorSetting(true);
            //prefab.AddComponent<AchievementRecive>();
            prefab.transform.Find("Recive").gameObject
                            .GetComponent<Button>().onClick
                            .AddListener(() => Recive(now.Id));
            //prefab.GetComponent<AchievementRecive>().Id = now.Id;

            // untouchable
            prefab.transform.GetChild(2).gameObject.GetComponent<Slider>().interactable = false;

        }

        // 나머지 출력
        size = normal.Count;
        for (int i = 0; i < size; i++) {
            Achievement now = normal[i] as Achievement;
            GameObject prefab = Instantiate(_quest_obj, Content.transform.position, Content.transform.rotation);
            // value 설정
            prefab.transform.SetParent(Content.transform, false);
            prefab.transform.Find("BackGround").gameObject.GetComponent<Image>().color = DEFAULT_BACKGROUND;
            prefab.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text = now.Name;
            if (now.Repeat) {
                // 반복 업적은 repeatreward가 없다
                int tmp_val = now.EndlessValue * (now.Checker + 1);
                prefab.GetComponent<AchievementSliderChecker>().NeedVal = tmp_val;

                prefab.transform.Find("Counter").gameObject.GetComponent<TMP_Text>().text = builder.Append(now.Counter.ToString())
                                                                                                    .Append(" / ")
                                                                                                    .Append(tmp_val)
                                                                                                    .ToString();
                builder.Clear();
            }
            else {
                prefab.GetComponent<AchievementSliderChecker>().NeedVal = 1;
                prefab.transform.Find("Counter").gameObject.GetComponent<TMP_Text>().text = builder.Append(now.Counter.ToString())
                                                                                                    .Append(" / 1")
                                                                                                    .ToString();
                builder.Clear();
            }
            prefab.GetComponent<AchievementSliderChecker>().NowVal = now.Counter;
            
            prefab.GetComponent<AchievementReciveBtnState>().ColorSetting(false);
            //prefab.AddComponent<AchievementRecive>();
            prefab.transform.Find("Recive").gameObject
                            .GetComponent<Button>().onClick
                            .AddListener(() => Recive(now.Id));
            //prefab.GetComponent<AchievementRecive>().Id = now.Id;

            // untouchable
            prefab.transform.GetChild(2).gameObject.GetComponent<Slider>().interactable = false;
        }

        // 완료된 것 출력
        size = already_do.Count;
        for (int i = 0; i < size; i++) {
            Achievement now = already_do[i] as Achievement;
            GameObject prefab = Instantiate(_quest_obj, Content.transform.position, Content.transform.rotation);
            // value 설정
            prefab.transform.SetParent(Content.transform, false);
            prefab.transform.Find("BackGround").gameObject.GetComponent<Image>().color = DEACTIVATE_BACKGROUND;
            prefab.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text = now.Name;
            if (now.Repeat) {

                int tmp_val = now.EndlessValue * (now.Checker + 1);
                prefab.GetComponent<AchievementSliderChecker>().NeedVal = tmp_val;

            }
            else {
                prefab.GetComponent<AchievementSliderChecker>().NeedVal = 1;
            }
            prefab.GetComponent<AchievementSliderChecker>().NowVal = now.Counter;
            prefab.transform.Find("Counter").gameObject.GetComponent<TMP_Text>().text = Complete_Text;
            prefab.transform.Find("Counter").gameObject.GetComponent<TMP_Text>().color = DISABLE_TEXT;
            prefab.transform.Find("Recive").Find("Text").gameObject.GetComponent<TMP_Text>().text = Complete_Text;
            prefab.transform.Find("Slider").Find("fill_area").Find("Fill").gameObject.GetComponent<Image>().color = BTN_DISABLE_STATE;
            prefab.GetComponent<AchievementReciveBtnState>().ColorSetting(false);
            //prefab.AddComponent<AchievementRecive>();
            prefab.transform.Find("Recive").gameObject
                            .GetComponent<Button>().onClick
                            .AddListener(() => Recive(now.Id));
            //prefab.GetComponent<AchievementRecive>().Id = now.Id;

            // untouchable
            prefab.transform.GetChild(2).gameObject.GetComponent<Slider>().interactable = false;
        }

        // 모두 출력햇으면 초기화
        InitArrayList();
    }

    public override void AllRecive() {
        // 갱신된 정보를 바탕으로 업적 목록 생성
        // list를 재정렬
        
        int size = datahub.Achievement.Count;

        // 받기 가능한 업적을 확인해서 가능한 횟수를 전부 받기
        for (int i = 0; i < size; i++) {
            Achievement achieve = datahub.Achievement[i] as Achievement;
            string query;

            // 받기가 가능한 업적 받기
            if (achieve.CanRecive) {

                GetReward(achieve);

                achieve.CanRecive = false;

                // 다시 받을수 있는 환경인지 재검사
                if (achieve.Repeat) {

                    // 반복 업적이면 다음 조건을 충족했는지 확인해서 다음 보상까지 계속 타먹기
                    bool repeat_checker = true;
                    //Debug.Log(achieve.Checker + " // " + achieve.RepeatRewardRequest.Count + " // " + achieve.RepeatRewardRequest[0]);

                    // 무한 반복퀘
                    while (repeat_checker) {
                        int requare = (achieve.Checker+1) * achieve.EndlessValue;
                        if (achieve.Counter >= requare) {
                            GetReward(achieve);
                            achieve.Checker++;
                        }
                        else {
                            repeat_checker = false;
                        }
                    }

                }
                else {
                    achieve.Checker++;
                }

                // 수정된 checker값 업데이트
                query = builder.Append("UPDATE achievement SET checker=")
                                                       .Append(achieve.Checker)
                                                       .Append(" WHERE id=")
                                                       .Append(achieve.Id)
                                                       .ToString();
                modifyDB.ControllDB(query, "achieve");
                builder.Clear();
            }
        }

        // piece 하나라도 있으면 type 2
        // 없으면 문자화하고 type1
        bool checker = false;
        for(int i = 0; i < announce_size; i++) {
            if (datahub.UnitCounter[datahub.Unit_Ids[i]] > 0) {
                checker = true;
                break;
            }
        }

        string announce_text = "";
        int type = 2;
        if (!checker) {
            type = 1;
            announce_text = ReciveAnnounce();
        }
        
        // 업적을 재소환
        Achieve_Connector.Connect();
        
        // 확인 창 띄우기
        ReciveConfirm.Instance.OpenConfirmWindow(announce_text, reward_gold_summary, type);
    }

    public override void Recive(int id) {

        //GameObject.Find("Canvas").transform.Find("Announce").Find("Panel").Find("Image").Find("Test").gameObject.GetComponent<TMP_Text>().text = "in recive";
        //Debug.Log("in Recive");
        // 획득 가능한지 확인
        Achievement achieve = datahub.Achievement[id] as Achievement;
        
        if (achieve.CanRecive) {
            string announce_text = "";

            GetReward(achieve);

            achieve.CanRecive = false;
            // checker를 추가하고
            achieve.Checker++;
            // DB를 modify하고 -> checker만 업데이트 해주면 됨
            string query = builder.Append("UPDATE achievement SET checker=")
                                            .Append(achieve.Checker)
                                            .Append(" WHERE id=")
                                            .Append(achieve.Id)
                                            .ToString();
            modifyDB.ControllDB(query, "achieve");
            builder.Clear();


            // piece 하나라도 있으면 type 2
            // 없으면 문자화하고 type1
            bool checker = false;
            for (int i = 0; i < announce_size; i++) {
                if (datahub.UnitCounter[datahub.Unit_Ids[i]] > 0) {
                    checker = true;
                    break;
                }
            }

            int type = 2;
            if (!checker) {
                type = 1;
                announce_text = ReciveAnnounce();
            }

            // 업적을 재소환
            Achieve_Connector.Connect();

            // 확인 창 띄우기
            ReciveConfirm.Instance.OpenConfirmWindow(announce_text, reward_gold_summary, type);
        }
    }

    /// <summary>
    /// 업적 보상 받기 함수
    /// </summary>
    /// <param name="achieve">현재 업적을 수령하는 업적 클래스</param>
    private void GetReward(Achievement achieve) {
        
        // 리워드를 수령
        bool piece_check = false;
        int recive_size = achieve.RewardList.Count;
        int counter;
        string query;

        for (int i = 0; i < recive_size; i++) {
            // 보상 대상 확인
            switch (achieve.RewardList[i] as string) {
                case "gold":
                    int reward_gold = (int)achieve.RewardVal[i];
                    datahub.User.Dot += reward_gold;
                    query = UtilityHub.query_builder.Append("UPDATE user SET dot=")
                                                    .Append(datahub.User.Dot)
                                                    .ToString();
                    UtilityHub.query_builder.Clear();
                    modifyDB.ControllDB(query, "user");

                    // 보상 누적 기록
                    reward_gold_summary += reward_gold;
                    break;
                // e
                case "e":
                    piece_check = true;
                    counter = (int)achieve.RewardVal[i];
                    UtilityHub.SelectRecivePiece( 1, counter);
                    
                    break;
                // d
                case "d":
                    piece_check = true;
                    counter = (int)achieve.RewardVal[i];
                    UtilityHub.SelectRecivePiece(2, counter);

                    break;
                // c
                case "c":
                    piece_check = true;
                    counter = (int)achieve.RewardVal[i];
                    UtilityHub.SelectRecivePiece(3, counter);

                    break;
                // b
                case "b":
                    counter = (int)achieve.RewardVal[i];
                    UtilityHub.SelectRecivePiece(4, counter);
                        
                    break;
                // a
                case "a":
                    piece_check = true;
                    counter = (int)achieve.RewardVal[i];
                    UtilityHub.SelectRecivePiece(5, counter);
                    
                    break;

                //s
                case "s":
                    piece_check = true;
                    counter = (int)achieve.RewardVal[i];
                    UtilityHub.SelectRecivePiece(6, counter);

                    break;
            }

            // 조각 보상 확인
            if (piece_check) {
                for (int k = 0; k < announce_size; k++) {
                    if (datahub.UnitCounter[datahub.Unit_Ids[k]] > 0) {
                        var unit = datahub.Unit_dic[datahub.Unit_Ids[k]] as Unit;
                        unit.Piece += datahub.UnitCounter[datahub.Unit_Ids[k]];
                        query = builder.Append("UPDATE unit SET piece=")
                                       .Append(unit.Piece)
                                       .Append(" WHERE id=")
                                       .Append(unit.Id)
                                       .ToString();
                        builder.Clear();
                        modifyDB.ControllDB(query, "unit");

                        // unit upgrade check
                        UIUnitPageSetting.Instance.CheckUnitLevel();
                    }
                }
            }
        }
    }

    private string ReciveAnnounce() {
        string announce_text = "";
        //total gold가 있으면 
        if (reward_gold_summary > 0) {
            announce_text += builder.Append("골드 + ")
                                    .Append(reward_gold_summary)
                                    .Append('\n')
                                    .ToString();
            builder.Clear();
        }
        reward_gold_summary = 0;
        //datahub.InitCounter();

        return announce_text;
    }
}
