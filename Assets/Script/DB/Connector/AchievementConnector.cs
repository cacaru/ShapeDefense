using ShapeDefenseSpace;
using System.Collections;
using System.Data;
using UnityEngine;
using static ShapeDefenseSpace.GameData;
/// <summary>
/// 업적 상태를 불러오는 함수
/// </summary>
/// 업적은 해당 scene으로 넘어갈때마다 갱신되서 불러야하므로 커넥트를 분리하여 구성
public class AchievementConnector : Singleton<AchievementConnector>, IQuestConnector
{
    private readonly ArrayList temp_achievement_list = new();

    public void Reset() {
        // do nothing
    }

    // 업적 불러오기
    public void Connect() {
        datahub.AchieveConnectEnd = false;

        InitArray();
        datahub.dbConnection.Open();
        IDbCommand command = datahub.dbConnection.CreateCommand();

        // 업적 데이터 받아오기
        command.CommandText = "SELECT * FROM achievement";
        IDataReader dataReader = command.ExecuteReader();
        // 0 번에 빈 값 넣기
        Achievement empty = new() { Id=0 };
        temp_achievement_list.Add(empty);

        while (dataReader.Read()) {
            Achievement achievement = new() {
                Id = dataReader.GetInt32(0),
                Name = dataReader.GetString(1),
            };
            /*
             * +로 다수의 보상 구분
             * _로 reward 내용과 내용에 따른 보상 액 구분
             * . 으로 반복에 따른 배수 구분
             *   -> .이 있으면 .앞의 값 * checker를 reward val에 넣기
             * , 로 반복 달성 필요량의 증가분 구분
             *   -> , 가 있으면 매치값으로 구분 repeat를 true로 설정하고 
             *   -> reward_val[checker]에 해당하는 값이 필요 reward 값으로 들어가고
             * */
            string check_reward_string = dataReader.GetString(2);
            string[] rewards = check_reward_string.Split('+');
            int checker = dataReader.GetInt32(3);
            int counter = dataReader.GetInt32(4);
            int end_time = 1;
            int endless_value = 0;

            ArrayList reward_list = new() { "" }; 
            ArrayList reward_val = new() { 0 }; 
            ArrayList repeat_request = new();
            // reward 가공
            // ,가 있으면 반복 보상이므로 여러 보상과는 다르게 가공
            if (check_reward_string.Contains(".") || check_reward_string.Contains(",")) {
                achievement.Repeat = true;
                // reward[0] == 보상 
                // reward[1] == checker에 따라 요구될 조건 

                // 보상 확인
                string[] temp_reward = rewards[0].Split("_");
                string[] reward_coefficient = temp_reward[0].Split(".");
                // 대부분 골드
                reward_list[0] = temp_reward[1];
                if (checker == 0) {
                    reward_val[0] = int.Parse(reward_coefficient[0]);
                }
                else {
                    reward_val[0] = int.Parse(reward_coefficient[0]) * checker;
                }

                // 조건 확인
                // ,로 이어져있다면 arraylist에 순차적으로 담기
                // 모든 반복퀘는 무한반복퀘
                if (rewards[1].Contains(".")) {
                    int reward_request_coefficient = int.Parse(rewards[1].Split(".")[0]);

                    end_time = -1;
                    endless_value = reward_request_coefficient;

                }
            }
            // ,가 없으면 +로 연결된 다수의 보상 or 단일 보상
            else {
                // 다수
                if (rewards.Length >= 2) {
                    // 각 리워드를 순서에 맞게 저장 -> 1번부터 (0번은 더미값)
                    int rewards_size = rewards.Length;
                    for (int i = 0; i < rewards_size; i++) {
                        string[] single_reward = rewards[i].Split("_");
                        reward_val.Add(int.Parse(single_reward[0]));
                        reward_list.Add(single_reward[1]);
                    }
                }
                // 단일
                else {
                    string[] single_reward = rewards[0].Split("_");
                    reward_val.Add(int.Parse(single_reward[0]));
                    reward_list.Add(single_reward[1]);
                }
            }

            // 보상을 받을 수 있는지 확인
            // 반복 보상
            if (achievement.Repeat) {
                // 무한반복퀘 / 일반 반복퀘를 나눔
                int for_reward = endless_value * (checker + 1);
                
                //Debug.Log(achievement.Name + " >> for_reward : " + for_reward + "   // counter : " + counter);
                achievement.CanRecive = counter >= for_reward && counter != 0;
            }
            //단일보상
            else {
                // counter 만 1 이상이라면 받을 수 있어야함
                // checker가 0 이 아니면 이미 완료된 업적이므로 받을 수 없어야함
                achievement.CanRecive = counter > 0 && checker == 0;
            }

            // 보상 가공완료
            achievement.Counter = counter;
            achievement.Checker = checker;
            achievement.RewardList = reward_list;
            achievement.RewardVal = reward_val;
            achievement.RepeatRewardRequest = repeat_request;
            achievement.EndlessValue = endless_value;
            achievement.EndTime = end_time;

            // 가공된 자료를 리스트에 저장
            temp_achievement_list.Add(achievement);
        }
        
        // 최종 가공된 업적 자료를 저장
        // 이미 있었다면 값 복사
        // 없으면 새로 대입

        if (datahub.AchieveConnectEnd) {
            int size = datahub.Achievement.Count;
            for(int i = 0; i < size; i++) {
                datahub.Achievement[i] = temp_achievement_list[i];
            }
        }
        else {
            datahub.Achievement = temp_achievement_list;
        }
        dataReader.Close();
        datahub.dbConnection.Close();
        datahub.AchieveConnectEnd = true;

        if (!datahub.Gaming && datahub.NowScene == SCENE_NUMBER.LOBBY) {
            // 업적 갱신
            GameObject.Find("QuestController").GetComponent<AchievementControll>().PageReset();
            GameObject.Find("QuestController").GetComponent<AchievementControll>().Show();
        }
    }

    public void InitArray() {
        int size = temp_achievement_list.Count;
        for(int i = 0; i < size; i++) {
            temp_achievement_list.RemoveAt(0);
        }
    }
}
