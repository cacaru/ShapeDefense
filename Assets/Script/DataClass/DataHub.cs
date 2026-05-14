using Mono.Data.Sqlite;
using ShapeDefenseSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Tilemaps;
using static ShapeDefenseSpace.GameData;

public class DataHub : Singleton<DataHub>
{
    // db 관련 정보
    private string connectionString;
    public IDbConnection dbConnection;    

    public STATE DB_State;

    void Start() {

        // for db load
#if UNITY_ANDROID
        connectionString = "Data Source=" + Application.persistentDataPath + "/ShapeDefenseDB.db" + ";Version=3;";
#else
        connectionString = "URI=file:" + Application.persistentDataPath + "/ShapeDefenseDB.db";
#endif

        dbConnection = new SqliteConnection(connectionString);

        unit_wait_time = new WaitForSecondsRealtime(1f);
        sta_wait_time = new WaitForSecondsRealtime(1f);

        // 30분이 이미 지나있으면 가능하게 바꿔야함
        AdvUnitTimerStart();
        AdvStaTimerStart();
    }

    #region Data

    // 내 정보를 담을 변수
    private User user;
    private Dictionary<int, Unit> unit_dict;
    private Dictionary<int, int> unit_counter;
    private int unit_number;
    private List<int> unit_ids;

    // 적 정보를 담을 변수
    private ArrayList enemy;
    // 업정 정보를 담을 변수
    private ArrayList achievement;
    // 설정 정보를 저장할 변수
    private ArrayList setoption;
    // 일일 퀘스트 정보 저장
    private ArrayList dailyquest;
    // 주간 퀘스트 정보 저장
    private ArrayList weeklyquest;

    private SCENE_NUMBER now_scene = SCENE_NUMBER.NONE;
    
    // Unit 창에서 상세 정보로 넘어갈 떄 줄 id 변수
    private int unit_id_with_page;
    // Unit 창에서 유닛 상세로 넘어가기전의 스크롤 위치 변수
    private Vector2 anchor = Vector2.zero; 
    // 첫 페이지에서 플레이어 정보를 갱신할 변수
    private bool user_connect_end = false;
    private bool db_connect_end = false;
    private bool achieve_connect_end = false;
    private bool daily_connect_end = false;
    private bool weekly_connect_end = false;
    // 일일 퀘스트 등 시작전에 해야할 로드가 끝나는지 확인할 변수
    private bool first_quest_checker = false;
    private bool quest_reset_checker = false;
    // 유닛 상세 목록에서 첫 페이지를 넘어갔을 때 확인될 변수
    private bool from_unit_detail = false;
    // 스태미나 충전 남은 시간을 표시할 변수
    private int remain_time = 120;

    // 조각 상자 광고 제한시간
    private int adv_unit_chest_recive_time = 0;
    private int adv_sta_chest_recive_time = 0;
    private WaitForSecondsRealtime unit_wait_time;
    private WaitForSecondsRealtime sta_wait_time;

    /// <summary>
    /// 게임에서 사용될 변수
    ///  게임을 나가거나 게임이 완료되면 초기화
    /// </summary>
    // 현재 게임 중인것을 판단할 변수
    private bool gaming = false;
    private int stage_number = 1;                       // 현재 맵 번호
    private int difficulty = 1;                         // 난이도
    private int stage_field_number = 45;                // 맵에 따른 소환가능 필드 최대 번호
    private ArrayList stage_field;                      // 소환 가능 필드를 저장해 둘 변수
    private Tilemap unit_map;                           // 현재 유닛 tile을 저장해둘 변수
    private int left_field;                             // 소환 가능한 필드의 남은 갯수
    //private int[] unit_counter;                         // 현재 소환한 유닛 수를 저장해둘 변수
    private int combine_waiting_pos;                    // 현재 조합할 재료 유닛의 위치 번호를 저장
    private int combine_target_id;                      // 조합할 목표 유닛의 번호 저장
    private int round_number = 0;                       // 현재 라운드
    private int end_round = 101;                        // 최종 라운드
    private int speed_up = 1;                           // 게임 속도 조절용 변수 // 0 일시정지 , 1 기본속도 , 2 배속
    private int speed_rate = 1;                         // 동적 객체 속도 조절용 변수
    private bool pause = false;                         // 일시정지용 변수
    private bool kill_last_boss = false;                // 마지막 보스를 잡았는지 확인용
    private bool is_show_unit_count = false;            // 현재 유닛 카운트 패널이 보여지고 있는지 확인용

    private int item_roll_value = 50;                   // 아이템변환 가격
    private int type_roll_value = 300;                  // 공격 형식 변환 가격

    private bool item_roll_active_checker = false;      // 현재 아이템 변환중인 상태를 확인할 변수 // false로 꺼져있으면 변환되지 않게

    // 강화도
    private int upgrade_e_value = 0;
    private int upgrade_d_value = 0;
    private int upgrade_c_value = 0;
    private int upgrade_b_value = 0;
    private int upgrade_a_value = 0;
    private int upgrade_s_value = 0;
    private readonly int max_upgrade = 10;

    // 소환재화 (점)
    private float dot = 80;
    private int need_dot = 10;
    private int core_count = 1;
    private int unicore_count = 0;

    // 스테이지에 따라 한 라운드에 등장할 몹 수와 게임 오버 기준 수 저장;
    private int now_enemy_counter = 0;
    private int enemy_counter = 40;
    private int max_enemy_counter = 120;
    
    #endregion


    #region property
    public User User { get { return user; } set { user = value; } }
    public Dictionary<int, Unit> Unit_dic { get { return unit_dict; } set { unit_dict = value; } }
    public List<int> Unit_Ids { get { return unit_ids; } set { unit_ids = value; } }
    public int Unit_Number { get { return unit_number; } set { unit_number = value; } }
    public ArrayList Enemy { get { return enemy; } set {  enemy = value; } }
    public ArrayList Achievement { get { return achievement; } set {  achievement = value; } }
    public ArrayList Setoption { get { return setoption; } set { setoption = value; } }
    public ArrayList DailyQuest { get { return dailyquest; } set { dailyquest = value; } }
    public ArrayList WeeklyQuest { get { return weeklyquest; } set { weeklyquest = value; } }
    public SCENE_NUMBER NowScene { get { return now_scene; } set { now_scene = value; } }

    public int UnitidWithPage { get {  return unit_id_with_page; } set { unit_id_with_page = value; } }
    public bool UserConnectEnd { get { return user_connect_end; } set { user_connect_end = value; } }
    public bool DBConnectEnd { get { return db_connect_end; } set { db_connect_end = value; } }
    public bool AchieveConnectEnd { get { return achieve_connect_end; } set { achieve_connect_end = value; } }
    public bool DailyConnectEnd { get { return daily_connect_end; } set { daily_connect_end = value; } }
    public bool WeeklyConnectEnd { get { return weekly_connect_end; } set { weekly_connect_end = value; } }
    public bool FirstQuestChecker { get { return first_quest_checker; } set {  first_quest_checker = value; } }
    public bool QuestResetChecker { get { return quest_reset_checker; } set { quest_reset_checker = value; } }
    public string ConnectionString { get {  return connectionString; } }
    public Vector2 Anchor { get { return anchor; } set { anchor = value; } }
    public bool FromUnitDetail { get { return from_unit_detail; } set { from_unit_detail = value; } }
    public int RemainTime { get { return remain_time; } set { remain_time = value; } }
    public int AdvUnitTime { get { return adv_unit_chest_recive_time; } set { adv_unit_chest_recive_time = value; } }
    public int AdvStaTime { get { return adv_sta_chest_recive_time; } set { adv_sta_chest_recive_time = value; } }

    public int UpgradeValueE { get { return upgrade_e_value; } set { upgrade_e_value = value; } }
    public int UpgradeValueD { get { return upgrade_d_value; } set {  upgrade_d_value = value; } }
    public int UpgradeValueC { get { return upgrade_c_value;} set { upgrade_c_value = value; } }
    public int UpgradeValueB { get { return upgrade_b_value; } set {  upgrade_b_value = value; } }
    public int UpgradeValueA { get { return upgrade_a_value;} set {  upgrade_a_value = value; } }
    public int UpgradeValueS { get { return upgrade_s_value; } set { upgrade_s_value = value; } }
    public int MaxUpgrade { get { return max_upgrade; } }
    public bool Gaming { get { return gaming; } set { gaming = value; } }
    public float Dot { 
        get { return dot; }  
        set { 
            dot = value;
            // 옵저버에게 값이 변화했음을 알림
            if (gaming) {
                InGameDotObserver.Instance.DotObserver();
            }

        } 
    }
    public int NeedDot {
        get { return need_dot; }
        set {
            need_dot = value;
            if (gaming) {
                InGameDotObserver.Instance.NeedDotObserver();
            }

        }
    }
    public int CoreCount { get { return core_count; } set { core_count = value; } }
    public int UnicoreCount { get { return unicore_count; } set { unicore_count = value; } }
    public int Difficulty { 
        get { return difficulty; } 
        set { 
            difficulty = value;
            // value에 따라 enemy_counter와 max_enemy_counter 값 수정
            this.enemy_counter = 40;
            switch (Difficulty) {
                case 1:
                    this.max_enemy_counter = 120;
                    this.end_round = 101;
                    break;
                case 2:
                    this.max_enemy_counter = 120;
                    this.end_round = 111;
                    break;
                case 3:
                    this.max_enemy_counter = 120;
                    this.end_round = 121;
                    break;
                case 4:
                    this.max_enemy_counter = 120;
                    this.end_round = 131;
                    break;
                case 5:
                    this.max_enemy_counter = 110;
                    this.end_round = 141;
                    break;
                case 6:
                    this.max_enemy_counter = 110;
                    this.end_round = 151;
                    break;
                case 7:
                    this.max_enemy_counter = 110;
                    this.end_round = 161;
                    break;
                case 8:
                    this.max_enemy_counter = 110;
                    this.end_round = 171;
                    break;
                case 9:
                    this.max_enemy_counter = 100;
                    this.end_round = 181;
                    break;
                case 10:
                    this.max_enemy_counter = 100;
                    this.end_round = 191;
                    break;
                case 11:
                    this.max_enemy_counter = 100;
                    this.end_round = 201;
                    break;
                case 12:
                    this.max_enemy_counter = 100;
                    this.end_round = 211;
                    break;
                case 13:
                    this.max_enemy_counter = 90;
                    this.end_round = 221;
                    break;
                case 14:
                    this.max_enemy_counter = 90;
                    this.end_round = 231;
                    break;
                case 15:
                    this.max_enemy_counter = 90;
                    this.end_round = 241;
                    break;

                default:
                    this.max_enemy_counter = 120;
                    this.end_round = 101;
                    break;
            }
        } 
    }

    public int StageNumber { get { return stage_number; } set { stage_number = value; } }
    public int RoundNumber { get { return round_number; } set { round_number = value; } }
    public int EndRound { get { return end_round; } set { end_round = value; } }
    public int NowEnemyCounter { 
        get { return now_enemy_counter; } 
        set { 
            now_enemy_counter = value;

            if (now_enemy_counter <= 0)
                now_enemy_counter = 0;
        }
    }
    public int EnemyCounter { get { return enemy_counter; } set { enemy_counter = value; } }
    public int MaxEnemyCounter { get { return max_enemy_counter; } set { max_enemy_counter = value; } }
    public int StageFieldNumber { get { return stage_field_number; } set { stage_field_number = value; } }
    public ArrayList StageField { get { return stage_field; } set { stage_field = value; } }
    public Tilemap UnitMap { get { return unit_map; } set { unit_map = value; } }
    public int LeftStageField { get { return left_field; } set { left_field = value; } }
    public Dictionary<int, int> UnitCounter { get { return unit_counter; } set { unit_counter = value; } } 
    public int CombineWaitingPos { get { return combine_waiting_pos; } set { combine_waiting_pos = value; } }
    public int CombineTargetId { get { return combine_target_id; } set { combine_target_id = value; } }
    public int SpeedUp { get { return speed_up; } set { speed_up = value; } }
    public int SpeedRate { get { return speed_rate; } set { speed_rate = value; } }
    public bool Pause { get { return pause; } set { pause = value; } }
    public bool KillLastBoss { get { return kill_last_boss; } set { kill_last_boss = value; } }
    public bool IsShowUnitCount { get { return is_show_unit_count; } set { is_show_unit_count = value; } }
    public int ItemRollValue { get { return item_roll_value; } set { item_roll_value = value; } }
    public int TypeRollValue { get { return type_roll_value; } set { type_roll_value = value; } }
    public bool ItemRollActive { get { return item_roll_active_checker; } set { item_roll_active_checker = value; } }
    public bool CombineCheckOption { 
        get {
            string str = PlayerPrefs.GetString("Combine_Check") == "" ? "true" : PlayerPrefs.GetString("Combine_Check");
            return str.Equals("true");
        }
        set {
            PlayerPrefs.SetString("Combine_Check", value.ToString());
            PlayerPrefs.Save();
        }
    }

    public bool RollCheckOption {
        get {
            string str = PlayerPrefs.GetString("Roll_Check") == "" ? "true" : PlayerPrefs.GetString("Roll_Check");
            return str.Equals("true");
        }
        set {
            string tmp = value ? "true" : "false";
            PlayerPrefs.SetString("Roll_Check", tmp);
        }
    }

    #endregion

    public void InitCounter() {
        int keys_size = unit_ids.Count;
        for (int i = 0; i < keys_size; i++) {
            unit_counter[unit_ids[i]] = 0;
        }
    }

    public void GameStatInit() {
        // 강화도 초기화
        UpgradeValueE = 0;
        UpgradeValueD = 0;
        UpgradeValueC = 0;
        UpgradeValueB = 0;
        UpgradeValueA = 0;
        UpgradeValueS = 0;

        // 게임 중 상태 제거
        Gaming = false;

        // 게임 내 재화 초기화
        Dot = 80;
        NeedDot = 10;
        CoreCount = 1;
        UnicoreCount = 0;
        // 시작 재화 스킬이 찍혀있는 경우
        if (User.StatusStartDotLevel > 0) {
            Dot += Dot * User.StatusStartDotLevel * 0.05f;
        }

        // 라운드 초기화
        RoundNumber = 0;
        // 스테이지 초기화
        StageNumber = 1;

        /*
        // 현재 유닛 수 초기화
        for (int i = 0; i < UnitCounter.Length; i++) {
           UnitCounter[i] = 0;
        }
        */
        InitCounter();

        // 조합 초기화 
        CombineWaitingPos = 0;
        CombineTargetId = 0;

        // 난이도 초기화
        Difficulty = 1;

        // 속도 초기화
        SpeedRate = 1;
        SpeedUp = 1;

        // 일시정지 해제
        Pause = false;

        //CombinePool.Instance.CheckingFunction();

        UnityEngine.Random.InitState((int)Time.time);

        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    public void AdvUnitTimerStart() {
        int before_watched_unit_adv_time = Convert.ToInt32(PlayerPrefs.GetString("Adv_UnitChest_RemainTime") != "" ? PlayerPrefs.GetString("Adv_UnitChest_RemainTime") : 0);
        int need_can_watched_unit_adv_time = before_watched_unit_adv_time + 3000;
        int now_time = Convert.ToInt32(DateTime.Now.ToString("MMddHHmmss"));

        if (now_time >= need_can_watched_unit_adv_time) {
            adv_unit_chest_recive_time = 0;
        }
        else {
            adv_unit_chest_recive_time = need_can_watched_unit_adv_time - now_time;
            StartCoroutine(AdvUnitTimer());
        }
    }

    IEnumerator AdvUnitTimer() {
        while (adv_unit_chest_recive_time > 0) {
            adv_unit_chest_recive_time--;
            yield return unit_wait_time;
        }

        // 여기로 나오면 adv_unit == 0
        PlayerPrefs.SetString("Adv_UnitChest_RemainTime", "");
        PlayerPrefs.Save();
    }

    public void AdvStaTimerStart() {
        int before_watched_sta_adv_time = Convert.ToInt32(PlayerPrefs.GetString("Adv_StaChest_RemainTime") != "" ? PlayerPrefs.GetString("Adv_StaChest_RemainTime") : 0);
        int need_can_watched_sta_adv_time = before_watched_sta_adv_time + 3000;
        int now_time = Convert.ToInt32(DateTime.Now.ToString("MMddHHmmss"));

        if (now_time >= need_can_watched_sta_adv_time) {
            adv_sta_chest_recive_time = 0;
        }
        else {
            adv_sta_chest_recive_time = need_can_watched_sta_adv_time - now_time;
            StartCoroutine(AdvStaTimer());
        }
    }

    IEnumerator AdvStaTimer() {
        while (adv_sta_chest_recive_time > 0) {
            adv_sta_chest_recive_time--;
            yield return sta_wait_time;
        }

        PlayerPrefs.SetString("Adv_StaChest_RemainTime", "");
        PlayerPrefs.Save();
    }
}