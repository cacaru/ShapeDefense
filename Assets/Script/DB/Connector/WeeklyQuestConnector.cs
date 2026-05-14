using ShapeDefenseSpace;
using System.Collections;
using System.Data;
using UnityEngine;
using static ShapeDefenseSpace.GameData;

public class WeeklyQuestConnector : Singleton<WeeklyQuestConnector> ,IQuestConnector 
{

    // 0 번에 뭐 있어도 상관 없음
    private readonly ArrayList temp_weeklyquest_list = new();

    // 주간 퀘스트 초기화
    public void Reset() {
        string query = UtilityHub.query_builder.Append("UPDATE weeklyquest SET checker=")
                                               .Append(0)
                                               .Append(", counter=")
                                               .Append(0)
                                               .ToString();
        modifyDB.ControllDB(query, "weekly");
        UtilityHub.query_builder.Clear();
    }

    public void Connect() {
        InitArray();
        datahub.dbConnection.Open();
        IDbCommand command = datahub.dbConnection.CreateCommand();

        // 업적 데이터 받아오기
        command.CommandText = "SELECT * FROM weeklyquest";
        IDataReader dataReader = command.ExecuteReader();

        WeeklyQuest tmp_weekly_obj = new() {
            Id = 0,
        };
        temp_weeklyquest_list.Add(tmp_weekly_obj);

        while (dataReader.Read()) {
            WeeklyQuest weekly = new() {
                Id = dataReader.GetInt32(0),
                Name = dataReader.GetString(1),
            };
            string[] reward = dataReader.GetString(2).Split("+");
            int checker = dataReader.GetInt32(3);
            int counter = dataReader.GetInt32(4);
            int request = dataReader.GetInt32(5);

            // 보상 정리
            // 일일 퀘스트는 골드와 경험치만 보상으로 주어짐
            ArrayList reward_list = new() { "", "" };
            ArrayList reward_val = new() { 0, 0 };

            string[] tmp = reward[0].Split("_");
            reward_list[0] = tmp[1];
            reward_val[0] = int.Parse(tmp[0]);

            tmp = reward[1].Split("_");
            reward_list[1] = tmp[1];
            reward_val[1] = int.Parse(tmp[0]);

            // 보상 요구치가 0이 아니면 counter가 request와 동일해야 받을 수 있음
            weekly.CanRecive = request > 0 ? counter >= request && checker == 0 : counter > 0 && checker == 0;

            weekly.Counter = counter;
            weekly.Checker = checker;
            weekly.RequestCounter = request;
            weekly.RewardVal = reward_val;
            weekly.RewardList = reward_list;

            temp_weeklyquest_list.Add(weekly);
        }

        // 최종 가공된 업적 자료를 저장
        if (datahub.WeeklyConnectEnd) {
            int size = datahub.WeeklyQuest.Count;
            for (int i = 0; i < size; i++) {
                datahub.WeeklyQuest[i] = temp_weeklyquest_list[i];
            }
        }
        else {
            datahub.WeeklyQuest = temp_weeklyquest_list;
        }
        dataReader.Close();
        datahub.dbConnection.Close();
        datahub.WeeklyConnectEnd = true;

        if (!datahub.Gaming && datahub.NowScene == SCENE_NUMBER.LOBBY) {
            // 업적 갱신
            GameObject.Find("QuestController").GetComponent<WeeklyQuestControll>().PageReset();
            GameObject.Find("QuestController").GetComponent<WeeklyQuestControll>().Show();
        }
    }

    public void InitArray() {
        int size = temp_weeklyquest_list.Count;
        for (int i = 0; i < size; i++) {
            temp_weeklyquest_list.RemoveAt(0);
        }
    }
}
