using UnityEngine;

/// <summary>
/// 적 이동 지정 스크립트 2
/// 맵에 따라 이동을 변경할 수 있도록 지정하기
/// </summary>

public class EnemyMove2 : EnemyMovement 
{
    private bool is_top;

    public EnemyMove2(Vector2 ori, Vector2 move_pos, bool is_top) {
        this.ori = ori;
        this.move_pos = move_pos;
        this.is_top = is_top;
        dis = 0.51f;
    }

    protected override void BifurcationChecker() {
        float tmp_x , tmp_y;
        /*
         * 윗길
         * 원점 -> 상 / 좌
         * point1 -> 상 / 우
         * point2 -> 하 / 우
         * point3 -> 하 / 좌
         */
        if (is_top) {
            if(ori == move_pos) {
                vertical = false;
                horizental = true;
            }
            tmp_x = ori.x;
            tmp_y = ori.y + (dis * 5);
            // point1
            if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
                vertical = true;
                horizental = true;
            }
            tmp_x = ori.x + (dis * 8);
            tmp_y = ori.y + (dis * 8);
            // point2
            if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
                vertical = true;
                horizental = false;
            }
            tmp_x = ori.x + (dis * 8);
            tmp_y = ori.y + (dis * 6);
            // point3
            if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
                vertical = false;
                horizental = false;
            }
        }
        /*
         *  아랫길
          원점 -> 하 / 좌
          point1 -> 하 / 우
          point2 -> 상 / 우
          point3 -> 상 / 좌
          point4 -> 하 / 좌
         */
        else {
            if (ori == move_pos) {
                vertical = false;
                horizental = false;
            }
            tmp_x = ori.x;
            tmp_y = ori.y - (dis * 2);
            // point1
            if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
                vertical = true;
                horizental = false;
            }
            tmp_x = ori.x + (dis * 6);
            tmp_y = ori.y - (dis * 2);
            // point2
            if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
                vertical = true;
                horizental = true;
            }
            tmp_x = ori.x + (dis * 9);
            tmp_y = ori.y + (dis * 3);
            // point3
            if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
                vertical = false;
                horizental = true;
            }
            tmp_x = ori.x + (dis * 8);
            tmp_y = ori.y + (dis * 6);
            // point4
            if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
                vertical = false;
                horizental = false;
            }
        }
    }
}
