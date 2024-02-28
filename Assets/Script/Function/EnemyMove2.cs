using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 적 이동 지정 스크립트 2
/// 맵에 따라 이동을 변경할 수 있도록 지정하기
/// </summary>

public class EnemyMove2 : MonoBehaviour {

    private Tilemap map;
    
    [SerializeField]
    private float speed = 0.005f;

    // 기본 위치
    private Vector2 ori;
    private Vector2 move_pos;
    // 이동 거리 
    private float dis = 0.512f;
    // 이동 방향 결정
    private bool upordown = false;              // true :: up, false :: down

    // 체크할 변수들
    private RaycastHit2D hit;
    private TileBase tile;

    // 이동 방향 제어
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
    // 이동은 하단/상단 -> 좌/우 살피기
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
        // 최상단부터 시작되는 것이라면 up과 right을 잠굼
        //if (pos.y >= 3.743f && pos.y <= 3.213f) {
        if (up_checker) { 
            up = false;
            right = false;
        }
        // 우선 위쪽 살피기
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
                //더이상 위로 향하지 못함 -> 재탐색
                up = false;
            }
        }

        // 우측
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
                //더이상 우측으로 향하지 못함
                right = false;
            }
        }

        // 아래
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

                // 한 번이라도 아래로 진행을 시작했다면 up을 잠시 봉인
                up_checker = true;                
                return pos;

            }
            else {
                //더이상 아래로 향하지 못함
                down = false;
            }
        }

        // 왼쪽
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
        // 아무 방향도 찾지못했다면 일시정지
        return pos;
    }

    private Vector2 MoveDownWay(Vector2 pos, bool up, bool down, bool right) {
        if (down_checker) {
            down = false;
        }
        // 오른쪽이 봉인되면 다시 내려갈 수 있게됨
        if (right_checker) {
            down_checker = false;
            up = false;
            right = false;
        }

        // 우선 아래 살피기
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
                //더이상 위로 향하지 못함 -> 재탐색
                down = false;
            }
        }

        // 우측
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
                //더이상 우측으로 향하지 못함
                right = false;
            }
        }

        // 위
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

                // 한 번이라도 위로 향하기 시작했다면 down을 봉인
                down_checker = true;
                return pos;
            }
            else {
                //더이상 아래로 향하지 못함
                up = false;
            }
        }

        // 왼쪽
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
        // 아무 방향도 찾지못했다면 일시정지
        return pos;
    }

    // speed 를 외부에서 조절할 수 있게 property 설정
    public float Speed { get { return speed; } set { speed = value; } }
}
