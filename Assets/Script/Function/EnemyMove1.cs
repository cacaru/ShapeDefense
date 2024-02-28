using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �� �̵� ���� ��ũ��Ʈ
/// �ʿ� ���� �̵��� ������ �� �ֵ��� �����ϱ�
/// </summary>

public class EnemyMove1 : MonoBehaviour
{
    private Vector2 root_point;
    private Vector2 first_point = new Vector2(0, -3.6f);
    private Vector2 second_point = new Vector2(3.6f, -3.6f);
    private Vector2 third_point = new Vector2(3.6f, 0f);

    private bool first_checker = false;
    private bool second_checker = false;
    private bool third_checker = false; 

    [SerializeField]
    private float speed = 0.005f;
    // Start is called before the first frame update
    void Start()
    {
        root_point = this.transform.position;
        first_point = root_point + first_point;
        second_point = root_point + second_point;
        third_point = root_point + third_point;
    }

    // Update is called once per frame
    void Update() {
        // root -> first
        if ( !first_checker && !second_checker && !third_checker) {
            if (this.transform.position.y > first_point.y) {
                Vector2 now = this.transform.position;
                now.y -= speed;
                if (now.y <= first_point.y) {
                    now = first_point;
                    first_checker = true;
                }
                this.transform.position = now;
            }
        }
        // first -> second
        else if ( first_checker && !second_checker && !third_checker) {
            if (this.transform.position.x < second_point.x) {
                Vector2 now = this.transform.position;
                now.x += speed;
                if (now.x >= second_point.x) {
                    now = second_point;
                    second_checker = true;
                }
                this.transform.position = now;
            }
        }
        // second -> third
        else if ( first_checker && second_checker && !third_checker) {
            if (this.transform.position.y < third_point.y) {
                Vector2 now = this.transform.position;
                now.y += speed;
                if (now.y >= third_point.y) {
                    now = third_point;
                    third_checker = true;
                }
                this.transform.position = now;
            }
        }
        // third -> root
        else if ( first_checker && second_checker && third_checker) {
            if (this.transform.position.x > root_point.x) {
                Vector2 now = this.transform.position;
                now.x -= speed;
                if (now.x <= root_point.x) {
                    now = root_point;
                    // �ٽ� first�� �����ϹǷ� ������ üĿ�� false �� ����
                    first_checker = false;
                    second_checker = false;
                    third_checker = false;
                }
                this.transform.position = now;
            }
        }
    }

    // speed �� �ܺο��� ������ �� �ְ� property ����
    public float Speed { get { return speed; } set {  speed = value; } }
}
