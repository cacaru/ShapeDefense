using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections;
using UnityEngine.Rendering.LookDev;
using TMPro.EditorUtilities;
using UnityEditor;
using System.Collections.Generic;

public class ConnectDB : MonoBehaviour {

    // ���� ������ ���� DataHub�� �����ϱ�
    private DataHub datahub;

    private void Start() {
        // datahub �����ϱ�
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
        IDbConnection dbConnection = new SqliteConnection(datahub.ConnectionString + datahub.DbName);
        dbConnection.Open();
        IDbCommand command = dbConnection.CreateCommand();

        // ���� ������ �޾ƿ���
        command.CommandText = "SELECT * FROM user";
        IDataReader dataReader = command.ExecuteReader();

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
        datahub.UserConnectEnd = true;

        // unit table�� �����͸� unit �� �ű��
        command.CommandText = "SELECT * FROM unit ORDER BY id";
        dataReader = command.ExecuteReader();

        ArrayList temp_unit_array = new();
        Unit empty_unit = new();
        empty_unit.Id = 0;
        temp_unit_array.Add(empty_unit);// 0�� �� ������ ä���
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
            // ���ս� ����
            string tmp = dataReader.GetString(12);
            if (tmp != "0" ) {
                string[] parts = tmp.Split(",");
                int max = parts.Length;
                List<CombineFunction> list = new List<CombineFunction>(max+1);
                foreach(string part in parts) {
                    CombineFunction tmp_cmp = new CombineFunction();
                    string[] one_part = part.Split("_");
                    // ���� ���
                    tmp_cmp.Result = int.Parse(one_part[1]);
                    // ���տ� �� �̿ܿ� �߰��� �ʿ��� ���� ��ȣ
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
                    // ���ս��� ��̿� ����
                    list.Add(tmp_cmp);
                }

                // ������ arraylist�� list�� ����
                unit.CombFucntion = list;
            }
            //Debug.Log(unit.Id + "," + unit.Name + "," + unit.NickName + "," + unit.Attack + "," + unit.AttackSpeed + "," + unit.UpgradeFigures +"," +
            //    unit.UpgradeValue + "," + unit.MaxUpgradeValue + "," + unit.Grade + "," + unit.NeedGold + "," + unit.Piece + "," + unit.NeedPiece);            // Unit ��ü�� �Űܴ��
            temp_unit_array.Add(unit);
        }
        datahub.Unit = temp_unit_array;
        dataReader.Close();

        // enemy ������ �ű��
        command.CommandText = "SELECT * FROM enemy";
        dataReader = command.ExecuteReader();

        ArrayList temp_enemy_array = new();
        // enemy 0���� �� �ֱ�
        Enemy empty_enemy = new() {
            Id = 0
        };
        temp_enemy_array.Add(empty_enemy);
        while (dataReader.Read()) {
            Enemy enemy = new() {
                Id = dataReader.GetInt32(0),
                Name = dataReader.GetString(1),
                Health = dataReader.GetInt32(2),
                UpgradeValue = dataReader.GetInt32(3),
                Speed = dataReader.GetInt32(4)
            };
            //Debug.Log(enemy.Id + "," + enemy.Name + "," + enemy.Health + "," + enemy.UpgradeValue + "," + enemy.Speed);
            temp_enemy_array.Add(enemy);
        }
        datahub.Enemy = temp_enemy_array;
        dataReader.Close();

        dbConnection.Close();
        gameObject.GetComponent<ConnectDB>().enabled = false;
    }

}