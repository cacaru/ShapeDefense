using UnityEngine;

/// <summary>
/// 적 이동 지정 스크립트 3
/// 맵에 따라 이동을 변경할 수 있도록 지정하기
/// </summary>

public class EnemyMove3 : EnemyMovement 
{

    private bool in_up = false;

    public EnemyMove3(Vector2 ori, Vector2 move_pos) {
        this.ori = ori;
        this.move_pos = move_pos;
        dis = 0.554f;
    }

    protected override void BifurcationChecker() {
        /*
         * 원점 -> 하 / 좌
         * 1 -> 하 / 우
         * 2 -> 하 / 좌
         * 3 -> 상 / 좌
         * 4 -> 상 / 우
         * 5 -> 상 / 좌
         * */
        float tmp_x = ori.x;
        float tmp_y;
        if (ori == move_pos) {
            vertical = false;
            horizental = false;
            in_up = false;
        }
        tmp_y = ori.y - (dis * 4);
        // point 1
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y) && !in_up) {
            vertical = true;
            horizental = false;
        }

        tmp_x = ori.x + (dis * 8);
        tmp_y = ori.y - (dis * 8);
        //point 2
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            vertical = false;
            horizental = false;
        }
        tmp_x = ori.x;
        tmp_y = ori.y - (dis * 8);
        // point 3
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            vertical = false;
            horizental = true;
        }

        tmp_x = ori.x;
        tmp_y = ori.y - (dis * 6);
        // point4
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            vertical = true;
            horizental = true;
            in_up = true;
        }

        tmp_x = ori.x + (dis * 8);
        tmp_y = ori.y;
        //point 5
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            vertical = false;
            horizental = true;
        }
    }

}
