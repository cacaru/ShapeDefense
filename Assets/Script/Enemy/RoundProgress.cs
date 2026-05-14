using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ShapeDefenseSpace;

using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;

public class RoundProgress : MonoBehaviour
{
    [SerializeField] private TMP_Text round;

    [SerializeField] private GameObject GameEnd;
    [SerializeField] private GameObject SuccessPanel;
    [SerializeField] private GameObject RewardContent;
    [SerializeField] private GameObject SkipBtn;
    [SerializeField] private TMP_Text RoundTimer;

    // skip 
    private TMP_Text min;
    private TMP_Text max;

    // 마지막 보스부터 감시
    private int watch_end_boss;
    private bool watcher = true;
    private readonly string final_round = "마지막";

    private Coroutine checker;
    private IEnumerator timer;
    private int left_time = 0;
    //private readonly float Sec = 0.016f;

    void Start()
    {
        SkipBtn.SetActive(false);
        Time.timeScale = 1;

        timer = Timer();

        //skip
        min = SkipBtn.transform.Find("Min").gameObject.GetComponent<TMP_Text>();
        max = SkipBtn.transform.Find("Max").gameObject.GetComponent<TMP_Text>();

        StartCoroutine(First());
    }

    IEnumerator First() {

        watch_end_boss = datahub.EndRound - 1;
        // 10초의 준비 이후 새 루틴 시작
        yield return wfs_3;

        //Debug.Log("게임 시작");
        checker = StartCoroutine(Checker());
        SkipBtn.SetActive(true);
        // 타이머 표기
        StartCoroutine(timer);
    }

    IEnumerator Checker() {
        while (true) {
            if (!datahub.Pause && left_time == 0) {
                left_time = 60;
                //Debug.Log(datahub.RoundNumber + " > start");
                RoundTimer.text = left_time.ToString();
                //Debug.Log("New round start");
                // 현 라운드 텍스트 변경
                datahub.RoundNumber++;
                round.text = datahub.RoundNumber.ToString();

                // 스킵 보상 수정
                min.text = ((int)(datahub.RoundNumber * 1 * 0.6)).ToString();
                max.text = (240 + (int)(datahub.RoundNumber * 3 * 0.6)).ToString();

                // 최종 라운드에 도달 했는지 확인
                int going = UtilityHub.EndRoundChecker(datahub.RoundNumber);
                if (going == 2) { break; }
                else if (going == 0) {
                    round.text = final_round;
                }
                if (datahub.RoundNumber >= watch_end_boss && watcher) {
                    watcher = false;
                    StartCoroutine(EndRoundWathcer());
                }
                // 90초 이후 새 라운드 진행
                gameObject.GetComponent<EnemySpawner>().NextSpawn();
                //round.text = (int.Parse(round.text) + 1).ToString();
            }
            yield return wfs_1;
        }
        RoundEnd();
    }

    IEnumerator EndRoundWathcer() {
        while (true) {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // 아무런 적 유닛이 남아있지 않다면 게임 종료
            if ((enemies == null || enemies.Length <= 0) && datahub.KillLastBoss) {
                watcher = true;
                StopAllCoroutines();
                RoundEnd();
                watch_end_boss = 9999;
                break;
            }
            yield return wfs_1;
        }
    }

    IEnumerator Timer() {

        while (true){
            yield return wfs_1;
            if (!datahub.Pause) {
                left_time--;
                RoundTimer.text = left_time.ToString();
            }
        }
        
    }

    public void RoundEnd() {
        // 라운드 종료가 되었을 때 보스를 잡는데 성공했다면 == enemy_boss가 존재하지 않으면 게임 보상 추가 
        // Enemy boss를 확인
        int ending = 0;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int left_unit_count = enemies.Length;
        foreach (var enemy in enemies) {
            // 남아 있는 것이 있음
            if (enemy.name.Equals("enemy_boss")) {
                ending = 1;
                break;
            }
        }
        if(left_unit_count >= datahub.MaxEnemyCounter) {
            ending = 1;
        }

        string text;
        bool success = false;
        // 실패했다면 게임 종료 화면에서 실패 announce 
        if (ending == 1) {
            text = "공략 실패";
        }
        // 성공
        else {
            success = true;
            text = "공략 완료";
        }
        // 보상 생성
        // 스테이지에 따라 각 보상 + 1
        // 보상은 남은 유닛수 에 반비례하여 지급
        // 1회 보상 확률 일반 40% 고급 40% 명품 15% 한정품 4% 개인제작 1%

        // [0] == unit_id_selecter 
        // [1] == piece_counter
        int[] random_select_result;
        string query;
        // success = 3 / fail = 2
        random_select_result = GenerateRandomReward(success);
        var unit = datahub.Unit_dic[random_select_result[0]] as Unit;
        unit.Piece += random_select_result[1] + (datahub.Difficulty - 1);
        // modify db
        query = UtilityHub.query_builder.Append("UPDATE unit SET piece=")
                                        .Append(unit.Piece)
                                        .Append(" WHERE id=")
                                        .Append(unit.Id)
                                        .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "unit");
        // reward prefab 생성
        GameObject pref = Instantiate(_game_clear_reward_obj, RewardContent.transform.position, RewardContent.transform.rotation);
        pref.transform.SetParent(RewardContent.transform, false);
        pref.transform.Find("Grade").GetComponent<Image>().color = unit.Grade switch {
            "E" => color_e,
            "D" => color_d,
            "C" => color_c,
            "B" => color_b,
            "A" => color_a,
            "S" => color_s,
            _ => core_color,
        };
        pref.transform.Find("Image").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.Id); // Resources.Load<Sprite>(UtilityHub.GetPath(unit.Id));
        pref.transform.Find("Piece").GetComponent<TMP_Text>().text = random_select_result[1].ToString();


        random_select_result = GenerateRandomReward(success);
        unit = datahub.Unit_dic[random_select_result[0]] as Unit;
        unit.Piece += random_select_result[1] + (datahub.Difficulty - 1);
        // modify db
        query = UtilityHub.query_builder.Append("UPDATE unit SET piece=")
                                        .Append(unit.Piece)
                                        .Append(" WHERE id=")
                                        .Append(unit.Id)
                                        .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "unit");
        // reward prefab 생성
        GameObject pref_2 = Instantiate(_game_clear_reward_obj, RewardContent.transform.position, RewardContent.transform.rotation);
        pref_2.transform.SetParent(RewardContent.transform, false);
        pref_2.transform.Find("Grade").GetComponent<Image>().color = unit.Grade switch {
            "E" => color_e,
            "D" => color_d,
            "C" => color_c,
            "B" => color_b,
            "A" => color_a,
            "S" => color_s,
            _ => core_color,
        };
        pref_2.transform.Find("Image").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.Id); // Resources.Load<Sprite>(UtilityHub.GetPath(unit.Id));
        pref_2.transform.Find("Piece").GetComponent<TMP_Text>().text = random_select_result[1].ToString();

        if (success) {
            random_select_result = GenerateRandomReward(success);
            unit = datahub.Unit_dic[random_select_result[0]] as Unit;
            unit.Piece += random_select_result[1] + (datahub.Difficulty - 1);
            // modify db
            query = UtilityHub.query_builder.Append("UPDATE unit SET piece=")
                                            .Append(unit.Piece)
                                            .Append(" WHERE id=")
                                            .Append(unit.Id)
                                            .ToString();
            UtilityHub.query_builder.Clear();
            modifyDB.ControllDB(query, "unit");
            // reward prefab 생성
            GameObject pref_3 = Instantiate(_game_clear_reward_obj, RewardContent.transform.position, RewardContent.transform.rotation);
            pref_3.transform.SetParent(RewardContent.transform, false);
            pref_3.transform.Find("Grade").GetComponent<Image>().color = unit.Grade switch {
                "E" => color_e,
                "D" => color_d,
                "C" => color_c,
                "B" => color_b,
                "A" => color_a,
                "S" => color_s,
                _ => core_color,
            };
            pref_3.transform.Find("Image").GetComponent<Image>().sprite = UtilityHub.GetSprite(unit.Id); // Resources.Load<Sprite>(UtilityHub.GetPath(unit.Id));
            pref_3.transform.Find("Piece").GetComponent<TMP_Text>().text = random_select_result[1].ToString();
        }

        // 경험치 획득
        // 기본 경험치 -- 5~10
        // 스테이지 추가 경험치 ( 스테이지 번호 * 2 )
        // 난이도 추가 경험치 ( 난이도 * 3 )
        int exp = success ? Random.Range(5, 11) + datahub.Difficulty * 5 : Random.Range(5, 11);
        datahub.User.Experience += exp;

        // 난이도에 따라 골드 지급
        float gold = success ? Random.Range(100, 501) + datahub.Difficulty * Random.Range(4, 7) : (datahub.RoundNumber * 1.2f * 3);
        // 결과 골드 획득 스탯을 찍었을 시
        if(datahub.User.StatusClearDotLevel > 0) {
            gold += gold * datahub.User.StatusClearDotLevel * 0.05f;
        }
        datahub.User.Dot += (int)gold;
        query = UtilityHub.query_builder.Append("UPDATE user SET dot=")
                                        .Append(datahub.User.Dot)
                                        .Append(", experience=")
                                        .Append(datahub.User.Experience)
                                        .ToString();
        modifyDB.ControllDB(query, "user");
        UtilityHub.query_builder.Clear();

        // 업적 갱신
        // (10라운드 클리어) 업적은 보스를 클리어하는 것으로 갱신함
        //achieve_observer.KillBoss(datahub.RoundNumber);
        achieve_observer.RoundClearQuestCheck(datahub.RoundNumber);
        if (success) {
            achieve_observer.FirstDifficultyClearCheck();
        }
        
        // 골드 보상 패널 수정
        SuccessPanel.transform.Find("Gold").gameObject.GetComponent<TMP_Text>().text =
            UtilityHub.query_builder.Append(((int)gold).ToString()).ToString();
        UtilityHub.query_builder.Clear();

        SuccessPanel.transform.Find("Exp").gameObject.GetComponent<TMP_Text>().text =
            UtilityHub.query_builder.Append(exp.ToString()).ToString();
        UtilityHub.query_builder.Clear();

        SuccessPanel.SetActive(true);
        
        GameEnd.transform.Find("background").Find("SettingText").GetComponent<TMP_Text>().text = text;
        GameEnd.SetActive(true);
    }

    private int[] GenerateRandomReward(bool success) {
        int random_number;
        int unit_id_selecter;
        int piece_counter;

        random_number = Random.Range(1, 101);
        // 36 28 20 10 5 1
        // E
        if (random_number >= 65) {
            // E 3개중 하나 다시 고르기
            unit_id_selecter = Random.Range(1001, 1004);
        }
        // D
        else if (random_number < 65 && random_number >= 37) {
            // D 중 하나
            unit_id_selecter = Random.Range(2001, 2004);
        }
        // C
        else if (random_number < 37 && random_number >= 17) {
            unit_id_selecter = Random.Range(3001, 3015);
        }
        // B
        else if (random_number < 17 && random_number >= 7) {
            unit_id_selecter = Random.Range(4001, 4012);
        }
        // A
        else if (random_number < 7 && random_number >= 2) {
            unit_id_selecter = Random.Range(5001, 5006);
        }
        // S
        else {
            unit_id_selecter = Random.Range(6001, 6006);
        }
        piece_counter = success ? Random.Range(1, 17) : Random.Range(1, 9);

        return new int[2] { unit_id_selecter, piece_counter };
    }

    public void Pause() {
        //StopCoroutine(checker);
        //Time.timeScale = 0;
        // enemy 멈추기
        StopCoroutine(timer);
        pause.EnemyPauseSetting(false);
    }

    public void ReStart() {
        //StartCoroutine(checker);
        //Time.timeScale = 1;
        StartCoroutine(timer);
        pause.EnemyPauseSetting(true);
    }

    // 건너뛰기 (스킵하기)
    public void SkipWaitTime() {
        // 정지상태면 눌려도 아무 행동하지 않음
        if (datahub.Pause) {
            return;
        }
        // boss가 있으면 보스타이머도 스킵해야함
        // 남은 시간만큼 스킵
        gameObject.GetComponent<EnemySpawner>().SkipBossTimer(left_time);

        // 현재 코루틴을 중지시키고
        left_time = 0;
        RoundTimer.text = left_time.ToString();
        StopCoroutine(checker);
        //Debug.Log("Skip):");

        // 다시 실행시킴
        checker = StartCoroutine(Checker());

        // 스킵 버튼을 5초간 잠굼
        SkipBtn.SetActive(false);
        StartCoroutine(nameof(SkipBtnStoper));

        // 소환이 남은 유닛 수 만큼의 dot를 추가 보상함 (스킵 메리트)
        datahub.Dot += (datahub.EnemyCounter - datahub.NowEnemyCounter) * Random.Range(3, 6) + (int)(datahub.RoundNumber * Random.Range(1, 4) * 0.6);
    }

    IEnumerator SkipBtnStoper() {
        while (true) {
            yield return wfs_5;
            if (!datahub.Pause) {
                // skipobtn 재실행
                SkipBtn.SetActive(true);
                break;
            }
        }
        
    }
}
