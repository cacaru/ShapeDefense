using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;

public class DataHub : MonoBehaviour
{
    // �����ν��Ͻ�ȭ
    public static DataHub _instance;

    // db ���� ����
    private readonly string dbname = "/ShapeDefenseDB.db";
    private readonly string connectionString = "URI=file:" + Application.streamingAssetsPath;
    void Awake() {
        if (_instance == null) {
            _instance = this;
        }
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(_instance);
    }

    #region Data

    // �� ������ ���� ����
    private User user;
    // ���� ������ ���� ����
    private ArrayList unit;
    // �� ������ ���� ����
    private ArrayList enemy;
    
    // Unit â���� �� ������ �Ѿ �� �� id ����
    private int unit_id;
    // ù ���������� �÷��̾� ������ ������ ����
    private bool user_connect_end = false;

    /// <summary>
    /// ���ӿ��� ���� ����
    ///  ������ �����ų� ������ �Ϸ�Ǹ� �ʱ�ȭ
    /// </summary>
    private int stage_number = 1;                       // ���� �� ��ȣ
    private int difficulty = 1;                         // ���̵�
    private int stage_field_number = 45;                // �ʿ� ���� ��ȯ���� �ʵ� �ִ� ��ȣ
    private ArrayList stage_field;                      // ��ȯ ���� �ʵ带 ������ �� ����
    private int[] unit_counter = new int[49];           // ���� ��ȯ�� ���� ���� �����ص� ����
    private int combine_waiting_pos;                    // ���� ������ ��� ������ ��ġ ��ȣ�� ����
    private int combine_target_id;                      // ������ ��ǥ ������ ��ȣ ����
    private int round_number = 0;                       // ���� ����
    private int end_round = 60;                         // ���� ����

    // ��ȭ��
    private int upgrade_no_value = 0;
    private int upgrade_ad_value = 0;
    private int upgrade_lu_value = 0;
    private int upgrade_li_value = 0;
    private int upgrade_cu_value = 0;
    private readonly int max_upgrade = 10;

    // ��ȯ��ȭ (��)
    private int dot = 80;

    // ���������� ���� �� ���忡 ������ �� ���� ���� ���� ���� �� ����;
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
            // value�� ���� enemy_counter�� max_enemy_counter �� ����
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