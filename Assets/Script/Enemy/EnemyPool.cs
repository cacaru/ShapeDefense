using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ShapeDefenseSpace;

using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;
using UnityEngine.UI;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject EnemyObj;
    [SerializeField] private GameObject EnemyParent;
    [SerializeField] private Image BossHPImg;
    [SerializeField] private Tilemap map;

    [SerializeField] private DieEffectPool dieeffectPool;

    private readonly Queue<GameObject> enemyqueue = new();

    private Vector3 ori_pos;

    private int enemy_active_layer;
    private int enemy_wait_layer;

    private Color alpha_1 = new(255f, 255f, 255f, 255f);

    private void Start() {

        ori_pos = EnemyParent.transform.position;
        
        enemy_active_layer = Layers.Enemy;
        enemy_wait_layer = Layers.EnemyPoolState;
        //Debug.Log(enemy_active_layer + " " + enemy_wait_layer);

        Initialize(125);
    }

    private GameObject CreateEnemy() {
        var enemy = Instantiate(EnemyObj, transform.position, Quaternion.identity);
        enemy.transform.SetParent(transform);
        // 부모 설정은 bullet을 받을 때 설정
        enemy.tag = "EnemyPoolState";

        enemy.SetActive(false);
        enemy.AddComponent<EnemyMove>();
        //enemy.GetComponent<EnemyMove>().Map = map;
        //enemy.GetComponent<EnemyMove>().Datahub = datahub;
        //enemy.GetComponent<EnemyMove>().DefaultSetting(map, datahub);
        return enemy;
    }

    private void Initialize(int count) {
        for (int i = 0; i < count; i++) {
            enemyqueue.Enqueue(CreateEnemy());
        }
    }
    

    public GameObject OnNormalEnemy(int now_round, int enemy_counter) {
        GameObject enemy;
        if (enemyqueue.Count > 0) {
            enemy = enemyqueue.Dequeue();
        }
        else {
            enemy = CreateEnemy();
        }
        enemy.tag = "Enemy";
        // target 설정이 가능하게 layer 업데이트
        enemy.layer = enemy_active_layer;
        enemy.transform.position = ori_pos;
        /*
        enemy.name = UtilityHub.query_builder.Append("enemy_")
                                             .Append(now_round)
                                             .Append("_")
                                             .Append(enemy_counter)
                                             .ToString();
        UtilityHub.query_builder.Clear();
        */
        // stat 정보를 담을 변수
        Enemy now_enemy = new();

        #region Enemy sprite select & stat saving
        int enemy_number_base = now_round / 10;

        // 성능 지정
        if(enemy_number_base == 0) {
            now_enemy = datahub.Enemy[1] as Enemy;
        }
        else {
            if (enemy_counter % 4 == 0 || enemy_counter % 4 == 3) {
                now_enemy = datahub.Enemy[enemy_number_base + 1] as Enemy;
            }
            else {
                now_enemy = datahub.Enemy[enemy_number_base] as Enemy;
            }
        }

        // sprite 지정
        switch(enemy_number_base % 6) {
            case 0:
                if(enemy_number_base == 0) {
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_1;
                }
                else {
                    if (enemy_counter % 4 == 0 || enemy_counter % 4 == 3) {
                        enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_1;
                    }
                    else {
                        enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_2;
                    }
                }
                break;
            case 1:
                if (enemy_counter % 4 == 0 || enemy_counter % 4 == 3) {
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_2;
                }
                else {
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_1;
                }
                break;
            case 2:
                if (enemy_counter % 4 == 0 || enemy_counter % 4 == 3) {
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_1;
                }
                else {
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_2;
                }
                break;
            case 3:
                if (enemy_counter % 4 == 0 || enemy_counter % 4 == 3) {
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_2;
                }
                else {
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_1;
                }
                break;
            case 4:
                if (enemy_counter % 4 == 0 || enemy_counter % 4 == 3) {
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_1;
                }
                else {
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_2;
                }
                break;
            case 5:
                if (enemy_counter % 4 == 0 || enemy_counter % 4 == 3) {
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_2;
                }
                else {
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_1;
                }
                break;
        }
        
        // 색 지정
        if(enemy_number_base == 0) {
            enemy_number_base += 1;
        }
        switch(enemy_number_base / 6) {
            case 0:
                enemy.GetComponent<SpriteRenderer>().color = f_enemy_color;
                break;
            case 1:
                if(now_enemy.Id % 6 == 0) {
                    enemy.GetComponent<SpriteRenderer>().color = f_enemy_color;
                }
                else {
                    enemy.GetComponent<SpriteRenderer>().color = e_enemy_color;
                }
                break;
            case 2:
                if (now_enemy.Id % 6 == 0) {
                    enemy.GetComponent<SpriteRenderer>().color = e_enemy_color;
                }
                else {
                    enemy.GetComponent<SpriteRenderer>().color = d_enemy_color;
                }
                break;
            case 3:
                if (now_enemy.Id % 6 == 0) {
                    enemy.GetComponent<SpriteRenderer>().color = d_enemy_color;
                }
                else {
                    enemy.GetComponent<SpriteRenderer>().color = c_enemy_color;
                }
                break;

            default:
                enemy.GetComponent<SpriteRenderer>().color = reinforced_enemy_color;
                break;
        }
     
        /*
        switch (now_round % 10) {
            case 0:
                now_enemy = datahub.Enemy[1] as Enemy;
                enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_1;
                break;
            case 1:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[2] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_2;
                }
                else {
                    now_enemy = datahub.Enemy[1] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_1;
                }
                break;
            case 2:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[3] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_1;
                }
                else {
                    now_enemy = datahub.Enemy[2] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_2;
                }
                break;
            case 3:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[4] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_2;
                }
                else {
                    now_enemy = datahub.Enemy[3] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_1;
                }
                break;
            case 4:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[5] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_1;
                }
                else {
                    now_enemy = datahub.Enemy[4] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_2;
                }
                break;
            case 5:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[6] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_2;
                }
                else {
                    now_enemy = datahub.Enemy[5] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_1;
                }
                break;
            case 6:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    is_reinforced = true;
                    now_enemy = datahub.Enemy[7] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_1;
                }
                else {
                    now_enemy = datahub.Enemy[6] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_2;
                }
                break;
            case 7:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[8] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_2;
                }
                else {
                    now_enemy = datahub.Enemy[7] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_1;
                }
                break;
            case 8:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[9] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_1;
                }
                else {
                    now_enemy = datahub.Enemy[8] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_2;
                }
                break;
            case 9:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[10] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_2;
                }
                else {
                    now_enemy = datahub.Enemy[9] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_1;
                }
                break;
            case 10:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[11] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_1;
                }
                else {
                    now_enemy = datahub.Enemy[10] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_2;
                }
                break;
            case 11:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[12] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_2;
                }
                else {
                    now_enemy = datahub.Enemy[11] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_1;
                }
                break;
            case 12:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[13] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_1;
                }
                else {
                    now_enemy = datahub.Enemy[12] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_2;
                }
                break;
            case 13:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[14] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_2;
                }
                else {
                    now_enemy = datahub.Enemy[13] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_1;
                }
                break;
            case 14:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[15] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_1;
                }
                else {
                    now_enemy = datahub.Enemy[14] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_2;
                }
                break;
            case 15:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[16] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_2;
                }
                else {
                    now_enemy = datahub.Enemy[15] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_1;
                }
                break;
            case 16:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[17] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_1;
                }
                else {
                    now_enemy = datahub.Enemy[16] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_2;
                }
                break;
            case 17:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[18] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_2;
                }
                else {
                    now_enemy = datahub.Enemy[17] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_1;
                }
                break;
            case 18:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[19] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_1;
                }
                else {
                    now_enemy = datahub.Enemy[18] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_2;
                }
                break;
            case 19:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[20] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_2;
                }
                else {
                    now_enemy = datahub.Enemy[19] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_1;
                }
                break;
            case 20:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[21] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_1;
                }
                else {
                    now_enemy = datahub.Enemy[20] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_clover_2;
                }
                break;
            case 21:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[22] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_2;
                }
                else {
                    now_enemy = datahub.Enemy[21] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_1;
                }
                break;
            case 22:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[23] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_1;
                }
                else {
                    now_enemy = datahub.Enemy[22] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_heart_2;
                }
                break;
            case 23:
                if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                    now_enemy = datahub.Enemy[24] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_2;
                }
                else {
                    now_enemy = datahub.Enemy[23] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = enemy_sprite_spade_1;
                }
                break;
        }
        */
        #endregion

        // 체력 지정
        // -> 난이도에 따라 추가 강화
        int difficulty = datahub.Difficulty;
        
        int health = (int)(now_round * (1 + 0.2 * difficulty) * now_enemy.UpgradeValue);
        //Debug.Log("health >> " + health + " // now_round > " + now_round + " // difficulty > " + (1 + 0.1 * difficulty) + " // upgradevalue >> " + now_enemy.UpgradeValue);
        float speed;
#if UNITY_EDITOR
        speed = 0.005f;
#elif UNITY_ANDROID
        speed = 0.008f;
#else   
        speed = 0.005f;
#endif
        speed *= now_enemy.Speed;

        //Debug.Log("in pool >> " + speed);
        //enemy.GetComponent<EnemyMove>().Speed = speed;
        enemy.GetComponent<EnemyMove>().DefaultSetting(map, speed);

        enemy.GetComponent<EnemyHp>().NameTag = enemy_counter + ((now_round - 1) * datahub.EnemyCounter);
        enemy.GetComponent<EnemyHp>().SetAllHP(health, health, health);

        enemy.GetComponent<EnemyDie>().SetPool(dieeffectPool, this);

        enemy.transform.SetParent(EnemyParent.transform);
        enemy.transform.position = ori_pos;
        
        enemy.SetActive(true);
        enemy.GetComponent<EnemyMove>().Active = true;
        return enemy;
    }

    public GameObject OnBossEnemy(int boss_round) {
        GameObject enemy;
        if (enemyqueue.Count > 0) {
            enemy = enemyqueue.Dequeue();
        }
        else {
            enemy = CreateEnemy();
        }
        enemy.tag = "Enemy";
        // target 설정이 가능하게 layer 업데이트
        enemy.layer = enemy_active_layer;
        enemy.name = "enemy_boss";
        
        // stage에 따라 보스 가 달라질 것
        Enemy boss_stat;
        int health;
        float speed;

        int random_boss_generator = Random.Range(1, 5);
        boss_stat = datahub.Enemy[random_boss_generator+24] as Enemy;
        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/boss_" + random_boss_generator);
        enemy.GetComponent<SpriteRenderer>().color = Color.red;
        speed = boss_stat.Speed * 0.003f;
        Debug.Log(speed);
        
        health = (int)(boss_round * (1 + 0.2 * datahub.Difficulty) * boss_stat.UpgradeValue);
        enemy.AddComponent<BossDrop>();
        enemy.GetComponent<BossDrop>().Droped = false;
        enemy.GetComponent<EnemyHp>().SetAllHP(health, health, health);

        enemy.GetComponent<EnemyHp>().BossHpImg = BossHPImg;

        enemy.GetComponent<EnemyDie>().SetPool(dieeffectPool, this);

        // boss는 id를 추가 기입
        enemy.GetComponent<EnemyHp>().id = random_boss_generator + 100;
        //enemy.GetComponent<EnemyMove>().Speed = speed;
        enemy.GetComponent<EnemyMove>().DefaultSetting(map, speed);

        // boss 위치 이동
        enemy.transform.SetParent(EnemyParent.transform);
        enemy.transform.position = ori_pos;

        enemy.SetActive(true);
        enemy.GetComponent<EnemyMove>().Active = true;
        return enemy;
    }


    public void BackEnemy(GameObject enemy) {

        enemy.name = "";
        enemy.tag = "EnemyPoolState";
        // target 설정이 가능하게 layer 업데이트
        enemy.layer = enemy_wait_layer;
        enemy.transform.position = ori_pos;

        enemy.transform.SetParent(this.transform);

        Destroy(enemy.GetComponent<EnemyMove>());

        enemy.GetComponent<EnemyHp>().Hp = 0;
        enemy.GetComponent<EnemyHp>().Maxhp = 0;
        enemy.GetComponent<EnemyHp>().Hid_HP = 0;

        // 나타나던 이펙트 초기화
        enemy.transform.Find("Poison").gameObject.SetActive(false);
        enemy.transform.Find("Blust").gameObject.SetActive(false);


        // 속도 초기화
        enemy.AddComponent<EnemyMove>();
        //enemy.GetComponent<EnemyMove>().Map = map;
        //enemy.GetComponent<EnemyMove>().Datahub = datahub;
        //enemy.GetComponent<EnemyMove>().DefaultSetting(map, datahub);
        //enemy.GetComponent<EnemyMove>().ShowSetting();

        // 이동중이던 상태 초기화
        // enemy.GetComponent<EnemyMove>().DecideMoveType(datahub.StageNumber, map);

        if (enemy.TryGetComponent<BossDrop>(out var comp) ) {
            Destroy(comp.GetComponent<BossDrop>());
        }

        enemy.SetActive(false);
        // hp 바 재생성
        enemy.transform.Find("Canvas").gameObject.SetActive(true);
        // 콜라이더 재생성
        enemy.GetComponent<BoxCollider2D>().enabled = true;

        enemy.GetComponent<SpriteRenderer>().color = alpha_1;

        enemyqueue.Enqueue(enemy);
    }
}
