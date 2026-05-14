using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.PublicData;
using static ShapeDefenseSpace.GameData;
using System.Text;

/// <summary>
/// 일일퀘스트 생성 함수
/// </summary>
/// 
public class DailyQuestControll : QuestController
{
    [SerializeField] private GameObject D_Content;
    [SerializeField] private GameObject D_AllReciveBtn;

    private readonly StringBuilder builder = new();

    void Start()
    {
        Content = D_Content;
        AllReciveBtn = D_AllReciveBtn;

        Show();
    }

    public override void Show() {
        PageReset();
        // 갱신된 정보를 바탕으로 업적 목록 생성
        // list를 재정렬
        ArrayList ori_list = datahub.DailyQuest;
        int size = ori_list.Count;

        for (int i = 1; i < size; i++) {
            DailyQuest achieve = ori_list[i] as DailyQuest;            
            // 완료된것 모아두기 == checker == 1
            if (achieve.Checker == 1 ) {
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
        if(size > 0) {
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
            DailyQuest now = can_recive[i] as DailyQuest;
            GameObject prefab = Instantiate(_quest_obj, Content.transform.position, Content.transform.rotation);
            // value 설정
            prefab.transform.SetParent(Content.transform, false);
            prefab.transform.Find("BackGround").gameObject.GetComponent<Image>().color = DEFAULT_BACKGROUND;
            prefab.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text = now.Name;

            // 요구치가 0보다 큰 것 이므로 요구치를 추가 작성해줘야함
            if (now.RequestCounter > 0) {
                prefab.GetComponent<AchievementSliderChecker>().NeedVal = now.RequestCounter;
                prefab.transform.Find("Counter").gameObject.GetComponent<TMP_Text>().text = builder.Append(now.Counter.ToString())
                                                                                                    .Append(" / ")
                                                                                                    .Append(now.RequestCounter.ToString())
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
            //prefab.AddComponent<DailyQuestRecive>();
            prefab.transform.Find("Recive").gameObject
                            .GetComponent<Button>().onClick
                            .AddListener(() => Recive(now.Id));
            //prefab.GetComponent<DailyQuestRecive>().Id = now.Id;

            // untouchable
            prefab.transform.GetChild(2).gameObject.GetComponent<Slider>().interactable = false;

        }

        // 나머지 출력
        size = normal.Count;
        for (int i = 0; i < size; i++) {
            DailyQuest now = normal[i] as DailyQuest;
            GameObject prefab = Instantiate(_quest_obj, Content.transform.position, Content.transform.rotation);
            // value 설정
            prefab.transform.SetParent(Content.transform, false);
            prefab.transform.Find("BackGround").gameObject.GetComponent<Image>().color = DEFAULT_BACKGROUND;
            prefab.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text = now.Name;

            // 요구치가 0보다 큰 것 이므로 요구치를 추가 작성해줘야함
            if (now.RequestCounter > 0) {
                prefab.GetComponent<AchievementSliderChecker>().NeedVal = now.RequestCounter;
                prefab.transform.Find("Counter").gameObject.GetComponent<TMP_Text>().text = builder.Append(now.Counter.ToString())
                                                                                                    .Append(" / ")
                                                                                                    .Append(now.RequestCounter.ToString())
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
            //prefab.AddComponent<DailyQuestRecive>();
            prefab.transform.Find("Recive").gameObject
                            .GetComponent<Button>().onClick
                            .AddListener(() => Recive(now.Id));
            //prefab.GetComponent<DailyQuestRecive>().Id = now.Id;

            // untouchable
            prefab.transform.GetChild(2).gameObject.GetComponent<Slider>().interactable = false;
        }

        // 완료된 것 출력
        size = already_do.Count;
        for (int i = 0; i < size; i++) {
            DailyQuest now = already_do[i] as DailyQuest;
            GameObject prefab = Instantiate(_quest_obj, Content.transform.position, Content.transform.rotation);
            // value 설정
            prefab.transform.SetParent(Content.transform, false);
            prefab.transform.Find("BackGround").gameObject.GetComponent<Image>().color = DEACTIVATE_BACKGROUND;
            prefab.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text = now.Name;
            // 요구치가 0보다 큰 것 이므로 요구치를 추가 작성해줘야함
            if (now.RequestCounter > 0) {
                prefab.GetComponent<AchievementSliderChecker>().NeedVal = now.RequestCounter;
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
            //prefab.AddComponent<DailyQuestRecive>();
            prefab.transform.Find("Recive").gameObject
                            .GetComponent<Button>().onClick
                            .AddListener(() => Recive(now.Id));
            //prefab.GetComponent<DailyQuestRecive>().Id = now.Id;

            // untouchable
            prefab.transform.GetChild(2).gameObject.GetComponent<Slider>().interactable = false;
        }
        InitArrayList();
    }


    public override void AllRecive() {
        // 갱신된 정보를 바탕으로 업적 목록 생성
        // list를 재정렬
        ArrayList ori_list = datahub.DailyQuest;
        int size = ori_list.Count;
        int total_gold = 0;
        int total_exp = 0;
        string query;

        // 받기 가능한 업적을 확인해서 가능한 횟수를 전부 받기
        for (int i = 0; i < size; i++) {
            ori_list = datahub.DailyQuest;
            DailyQuest achieve = ori_list[i] as DailyQuest;
            // 받기가 가능한 업적 받기
            if (achieve.CanRecive) {

                // 골드와 경험치 보상 수령
                int reward_gold = (int)achieve.RewardVal[0];
                int reward_exp = (int)achieve.RewardVal[1];

                total_gold += reward_gold;
                total_exp += reward_exp;

                achieve.CanRecive = false;
                achieve.Checker++;
 
                // 수정된 checker값 업데이트
                query = builder.Append("UPDATE dailyquest SET checker=")
                                                       .Append(achieve.Checker)
                                                       .Append(" WHERE id=")
                                                       .Append(achieve.Id)
                                                       .ToString();
                modifyDB.ControllDB(query, "daily");
                builder.Clear();

                // 업적 목록을 update 
                //Daily_Connector.Connect();
            }

        }

        string announce_text = "";

        //total gold가 있으면 
        if (total_gold > 0) {
            announce_text += builder.Append("골드 + ")
                                                 .Append(total_gold)
                                                 .Append('\n')
                                                 .ToString();
            builder.Clear();
        }

        //total exp가 있으면 
        if (total_exp > 0) {
            announce_text += builder.Append("경험치 + ")
                                                 .Append(total_exp)
                                                 .Append('\n')
                                                 .ToString();
            builder.Clear();
        }

        datahub.User.Dot += total_gold;
        datahub.User.Experience += total_exp;

        query = builder.Append("UPDATE user SET dot=")
                                        .Append(datahub.User.Dot)
                                        .Append(", experience=")
                                        .Append (datahub.User.Experience)
                                        .ToString();
        modifyDB.ControllDB(query, "user");
        builder.Clear();

        // 업적을 재소환
        Daily_Connector.Connect();

        // 확인 창 띄우기
        ReciveConfirm.Instance.OpenConfirmWindow(announce_text, 0, 1);

    }

    public override void Recive(int id) {
        DailyQuest quest = datahub.DailyQuest[id] as DailyQuest;
        string announce;

        if (quest.CanRecive) {

            // 골드와 경험치 보상 수령
            int reward_gold = (int)quest.RewardVal[0];
            int reward_exp = (int)quest.RewardVal[1];

            datahub.User.Dot += reward_gold;
            datahub.User.Experience += reward_exp;

            announce = builder.Append("골드 + ")
                            .Append(reward_gold)
                            .Append("\n")
                            .Append("경험치 + ")
                            .Append(reward_exp)
                            .ToString();
            builder.Clear();

            quest.CanRecive = false;
            quest.Checker++;

            // dot 업데이트
            string query = builder.Append("UPDATE user SET dot=")
                                    .Append(datahub.User.Dot)
                                    .Append(", experience=")
                                    .Append(datahub.User.Experience)
                                    .ToString();
            modifyDB.ControllDB(query, "user");
            builder.Clear();

            // 수정된 checker값 업데이트
            query = builder.Append("UPDATE dailyquest SET checker=")
                            .Append(quest.Checker)
                            .Append(" WHERE id=")
                            .Append(quest.Id)
                            .ToString();
            modifyDB.ControllDB(query, "daily");
            builder.Clear();

            // 업적 update
            Daily_Connector.Connect();

            // pannel 띄우기
            ReciveConfirm.Instance.OpenConfirmWindow(announce, 0, 1);
        }

    }
}