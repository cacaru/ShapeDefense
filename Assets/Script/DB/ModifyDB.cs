using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;


public class ModifyDB : Singleton<ModifyDB>, IStatement
{
    private Queue<string> queries;
    private Queue<string> db_names;

    private bool unusing = true;

    private bool corouting = false;

    private STATE state;
    public STATE GetState() {
        return state;
    }

    private void Start() {
        queries = new Queue<string>();
        db_names = new Queue<string>();
        state = STATE.DB_WAIT;
    }

    IEnumerator CheckDB() {
        while(true) {
            if(queries.Count > 0 && unusing && datahub.DB_State == STATE.DB_CONNECTED) {
                UseDB(queries.Dequeue(), db_names.Dequeue());
            }
            yield return wff;

            if(queries.Count <= 0) {
                state = STATE.DB_WAIT;
                corouting = false;
                break;
            }
        }
    }


    public void ControllDB(string query, string db_name) {
        queries.Enqueue(query);
        db_names.Enqueue(db_name);

        if (!corouting) {
            state = STATE.DB_MODIFYING;
            StartCoroutine(CheckDB());
            corouting = true;
        }
    }

    private void UnUsing(bool b) {
        unusing = b;
    }

    private void UseDB(string query, string db_name) {
        
        UnUsing(false);
        IDbConnection dbConnection = new SqliteConnection(datahub.ConnectionString);
        dbConnection.Open();

        // query 적용
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = query;
        command.ExecuteNonQuery();

        IDataReader dataReader;
        switch (db_name) {
            // 수정된 강화도 및 유닛 정보 가져오기
            case "unit":
                command.CommandText = "SELECT * FROM unit";
                dataReader = command.ExecuteReader();

                Dictionary<int, Unit> temp_unit_dic = new();
               // ArrayList temp_unit_array = new();
               // Unit empty_unit = new() {
                //    Id = 0
                //};
                //temp_unit_array.Add(empty_unit);// 0을 빈 값으로 채우기
                int id = 0;
                while (dataReader.Read()) {
                    Unit unit = new() {
                        Id = dataReader.GetInt32(0),
                        Name = dataReader.GetString(1),
                        NickName = dataReader.GetString(2),
                        Attack = dataReader.GetInt32(3),
                        AttackSpeed = dataReader.GetInt32(4),
                        UpgradeFigures = dataReader.GetInt32(5),
                        UpgradeValue = dataReader.GetInt32(6),
                        MaxUpgradeValue = dataReader.GetInt32(7),
                        Type = dataReader.GetString(8),
                        Grade = dataReader.GetString(9),
                        NeedGold = dataReader.GetInt32(10),
                        Piece = dataReader.GetInt32(11),
                        NeedPiece = dataReader.GetInt32(12),
                    };
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
                    //temp_unit_array.Add(unit);
                    temp_unit_dic.Add(unit.Id, unit);
                }
                //datahub.Unit = temp_unit_array;
                datahub.Unit_dic = temp_unit_dic;
                dataReader.Close();
                break;
            // 적 정보 수정 -> 할일이 없음
            case "enemy":
                break;

            // 골드 및 조각 수급, 스테미나 회복
            case "user":
                // 적용된 쿼리의 결과 가져와 적용하기
                command.CommandText = "SELECT * FROM user";
                dataReader = command.ExecuteReader();
                //User temp_user = new();
                while (dataReader.Read()) {
                    datahub.User.Nickname = dataReader.GetString(0);
                    datahub.User.Dot = dataReader.GetInt32(1);
                    datahub.User.Level = dataReader.GetInt32(2);
                    datahub.User.Experience = dataReader.GetInt32(3);
                    datahub.User.NeedExperience = dataReader.GetInt32(4);
                    datahub.User.Stamina = dataReader.GetInt32(5);
                    datahub.User.MaxStamina = dataReader.GetInt32(6);
                    datahub.User.ChangeNickNameRecode = dataReader.GetInt32(7);
                    datahub.User.SkillPoint = dataReader.GetInt32(8);
                    datahub.User.MaxSkillPoint = dataReader.GetInt32(9);
                    datahub.User.StatusAttackLevel = dataReader.GetInt32(10);
                    datahub.User.StatusAttackSpeedLevel = dataReader.GetInt32(11);
                    datahub.User.StatusStartDotLevel = dataReader.GetInt32(12);
                    datahub.User.StatusGainDotLevel = dataReader.GetInt32(13);
                    datahub.User.StatusClearDotLevel = dataReader.GetInt32(14);
                }
                //datahub.User = temp_user;

                dataReader.Close();
                break;

            // 업적 횟수 및 완료보상 받기 이후 수정
            case "achieve":
                AchievementConnector.Instance.Connect(); // gameObject.GetComponent<AchievementConnector>().Connect();
                break;

            // 업적 횟수 및 완료보상 받기 이후 수정
            case "daily":
                DailyQuestConnector.Instance.Connect(); //gameObject.GetComponent<DailyQuestConnector>().Connect();

                break;

            // 업적 횟수 및 완료보상 받기 이후 수정
            case "weekly":
                WeeklyQuestConnector.Instance.Connect(); //gameObject.GetComponent<WeeklyQuestConnector>().Connect();

                break;

            // 설정 정보 다시 받아오기
            case "setting":
                // SETTING DATA 불러오기
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
                ColorSettingLoad.Instance.ColorSetLoad();
                break;
        }
        dbConnection.Close();
        UnUsing(true);
    }


}
