using System.Collections;
using UnityEngine;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;

/// <summary>
/// 몬스터가 맞았을 때 hp 줄이기 및 죽었을 때 인게임 소환재화 및 enemy 삭제 이벤트
/// </summary>
public class EnemyHit : MonoBehaviour {

    private GameObject poison_effect;
    private GameObject blust_effect;
    private GameObject paralysis_effect;

    private bool poisoning = false;
    private bool poisoning_nesting = false;
    private int bloody_counter = 0;

    public float speed = 0;

    void Start() {
        poison_effect = transform.Find("Poison").gameObject;
        blust_effect = transform.Find("Blust").gameObject;
        paralysis_effect = transform.Find("Paralysis").gameObject;
    }

    /// <summary>
    /// 데미지 받기
    /// </summary>
    /// <param name="damage">받을 데미지</param>
    /// <param name="damage_type">공격 타입</param>
    public void GetDamage(Damage damage) {
        // 0 : 기본(단타), -> 모든 상황에서 default
        // 1 : 출혈(10초동안 2초당 등급비례 데미지),
        // 2 : 폭발형 ( 등급비례 반경에 등급비례데미지)

        // 공격을 맞았을 때 데미지 만큼 hp를 줄이기
        DamageCal(damage.damage);

        // 타입에 따른 이벤트 개시
        switch(damage.damage_type) {
            case 7001:
                // 독데미지
                poisoning = true;
                if (!poisoning_nesting) {
                    StartCoroutine(Poisoning(damage.damage));
                }
                else {
                    // 1초 갱신
                    bloody_counter = bloody_counter > 0 ? bloody_counter - 1 : 0;
                }
                break;
            case 7002:
                //폭발 -> 등급에 따라 상승하는 범위에 해당한 모든 Enemy에게 type 0 의 데미지 가산 -> 본인 제외
                // 1회성이므로 바로 실행
                StartCoroutine(Blusting());
                BlustTargetCheck(damage);
                break;

            case 7003:
                // 마비 -> 0.5초 정지
                StartCoroutine(Paralysising());
                StartCoroutine(ParalysisActive());
                break;
            default:
                // do nothing
                break;
        }
        

    }


    // 출혈은 10초간 지속피해를 입힙니다.
    IEnumerator Poisoning(float boolding_damage) {
        bloody_counter = 0;
        while (true) {
            if(poisoning && !datahub.Pause) {
                bloody_counter++;
                // 출혈 이펙트 키기
                StartCoroutine(BloodyEffect());
                DamageCal(boolding_damage);
            }

            if (bloody_counter == 10) {
                bloody_counter = 0;
                poisoning_nesting = false;
                break;
            }
            yield return wfs_1;
        }
    }

    // 폭발피해 범위 및 타겟 지정
    private void BlustTargetCheck(Damage now_damage) {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, .4f, LayerMask.GetMask("Enemy"));

        // 맞은 타겟이 있으면 데미지 가산
        if (hits.Length > 0) {
            Damage damage = new();
            damage.SetDamage(now_damage.damage, 0);
            foreach (var hit in hits) {
                // enemy에게 데미지 가하기
                hit.gameObject.GetComponent<EnemyHit>().GetDamage(damage);
            }
        }
    }

    // 마비로 인한 일시 정지 진행
    IEnumerator ParalysisActive() {
        GetComponent<EnemyMove>().speed = 0f;
        yield return wfs_1;

        GetComponent<EnemyMove>().speed = speed;
    }

    /*
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new(3f, 3f));
    }
    */
    // 출혈피해 이펙트
    IEnumerator BloodyEffect() {
        poison_effect.SetActive(true);
        yield return wfs_2;
        poison_effect.SetActive(false);
    }

    // 폭발피해 이펙트
    IEnumerator Blusting() {
        blust_effect.SetActive(true);
        yield return wfs_4;
        blust_effect.SetActive(false);
    }
    
    // 마비 이펙트
    IEnumerator Paralysising() {
        paralysis_effect.SetActive(true);
        yield return wfs_1;
        paralysis_effect.SetActive(false);
    }



    private void DamageCal(float damage) {
        float hp = gameObject.GetComponent<EnemyHp>().Hp - damage;

        // 가산된 hp가 0 <= 일 경우 
        if (hp <= 0) {
            gameObject.tag = "Dead";
            StopAllCoroutines();
            // collider 끄기
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            // datahub.Dot를 증가시킴
            float val = 2 + datahub.RoundNumber / 10;
            // 처치시 획득 재화 스탯을 찍엇을 시
            if(datahub.User.StatusGainDotLevel > 0) {
                val += val * datahub.User.StatusGainDotLevel * 0.05f;
            }
            datahub.Dot += val;

            // 이 enemy가 보스면 - drop을 활성화
            if (gameObject.name.Equals("enemy_boss")) {
                gameObject.GetComponent<BossDrop>().Drop();
            }
            //Debug.Log("Die");
            //datahub.NowEnemyCounter--;
            // 이 enemy를 제거 
            gameObject.GetComponent<EnemyDie>().Die();
        }
        //아니면 hp를 설정하고 넘김

        gameObject.GetComponent<EnemyHp>().Hp = hp;

    }



    public void PoisonControll(bool value) {
        poisoning = value;
    }
}

