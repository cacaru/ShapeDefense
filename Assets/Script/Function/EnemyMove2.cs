using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// �� �̵� ���� ��ũ��Ʈ 2
/// �ʿ� ���� �̵��� ������ �� �ֵ��� �����ϱ�
/// </summary>

public class EnemyMove2 : MonoBehaviour {

    private Tilemap map;
    
    [SerializeField]
    private float speed = 0.005f;

    // �⺻ ��ġ
    private Vector2 ori;
    private Vector2 move_pos;
    // �̵� �Ÿ� 
    private float dis = 0.512f;
    // �̵� ���� ����
    private bool upordown = false;              // true :: up, false :: down

    // üũ�� ������
    private RaycastHit2D hit;
    private TileBase tile;

    // �̵� ���� ����
    private bool up_checker = false;
    private bool down_checker = false;
    private bool right_checker = false;

    private Vector2 next;

    // Start is called before the first frame update
    void Start() {
        map = GameObject.Find("Grid").transform.Find("Map").GetComponent<Tilemap>();
        ori = transform.position;
        move_pos = transform.position;
        int upordown_val = Random.Range(0, 2);

        if (upordown_val == 0) upordown = true;
        else upordown = false;
    }

    // Update is called once per frame
    // �̵��� �ϴ�/��� -> ��/�� ���Ǳ�
    void Update() {
        // up
        if (upordown) {
            next = MoveUpWay(transform.position, true, true, true);
            transform.position = next;
        }
        // down
        else {
            next = MoveDownWay(transform.position, true, true, true);
            transform.position = next;
        }

        UpOrDownChecker();
    }

    private void UpOrDownChecker() {
        if (transform.position.x == ori.x && transform.position.y == ori.y) {
            up_checker = false;
            down_checker = false;
            right_checker = false;
            int upordown_val = Random.Range(0, 2);

            if (upordown_val == 0) upordown = true;
            else upordown = false;
        }
    }

    private Vector2 MoveUpWay(Vector2 pos, bool up, bool down, bool right) {
        // �ֻ�ܺ��� ���۵Ǵ� ���̶�� up�� right�� ���
        //if (pos.y >= 3.743f && pos.y <= 3.213f) {
        if (up_checker) { 
            up = false;
            right = false;
        }
        // �켱 ���� ���Ǳ�
        if (up) {
            Vector2 tmp = new( move_pos.x, move_pos.y + dis );
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetComponent<Tilemap>().GetTile(map.WorldToCell(hit.point));
            string name = tile.name;
            Debug.Log(name);
            if (name.Contains("gray")) {
                pos.y += speed;
                if(pos.y >= move_pos.y + dis) {
                    pos.y = move_pos.y + dis;
                    move_pos.y = pos.y;
                }
                return pos;
            }
            else {
                //���̻� ���� ������ ���� -> ��Ž��
                up = false;
            }
        }

        // ����
        if (!up && right) {
            Vector2 tmp = new(move_pos.x + dis, move_pos.y);
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetComponent<Tilemap>().GetTile(map.WorldToCell(hit.point));
            string name = tile.name;
            Debug.Log(name);
            if (name.Contains("gray")) {
                pos.x += speed;
                if(pos.x >= move_pos.x + dis) {
                    pos.x = move_pos.x + dis;
                    move_pos.x = pos.x;
                }
                return pos;
            }
            else {
                //���̻� �������� ������ ����
                right = false;
            }
        }

        // �Ʒ�
        if (!up && !right && down) {
            Vector2 tmp = new(move_pos.x, move_pos.y - dis);
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetComponent<Tilemap>().GetTile(map.WorldToCell(hit.point));
            string name = tile.name;
            Debug.Log("down target is " + name);
            if (name.Contains("gray")) {
                pos.y -= speed;
                if (pos.y <= move_pos.y - dis) {
                    pos.y = move_pos.y - dis;
                    move_pos.y = pos.y;
                }

                // �� ���̶� �Ʒ��� ������ �����ߴٸ� up�� ��� ����
                up_checker = true;                
                return pos;

            }
            else {
                //���̻� �Ʒ��� ������ ����
                down = false;
            }
        }

        // ����
        if (!up && !right && !down) {
            Vector2 tmp = new(move_pos.x - dis, move_pos.y);
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetComponent<Tilemap>().GetTile(map.WorldToCell(hit.point));
            string name = tile.name;
            Debug.Log(name);
            if (name.Contains("gray")) {
                pos.x -= speed;
                if (pos.x <= move_pos.x - dis) {
                    pos.x = move_pos.x - dis;
                    move_pos.x = pos.x;
                }
                return pos;
            }
        }
        // �ƹ� ���⵵ ã�����ߴٸ� �Ͻ�����
        return pos;
    }

    private Vector2 MoveDownWay(Vector2 pos, bool up, bool down, bool right) {
        if (down_checker) {
            down = false;
        }
        // �������� ���εǸ� �ٽ� ������ �� �ְԵ�
        if (right_checker) {
            down_checker = false;
            up = false;
            right = false;
        }

        // �켱 �Ʒ� ���Ǳ�
        if (down) {
            Vector2 tmp = new(move_pos.x, move_pos.y - dis);
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetComponent<Tilemap>().GetTile(map.WorldToCell(hit.point));
            string name = tile.name;
            Debug.Log(name);
            if (name.Contains("gray")) {
                pos.y -= speed;
                if (pos.y <= move_pos.y - dis) {
                    pos.y = move_pos.y - dis;
                    move_pos.y = pos.y;
                }
                return pos;
            }
            else {
                //���̻� ���� ������ ���� -> ��Ž��
                down = false;
            }
        }

        // ����
        if (!down && right) {
            Vector2 tmp = new(move_pos.x + dis, move_pos.y);
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetComponent<Tilemap>().GetTile(map.WorldToCell(hit.point));
            string name = tile.name;
            Debug.Log(name);
            if (name.Contains("gray")) {
                pos.x += speed;
                if (pos.x >= move_pos.x + dis) {
                    pos.x = move_pos.x + dis;
                    move_pos.x = pos.x;
                }
                return pos;
            }
            else {
                //���̻� �������� ������ ����
                right = false;
            }
        }

        // ��
        if (!down && !right && up) {
            Vector2 tmp = new(move_pos.x, move_pos.y + dis);
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetComponent<Tilemap>().GetTile(map.WorldToCell(hit.point));
            string name = tile.name;
            if (name.Contains("gray")) {
                pos.y += speed;
                if (pos.y >= move_pos.y + dis) {
                    pos.y = move_pos.y + dis;
                    move_pos.y = pos.y;
                }

                // �� ���̶� ���� ���ϱ� �����ߴٸ� down�� ����
                down_checker = true;
                return pos;
            }
            else {
                //���̻� �Ʒ��� ������ ����
                up = false;
            }
        }

        // ����
        if (!down && !right && !up) {
            Vector2 tmp = new(move_pos.x - dis, move_pos.y);
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetComponent<Tilemap>().GetTile(map.WorldToCell(hit.point));
            string name = tile.name;
            Debug.Log("left tile is " + name);
            if (name.Contains("gray")) {
                pos.x -= speed;
                if (pos.x <= move_pos.x - dis) {
                    pos.x = move_pos.x - dis;
                    move_pos.x = pos.x;
                    if (pos.x <= 1.93f) {
                        right_checker = true;
                    }
                }
                
                return pos;
            }
        }
        // �ƹ� ���⵵ ã�����ߴٸ� �Ͻ�����
        return pos;
    }

    // speed �� �ܺο��� ������ �� �ְ� property ����
    public float Speed { get { return speed; } set { speed = value; } }
}
