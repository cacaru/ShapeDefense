using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;

public class DataHub : MonoBehaviour
{
    // 단일인스턴스화
    public static DataHub _instance;

    // db 관련 정보
    private readonly string dbname = "/ShapeDefenseDB.db";
    private readonly string connectionString = "URI=file:" + Application.streamingAssetsPath;
    void Awake() {
        if (_instance == null) {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(_instance);
    }

    #region Data

    // 내 정보를 담을 변수
    private User user;
    // 유닛 정보를 담을 변수
    private ArrayList unit;
    // 적 정보를 담을 변수
    private ArrayList enemy;
    
    // Unit 창에서 상세 정보로 넘어갈 떄 줄 id 변수
    private int unit_id;
    // 첫 페이지에서 플레이어 정보를 갱신할 변수
    private bool user_connect_end = false;

    /// <summary>
    /// 게임에서 사용될 변수
    ///  게임을 나가거나 게임이 완료되면 초기화
    /// </summary>
    private int stage_number = 1;                       // 현재 맵 번호
    private int difficulty = 1;                         // 난이도
    private int stage_field_number = 45;                // 맵에 따른 소환가능 필드 최대 번호
    private ArrayList stage_field;                      // 소환 가능 필드를 저장해 둘 변수
    private int[] unit_counter = new int[49];           // 현재 소환한 유닛 수를 저장해둘 변수
    private int combine_waiting_pos;                    // 현재 조합할 재료 유닛의 위치 번호를 저장
    private int combine_target_id;                      // 조합할 목표 유닛의 번호 저장
    private int round_number = 0;                       // 현재 라운드
    private int end_round = 60;                         // 최종 라운드

    // 강화도
    private int upgrade_no_value = 0;
    private int upgrade_ad_value = 0;
    private int upgrade_lu_value = 0;
    private int upgrade_li_value = 0;
    private int upgrade_cu_value = 0;
    private readonly int max_upgrade = 10;

    // 소환재화 (점)
    private int dot = 80;

    // 스테이지에 따라 한 라운드에 등장할 몹 수와 게임 오버 기준 수 저장;
    private int enemy_counter = 40;
    private int max_enemy_counter = 120;
    
    #endregion

    #region property
    public User User { get { return user; } set { user = value; } }
    public ArrayList Unit { get { return unit; } set { unit = value; } }
    public ArrayList Enemy { get { return enemy; } set {  enemy = value; } }
    public int Unitid { get {  return unit_id; } set { unit_id = value; } }
    public bool UserConnectEnd { get {  return user_connect_end; } set { user_connect_end = value; } }
    public string DbName { get { return dbname; } }
    public string ConnectionString { get {  return connectionString; } }

    public int UpgradeNoValue { get { return upgrade_no_value; } set { upgrade_no_value = value; } }
    public int UpgradeAdValue { get { return upgrade_ad_value; } set {  upgrade_ad_value = value; } }
    public int UpgradeLuValue { get { return upgrade_lu_value;} set { upgrade_lu_value = value; } }
    public int UpgradeLiValue { get { return upgrade_li_value; } set {  upgrade_li_value = value; } }
    public int UpgradeCuValue { get { return upgrade_cu_value;} set {  upgrade_cu_value = value; } }
    public int MaxUpgrade { get { return max_upgrade; } }
    public int Dot { get { return dot; }  set { dot = value; } }
    public int StageNumber { 
        get { return stage_number; } 
        set { 
            stage_number = value; 
            // value에 따라 enemy_counter와 max_enemy_counter 값 수정
            switch(stage_number) {
                case 1:
                    this.enemy_counter = 40;
                    this.max_enemy_counter = 120;
                    this.end_round = 60;
                    break;
                case 2:
                    this.enemy_counter = 42;
                    this.max_enemy_counter = 120;
                    this.end_round = 75;
                    break;
                case 3:
                    this.enemy_counter = 45;
                    this.max_enemy_counter = 120;
                    this.end_round = 90;
                    break;
                default:
                    this.enemy_counter = 40;
                    this.max_enemy_counter = 120;
                    this.end_round = 60;
                    break;
            }
        } 
    }
    public int RoundNumber { get { return round_number; } set { round_number = value; } }
    public int EndRound { get { return end_round; } }
    public int EnemyCounter { get { return enemy_counter; } }
    public int MaxEnemyCounter { get { return max_enemy_counter; } }
    public int StageFieldNumber { get { return stage_field_number; } set { stage_field_number = value; } }
    public ArrayList StageField { get { return stage_field; } set { stage_field = value; } }
    public int[] UnitCounter { get { return unit_counter; } set { unit_counter = value; } } 
    public int CombineWaitingPos { get { return combine_waiting_pos; } set { combine_waiting_pos = value; } }
    public int CombineTargetId { get { return combine_target_id; } set { combine_target_id = value; } }
    public int Difficulty { get { return difficulty; } set {  difficulty = value; } }

    #endregion

}