using System.Collections;
using System.Data;
using UnityEngine;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;
public class DailyQuestConnector : Singleton<DailyQuestConnector> , IQuestConnector
{
    private readonly ArrayList temp_dailyquest_list = new();

    // 일일 퀘스트 초기화
    public void Reset() {
        string query = UtilityHub.query_builder.Append("UPDATE dailyquest SET checker=0, counter=0")
                                               .ToString();
        modifyDB.ControllDB(query, "daily");
        UtilityHub.query_builder.Clear();
    }
    
    public void Connect() {
        InitArray();
        datahub.dbConnection.Open();
        IDbCommand command = datahub.dbConnection.CreateCommand();

        // 업적 데이터 받아오기
        command.CommandText = "SELECT * FROM dailyquest";
        IDataReader dataReader = command.ExecuteReader();

        // 빈 값을 하나 넣기
        DailyQuest temp_0 = new(){
            Id = 0
        };
        temp_dailyquest_list.Add(temp_0);

        while (dataReader.Read()) {
            DailyQuest daily = new() {
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
            daily.CanRecive = request > 0 ? counter >= request && checker == 0 : counter > 0 && checker == 0;

            daily.Counter = counter;
            daily.Checker = checker;
            daily.RequestCounter = request;
            daily.RewardVal = reward_val;
            daily.RewardList = reward_list;

            temp_dailyquest_list.Add(daily);
        }

        // 최종 가공된 업적 자료를 저장
        if (datahub.DailyConnectEnd) {
            int size = datahub.DailyQuest.Count;
            for (int i = 0; i < size; i++) {
                datahub.DailyQuest[i] = temp_dailyquest_list[i];
            }
        }
        else {
            datahub.DailyQuest = temp_dailyquest_list;
        }

        dataReader.Close();
        datahub.dbConnection.Close();
        datahub.DailyConnectEnd = true;

        if (!datahub.Gaming && datahub.NowScene == SCENE_NUMBER.LOBBY) {
            // 업적 갱신
            GameObject.Find("QuestController").GetComponent<DailyQuestControll>().PageReset();
            GameObject.Find("QuestController").GetComponent<DailyQuestControll>().Show();
        }
    }

    public void InitArray() {
        int size = temp_dailyquest_list.Count;
        for (int i = 0; i < size; i++) {
            temp_dailyquest_list.RemoveAt(0);
        }
    }
}
