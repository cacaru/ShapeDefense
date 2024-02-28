using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ModifyDB : MonoBehaviour
{
    private DataHub datahub;

    private void Start() {
        datahub = GetComponent<DataHub>();
    }

    public void ControllDB(string query, string db_name) {
        IDbConnection dbConnection = new SqliteConnection(datahub.ConnectionString + datahub.DbName);
        dbConnection.Open();

        // query 적용
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = query;
        command.ExecuteNonQuery();

        IDataReader dataReader;
        switch (db_name) {
            // 강화도 저장
            case "unit":
                command.CommandText = "SELECT * FROM unit ORDER BY id";
                dataReader = command.ExecuteReader();

                ArrayList temp_unit_array = new();
                Unit empty_unit = new();
                empty_unit.Id = 0;
                temp_unit_array.Add(empty_unit);// 0을 빈 값으로 채우기
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
                        Grade = dataReader.GetString(8),
                        NeedGold = dataReader.GetInt32(9),
                        Piece = dataReader.GetInt32(10),
                        NeedPiece = dataReader.GetInt32(11)
                    };
                    // 조합식 정제
                    string tmp = dataReader.GetString(12);
                    if (tmp != "0") {
                        string[] parts = tmp.Split(",");
                        int max = parts.Length;
                        List<CombineFunction> list = new List<CombineFunction>(max + 1);
                        foreach (string part in parts) {
                            CombineFunction tmp_cmp = new CombineFunction();
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
                            }
                            // 조합식을 어레이에 저장
                            list.Add(tmp_cmp);
                        }

                        // 정제된 arraylist를 list로 저장
                        unit.CombFucntion = list;
                    }
                    //Debug.Log(unit.Id + "," + unit.Name + "," + unit.NickName + "," + unit.Attack + "," + unit.AttackSpeed + "," + unit.UpgradeFigures +"," +
                    //    unit.UpgradeValue + "," + unit.MaxUpgradeValue + "," + unit.Grade + "," + unit.NeedGold + "," + unit.Piece + "," + unit.NeedPiece);            // Unit 객체에 옮겨담기
                    temp_unit_array.Add(unit);
                }
                datahub.Unit = temp_unit_array;
                dataReader.Close();
                break;
            // 적 정보 수정 -> 할일이 없음
            case "enemy":
                break;

            // 골드 및 조각 수급
            case "user":
                // 적용된 쿼리의 결과 가져와 적용하기
                command.CommandText = "SELECT * FROM user";
                dataReader = command.ExecuteReader();
                User temp_user = new();
                while (dataReader.Read()) {
                    temp_user.Nickname = dataReader.GetString(0);
                    temp_user.Dot = dataReader.GetInt32(1);
                    temp_user.Level = dataReader.GetInt32(2);
                    temp_user.Experience = dataReader.GetFloat(3);
                    temp_user.Stamina = dataReader.GetInt32(4);
                    temp_user.MaxStamina = dataReader.GetInt32(5);
                }
                //Debug.Log(temp_user.Nickname + " " + temp_user.Dot + " " + temp_user.Level + " " + temp_user.Experience + " " + temp_user.Stamina + " " + temp_user.MaxStamina);
                datahub.User = temp_user;

                dataReader.Close();
                break;
        }
    }

}
