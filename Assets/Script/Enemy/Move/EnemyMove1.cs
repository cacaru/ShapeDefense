using UnityEngine;

/// <summary>
/// 적 이동 지정 스크립트
/// 맵에 따라 이동을 변경할 수 있도록 지정하기
/// </summary>
public class EnemyMove1 : EnemyMovement 
{
    public EnemyMove1(Vector2 ori, Vector2 move_pos) {
        this.ori = ori;
        this.move_pos = move_pos;
        dis = 0.51f;
    }

    protected override void BifurcationChecker() {
        // 원점 -> 하/ 좌
        // point 1 -> 하 / 우
        // point 2 -> 상 / 우
        // point 3 -> 상 / 좌
        float tmp_x = ori.x;
        float tmp_y;
        if (ori == move_pos) {
            vertical = false;
            horizental = false;
        }
        // point 1
        tmp_y = ori.y - (dis * 6);
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            vertical = true;
            horizental = false;
        }

        //point 2
        tmp_x = ori.x + (dis * 6);
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            vertical = true;
            horizental = true;
        }

        // point 3
        tmp_y = ori.y;
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            vertical = false;
            horizental = true;
        }
    }
}
