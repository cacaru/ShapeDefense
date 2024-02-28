using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;

    private DataHub datahub;

    private int enemy_counter = 1;
    private int max_enemy_counter = 40;
    private int round = 0;

    private Vector3 ori;

    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
        enemy_counter = 1;
        max_enemy_counter = datahub.EnemyCounter;
        ori = transform.position;
    }

    IEnumerator Spawn() {
        while(true){
            // ���� 5���� �����ʹ� ����� ������ ����
            int going = UtilityHub.EndRoundChecker(datahub.StageNumber, datahub.RoundNumber);
            if (going == 0) { break; }
            
            // boss
            if (datahub.RoundNumber > 1 && datahub.RoundNumber % 10 == 0) {
                BossSpawn();
                yield break;
            }
            else {
                InstanceSpawn();
            }
            if (enemy_counter > max_enemy_counter) {
                StopCoroutine(nameof(Spawn));
            }
            yield return new WaitForSeconds(0.8f);
        }
    }

    // ���� ��ȯ
    private void InstanceSpawn() {
        //Debug.Log("Instance_Spwan_Active");
        GameObject enemy = Instantiate(_enemy, ori, Quaternion.identity);
        enemy.transform.SetParent(transform);
        enemy.name = "enemy_" + (enemy_counter + (max_enemy_counter * (round - 1)));
        // ���������� ���忡 ���� enemy�� ������ �����ؾ���
        // �⺻ �������� ����
        // hp, speed, spirte �̹����� �����ؾ���
        int now_round = datahub.RoundNumber;
        Enemy now_enemy = new();
        float speed = 0.003f;
        // sprite �̹��� ����
        switch (datahub.StageNumber) {
            case 1:
                // �ӵ� ����
                enemy.AddComponent<EnemyMove1>();
                //~5 Ŭ�ι� ��
                if (now_round <= 5) {
                    now_enemy = datahub.Enemy[1] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_1");
                }
                // ~10  Ŭ�ι� �� 3 �� 1 
                else if (now_round > 5 && now_round < 10) {
                    if (enemy_counter % 4 == 0) {
                        now_enemy = datahub.Enemy[2] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_2");
                    }
                    else {
                        now_enemy = datahub.Enemy[1] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_1");
                    }
                }
                // ~15 Ŭ�ι� �� 2 �� 1 ��Ʈ �� 1
                else if (now_round > 10 && now_round <= 15) {
                    if (enemy_counter % 4 == 0) {
                        now_enemy = datahub.Enemy[3] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_1");
                    }
                    else if(enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[2] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_2");
                    }
                    else {
                        now_enemy = datahub.Enemy[1] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_1");
                    }
                }
                // ~20 Ŭ�� 1 Ŭ�� 1 �Ϻ� 2
                else if (now_round > 15 && now_round < 20) {
                    if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[3] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_1");
                    }
                    else if (enemy_counter % 2 == 0) {
                        now_enemy = datahub.Enemy[2] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_2");
                    }
                    else {
                        now_enemy = datahub.Enemy[1] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_1");
                    }
                }
                // ~25 Ŭ��1 �Ϻ�2 ����1 
                else if (now_round > 20 && now_round <= 25) {
                    if (enemy_counter % 4 == 0 ) {
                        now_enemy = datahub.Enemy[4] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_2");
                    }
                    else if (enemy_counter % 3 == 0 || enemy_counter % 2 == 0) {
                        now_enemy = datahub.Enemy[3] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_1");
                    }
                    else {
                        now_enemy = datahub.Enemy[2] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_2");
                    }
                }
                // ~30 Ŭ��1 �Ϻ�1 ����2
                else if (now_round > 25 && now_round < 30) {
                    if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[4] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_2");
                    }
                    else if (enemy_counter % 2 == 0) {
                        now_enemy = datahub.Enemy[3] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_1");
                    }
                    else {
                        now_enemy = datahub.Enemy[2] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_2");
                    }
                }
                // ~35 �Ϻ�2 ����2
                else if (now_round > 30 && now_round <= 35) {
                    if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[4] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_2");
                    }
                    else {
                        now_enemy = datahub.Enemy[3] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_1");
                    }
                }
                // ~40 �Ϻ�1 ����2 �ٺ�1
                else if (now_round > 35 && now_round < 40) {
                    if (enemy_counter % 4 == 0) {
                        now_enemy = datahub.Enemy[5] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/diamond_1");
                    }
                    else if (enemy_counter % 3 == 0 || enemy_counter % 2 == 0) {
                        now_enemy = datahub.Enemy[4] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_2");
                    }
                    else {
                        now_enemy = datahub.Enemy[3] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_1");
                    }
                }
                // ~45 �Ϻ� 1 ����1 �ٺ�2
                else if (now_round > 40 && now_round <= 45) {
                    if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[5] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/diamond_1");
                    }
                    else if (enemy_counter % 2 == 0) {
                        now_enemy = datahub.Enemy[4] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_2");
                    }
                    else {
                        now_enemy = datahub.Enemy[3] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_1");
                    }
                }
                // ~50 ����2 �ٺ�2
                else if (now_round > 45 && now_round < 50) {
                    if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[5] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/diamond_1");
                    }
                    else {
                        now_enemy = datahub.Enemy[4] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_2");
                    }
                }
                // ~55 ����1 �ٺ�2 ����1
                else if (now_round > 50 && now_round <= 55) {
                    if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[6] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/diamond_2");
                    }
                    else if (enemy_counter % 2 == 0) {
                        now_enemy = datahub.Enemy[5] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/diamond_1");
                    }
                    else {
                        now_enemy = datahub.Enemy[4] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_2");
                    }
                }
                // ~60 �ٺ�2 ����2
                else if (now_round > 55 && now_round < 60) {
                    if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[6] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/diamond_2");
                    }
                    else {
                        now_enemy = datahub.Enemy[5] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/diamond_1");
                    }
                }

                speed = now_enemy.Speed * 0.003f;
                enemy.GetComponent<EnemyMove1>().Speed = speed;
                break;
            case 2:
                enemy.AddComponent<EnemyMove2>();
               
                //~5 Ŭ�ι� ��
                if (now_round <= 5) {
                    now_enemy = datahub.Enemy[1] as Enemy;
                    enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_1");
                }
                // ~10  Ŭ�ι� �� 3 �� 1 
                else if (now_round > 5 && now_round < 10) {
                    if (enemy_counter % 4 == 0) {
                        now_enemy = datahub.Enemy[2] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_2");
                    }
                    else {
                        now_enemy = datahub.Enemy[1] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_1");
                    }
                }
                // ~15 Ŭ�ι� �� 2 �� 1 ��Ʈ �� 1
                else if (now_round > 10 && now_round <= 15) {
                    if (enemy_counter % 4 == 0) {
                        now_enemy = datahub.Enemy[3] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_1");
                    }
                    else if (enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[2] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_2");
                    }
                    else {
                        now_enemy = datahub.Enemy[1] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_1");
                    }
                }
                // ~20 Ŭ�� 1 Ŭ�� 1 �Ϻ� 2
                else if (now_round > 15 && now_round < 20) {
                    if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[3] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_1");
                    }
                    else if (enemy_counter % 2 == 0) {
                        now_enemy = datahub.Enemy[2] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_2");
                    }
                    else {
                        now_enemy = datahub.Enemy[1] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_1");
                    }
                }
                // ~25 Ŭ��1 �Ϻ�2 ����1 
                else if (now_round > 20 && now_round <= 25) {
                    if (enemy_counter % 4 == 0) {
                        now_enemy = datahub.Enemy[4] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_2");
                    }
                    else if (enemy_counter % 3 == 0 || enemy_counter % 2 == 0) {
                        now_enemy = datahub.Enemy[3] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_1");
                    }
                    else {
                        now_enemy = datahub.Enemy[2] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_2");
                    }
                }
                // ~30 Ŭ��1 �Ϻ�1 ����2
                else if (now_round > 25 && now_round < 30) {
                    if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[4] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_2");
                    }
                    else if (enemy_counter % 2 == 0) {
                        now_enemy = datahub.Enemy[3] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_1");
                    }
                    else {
                        now_enemy = datahub.Enemy[2] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/clover_2");
                    }
                }
                // ~35 �Ϻ�2 ����2
                else if (now_round > 30 && now_round <= 35) {
                    if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[4] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_2");
                    }
                    else {
                        now_enemy = datahub.Enemy[3] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_1");
                    }
                }
                // ~40 �Ϻ�1 ����2 �ٺ�1
                else if (now_round > 35 && now_round < 40) {
                    if (enemy_counter % 4 == 0) {
                        now_enemy = datahub.Enemy[5] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/diamond_1");
                    }
                    else if (enemy_counter % 3 == 0 || enemy_counter % 2 == 0) {
                        now_enemy = datahub.Enemy[4] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_2");
                    }
                    else {
                        now_enemy = datahub.Enemy[3] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_1");
                    }
                }
                // ~45 �Ϻ� 1 ����1 �ٺ�2
                else if (now_round > 40 && now_round <= 45) {
                    if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[5] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/diamond_1");
                    }
                    else if (enemy_counter % 2 == 0) {
                        now_enemy = datahub.Enemy[4] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_2");
                    }
                    else {
                        now_enemy = datahub.Enemy[3] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_1");
                    }
                }
                // ~50 ����2 �ٺ�2
                else if (now_round > 45 && now_round < 50) {
                    if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[5] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/diamond_1");
                    }
                    else {
                        now_enemy = datahub.Enemy[4] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_2");
                    }
                }
                // ~55 ����1 �ٺ�2 ����1
                else if (now_round > 50 && now_round <= 55) {
                    if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[6] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/diamond_2");
                    }
                    else if (enemy_counter % 2 == 0) {
                        now_enemy = datahub.Enemy[5] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/diamond_1");
                    }
                    else {
                        now_enemy = datahub.Enemy[4] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/heart_2");
                    }
                }
                // ~60 �ٺ�2 ����2
                else if (now_round > 55 && now_round < 60) {
                    if (enemy_counter % 4 == 0 || enemy_counter % 3 == 0) {
                        now_enemy = datahub.Enemy[6] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/diamond_2");
                    }
                    else {
                        now_enemy = datahub.Enemy[5] as Enemy;
                        enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/diamond_1");
                    }
                }
                speed = now_enemy.Speed * 0.003f;
                enemy.GetComponent<EnemyMove2>().Speed = speed;
                break;
        }

        // ü�� ����
        // -> ���̵��� ���� �߰� ��ȭ
        int difficulty = datahub.Difficulty;
        int health = now_enemy.Health + (now_round-1) * now_enemy.UpgradeValue + (difficulty-1) * now_enemy.UpgradeValue;
        enemy.transform.Find("Canvas").Find("Hp").GetComponent<EnemyHp>().Hp = health;
        enemy.transform.Find("Canvas").Find("Hp").GetComponent<EnemyHp>().Maxhp = health;
        
        enemy_counter++;
        //Debug.Log(enemy_counter);
    }

    // ���� ��ȯ
    private void BossSpawn() {
        int boss_round = datahub.RoundNumber / 10;
        GameObject enemy = Instantiate(_enemy, ori, Quaternion.identity);
        enemy.transform.SetParent(transform);
        enemy.name = "enemy_boss";
        // stage�� ���� ���� �� �޶��� ��
        switch (datahub.StageNumber) {
            case 1:
                Enemy boss_stat = datahub.Enemy[9] as Enemy;
                enemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Enemy/boss_1");
                int health = boss_stat.Health + (boss_round -1) * boss_stat.UpgradeValue + (datahub.Difficulty - 1) * boss_stat.UpgradeValue;
                float speed = boss_stat.Speed * 0.003f;
                enemy.transform.Find("Canvas").Find("Hp").GetComponent<EnemyHp>().Hp = health;
                enemy.transform.Find("Canvas").Find("Hp").GetComponent<EnemyHp>().Maxhp = health;
                enemy.AddComponent<EnemyMove1>();
                enemy.GetComponent<EnemyMove1>().Speed = speed;
                enemy.AddComponent<BossDrop>();
                break;
        }
    }
    public void NextSpawn() {
        enemy_counter = 1;
        round++;
        StartCoroutine("Spawn");
    }

    public void StopSpawn() {
        StopCoroutine("Spawn");
    }
}
