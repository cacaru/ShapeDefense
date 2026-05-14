
using System.Collections;
using UnityEngine;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;

/// <summary>
/// unit attack function
/// default : false
/// active : attack per attack speed from unit data
/// </summary>
public class UnitAttack : MonoBehaviour
{
    [SerializeField] private BulletPool bulletpool;

    // 공격 속도등 정보를 받아올 변수
    private int this_id = 0;
    private Damage damage;
    //private int hp_target_point = 0;
    private float attack_speed;
    [SerializeField] private bool active = false;
    [SerializeField] private bool has_target = false;
    [SerializeField] private bool attacking = false;
    [SerializeField] private GameObject target = null;

    private GameObject Target { 
        set { 
            target = value;
            if(target != null && !attacking && damage.damage > 0) {
                StartCoroutine(Shot());
            }
        } 
    }
    // 공격 범위 설정
    private Vector2 AttackRange = new(0, 0);
    private int enemy_layer;
    private float target_dis = 999f;
    /*
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, AttackRange);
    }
    */
    private Color visible = new(1f,1f,1f,1f);
    // 공격 주기
    private WaitForSeconds wfs;
    public int Id { 
        set {
            // 아이디에 따라 공속과 공격력을 받아옴
            this_id = value;
            attacking = false;
            SetDamage();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy_layer = LayerMask.GetMask("Enemy");
        damage = new();
    }

    
    IEnumerator StartAttack() {
        // 범위 내 적의 수
        int count ;

        while (true) {
            // 공격범위 내 타깃 탐색
            var targets = Physics2D.OverlapBoxAll(transform.position, AttackRange, 0, enemy_layer);
            count = targets.Length;
            if (!datahub.Pause && active && count > 0) {
                // 범위 내 가장 가까운 적을 찾기
                target_dis = Vector3.Distance(transform.position, targets[0].transform.position);
                GameObject tmp_target = targets[0].gameObject;
                foreach (var col in targets) {
                    float dis = Vector3.Distance(transform.position, col.transform.position);
                    if (dis < target_dis) {
                        target_dis = dis;
                        tmp_target = col.gameObject;
                    }
                }
                Target = tmp_target;
                has_target = true;
            }

            if (count == 0) {
                has_target = false;
            }
            yield return attack_frame;
        }
    }

    /*
    // Update is called once per frame
    void Update()
    {
        // 공격범위 내 타깃 탐색
        var targets = Physics2D.OverlapBoxAll(transform.position, AttackRange, 0, enemy_layer);
        count = targets.Length;
        if(!datahub.Pause && active && count > 0) {
            // 범위 내 가장 가까운 적을 찾기
            target_dis = Vector3.Distance(transform.position, targets[0].transform.position);
            GameObject tmp_target = targets[0].gameObject;
            foreach(var col in targets) {
                float dis = Vector3.Distance(transform.position, col.transform.position);
                if (dis < target_dis) {
                    target_dis = dis;
                    tmp_target = col.gameObject;
                }
            }
            Target = tmp_target;
            has_target = true;
        }

        if(count == 0) {
            has_target = false;
        }
    }
    */

    public void SetDamage() {
        // 기존의 공격들을 멈춤
        StopAllCoroutines();
        Target = null;
        if (this_id > 0) {
            var tmp = datahub.Unit_dic[this_id] as Unit;

            // 속도 선 지정
            attack_speed = tmp.AttackSpeed;
            // 공격속도 스탯 찍었을 시
            if(datahub.User.StatusAttackSpeedLevel > 0) {
                attack_speed += datahub.User.StatusAttackSpeedLevel * .05f;
            }
            
            wfs = attack_speed switch {
                1 => attack_speed_1,
                1.05f => attack_speed_1_1,
                1.1f => attack_speed_1_2,
                1.15f => attack_speed_1_3,
                1.2f => attack_speed_1_4,
                1.25f => attack_speed_1_5,
                1.3f => attack_speed_1_6,
                1.35f => attack_speed_1_7,
                1.4f => attack_speed_1_8,
                1.45f => attack_speed_1_9,
                1.5f => attack_speed_1_10,
                2 => attack_speed_2,
                2.05f => attack_speed_2_1,
                2.1f => attack_speed_2_2,
                2.15f => attack_speed_2_3,
                2.2f => attack_speed_2_4,
                2.25f => attack_speed_2_5,
                2.3f => attack_speed_2_6,
                2.35f => attack_speed_2_7,
                2.4f => attack_speed_2_8,
                2.45f => attack_speed_2_9,
                2.5f => attack_speed_2_10,
                3 => attack_speed_3,
                3.05f => attack_speed_3_1,
                3.1f => attack_speed_3_2,
                3.15f => attack_speed_3_3,
                3.2f => attack_speed_3_4,
                3.25f => attack_speed_3_5,
                3.3f => attack_speed_3_6,
                3.35f => attack_speed_3_7,
                3.4f => attack_speed_3_8,
                3.45f => attack_speed_3_9,
                3.5f => attack_speed_3_10,
                4 => attack_speed_4,
                4.05f => attack_speed_4_1,
                4.1f => attack_speed_4_2,
                4.15f => attack_speed_4_3,
                4.2f => attack_speed_4_4,
                4.25f => attack_speed_4_5,
                4.3f => attack_speed_4_6,
                4.35f => attack_speed_4_7,
                4.4f => attack_speed_4_8,
                4.45f => attack_speed_4_9,
                4.5f => attack_speed_4_10,
                5 => attack_speed_5,
                5.05f => attack_speed_5_1,
                5.1f => attack_speed_5_2,
                5.15f => attack_speed_5_3,
                5.2f => attack_speed_5_4,
                5.25f => attack_speed_5_5,
                5.3f => attack_speed_5_6,
                5.35f => attack_speed_5_7,
                5.4f => attack_speed_5_8,
                5.45f => attack_speed_5_9,
                5.5f => attack_speed_5_10,
                6 => attack_speed_6,
                6.05f => attack_speed_6_1,
                6.1f => attack_speed_6_2,
                6.15f => attack_speed_6_3,
                6.2f => attack_speed_6_4,
                6.25f => attack_speed_6_5,
                6.3f => attack_speed_6_6,
                6.35f => attack_speed_6_7,
                6.4f => attack_speed_6_8,
                6.45f => attack_speed_6_9,
                6.5f => attack_speed_6_10,
                7 => attack_speed_7,
                7.05f => attack_speed_7_1,
                7.1f => attack_speed_7_2,
                7.15f => attack_speed_7_3,
                7.2f => attack_speed_7_4,
                7.25f => attack_speed_7_5,
                7.3f => attack_speed_7_6,
                7.35f => attack_speed_7_7,
                7.4f => attack_speed_7_8,
                7.45f => attack_speed_7_9,
                7.5f => attack_speed_7_10,
                _ => attack_speed_1
            };

            float tmp_damage = tmp.Attack + (tmp.UpgradeFigures * tmp.UpgradeValue);
            // 공격력 스탯 찍었을 시
            if(datahub.User.StatusAttackLevel > 0) {
                tmp_damage += tmp_damage * datahub.User.StatusAttackLevel * 0.05f;
            }
            // 스테이지 한정 강화도에 따른 강화 추가
            tmp_damage += tmp.Grade switch {
                "E" => datahub.UpgradeValueE * tmp.UpgradeFigures,
                "D" => datahub.UpgradeValueD * tmp.UpgradeFigures,
                "C" => datahub.UpgradeValueC * tmp.UpgradeFigures,
                "B" => datahub.UpgradeValueB * tmp.UpgradeFigures,
                "A" => datahub.UpgradeValueA * tmp.UpgradeFigures,
                "S" => datahub.UpgradeValueS * tmp.UpgradeFigures,
                _ => 0
            };

            // 공격 범위 설정
            int attack_range = tmp.Grade switch {
                "E" or "D" => 1,
                "C" or "B" => 2,
                "A" or "S" => 3,
                _ => 0
            };
            AttackRange.x = attack_range;
            AttackRange.y = attack_range;
            
            // 공격 가능 개체인지 확인
            if (tmp_damage > 0 && attack_speed > 0) {
                // 가능하면 타입에 따라 데미지 재 설정
                int tmp_type = GetComponent<Field>().Type;
                tmp_damage = tmp_type switch {
                    7001 => tmp_damage / 2,
                    7002 => tmp_damage / 3,
                    7003 => tmp_damage / 2,
                    _ => tmp_damage,
                };
                
                damage.SetDamage(tmp_damage, tmp_type);
                attacking = false;
                active = true;
                StartCoroutine(StartAttack());
            }
        }
        else {
            damage.Init();
            attack_speed = 0;
            attacking = false;
            wfs = null;
            //do nothing
            active = false;
            StopAllCoroutines();
        }
    }

    IEnumerator Shot() {
        attacking = true;
        while (true) {
            yield return wfs;
            if(has_target && active && !datahub.Pause) {
                // pooling 으로부터 받아옴
                GameObject tmp = bulletpool.GetObject(gameObject, target, damage);
                ImageSetting(this_id, tmp);
                //target.GetComponent<EnemyHp>().Hid_HP -= damage;
                tmp.GetComponent<SpriteRenderer>().color = visible;
            }
            if (!active) break;
        }
    }    

    public void StopShot() {
        StopCoroutine(Shot());
    }


    public void ShotControll(bool value) {
        active = value;
        if (active) SetDamage();
    }

    private void ImageSetting(int id, GameObject bullet) {
        Sprite path = datahub.Unit_dic[id].Type switch {
            "C" => cir_bullet,
            "T" => tri_bullet,
            "SQ" => squ_bullet,
            "ST" => sta_bullet,
            "M" => moo_bullet,
            "SU" => sun_bullet,
            _ => cir_bullet
        };

        bullet.GetComponent<SpriteRenderer>().sprite = path;
    }
    
}
