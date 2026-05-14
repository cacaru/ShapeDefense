using System;
using System.Collections;
using UnityEngine;

using static ShapeDefenseSpace.GameData;

public class BulletMove : MonoBehaviour {
    // 향해 나아갈 타겟
    public GameObject target;
    public GameObject Target { 
        set {
            target = value;
            if(target != null) {
                arrivePoint = target.transform.position;
                target_tag = target.GetComponent<EnemyHp>().NameTag;
                target.GetComponent<EnemyHp>().Hid_HP -= damage.damage;

                StartCoroutine(Move());
            }
        } 
    }

    // 이 공격이 가질 데미지
    public Damage damage;

    // 나아갈 속도
    private float speed;

    private Vector3 arrivePoint;
    private int target_tag;

    private BulletPool pool;
    private DieEffectPool dieEffectPool;

    public void SetPool(BulletPool pool, DieEffectPool dieEffectPool) {
        this.pool = pool;
        this.dieEffectPool = dieEffectPool;
    }

    void Start() {
#if UNITY_EDITOR
        speed = 0.1f;
#elif UNITY_ANDROID
        speed = 0.08f;
#else
        speed = 0.1f;
#endif
    }

    IEnumerator Move() {
        while (true) {
            if (target == null) {
                BackBulletPool();
                break;
            }

            if (!datahub.Pause) {
                if (target.CompareTag("Enemy")) {
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * datahub.SpeedRate);
                    arrivePoint = target.transform.position;
                }
                // 타겟이 사라지면 탄환을 바로 반환시키기
                else {
                    // target 이 사라지면 마지막 위치까지 이동 후 삭제
                    //transform.position = Vector3.MoveTowards(transform.position, arrivePoint, speed * datahub.SpeedRate);
                    BackBulletPool();
                    break;
                }

                if (transform.position == arrivePoint) {
                    BackBulletPool();
                    break;
                }
            }

            yield return wff;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        // target에 충돌하면
        if (collision.TryGetComponent<EnemyHp>(out var target_tag_comp)) {
            if(target_tag_comp.NameTag == target_tag) {
                // 데미지 가하기
                collision.gameObject.GetComponent<EnemyHit>().GetDamage(damage);
                //Debug.Log("Target hit");
                // 이 bullet 삭제
                BackBulletPool();
            }
        }
    }

    private void BackBulletPool() {
        StopCoroutine(Move());
        // die effect on
        try {
            dieEffectPool.BulletEffectActive(transform.position);
        }
        catch {
            Debug.Log(dieEffectPool.name);
        }
        
        pool.BackObject(gameObject);
    }

    public void InitMove() {
        target = null;
        target_tag = 0;
        damage = null;
    }

    public void SetPos(Vector2 pos) {
        gameObject.transform.position = pos;
    }
}
