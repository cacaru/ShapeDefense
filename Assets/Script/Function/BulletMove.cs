using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // 향해 나아갈 타겟
    public GameObject target;

    // 이 공격이 가질 데미지
    public int damage;
    
    // 나아갈 속도
    private readonly float speed = 0.02f;

    private Vector3 arrivePoint;
    private string targetName;

    // Start is called before the first frame update
    void Start()
    {
        arrivePoint = target.transform.position;
        targetName = target.name;
    }

    // Update is called once per frame
    void Update()
    {
        if ( target != null) {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, speed);
            arrivePoint = target.transform.position;
        }
        else {
            // target 이 사라지면 마지막 위치까지 이동 후 삭제
            transform.position = Vector3.MoveTowards(gameObject.transform.position, arrivePoint, speed);
            if ( transform.position == arrivePoint ) {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // target에 충돌하면
        if (collision.name == targetName) {
            // 데미지 가하기
            collision.gameObject.GetComponent<EnemyHit>().Damage = damage;

            // 이 bullet 삭제
            Destroy(gameObject);
        }
    }
}
