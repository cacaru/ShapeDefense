using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 적 이동 지정 스크립트 6
/// 맵에 따라 이동을 변경할 수 있도록 지정하기
/// </summary>

public class EnemyMove6 : EnemyMovement {

    // 이동 방향 결정
    private bool random_switch = false;              // true :: up, false :: down

    public EnemyMove6(Vector2 ori, Vector2 move_pos) {
        this.ori = ori;
        this.move_pos = move_pos;
        dis = 0.51f;
    }

    protected override void BifurcationChecker() {

        // 원점일때 좌 -> 하
        // point1 == 우 , 하 ( 랜덤 )
        // point2 == 좌 -> 하
        // point3 == 우 -> 하
        // point4 == 우 -> 상
        // point5 == 좌 , 상 ( 랜덤 )
        // point6 == 우 -> 상
        // point7 == 좌 -> 상
        float tmp_x;
        float tmp_y;
        if (ori == move_pos) {
            horizental = false;
            vertical = false;
        }
        tmp_x = ori.x;
        tmp_y = ori.y - (dis * 2);
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            SwitchControll();
            vertical = true;
        }
        tmp_x = ori.x + (dis * 2);
        tmp_y = ori.y - (dis * 5);
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            random_switch = false;
            vertical = false;
        }

        tmp_x = ori.x + dis;
        tmp_y = ori.y - (dis * 6);
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            random_switch = true;
        }

        tmp_y = ori.y - (dis * 7);
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            random_switch = false;
        }

        tmp_x = ori.x;
        tmp_y = ori.y - (dis * 8);
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            // 스위치 끄기
            random_switch = false;
            vertical = true;
        }
        tmp_x = ori.x + dis;
        tmp_y = ori.y - (dis * 9);
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            horizental = true;
        }
        tmp_x = ori.x + (dis * 7);
        tmp_y = ori.y - (dis * 8);
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            SwitchControll();
            vertical = false;
        }
        tmp_x = ori.x + (dis * 5);
        tmp_y = ori.y - (dis * 4);
        if ((Mathf.Approximately(move_pos.x, tmp_x) ||
             Mathf.Approximately(move_pos.x, tmp_x + dis * 2)) &&
             Mathf.Approximately(move_pos.y, tmp_y)) {
            // 스위치 끄기
            random_switch = false;
            vertical = true;
        }

        tmp_x = ori.x + (dis * 6);
        tmp_y = ori.y - (dis * 3);
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            // 스위치 끄기
            random_switch = true;
        }
        tmp_y = ori.y - (dis * 2);
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            random_switch = false;
        }
        tmp_x = ori.x + dis * 7;
        tmp_y = ori.y - dis;
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            vertical = false;
        }
    }

    private void SwitchControll() {
        int r = Random.Range(0, 100);
        if (r % 2 == 0) {
            random_switch = true;
        }
        else {
            random_switch = false;
        }
        //Debug.Log(random_switch);
    }

    /// <summary>
    ///  이동 위치 계산 함수
    /// </summary>
    /// <param name="pos">현재 위치</param>
    /// <param name="speed">이동 속도</param>
    /// <param name="speed_rate">속도 배율</param>
    public override Vector2 MoveResult(Vector2 pos, float speed, int speed_rate) {

        // 좌우 우선 체크
        if (vertical && !random_switch) {
            Vector2 tmp = new(move_pos.x + dis, move_pos.y);
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetComponent<Tilemap>().GetTile(map.WorldToCell(hit.point));
            string name = tile.name;
            //Debug.Log("EnemyMove3 >> check_right > " + name);
            if (name.Contains("gray")) {
                pos.x += speed * speed_rate;
                if (pos.x >= move_pos.x + dis) {
                    pos.x = move_pos.x + dis;
                    move_pos.x = pos.x;
                    BifurcationChecker();
                }
                return pos;
            }

        }
        else if (!vertical && !random_switch) {
            Vector2 tmp = new(move_pos.x - dis, move_pos.y);
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetComponent<Tilemap>().GetTile(map.WorldToCell(hit.point));
            string name = tile.name;
            //Debug.Log(name);
            if (name.Contains("gray")) {
                pos.x -= speed * speed_rate;
                if (pos.x <= move_pos.x - dis) {
                    pos.x = move_pos.x - dis;
                    move_pos.x = pos.x;
                    BifurcationChecker();
                }
                return pos;
            }

        }

        // 상하 이동
        if (horizental) {
            Vector2 tmp = new(move_pos.x, move_pos.y + dis);
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetComponent<Tilemap>().GetTile(map.WorldToCell(hit.point));
            string name = tile.name;
            //Debug.Log(name);
            if (name.Contains("gray")) {
                pos.y += speed * speed_rate;
                if (pos.y >= move_pos.y + dis) {
                    pos.y = move_pos.y + dis;
                    move_pos.y = pos.y;
                    BifurcationChecker();
                }
                return pos;
            }

        }
        else {
            Vector2 tmp = new(move_pos.x, move_pos.y - dis);
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetTile(map.WorldToCell(hit.point));
            string name = tile.name;
            //Debug.Log(name);
            if (name.Contains("gray")) {
                pos.y -= speed * speed_rate;
                if (pos.y <= move_pos.y - dis) {
                    pos.y = move_pos.y - dis;
                    move_pos.y = pos.y;
                    BifurcationChecker();
                }
                return pos;
            }
        }


        // 아무 방향도 찾지못했다면 일시정지
        return pos;
    }
}
