using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;

public class EnemySpawner : SceneSingleton<EnemySpawner>
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject boss_timer;
    [SerializeField] private EnemyPool pool;

    private GameObject OuterTimer;

    private int max_enemy_counter = 40;

    private bool boss_running = false;
    private readonly float fill_amount = 1 / 300f;
    private readonly float end_boss_fill_amount = 1 / 60f;
    //private readonly float start_time = 1;
    private readonly float end_time = 0;
    private float now_time = 1;

    // Start is called before the first frame update
    void Start()
    {
        max_enemy_counter = datahub.EnemyCounter;
        OuterTimer = boss_timer.transform.Find("OutTimer").gameObject;
    }

    IEnumerator Spawn() {
        datahub.NowEnemyCounter = 1;
        int enemy_counter = 1;
        int round_number = datahub.RoundNumber;
        
        while(true){
            if (!datahub.Pause) {
                if (enemy_counter > max_enemy_counter) {
                    yield break;
                }

                // boss
                if (round_number > 1 && round_number % 10 == 0) {
                    BossSpawn();
                    yield break;
                }
                else {
                    int going = UtilityHub.EndRoundChecker(round_number);
                    if (going == 0) { break; }

                    InstanceSpawn(round_number, enemy_counter);
                    datahub.NowEnemyCounter++;
                    enemy_counter++;
                }
            }
            yield return wfs_1;
        }
    }

    private void InstanceSpawn(int round_number, int enemy_counter) {
        var obj = pool.OnNormalEnemy(round_number, enemy_counter);
        obj.SetActive(true);
    }

    private void BossSpawn() {
        var obj = pool.OnBossEnemy( datahub.RoundNumber );
        obj.SetActive(true);

        // boss timer start
        InitTimer();
        // 마지막 보스라면 다른 타이머 설정
        int boss_id = obj.GetComponent<EnemyHp>().id;
        boss_timer.transform.Find("Boss").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Enemy/boss_" + (boss_id - 100) + "_white");
        boss_timer.transform.Find("BossHp").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Enemy/boss_" + (boss_id - 100));
        boss_timer.SetActive(true);
        if(datahub.RoundNumber+1 == datahub.EndRound) {
            StartCoroutine(EndBossTimerStart());
        }
        else {
            StartCoroutine(BossTimerStart());
        }
        
    }

    IEnumerator BossTimerStart() {
        now_time = 1;
        OuterTimer.GetComponent<Image>().fillAmount = 1;

        boss_running = true;
        while(now_time >= end_time) {
            
            if (!datahub.Pause) {
                now_time -= fill_amount;
                OuterTimer.GetComponent<Image>().fillAmount = now_time;
            }

            yield return wfs_1;
        }
    }

    IEnumerator EndBossTimerStart() {
        now_time = 1;
        OuterTimer.GetComponent<Image>().fillAmount = 1;

        while( now_time >= end_time ) {
            if(!datahub.Pause) {
                now_time -= end_boss_fill_amount;
                OuterTimer.GetComponent<Image>().fillAmount = now_time;
            }

            yield return wfs_1;
        }

    }

    public void SkipBossTimer(int round_time) {
        if (boss_running) {
            now_time -= fill_amount * round_time;
        }
        
    }

    public void StopTimer() {
        boss_running = false;
        StopCoroutine(BossTimerStart());
        InitTimer();
    }

    private void InitTimer() {
        now_time = 1;
        OuterTimer.GetComponent<Image>().fillAmount = 1;
        boss_timer.transform.Find("BossHp").gameObject.GetComponent<Image>().fillAmount = 1;
        boss_timer.SetActive(false);
    }

    public void NextSpawn() {
        StartCoroutine(Spawn());
    }

    public void StopSpawn() {
        StopCoroutine(nameof(Spawn));
    }

    public void RestartSpawn() {
        StartCoroutine(nameof(Spawn));
    }
}
