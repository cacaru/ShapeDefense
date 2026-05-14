using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;


/// <summary>
/// 적 이동 지정 스크립트 7
/// 맵에 따라 이동을 변경할 수 있도록 지정하기
/// </summary>

public class EnemyMove8 : EnemyMovement {

    // true -> 왼쪽 필드
    // false -> 오른쪽 필드
    private bool field_selecter;
    private bool only_horizental = false;
    private bool only_vertical = false;

    public EnemyMove8(Vector2 ori, Vector2 move_pos, bool field_selecter) {
        this.ori = ori;
        this.move_pos = move_pos;
        this.field_selecter = field_selecter;

        dis = 0.405f;
    }

    protected override void BifurcationChecker() {
        
        // 하단일 때
        if (field_selecter) {
            // 기본 우측->아래
            if (ori == move_pos) {
                horizental = false;
                vertical = true;
            }
            // 좌 상
            if(Mathf.Approximately(move_pos.x , ori.x + (dis * 12)) && Mathf.Approximately(move_pos.y, ori.y - dis * 5)) {
                horizental = true;
                vertical = false;
            }
            // 위로만 가야함
            if (Mathf.Approximately(move_pos.x, ori.x + (dis * 4)) && Mathf.Approximately(move_pos.y, ori.y - dis)) {
                only_horizental = true;
                only_vertical = false;
            }
            // 좌 상으로 돌리기
            if (Mathf.Approximately(move_pos.x, ori.x + (dis * 4)) && Mathf.Approximately(move_pos.y, ori.y + dis * 2)) {
                only_horizental = false;
                only_vertical = false;
            }

            // 좌 하
            if (Mathf.Approximately(move_pos.x , ori.x) && Mathf.Approximately(move_pos.y , ori.y + dis * 4)) {
                horizental = false;
            }
        }

        // 상단일 때
        else {
            // 우 / 상 
            if (ori == move_pos) {
                horizental = true;
                vertical = true;
            }
            // 좌 / 하
            if (Mathf.Approximately(move_pos.x, ori.x + dis * 12) && Mathf.Approximately(move_pos.y, ori.y + dis * 5)) {
                horizental = false;
                vertical = false;
            }

            // 아래로만 가야함
            if (Mathf.Approximately(move_pos.x, ori.x + (dis * 4)) && Mathf.Approximately(move_pos.y, ori.y + dis)) {
                only_horizental = true;
                only_vertical = false;
            }
            // 좌 하로 돌리기
            if (Mathf.Approximately(move_pos.x, ori.x + (dis * 4)) && Mathf.Approximately(move_pos.y, ori.y - dis * 2)) {
                only_horizental = false;
                only_vertical = false;
            }

            // 좌 / 상
            if (Mathf.Approximately(move_pos.x , ori.x) && Mathf.Approximately(move_pos.y, ori.y - (dis * 4))) {
                horizental = true;
                vertical = false;
            }
        }
    }

    private Vector2 tmp = new(0, 0);
    public override Vector2 MoveResult(Vector2 pos, float speed, int speed_rate) {
        // 좌우 우선 체크
        if (vertical && !only_horizental) {
            tmp.x = move_pos.x + dis; tmp.y = move_pos.y;
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetTile(map.WorldToCell(hit.point));
            //string name = tile.name;
            //Debug.Log("EnemyMove3 >> check_right > " + name);
            if (tile.name.Contains("gray")) {
                pos.x += speed * speed_rate;
                if (pos.x >= move_pos.x + dis) {
                    pos.x = move_pos.x + dis;
                    move_pos.x = pos.x;
                }
                BifurcationChecker();
                return pos;
            }

        }
        else if(!vertical && !only_horizental){
            tmp.x = move_pos.x - dis; tmp.y = move_pos.y;
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetTile(map.WorldToCell(hit.point));
            //Debug.Log(tile.name);
            if (tile.name.Contains("gray")) {
                pos.x -= speed * speed_rate;
                if (pos.x <= move_pos.x - dis) {
                    pos.x = move_pos.x - dis;
                    move_pos.x = pos.x;
                }
                BifurcationChecker();
                return pos;
            }

        }

        // 상하 이동
        if (horizental && !only_vertical) {
            tmp.x = move_pos.x; tmp.y = move_pos.y + dis;
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetTile(map.WorldToCell(hit.point));
            //string name = tile.name;
            //Debug.Log("hit : " + hit.point + "top : " + name);
            if (tile.name.Contains("gray")) {
                pos.y += speed * speed_rate;
                if (pos.y >= move_pos.y + dis) {
                    pos.y = move_pos.y + dis;
                    move_pos.y = pos.y;
                }
                BifurcationChecker();
                return pos;
            }

        }
        else if(!horizental && !only_vertical){
            tmp.x = move_pos.x; tmp.y = move_pos.y - dis;
            hit = Physics2D.Raycast(tmp, Vector2.zero, 0f);
            tile = map.GetTile(map.WorldToCell(hit.point));
            //string name = tile.name;
            if (tile.name.Contains("gray")) {
                pos.y -= speed * speed_rate;
                if (pos.y <= move_pos.y - dis) {
                    pos.y = move_pos.y - dis;
                    move_pos.y = pos.y;
                }
                BifurcationChecker();
                return pos;
            }
        }
        //Debug.Log("lost pos");
        // 아무 방향도 찾지못했다면 일시정지
        return pos;
    }

}
