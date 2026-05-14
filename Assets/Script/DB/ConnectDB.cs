using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;
using System.Data.Common;

public class ConnectDB : Singleton<ConnectDB> {

    [SerializeField] private TMP_Text TestObj;
    [SerializeField] private TMP_Text FileObj;
    #region value
    private readonly User temp_user = new();
    //private readonly Unit empty_unit = new() { Id = 0 };
    private readonly Enemy empty_enemy = new() { Id = 0 };

    #endregion
    private IDbConnection dbConnection;
    private IDbCommand command;
    private IDataReader dataReader;
    private string path;

    private List<int> ori_ids = new();

    delegate void CONNECTDATABASEDELEGATE();
    CONNECTDATABASEDELEGATE connect_delegate;

    private void Start() {

        #region ids setting
        ori_ids.Add(1001);
        ori_ids.Add(1002);
        ori_ids.Add(1003);
        ori_ids.Add(1004);
        ori_ids.Add(1005);
        ori_ids.Add(1006);

        ori_ids.Add(2001);
        ori_ids.Add(2002);
        ori_ids.Add(2003);

        ori_ids.Add(3001);
        ori_ids.Add(3002);
        ori_ids.Add(3003);
        ori_ids.Add(3004);
        ori_ids.Add(3005);
        ori_ids.Add(3006);
        ori_ids.Add(3007);
        ori_ids.Add(3008);
        ori_ids.Add(3009);
        ori_ids.Add(3010);
        ori_ids.Add(3011);
        ori_ids.Add(3012);
        ori_ids.Add(3013);
        ori_ids.Add(3014);
        ori_ids.Add(3015);

        ori_ids.Add(4001);
        ori_ids.Add(4002);
        ori_ids.Add(4003);
        ori_ids.Add(4004);
        ori_ids.Add(4005);
        ori_ids.Add(4006);
        ori_ids.Add(4007);
        ori_ids.Add(4008);
        ori_ids.Add(4009);
        ori_ids.Add(4010);
        ori_ids.Add(4011);
        ori_ids.Add(4012);

        ori_ids.Add(5001);
        ori_ids.Add(5002);
        ori_ids.Add(5003);
        ori_ids.Add(5004);
        ori_ids.Add(5005);
        ori_ids.Add(5006);

        ori_ids.Add(6001);
        ori_ids.Add(6002);
        ori_ids.Add(6003);
        ori_ids.Add(6004);
        ori_ids.Add(6005);
        ori_ids.Add(6006);

        ori_ids.Add(301);
        ori_ids.Add(302);
        ori_ids.Add(303);
        ori_ids.Add(304);
        ori_ids.Add(305);
        ori_ids.Add(306);

        ori_ids.Add(401);
        ori_ids.Add(402);
        ori_ids.Add(403);
        ori_ids.Add(404);
        ori_ids.Add(405);
        ori_ids.Add(406);

        ori_ids.Add(501);
        ori_ids.Add(502);
        ori_ids.Add(503);
        #endregion

        path = datahub.ConnectionString;
        dbConnection = new SqliteConnection(path);

        connect_delegate += DB_CHECK;
        connect_delegate += Connect_UserDB;
        connect_delegate += Connect_UnitDB;
        
        connect_delegate += Connect_EnemyDB;
        connect_delegate += Connect_QuestDB;
        connect_delegate += Connect_SettingDB;
        connect_delegate += ConnectEnd;

        connect_delegate();
    }
    private void TEXT_unit() {
        string download_path;
        if (Application.platform == RuntimePlatform.Android) {
            // 안드로이드 Java 클래스를 사용하여 다운로드 폴더 경로 가져오기
            using (AndroidJavaClass envClass = new AndroidJavaClass("android.os.Environment")) {
                using (AndroidJavaObject downloadsFolder = envClass.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", envClass.GetStatic<string>("DIRECTORY_DOWNLOADS"))) {
                    download_path = downloadsFolder.Call<string>("getAbsolutePath");
                }
            }
        }
        else {
            // 다른 플랫폼에서는 지원하지 않음
            // alert 생성 고려
            //Debug.LogWarning("This method only works on Android.");
            download_path = null;
        }

        if(download_path != null) {
            // unit 내용을 text 파일화 하여 그곳에 작성하기
            var unit_list = new List<string>();
            for(int i = 0; i < datahub.Unit_Number; i++) {
                unit_list.Add(datahub.Unit_dic[datahub.Unit_Ids[i]].Show_Info());
            }

            //File.WriteAllLines(download_path + "/shape_defense_err_text.txt", unit_list);
            
        }
    }
    public void DB_CHECK() {
        datahub.DB_State = STATE.DB_CONNECTING;
        string default_path = Application.persistentDataPath + "/ShapeDefenseDB.db";

        TestObj.text = default_path;
        FileObj.text = Application.platform.ToString();

        #region file_create_or_copy
        if (Application.platform == RuntimePlatform.Android) {
            // 파일 검사
            try {
                if (!File.Exists(default_path)) {
                    FileObj.text = "In Checker";
                    WWW temp_load_db = new("jar:file://" + Application.dataPath + "!/assets/ShapeDefenseDB.db");
                    while (!temp_load_db.isDone) { };
                    File.WriteAllBytes(default_path, temp_load_db.bytes);
                }
            }
            catch (Exception err) {
                FileObj.text = err.Message;
                Debug.LogError(err.Message);
            }
        }
        else {
            // 파일 검사
            if (!File.Exists(default_path)) {
                File.Copy(Application.streamingAssetsPath + "/ShapeDefenseDB.db", default_path);
            }
        }
        #endregion

        //FileObj.text = path;
        TestObj.text = dbConnection.State.ToString();

        try {
            dbConnection.Open();
            TestObj.text = "db 연결";
            FileObj.text = path + " Connect ";
        }
        catch (Exception err) {
            Debug.LogError(err.Message);
            TestObj.text = err.Message;
            FileObj.text = path + " COnnect Err";
            // open이 안되면 이하는 어짜피 불가능
            return;
        }
        command = dbConnection.CreateCommand();

        // db가 비어있는지 확인하는 방법?
        command.CommandText = "SELECT count(*) FROM sqlite_master WHERE type = 'table'";
        dataReader = command.ExecuteReader();
        int table_count = 0;
        while (dataReader.Read()) {
            table_count = dataReader.GetInt32(0);
        }
        dataReader.Close();

        // 새로운 상태라면 테이블을 생성하고 새로운 데이터를 추가
        if (table_count <= 0) {
            Create_User();
            Create_Unit();
            Create_Transcend();
            Create_Enemy();
            Create_Achieve();
            Create_DailyQuest();
            Create_WeeklyQuest();
            Create_Item();
            Create_Setting();
        }

        LoadingProgressActive();
        TestObj.text = "DB생성 종료";

        dbConnection.Close();
    }

    private void Create_User() {
        command.CommandText = "CREATE TABLE user(nickname CHAR NOT NULL DEFAULT '플레이어', dot INTEGER NOT NULL DEFAULT 0, level INTEGER NOT NULL DEFAULT 1, experience INTEGER NOT NULL DEFAULT 0, max_exp INTEGER NOT NULL DEFAULT 100, stamina INTEGER NOT NULL DEFAULT 20, max_stamina INTEGER NOT NULL DEFAULT 20, nickname_change_recode INTEGER NOT NULL DEFAULT 0, skill_point INTEGER NOT NULL DEFAULT 0, max_skill_point INTEGER NOT NULL DEFAULT 0, status_attack_level INTEGER NOT NULL DEFAULT 0, status_attackspeed_level INTEGER NOT NULL DEFAULT 0, status_start_dot_level INTEGER NOT NULL DEFAULT 0, status_gain_dot_level INTEGER NOT NULL DEFAULT 0, status_clear_dot_level INTEGER NOT NULL DEFAULT 0)";
        command.ExecuteNonQuery();

        #region user
        command.CommandText = "INSERT INTO user VALUES ('플레이어', 0, 1, 0, 100, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0)";
        command.ExecuteNonQuery();
        #endregion
    }

    private void Create_Unit() {
        command.CommandText = "CREATE TABLE unit (id INT PRIMARY KEY NOT NULL UNIQUE, name CHAR NOT NULL, nick_name CHAR, attack INT NOT NULL DEFAULT 0, attack_speed INT NOT NULL DEFAULT 1, upgrade_figures INT NOT NULL DEFAULT 0, upgrade_value INT NOT NULL DEFAULT 0, upgrade_max_value INT NOT NULL DEFAULT 15, type CHAR NOT NULL DEFAULT 'I', grade CHAR NOT NULL, need_gold INT NOT NULL DEFAULT 100, piece INT NOT NULL DEFAULT 0, \tneed_piece INT NOT NULL DEFAULT 10, combine_function CHAR)";
        command.ExecuteNonQuery();

        #region unit
        //E
        command.CommandText = "INSERT INTO unit VALUES (1001, 'e_circle', 'E급원', 10, 2, 1, 0, 15, 'C', 'E', 100, 0, 10, '1001_2001,2001^2001_3001,2001^2002_3010,2001^2003_3011,2002^2002_3004,2002^2003_3012,2003^2003_3007')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (1002, 'e_triangle', 'E급세모', 6, 3, 1, 0, 15, 'T', 'E', 100, 0, 10, '1002_2002,2001^2001_3002,2001^2002_3010,2001^2003_3011,2002^2002_3005,2002^2003_3012,2003^2003_3008')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (1003, 'e_square', 'E급네모', 20, 1, 3, 0, 15, 'SQ', 'E', 100, 0, 10, '1003_2003,2001^2001_3003,2001^2002_3010,2001^2003_3011,2002^2002_3006,2002^2003_3012,2003^2003_3009')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (1004, 'e_star', '별조각', 0, 0, 0, 0, 0, 'ST', 'E', 0, 0, 0, '2001^2002^2003_3013,3010^3013^2003^304_4010,3011^3013^2002^304_4010,3012^3013^2001^304_4010,4010^4010^3013^404_5004,5004^4010^501_6004,5004^4010^502_6004,5004^4010^503_6004,1004^1004_1005,1004^1004_1006')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (1005, 'e_moon', '달조각', 0, 0, 0, 0, 0, 'M', 'E', 0, 0, 0, '2001^2002^2003_3014,2001^3012^3014^305_4011,2002^3011^3014^305_4011,2003^3010^3014^305_4011,4011^4011^3014^405_5005,4011^5005^501_6005,4011^5005^502_6005,4011^5005^503_6005,1005^1005_1004,1005^1005_1006')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (1006, 'e_sun', '해조각', 0, 0, 0, 0, 0, 'SU', 'E', 0, 0, 0, '2001^2002^2003_3015,2001^3012^3015^306_4012,2002^3011^3015^306_4012,2003^3010^3015^306_4012,4012^4012^3015^406_5006,4012^5006^501_6006,4012^5006^502_6006,4012^5006^503_6006,1006^1006_1004,1006^1006_1005')";
        command.ExecuteNonQuery();
        //D
        command.CommandText = "INSERT INTO unit VALUES (2001, 'd_circle', 'D급원', 17, 2, 2, 0, 15, 'C', 'D', 200, 0, 10, '2001^1001_3001,2001^1002_3002,2001^1003_3003,2002^1001_3010,2002^1002_3010,2002^1003_3010,2003^1001_3011,2003^1002_3011,2003^1003_3011,2002^2003^1004_3013,2002^2003^1005_3014,2002^2003^1006_3015,3010^3001^301_4001,3010^3002^301_4002,3010^3003^301_4003,3011^3001^301_4001,3011^3002^301_4002,3011^3003^301_4003,3012^3001^301_4001,3012^3002^301_4002,3012^3003^301_4003,3012^3013^304^1004_4010,3012^3014^305^1005_4011,3012^3015^306^1006_4012')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (2002, 'd_triangle', 'D급세모', 11, 3, 1, 0, 15, 'T', 'D', 200, 0, 10, '2001^1001_3010,2001^1002_3010,2001^1003_3010,2002^1001_3004,2002^1002_3005,2002^1003_3006,2003^1001_3012,2003^1002_3012,2003^1003_3012,2001^2003^1004_3013,2001^2003^1005_3014,2001^2003^1006_3015,3010^3004^302_4004,3010^3005^302_4005,3010^3006^302_4006,3011^3004^302_4004,3011^3005^302_4005,3011^3006^302_4006,3011^3013^304^1004_4010,3011^3014^305^1005_4011,3011^3015^306^1006_4012,3012^3004^302_4004,3012^3005^302_4005,3012^3006^302_4006')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (2003, 'd_square', 'D급네모', 35, 1, 5, 0, 15, 'SQ', 'D', 200, 0, 10, '2001^1001_3011,2001^1002_3011,2001^1003_3011,2002^1001_3012,2002^1002_3012,2002^1003_3012,2003^1001_3007,2003^1002_3008,2003^1003_3009,2001^2002^1004_3013,2001^2002^1005_3014,2001^2002^1006_3015,3010^3007^303_4007,3010^3008^303_4008,3010^3009^303_4009,3010^3013^304^1004_4010,3010^3014^305^1005_4011,3010^3015^306^1006_4012,3011^3007^303_4007,3011^3008^303_4008,3011^3009^303_4009,3012^3007^303_4007,3012^3008^303_4008,3012^3009^303_4009')";
        command.ExecuteNonQuery();
        //C
        command.CommandText = "INSERT INTO unit VALUES (3001, 'c_circle_1', 'C급쌍원', 23, 3, 3, 0, 15, 'C', 'C', 400, 0, 10, '3010^2001^301_4001,3011^2001^301_4001,3012^2001^301_4001,4001^4001^401_5001,4002^4002^401_5001,4003^4003^401_5001')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (3002, 'c_circle_2', 'C급삼원', 23, 3, 3, 0, 15, 'C', 'C', 400, 0, 10, '3010^2001^301_4002,3011^2001^301_4002,3012^2001^301_4002')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (3003, 'c_circle_3', 'C급사원', 23, 3, 3, 0, 15, 'C', 'C', 400, 0, 10, '3010^2001^301_4003,3011^2001^301_4003,3012^2001^301_4003')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (3004, 'c_triangle_1', 'C급원각', 17, 4, 2, 0, 15, 'T', 'C', 400, 0, 10, '3010^2002^302_4004,3011^2002^302_4004,3012^2002^302_4004')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (3005, 'c_triangle_2', 'C급쌍각', 14, 5, 2, 0, 15, 'T', 'C', 400, 0, 10, '3010^2002^302_4005,3011^2002^302_4005,3012^2002^302_4005,4004^4004^402_5002,4005^4005^402_5002,4006^4006^402_5002')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (3006, 'c_triangle_3', 'C급사각', 17, 4, 2, 0, 15, 'T', 'C', 400, 0, 10, '3010^2002^302_4006,3011^2002^302_4006,3012^2002^302_4006')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (3007, 'c_square_1', 'C급원사', 35, 2, 5, 0, 15, 'SQ', 'C', 400, 0, 10, '3010^2003^303_4007,3011^2003^303_4007,3012^2003^303_4007')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (3008, 'c_square_2', 'C급삼사', 35, 2, 5, 0, 15, 'SQ', 'C', 400, 0, 10, '3010^2003^303_4008,3011^2003^303_4008,3012^2003^303_4008')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (3009, 'c_square_3', 'C급쌍사', 70, 1, 11, 0, 15, 'SQ', 'C', 400, 0, 10, '3010^2003^303_4009,3011^2003^303_4009,3012^2003^303_4009,4007^4007^403_5003,4008^4008^403_5003,4009^4009^403_5003')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (3010, 'c_amalgation_1', '원삼융합체', 23, 3, 3, 0, 15, 'A', 'C', 400, 0, 10, '3001^2001^301_4001,3002^2001^301_4002,3003^2001^301_4003,3004^2002^302_4004,3005^2002^302_4005,3006^2002^302_4006,3007^2003^303_4007,3008^2003^303_4008,3009^2003^303_4009,3013^2003^304^1004_4010,3014^2003^305^1005_4011,3015^2003^306^1006_4012')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (3011, 'c_amalgation_2', '원사융합체', 23, 3, 3, 0, 15, 'A', 'C', 400, 0, 10, '3001^2001^301_4001,3002^2001^301_4002,3003^2001^301_4003,3004^2002^302_4004,3005^2002^302_4005,3006^2002^302_4006,3007^2003^303_4007,3008^2003^303_4008,3009^2003^303_4009,3013^2002^304^1004_4010,3014^2002^305^1005_4011,3015^2002^306^1006_4012')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (3012, 'c_amalgation_3', '사삼융합체', 23, 3, 3, 0, 15, 'A', 'C', 400, 0, 10, '3001^2001^301_4001,3002^2001^301_4002,3003^2001^301_4003,3004^2002^302_4004,3005^2002^302_4005,3006^2002^302_4006,3007^2003^303_4007,3008^2003^303_4008,3009^2003^303_4009,3013^2001^304^1004_4010,3014^2001^305^1005_4011,3015^2001^306^1006_4012')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (3013, 'c_star', 'C급별', 18, 6, 3, 0, 15, 'ST', 'C', 400, 0, 10, '3010^2003^304^1004_4010,3011^2002^304^1004_4010,3012^2001^304^1004_4010,4010^4010^404^1004_5004')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (3014, 'c_moon', 'C급달', 18, 6, 3, 0, 15, 'M', 'C', 400, 0, 10, '3010^2003^305^1005_4011,3011^2002^305^1005_4011,3012^2001^305^1005_4011,4011^4011^405^1005_5005')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (3015, 'c_sun', 'C급해', 18, 6, 3, 0, 15, 'SU', 'C', 400, 0, 10, '3010^2003^306^1006_4012,3011^2002^306^1006_4012,3012^2001^306^1006_4012,4012^4012^406^1006_5006')";
        command.ExecuteNonQuery();
        //B
        command.CommandText = "INSERT INTO unit VALUES (4001, 'b_circle_1', 'B급쌍사원', 43, 4, 7, 0, 15, 'C', 'B', 800, 0, 10, '4001^3001^401_5001,5001^501_6001,5001^502_6001,5001^503_6001')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (4002, 'b_circle_2', 'B급각쌍원', 43, 4, 7, 0, 15, 'C', 'B', 800, 0, 10, '4002^3001^401_5001')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (4003, 'b_circle_3', 'B급역사원', 43, 4, 7, 0, 15, 'C', 'B', 800, 0, 10, '4003^3001^401_5001')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (4004, 'b_triangle_1', 'B급역원각', 29, 6, 4, 0, 15, 'T', 'B', 800, 0, 10, '4004^3005^402_5002')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (4005, 'b_triangle_2', 'B급사쌍각', 35, 5, 5, 0, 15, 'T', 'B', 800, 0, 10, '4005^3005^402_5002,5002^501_6002,5002^502_6002,5002^503_6002')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (4006, 'b_triangle_3', 'B급역쌍각', 35, 5, 5, 0, 15, 'T', 'B', 800, 0, 10, '4006^3005^402_5002')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (4007, 'b_square_1', 'B급원이사', 58, 3, 9, 0, 15, 'SQ', 'B', 800, 0, 10, '4007^3009^403_5003')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (4008, 'b_square_2', 'B급각이사', 58, 3, 9, 0, 15, 'SQ', 'B', 800, 0, 10, '4008^3009^403_5003')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (4009, 'b_square_3', 'B급응사', 87, 2, 14, 0, 15, 'SQ', 'B', 800, 0, 10, '4009^3009^403_5003,5003^501_6003,5003^502_6003,5003^503_6003')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (4010, 'b_star', 'B급별', 38, 7, 6, 0, 15, 'ST', 'B', 800, 0, 10, '4010^3013^404^1004_5004,5004^501^1004_6004,5004^502^1004_6004,5004^503^1004_6004')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (4011, 'b_moon', 'B급달', 38, 7, 6, 0, 15, 'M', 'B', 800, 0, 10, '4011^3014^405^1005_5005,5005^501^1005_6005,5005^502^1005_6005,5005^503^1005_6005')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (4012, 'b_sun', 'B급해', 38, 7, 6, 0, 15, 'SU', 'B', 800, 0, 10, '4012^3015^406_5006,5006^501^1006_6006,5006^502^1006_6006,5006^503^1006_6006')";
        command.ExecuteNonQuery();
        //A
        command.CommandText = "INSERT INTO unit VALUES (5001, 'a_circle', 'A급원', 105, 4, 17, 0, 15, 'C', 'A', 1500, 0, 10, '4001^501_6001,4001^502_6001,4001^503_6001')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (5002, 'a_triangle', 'A급세모', 84, 5, 14, 0, 15, 'T', 'A', 1500, 0, 10, '4005^501_6002,4005^502_6002,4005^503_6002')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (5003, 'a_square', 'A급네모', 140, 3, 23, 0, 15, 'SQ', 'A', 1500, 0, 10, '4009^501_6003,4009^502_6003,4009^503_6003')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (5004, 'a_star', 'A급별', 105, 6, 17, 0, 15, 'ST', 'A', 1500, 0, 10, '4010^501^1004_6004,4010^502^1004_6004,4010^503^1004_6004')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (5005, 'a_moon', 'A급달', 105, 6, 17, 0, 15, 'M', 'A', 1500, 0, 10, '4011^501^1005_6005,4011^502^1005_6005,4011^503^1005_6005')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (5006, 'a_sun', 'A급해', 105, 6, 17, 0, 15, 'SU', 'A', 1500, 0, 10, '4012^501^1006_6006,4012^502^1006_6006,4012^503^1006_6006')";
        command.ExecuteNonQuery();
        //S
        command.CommandText = "INSERT INTO unit VALUES (6001, 's_circle', 'S급원', 200, 5, 33, 0, 15, 'C', 'S', 2500, 0, 10, '0')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (6002, 's_triangle', 'S급세모', 166, 6, 27, 0, 0, 'T', 'S', 2500, 0, 10, '0')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (6003, 's_square', 'S급네모', 250, 4, 41, 0, 0, 'SQ', 'S', 2500, 0, 10, '0')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (6004, 's_star', 'S급별', 214, 7, 35, 0, 0, 'ST', 'S', 2500, 0, 10, '0')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (6005, 's_moon', 'S급달', 214, 7, 35, 0, 0, 'M', 'S', 2500, 0, 10, '0')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (6006, 's_sun', 'S급해', 214, 7, 35, 0, 0, 'SU', 'S', 2500, 0, 10, '0')";
        command.ExecuteNonQuery();

        // Item
        command.CommandText = "INSERT INTO unit VALUES (301, 'c_circle_0', '원의결정', 0, 0, 0, 0, 0, 'I', 'IC', 0, 0, '0', '2001^3010^3001_4001,2001^3010^3002_4002,2001^3010^3003_4003,2001^3011^3001_4001,2001^3011^3002_4002,2001^3011^3003_4003,2001^3012^3001_4001,2001^3012^3002_4002,2001^3012^3003_4003')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (302, 'c_triangle_0', '세모의결정', 0, 0, 0, 0, 0, 'I', 'IC', 0, 0, '0', '2002^3010^3004_4004,2002^3010^3005_4005,2002^3010^3006_4006,2002^3011^3004_4004,2002^3011^3005_4005,2002^3011^3006_4006,2002^3012^3004_4004,2002^3012^3005_4005,2002^3012^3006_4006')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (303, 'c_square_0', '네모의결정', 0, 0, 0, 0, 0, 'I', 'IC', 0, 0, '0', '2003^3010^3007_4007,2003^3010^3008_4008,2003^3010^3009_4009,2003^3011^3007_4007,2003^3011^3008_4008,2003^3011^3009_4009,2003^3012^3007_4007,2003^3012^3008_4008,2003^3012^3009_4009')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (304, 'c_star_0', '별의결정', 0, 0, 0, 0, 0, 'I', 'IC', 0, 0, '0', '2001^3012^3013^1004_4010,2002^3011^3013^1004_4010,2003^3010^3013^1004_4010,305^404^405_501,306^404^406_503')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (305, 'c_moon_0', '달의결정', 0, 0, 0, 0, 0, 'I', 'IC', 0, 0, '0', '2001^3012^3014^1005_4011,2002^3011^3014^1005_4011,2003^3010^3014^1005_4011,304^404^405_501,306^405^406_502')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (306, 'c_sun_0', '달의결정', 0, 0, 0, 0, 0, 'I', 'IC', 0, 0, '0', '2001^3012^3015^1006_4012,2002^3011^3015^1006_4012,2003^3010^3015^1006_4012,304^406^404_503,305^406^405_502')";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO unit VALUES (401, 'b_circle_0', '원의수정', 0, 0, 0, 0, 0, 'I', 'IB', 0, 0, '0', '3001^4001^4001_5001,3001^4002^4002_5001,3001^4003^4003_5001')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (402, 'b_triangle_0', '세모의수정', 0, 0, 0, 0, 0, 'I', 'IB', 0, 0, '0', '3005^4004^4004_5002,3005^4005^4005_5002,3005^4006^4006_5002')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (403, 'b_square_0', '네모의수정', 0, 0, 0, 0, 0, 'I', 'IB', 0, 0, '0', '3009^4007^4007_5003,3009^4008^4008_5003,3009^4009^4009_5003')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (404, 'b_star_0', '별의수정', 0, 0, 0, 0, 0, 'I', 'IB', 0, 0, '0', '3013^4010^4010^1004_5004,304^305^405_501,304^306^406_503')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (405, 'b_moon_0', '달의수정', 0, 0, 0, 0, 0, 'I', 'IB', 0, 0, '0', '3014^4011^4011^1005_5005,305^304^404_501,305^306^406_502')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (406, 'b_sun_0', '해의수정', 0, 0, 0, 0, 0, 'I', 'IB', 0, 0, '0', '3015^4012^4012^1006_5006,306^405^305_502,306^404^304_503')";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO unit VALUES (501, 'a_harmony_of_star_moon', '달과별의조화', 0, 0, 0, 0, 0, 'I', 'IA', 0, 0, '0', '5001^4001_6001,5002^4005_6002,5003^4009_6003,5004^4010^1004_6004,5005^4011^1005_6005,5006^4012^1006_6006')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (502, 'a_harmony_of_sun_moon', '해와달의조화', 0, 0, 0, 0, 0, 'I', 'IA', 0, 0, '0', '5001^4001_6001,5002^4005_6002,5003^4009_6003,5004^4010^1004_6004,5005^4011^1005_6005,5006^4012^1006_6006')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO unit VALUES (503, 'a_harmony_of_sun_star', '해와별의조화', 0, 0, 0, 0, 0, 'I', 'IA', 0, 0, '0', '5001^4001_6001,5002^4005_6002,5003^4009_6003,5004^4010^1004_6004,5005^4011^1005_6005,5006^4012^1006_6006')";
        command.ExecuteNonQuery();
        #endregion
    }

    // 24 12 24 초월 테이블 추가
    private void Create_Transcend() {
        command.CommandText = "CREATE TABLE transcend (id INT PRIMARY KEY NOT NULL, possible INTEGER NOT NULL DEFAULT 0, value INTEGER NOT NULL DEFAULT 0, piece INTEGER NOT NULL DEFAULT 150, probabillity CHAR DEFAULT '80,65,50,35,20')";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO user VALUES ('1001', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('1002', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('1003', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO user VALUES ('2001', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('2002', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('2003', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO user VALUES ('3001', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('3002', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('3003', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('3004', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('3005', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('3006', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('3007', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('3008', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('3009', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('3010', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('3011', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('3012', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('3013', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('3014', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('3015', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO user VALUES ('4001', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('4002', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('4003', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('4004', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('4005', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('4006', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('4007', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('4008', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('4009', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('4010', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('4011', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('4012', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO user VALUES ('5001', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('5002', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('5003', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('5004', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('5005', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('5006', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO user VALUES ('6001', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('6002', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('6003', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('6004', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('6005', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO user VALUES ('6006', 0, 0, 150, '80,65,50,35,20')";
        command.ExecuteNonQuery();
    }

    private void Create_Enemy() {
        command.CommandText = "CREATE TABLE enemy(id INT PRIMARY KEY NOT NULL UNIQUE, name CHAR NOT NULL, health INT NOT NULL, upgrade_value INT NOT NULL, speed INT NOT NULL DEFAULT 1)";
        command.ExecuteNonQuery();

        #region enemy
        command.CommandText = "INSERT INTO enemy VALUES (1, 'f_clover_1', 0, 40, 1)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (2, 'f_clover_2', 0, 40, 2)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (3, 'f_heart_1', 0, 40, 1)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (4, 'f_heart_2', 0, 40, 2)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (5, 'f_spade_1', 0, 40, 3)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (6, 'f_spade_2', 0, 40, 4)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (7, 'e_clover_1', 0, 42, 1)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (8, 'e_clover_2', 0, 42, 2)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (9, 'e_heart_1', 0, 42, 1)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (10, 'e_heart_2', 0, 42, 2)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (11, 'e_spade_1', 0, 50, 3)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (12, 'e_spade_2', 0, 50, 4)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (13, 'd_clover_1', 0, 50, 1)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (14, 'd_clover_2', 0, 50, 2)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (15, 'd_heart_1', 0, 50, 1)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (16, 'd_heart_2', 0, 50, 2)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (17, 'd_spade_1', 0, 50, 3)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (18, 'd_spade_2', 0, 50, 4)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (19, 'c_clover_1', 0, 50, 1)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (20, 'c_clover_2', 0, 50, 2)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (21, 'c_heart_1', 0, 50, 1)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (22, 'c_heart_2', 0, 50, 2)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (23, 'c_spade_1', 0, 50, 3)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (24, 'c_spade_2', 0, 50, 4)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (101, 'boss_1', 1000, 2000, 5)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (102, 'boss_2', 1000, 2000, 5)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (103, 'boss_3', 1000, 2000, 5)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO enemy VALUES (104, 'boss_4', 1000, 2000, 5)";
        command.ExecuteNonQuery();
        #endregion
    }

    private void Create_Achieve() {

        command.CommandText = "CREATE TABLE achievement('id' INT NOT NULL UNIQUE, 'name' CHAR NOT NULL, 'reward' CHAR NOT NULL, 'checker' INT NOT NULL DEFAULT 0, 'counter' INT NOT NULL DEFAULT 0, PRIMARY KEY(id))";
        command.ExecuteNonQuery();

        #region achievement
        command.CommandText = "INSERT INTO achievement VALUES (1, '첫 10라운드 클리어', '150_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (2, '첫 20라운드 클리어', '200_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (3, '첫 30라운드 클리어', '250_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (4, '첫 40라운드 클리어', '300_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (5, '첫 50라운드 클리어', '350_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (6, '첫 60라운드 클리어', '400_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (7, '첫 70라운드 클리어', '450_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (8, '첫 80라운드 클리어', '500_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (9, '첫 90라운드 클리어', '600_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (10, '첫 100라운드 클리어', '700_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (11, '첫 110라운드 클리어', '800_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (12, '첫 120라운드 클리어', '1000_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (13, '첫 난이도 1 클리어', '40_e', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (14, '첫 난이도 2 클리어', '40_e+20_d', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (15, '첫 난이도 3 클리어', '40_e+20_d+10_c', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (16, '첫 난이도 4 클리어', '40_e+30_d+20_c+10_b', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (17, '첫 난이도 5 클리어', '40_e+40_d+30_c+20_b+10_a', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (18, '첫 난이도 6 클리어', '40_e+40_d+40_c+30_b+20_a+10_s', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (19, '첫 난이도 7 클리어', '50_e+40_d+40_c+40_b+30_a+20_s', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (20, '첫 난이도 8 클리어', '50_e+50_d+40_c+40_b+40_a+30_s', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (21, '첫 난이도 9 클리어', '50_e+50_d+50_c+40_b+40_a+40_s', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (22, '첫 난이도 10 클리어', '60_e+50_d+50_c+50_b+40_a+40_s', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (23, '첫 난이도 11 클리어', '60_e+60_d+50_c+50_b+50_a+40_s', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (24, '첫 난이도 12 클리어', '60_e+60_d+60_c+50_b+50_a+50_s', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (25, '첫 난이도 13 클리어', '60_e+60_d+60_c+60_b+50_a+50_s', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (26, '첫 난이도 14 클리어', '60_e+60_d+60_c+60_b+60_a+50_s', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (27, '첫 난이도 15 클리어', '60_e+60_d+60_c+60_b+60_a+60_s', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (28, '10라운드 보스 n회 반복 클리어', '10.n_gold+10.n', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (29, '20라운드 보스 n회 반복 클리어', '10.n_gold+10.n', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (30, '30라운드 보스 n회 반복 클리어', '10.n_gold+10.n', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (31, '40라운드 보스 n회 반복 클리어', '10.n_gold+10.n', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (32, '50라운드 보스 n회 반복 클리어', '10.n_gold+10.n', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (33, '60라운드 보스 n회 반복 클리어', '10.n_gold+10.n', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (34, '70라운드 보스 n회 반복 클리어', '10.n_gold+10.n', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (35, '80라운드 보스 n회 반복 클리어', '10.n_gold+10.n', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (36, '90라운드 보스 n회 반복 클리어', '10.n_gold+10.n', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (37, '100라운드 보스 n회 반복 클리어', '10.n_gold+10.n', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (38, '110라운드 보스 n회 반복 클리어', '10.n_gold+10.n', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (39, '120라운드 보스 n회 반복 클리어', '10.n_gold+10.n', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (40, '강화 n회 완료', '100.n_gold+10.n', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (41, '유닛 생성 n회 완료', '10.n_gold+100.n', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (42, '1000점 가지고 있기', '1000_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (43, '3000점 가지고 있기', '3000_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (44, '5000점 가지고 있기', '5000_gold', 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO achievement VALUES (45, 'n점 소모하기', '1.n_gold+1000.n', 0, 0)";
        command.ExecuteNonQuery();
        #endregion
    }

    private void Create_DailyQuest() {
        command.CommandText = "CREATE TABLE dailyquest(id INTEGER NOT NULL,name CHAR NOT NULL,reward CHAR NOT NULL DEFAULT 100,checker INTEGER NOT NULL DEFAULT 0,counter INTEGER NOT NULL DEFAULT 0,requestcounter INTEGER NOT NULL DEFAULT 0, PRIMARY KEY(id))";
        command.ExecuteNonQuery();

        #region daily quest
        command.CommandText = "INSERT INTO dailyquest VALUES (1, '출석하기', '100', 0, 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO dailyquest VALUES (2, '스태미나 20 소모하기', '100', 0, 0, 20)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO dailyquest VALUES (3, '상자 1회 오픈하기', '100', 0, 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO dailyquest VALUES (4, '100라운드 클리어하기', '200', 0, 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO dailyquest VALUES (5, 'A급 1회 조합하기', '200', 0, 0, 0)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO dailyquest VALUES (6, '모든 퀘스트 완료하기', '300', 0, 0, 5)";
        command.ExecuteNonQuery();
        #endregion
    }

    private void Create_WeeklyQuest() {
        command.CommandText = "CREATE TABLE weeklyquest (id INTEGER NOT NULL, name CHAR NOT NULL, reward CHAR NOT NULL DEFAULT '100_gold', checker INTEGER NOT NULL DEFAULT 0, counter INTEGER NOT NULL DEFAULT 0, requestconter INTEGER NOT NULL DEFAULT 0, PRIMARY KEY(id))";
        command.ExecuteNonQuery();

        #region weekly quest
        command.CommandText = "INSERT INTO weeklyquest VALUES (1, '3일 출석하기', '1000_gold', 0, 0, 3)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO weeklyquest VALUES (2, 'S급 2회 조합하기', '1000_gold', 0, 0, 2)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO weeklyquest VALUES (3, '도합 500라운드 통과하기', '1000_gold', 0, 0, 500)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO weeklyquest VALUES (4, '200 스태미나 소모하기', '1000_gold', 0, 0, 200)";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO weeklyquest VALUES (5, 'A급 이상 상자 1회 오픈하기', '1000_gold', 0, 0, 0)";
        command.ExecuteNonQuery();
        #endregion
    }

    private void Create_Item() {
        command.CommandText = "CREATE TABLE item(id INT PRIMARY KEY NOT NULL UNIQUE, name CHAR NOT NULL, percentage CHAR NOT NULL DEFAULT 0)";
        command.ExecuteNonQuery();

        #region item
        command.CommandText = "INSERT INTO item VALUES (1, 'gray_chest', '50+40+5+3+2')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO item VALUES (2, 'green_chest', '40+30+15+10+5')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO item VALUES (3, 'blue_chest', '35+25+15+15+10')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO item VALUES (4, 'purple_chest', '30+20+20+20+10')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO item VALUES (5, 'red_chest', '20+10+30+25+15')";
        command.ExecuteNonQuery();
        #endregion
    }

    private void Create_Setting() {
        command.CommandText = "CREATE TABLE setting(id INT PRIMARY KEY NOT NULL UNIQUE, name CHAR NOT NULL, value INT NOT NULL)";
        command.ExecuteNonQuery();

        #region setting
        command.CommandText = "INSERT INTO setting VALUES (1, 'background_top', '155,194,230')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO setting VALUES (2, 'background_bottom', '255,255,255')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO setting VALUES (3, 'icon_background', '255,246,165')";
        command.ExecuteNonQuery();
        command.CommandText = "INSERT INTO setting VALUES (4, 'icon', '0,0,0')";
        command.ExecuteNonQuery();
        #endregion
    }

    public void Connect_UserDB() {
        /*************************************************************************************************************/
        #region 유저 데이터 받기
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT * FROM user";
        dataReader = command.ExecuteReader();
        //TestObj.text = "유저 데이터 받기 시작";
        
        while (dataReader.Read()) {
            temp_user.Nickname = dataReader.GetString(0);
            temp_user.Dot = dataReader.GetInt32(1);
            temp_user.Level = dataReader.GetInt32(2);
            temp_user.Experience = dataReader.GetInt32(3);
            temp_user.NeedExperience = dataReader.GetInt32(4);
            temp_user.Stamina = dataReader.GetInt32(5);
            temp_user.MaxStamina = dataReader.GetInt32(6);
            temp_user.ChangeNickNameRecode = dataReader.GetInt32(7);
            temp_user.SkillPoint = dataReader.GetInt32(8);
            temp_user.MaxSkillPoint = dataReader.GetInt32(9);
            temp_user.StatusAttackLevel = dataReader.GetInt32(10);
            temp_user.StatusAttackSpeedLevel = dataReader.GetInt32(11);
            temp_user.StatusStartDotLevel = dataReader.GetInt32(12);
            temp_user.StatusGainDotLevel = dataReader.GetInt32(13);
            temp_user.StatusClearDotLevel = dataReader.GetInt32(14);
        }
        //Debug.Log(temp_user.Nickname + " " + temp_user.Dot + " " + temp_user.Level + " " + temp_user.Experience + " " + temp_user.Stamina + " " + temp_user.MaxStamina);
        datahub.User = temp_user;
        dataReader.Close();
        datahub.User.UserCorrectCheck();
        LoadingProgressActive();

        TestObj.text = "유저 데이터 받기 완료";
        dbConnection.Close();
        #endregion
    }

    public void Connect_UnitDB() {
        #region 유닛 데이터 받기
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        // unit table의 데이터를 unit 에 옮기기
        command.CommandText = "SELECT * FROM unit";
        dataReader = command.ExecuteReader();
        bool recent_db_count_checker = false;
        TestObj.text = "유닛 데이터 읽기 시작";
        int id = 0;
        
        //ArrayList array = new() { empty_unit };
        Dictionary<int, Unit> dic = new();

        while (dataReader.Read()) {
            if (dataReader.FieldCount >= 14) {
                recent_db_count_checker = true;
            }

            Unit unit = new();
            // 최신 버전일경우
            if (recent_db_count_checker) {
                unit.Id = dataReader.GetInt32(0);
                unit.Name = dataReader.GetString(1);
                unit.NickName = dataReader.GetString(2);
                unit.Attack = dataReader.GetInt32(3);
                unit.AttackSpeed = dataReader.GetInt32(4);
                unit.UpgradeFigures = dataReader.GetInt32(5);
                unit.UpgradeValue = dataReader.GetInt32(6);
                unit.MaxUpgradeValue = dataReader.GetInt32(7);
                unit.Type = dataReader.GetString(8);
                unit.Grade = dataReader.GetString(9);
                unit.NeedGold = dataReader.GetInt32(10);
                unit.Piece = dataReader.GetInt32(11);
                unit.NeedPiece = dataReader.GetInt32(12);
                // 조합식 정제
                string tmp = dataReader.GetString(13);
                if (tmp != "0") {
                    string[] parts = tmp.Split(",");
                    int max = parts.Length;
                    List<CombineFunction> list = new(max + 1);
                    foreach (string part in parts) {
                        CombineFunction tmp_cmp = new();
                        string[] one_part = part.Split("_");
                        // 조합 결과
                        tmp_cmp.Result = int.Parse(one_part[1]);
                        // 조합에 나 이외에 추가로 필요한 유닛 번호
                        string[] details = one_part[0].Split("^");
                        int count = details.Length;
                        switch (count) {
                            case 1:
                                tmp_cmp.A = int.Parse(details[0]);
                                tmp_cmp.NeedCount = 1;
                                break;
                            case 2:
                                tmp_cmp.A = int.Parse(details[0]);
                                tmp_cmp.B = int.Parse(details[1]);
                                tmp_cmp.NeedCount = 2;
                                break;
                            case 3:
                                tmp_cmp.A = int.Parse(details[0]);
                                tmp_cmp.B = int.Parse(details[1]);
                                tmp_cmp.C = int.Parse(details[2]);
                                tmp_cmp.NeedCount = 3;
                                break;
                            case 4:
                                tmp_cmp.A = int.Parse(details[0]);
                                tmp_cmp.B = int.Parse(details[1]);
                                tmp_cmp.C = int.Parse(details[2]);
                                tmp_cmp.D = int.Parse(details[3]);
                                tmp_cmp.NeedCount = 4;
                                break;
                        }
                        // 조합식을 어레이에 저장
                        tmp_cmp.Id = id;
                        id++;
                        list.Add(tmp_cmp);
                    }
                    // 정제된 arraylist를 list로 저장
                    unit.CombFucntion = list;
                }
                else {
                    unit.CombFucntion = new List<CombineFunction>(0);
                }
            }
            // 구 버전일 경우
            else {
                // 현 db의 정보를 신규 db로 이관해야함
                // 현 db의 정보를 임시로 저장해두고
                // 빠진 정보(신규에 있었어야할 정보)를 수동으로 넣고
                // 현 db를 드랍하고 신규 db를 create
                // 신규 db에 현 db를 업데이트하고 종료
                unit.Id = dataReader.GetInt32(0);
                unit.Name = dataReader.GetString(1);
                unit.NickName = dataReader.GetString(2);
                unit.Attack = dataReader.GetInt32(3);
                unit.AttackSpeed = dataReader.GetInt32(4);
                unit.UpgradeFigures = dataReader.GetInt32(5);
                unit.UpgradeValue = dataReader.GetInt32(6);
                unit.MaxUpgradeValue = dataReader.GetInt32(7);
                unit.Grade = dataReader.GetString(8);
                unit.NeedGold = dataReader.GetInt32(9);
                unit.Piece = dataReader.GetInt32(10);
                unit.NeedPiece = dataReader.GetInt32(11);
                // 조합식 정제
                string tmp = dataReader.GetString(12);
                if (tmp != "0") {
                    string[] parts = tmp.Split(",");
                    int max = parts.Length;
                    List<CombineFunction> list = new(max + 1);
                    foreach (string part in parts) {
                        CombineFunction tmp_cmp = new();
                        string[] one_part = part.Split("_");
                        // 조합 결과
                        tmp_cmp.Result = int.Parse(one_part[1]);
                        // 조합에 나 이외에 추가로 필요한 유닛 번호
                        string[] details = one_part[0].Split("^");
                        int count = details.Length;
                        switch (count) {
                            case 1:
                                tmp_cmp.A = int.Parse(details[0]);
                                tmp_cmp.NeedCount = 1;
                                break;
                            case 2:
                                tmp_cmp.A = int.Parse(details[0]);
                                tmp_cmp.B = int.Parse(details[1]);
                                tmp_cmp.NeedCount = 2;
                                break;
                            case 3:
                                tmp_cmp.A = int.Parse(details[0]);
                                tmp_cmp.B = int.Parse(details[1]);
                                tmp_cmp.C = int.Parse(details[2]);
                                tmp_cmp.NeedCount = 3;
                                break;
                            case 4:
                                tmp_cmp.A = int.Parse(details[0]);
                                tmp_cmp.B = int.Parse(details[1]);
                                tmp_cmp.C = int.Parse(details[2]);
                                tmp_cmp.D = int.Parse(details[3]);
                                tmp_cmp.NeedCount = 4;
                                break;
                        }
                        // 조합식을 어레이에 저장
                        tmp_cmp.Id = id;
                        id++;
                        list.Add(tmp_cmp);
                    }
                    // 정제된 arraylist를 list로 저장
                    unit.CombFucntion = list;
                }
                else {
                    unit.CombFucntion = new List<CombineFunction>(0);
                }
                unit.Type = unit.Id switch {
                    3010 or 3011 or 3012 => "A",
                    1001 or 2001 or 3001 or 3002 or 3003 or 4001 or 4002 or 4003 or 5001 or 6001 => "C",
                    1002 or 2002 or 3004 or 3005 or 3006 or 4004 or 4005 or 4006 or 5002 or 6002 => "T",
                    1003 or 2003 or 3007 or 3008 or 3009 or 4007 or 4008 or 4009 or 5003 or 6003 => "SQ",
                    1004 or 3013 or 4010 or 5004 or 6004 => "ST",
                    1005 or 3014 or 4011 or 5005 or 6005 => "M",
                    1006 or 3015 or 4012 or 5006 or 6006 => "SU",
                    _ => "I",
                };
            }

            dic.Add(unit.Id, unit);
            //array.Add(unit);
        }

        //datahub.Unit = array;
        datahub.Unit_dic = dic;

        //datahub.UnitCounter = new int[array.Count + 1];
        // dic 생성
        datahub.UnitCounter = new();
        datahub.Unit_Number = datahub.Unit_dic.Count;
        datahub.Unit_Ids = new List<int>(datahub.Unit_dic.Keys);
        for (int i = 0; i < datahub.Unit_Number; i++) {
            datahub.UnitCounter[datahub.Unit_Ids[i]] = 0;
        }
        dataReader.Close();

        // 재점검
        // id 체크
        // id가 이상하면 원상복구를 진행
        if (datahub.Unit_Ids[0] != 1001) {
            Unit temp_value;
            for(int i = 0; i < datahub.Unit_Number; i++) {
                temp_value = datahub.Unit_dic[datahub.Unit_Ids[i]];
                datahub.Unit_dic.Remove(datahub.Unit_Ids[i]);
                datahub.Unit_dic.Add(ori_ids[i], temp_value);
            }
        }

        // 현 db drop 이후 새 db 작성 
        if (recent_db_count_checker) {
            command.CommandText = "DROP TABLE unit";
            command.ExecuteNonQuery();

            Create_Unit();

            string query;
            // 새로 생긴 db에 upgradevalue, piece를 업데이트함
            for (int i = 0; i < datahub.Unit_Number; i++) {
                var now_unit = datahub.Unit_dic[datahub.Unit_Ids[i]];


                query = UtilityHub.query_builder.Append("UPDATE unit SET piece=")
                                                .Append(now_unit.Piece)
                                                .Append(", upgrade_value=")
                                                .Append(now_unit.UpgradeValue)
                                                .Append(" WHERE id = ")
                                                .Append(now_unit.Id)
                                                .ToString();
                command.CommandText = query;
                command.ExecuteNonQuery();
                UtilityHub.query_builder.Clear();
            }
        }

        LoadingProgressActive();
        dbConnection.Close();
        #endregion
        TestObj.text = "유닛 데이터 받기 완료";

    }

    public void Connect_EnemyDB() {
        #region Enemy 데이터 받기
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        // enemy 데이터 옮기기
        command.CommandText = "SELECT * FROM enemy";
        dataReader = command.ExecuteReader();
        ArrayList array = new() { empty_enemy };
        // enemy 0번에 빈값 넣기      

        while (dataReader.Read()) {
            Enemy enemy = new() {
                Id = dataReader.GetInt32(0),
                Name = dataReader.GetString(1),
                Health = dataReader.GetInt32(2),
                UpgradeValue = dataReader.GetInt32(3),
                Speed = dataReader.GetInt32(4)
            };
            //Debug.Log(enemy.Id + "," + enemy.Name + "," + enemy.Health + "," + enemy.UpgradeValue + "," + enemy.Speed);
            array.Add(enemy);
        }
        datahub.Enemy = array;
        dataReader.Close();

        if(datahub.Enemy.Count != 29) {
            // enemy table을 변경해야함
            command.CommandText = "DROP TABLE enemy";
            command.ExecuteNonQuery();
            Create_Enemy();
            Connect_EnemyDB();
            return;
        }

        LoadingProgressActive();

        dbConnection.Close();
        #endregion
        //TestObj.text = "적 데이터 받기";
    }
    public void Connect_QuestDB() {
        #region 업적 받기
        try {
            // achievement 불러오기
            Achieve_Connector.Connect();
        }
        catch (Exception err) {
            Debug.LogError(err.Message);
            //TestObj.text = err.Message;
        }
        #endregion

        #region 일일퀘스트 받기
        try {
            // achievement 불러오기
            Daily_Connector.Connect();
        }
        catch (Exception err) {
            Debug.LogError(err.Message);
            //TestObj.text = err.Message;
        }
        #endregion

        // 일일 퀘스트 재시작 체크
        GameObject.Find("DBObject").GetComponent<AttendanceCheck>().enabled = true;

        #region 주간퀘스트 받기
        try {
            // achievement 불러오기
            Weekly_Connector.Connect();
        }
        catch (Exception err) {
            Debug.LogError(err.Message);
            //TestObj.text = err.Message;
        }
        #endregion
    }
    
    public void Connect_SettingDB(){
        #region 설정 받기
        dbConnection.Open();
        command = dbConnection.CreateCommand();
        command.CommandText = "SELECT * FROM setting";
        dataReader = command.ExecuteReader();

        //ArrayList temp_set_array = new();
        while (dataReader.Read()) {
            PlayerPrefs.SetString(dataReader.GetString(1), dataReader.GetString(2));
            PlayerPrefs.Save();
            /*
            SetOption option = new() {
                Id = dataReader.GetInt32(0),
                Name = dataReader.GetString(1),
                Value = dataReader.GetString(2),
            };
            temp_set_array.Add(option);
            */
        }
        //datahub.Setoption = temp_set_array;
        dataReader.Close();
        LoadingProgressActive();
        #endregion
        dbConnection.Close();
        //TestObj.text = "세팅 데이터 받기";
        ColorSettingLoad.Instance.ColorSetLoad();
    }

    private void ConnectEnd() {
        TestObj.text = "업적 데이터 받기";
        TestObj.text = "데이터 연동 완료";
        datahub.DBConnectEnd = true;

        FirstLoadingObserver.Instance.LoadProgress();

        // unit 확인 후 이상있으면 text 파일로 추출해버리기
        for(int i = 0; i < datahub.Unit_Number; i++) {
            if (datahub.Unit_dic[datahub.Unit_Ids[i]].Id != ori_ids[i]) {
                TEXT_unit();
                break;
            }
        }
    }

    private void LoadingProgressActive() {
        if (!datahub.DBConnectEnd) {
            FirstLoadingObserver.Instance.LoadProgress();
        }
    }
}