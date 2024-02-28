using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // ���� ���ư� Ÿ��
    public GameObject target;

    // �� ������ ���� ������
    public int damage;
    
    // ���ư� �ӵ�
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
            // target �� ������� ������ ��ġ���� �̵� �� ����
            transform.position = Vector3.MoveTowards(gameObject.transform.position, arrivePoint, speed);
            if ( transform.position == arrivePoint ) {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // target�� �浹�ϸ�
        if (collision.name == targetName) {
            // ������ ���ϱ�
            collision.gameObject.GetComponent<EnemyHit>().Damage = damage;

            // �� bullet ����
            Destroy(gameObject);
        }
    }
}
