
using ShapeDefenseSpace;
using UnityEngine;
using static ShapeDefenseSpace.GameData;

/// <summary>
/// 업적들에 관련된 행위가 일어났을 떄 각 업적의 counter를 증가시킬 함수
/// </summary>

public class AchievementObserver : Singleton<AchievementObserver>
{
    private string query;

    // 유닛 생성
    public void UnitCreate() {
        // 소환 횟수 업적 증가
        Achievement achieve = datahub.Achievement[41] as Achievement;
        achieve.Counter++;
        // db modify
        query = UtilityHub.query_builder.Append("UPDATE achievement SET counter=")
                                               .Append(achieve.Counter)
                                               .Append(" WHERE id=41")
                                               .ToString();
        modifyDB.ControllDB(query, "achieve");
        UtilityHub.query_builder.Clear();
    }

    // 보스 반복 처치
    public void KillBoss(int round) {
        int key = round / 10;
        Achievement achieve = key switch {
            1 => datahub.Achievement[28] as Achievement,
            2 => datahub.Achievement[29] as Achievement,
            3 => datahub.Achievement[30] as Achievement,
            4 => datahub.Achievement[31] as Achievement,
            5 => datahub.Achievement[32] as Achievement,
            6 => datahub.Achievement[33] as Achievement,
            7 => datahub.Achievement[34] as Achievement,
            8 => datahub.Achievement[35] as Achievement,
            9 => datahub.Achievement[36] as Achievement,
            10 => datahub.Achievement[37] as Achievement,
            11 => datahub.Achievement[38] as Achievement,
            12 => datahub.Achievement[39] as Achievement,
            _ => null,
        };
        if(achieve != null ) {
            achieve.Counter += 1;
            query = UtilityHub.query_builder.Append("UPDATE achievement SET counter=")
                                                   .Append(achieve.Counter)
                                                   .Append(" WHERE id=")
                                                   .Append(achieve.Id)
                                                   .ToString();
            modifyDB.ControllDB(query, "achieve");
            UtilityHub.query_builder.Clear();
        }

        //첫 클리어 업적 확인
        Achievement first_checker = key switch {
            1 => datahub.Achievement[1] as Achievement,
            2 => datahub.Achievement[2] as Achievement,
            3 => datahub.Achievement[3] as Achievement,
            4 => datahub.Achievement[4] as Achievement,
            5 => datahub.Achievement[5] as Achievement,
            6 => datahub.Achievement[6] as Achievement,
            7 => datahub.Achievement[7] as Achievement,
            8 => datahub.Achievement[8] as Achievement,
            9 => datahub.Achievement[9] as Achievement,
            10 => datahub.Achievement[10] as Achievement,
            11 => datahub.Achievement[11] as Achievement,
            12 => datahub.Achievement[12] as Achievement,
            _ => null
        };

        if(first_checker != null && first_checker.Counter == 0 ) {
            first_checker.Counter += 1;
            query = UtilityHub.query_builder.Append("UPDATE achievement SET counter=")
                                                    .Append(first_checker.Counter)
                                                    .Append(" WHERE id=")
                                                    .Append(first_checker.Id)
                                                    .ToString();
            modifyDB.ControllDB(query, "achieve");
            UtilityHub.query_builder.Clear();
        }
    }

    // 첫 난이도 클리어 업적
    public void FirstDifficultyClearCheck() {
        Achievement first_checker = datahub.Difficulty switch {
            1 => datahub.Achievement[13] as Achievement,
            2 => datahub.Achievement[14] as Achievement,
            3 => datahub.Achievement[15] as Achievement,
            4 => datahub.Achievement[16] as Achievement,
            5 => datahub.Achievement[17] as Achievement,
            6 => datahub.Achievement[18] as Achievement,
            7 => datahub.Achievement[19] as Achievement,
            8 => datahub.Achievement[20] as Achievement,
            9 => datahub.Achievement[21] as Achievement,
            10 => datahub.Achievement[22] as Achievement,
            11 => datahub.Achievement[23] as Achievement,
            12 => datahub.Achievement[24] as Achievement,
            13 => datahub.Achievement[25] as Achievement,
            14 => datahub.Achievement[26] as Achievement,
            15 => datahub.Achievement[27] as Achievement,
            _ => null
        };
        if(first_checker != null && first_checker.Counter == 0) {
            first_checker.Counter++;
            query = UtilityHub.query_builder.Append("UPDATE achievement SET counter=")
                                            .Append(first_checker.Counter)
                                            .Append(" WHERE id=")
                                            .Append(first_checker.Id)
                                            .ToString();
            modifyDB.ControllDB(query, "achieve");
            UtilityHub.query_builder.Clear();
        }
    }

    // n점 소모하기
    public void UseDot(int use) {
        Achievement achieve = datahub.Achievement[45] as Achievement;
        achieve.Counter += use;
        query = UtilityHub.query_builder.Append("UPDATE achievement SET counter=")
                                               .Append(achieve.Counter)
                                               .Append(" WHERE id=")
                                               .Append(achieve.Id)
                                               .ToString();
        modifyDB.ControllDB(query, "achieve");
        UtilityHub.query_builder.Clear();
    }

    // n점 소지하고있기
    public void OwnDotCheck() {
        Achievement tmp;
        if(datahub.Dot >= 1000 && datahub.Dot < 3000) {
            tmp = datahub.Achievement[42] as Achievement;
            if(tmp.Counter == 0) {
                tmp.Counter += 1;
                query = UtilityHub.query_builder.Append("UPDATE achievement SET counter=")
                                                .Append(tmp.Counter)
                                                .Append(" WHERE id=")
                                                .Append(42)
                                                .ToString();
                UtilityHub.query_builder.Clear();
                modifyDB.ControllDB(query, "achieve");
            }
        }
        else if (datahub.Dot >= 3000 && datahub.Dot < 5000) {
            tmp = datahub.Achievement[43] as Achievement;
            if (tmp.Counter == 0) {
                tmp.Counter += 1;
                query = UtilityHub.query_builder.Append("UPDATE achievement SET counter=")
                                                .Append(tmp.Counter)
                                                .Append(" WHERE id=")
                                                .Append(43)
                                                .ToString();
                UtilityHub.query_builder.Clear();
                modifyDB.ControllDB(query, "achieve");
            }
        }
        else if (datahub.Dot >= 5000) {
            tmp = datahub.Achievement[44] as Achievement;
            if (tmp.Counter == 0) {
                tmp.Counter += 1;
                query = UtilityHub.query_builder.Append("UPDATE achievement SET counter=")
                                                .Append(tmp.Counter)
                                                .Append(" WHERE id=")
                                                .Append(44)
                                                .ToString();
                UtilityHub.query_builder.Clear();
                modifyDB.ControllDB(query, "achieve");
            }
        }
    }


    // 강화 업적
    public void UpgradeUnitCheck(int count) {
        var achieve = datahub.Achievement[40] as Achievement;
        achieve.Counter += count;
        query = UtilityHub.query_builder.Append("UPDATE achievement SET counter=")
                                        .Append(achieve.Counter)
                                        .Append(" WHERE id=")
                                        .Append(40)
                                        .ToString();
        modifyDB.ControllDB(query, "achieve");
        UtilityHub.query_builder.Clear();
    }

    // 출석체크 확인
    public void AttendanseCheck() {
        // 일일 퀘스트
        DailyQuest daily = datahub.DailyQuest[1] as DailyQuest;

        // counter는 하루에 하나만 늘어나므로 0이어야 작동함
        if ( daily.Counter == 0 ) {
            daily.Counter = 1;
            query = UtilityHub.query_builder.Append("UPDATE dailyquest SET counter=")
                                                   .Append(daily.Counter)
                                                   .Append(" WHERE id=")
                                                   .Append(daily.Id)
                                                   .ToString();
            modifyDB.ControllDB(query, "daily");
            UtilityHub.query_builder.Clear();

            // 주간 퀘스트
            WeeklyQuest weekly = datahub.WeeklyQuest[1] as WeeklyQuest;
            weekly.Counter += 1;
            query = UtilityHub.query_builder.Append("UPDATE weeklyquest SET counter=")
                                            .Append(weekly.Counter)
                                            .Append(" WHERE id=")
                                            .Append(weekly.Id)
                                            .ToString();
            modifyDB.ControllDB(query, "weekly");
            UtilityHub.query_builder.Clear();
        }

        DailyObserver();
    }

    // stamia 소모 퀘스트 확인
    public void StaminaQuestCheck(int used) {

        // daily
        var tmp = datahub.DailyQuest[2] as DailyQuest;
        tmp.Counter += used;
        query = UtilityHub.query_builder.Append("UPDATE dailyquest SET counter=")
                                        .Append(tmp.Counter)
                                        .Append(" WHERE id=")
                                        .Append(2)
                                        .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "daily");
        if(tmp.Counter >= tmp.RequestCounter) {
            DailyObserver();
        }

        // weekly
        var tmp_2 = datahub.WeeklyQuest[4] as WeeklyQuest;
        tmp_2.Counter += used;
        query = UtilityHub.query_builder.Append("UPDATE weeklyquest SET counter=")
                                        .Append(tmp_2.Counter)
                                        .Append(" WHERE id=")
                                        .Append(4)
                                        .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "weekly");
    }

    // 상자 구매 퀘스트
    public void BuyChestQuestCheck(int key) {

        // 일일 퀘스트 아무 상자나 1회 구매하기
        var tmp = datahub.DailyQuest[3] as DailyQuest;
        if(tmp.Counter == 0) {
            tmp.Counter += 1;
            query = UtilityHub.query_builder.Append("UPDATE dailyquest SET counter=")
                                            .Append(tmp.Counter)
                                            .Append(" WHERE id=")
                                            .Append(3)
                                            .ToString();
            UtilityHub.query_builder.Clear();
            modifyDB.ControllDB(query, "daily");
            DailyObserver();
        }

        // 주간퀘 A급 상자 구매하기
        if(key == 5) {
            var week = datahub.WeeklyQuest[5] as WeeklyQuest;
            if(week.Counter == 0) {
                week.Counter += 1;
                query = UtilityHub.query_builder.Append("UPDATE weeklyquest SET counter=")
                                                .Append(week.Counter)
                                                .Append(" WHERE id=")
                                                .Append(5)
                                                .ToString();
                UtilityHub.query_builder.Clear();
                modifyDB.ControllDB(query, "weekly");
            }
        }
    }

    // 조합 퀘스트
    public void CombineQuestCheck(string combine_grade_type) {

        switch (combine_grade_type) {
            case "A":
                //일일 퀘스트 
                // A급 1회 조합
                var daily = datahub.DailyQuest[5] as DailyQuest;
                if (daily.Counter == 0) {
                    daily.Counter += 1;
                    query = UtilityHub.query_builder.Append("UPDATE dailyquest SET counter=")
                                                    .Append(daily.Counter)
                                                    .Append(" WHERE id=")
                                                    .Append(5)
                                                    .ToString();
                    UtilityHub.query_builder.Clear();
                    modifyDB.ControllDB(query, "daily");
                    DailyObserver();
                }
                break;
            case "S":
                // 주간 퀘스트 : S급 2회 조합하기
                var weekly = datahub.WeeklyQuest[2] as WeeklyQuest;
                weekly.Counter += 1;
                query = UtilityHub.query_builder.Append("UPDATE weeklyquest SET counter=")
                                                .Append(weekly.Counter)
                                                .Append(" WHERE id=")
                                                .Append(2)
                                                .ToString();
                UtilityHub.query_builder.Clear();
                modifyDB.ControllDB(query, "weekly");
                break;
        }

    }

    // 라운드 클리어 퀘스트
    public void RoundClearQuestCheck(int round) {

        // 일일 퀘스트 100라운드 통과하기
        if(round >= 100) {
            var daily = datahub.DailyQuest[4] as DailyQuest;
            if(daily.Counter == 0) {
                daily.Counter += 1;
                query = UtilityHub.query_builder.Append("UPDATE dailyquest SET counter=")
                                                .Append(daily.Counter)
                                                .Append(" WHERE id=")
                                                .Append(4)
                                                .ToString();
                UtilityHub.query_builder.Clear();
                modifyDB.ControllDB(query, "daily");
                DailyObserver();
            }
        }
        // 주간퀘스트 도합 500라운드 통과하기
        var weekly = datahub.WeeklyQuest[3] as WeeklyQuest;
        weekly.Counter += round;
        query = UtilityHub.query_builder.Append("UPDATE weeklyquest SET counter=")
                                        .Append(weekly.Counter)
                                        .Append(" WHERE id=")
                                        .Append(3)
                                        .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "weekly");
    }

    public void DailyObserver() {

        // 1~5를 돌면서 counter가 requestcounter보다 같거나 크면 counter++
        int counter = 0;
        DailyQuest daily;
        for(int i = 1; i <= 5; i++) {
            daily = datahub.DailyQuest[i] as DailyQuest;
            if(daily.Counter > 0 && daily.Counter >= daily.RequestCounter) {
                counter++;
            }
        }

        daily = datahub.DailyQuest[6] as DailyQuest;
        daily.Counter = counter;
        query = UtilityHub.query_builder.Append("UPDATE dailyquest SET counter=")
                                        .Append(daily.Counter)
                                        .Append(" WHERE id=")
                                        .Append(6)
                                        .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "daily");
    }
}
