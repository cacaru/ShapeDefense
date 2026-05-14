using UnityEngine;


/// <summary>
/// 적 이동 지정 스크립트 4
/// 맵에 따라 이동을 변경할 수 있도록 지정하기
/// </summary>

public class EnemyMove4 : EnemyMovement {

    public EnemyMove4(Vector2 ori, Vector2 move_pos) {
        this.ori = ori;
        this.move_pos = move_pos;
        dis = 0.51f;
    }

    protected override void BifurcationChecker() {
        // 좌측 우선 -> 아래로
        // point1 좌 -> 우
        // point2 하 -> 상
        // point3 우 -> 좌
        // 원점일 때 올라감 -> 내려감으로 변경
        
        if(ori == move_pos) {
            horizental = false;
            vertical = false;
        }
        float tmp_x = ori.x - (dis * 3);
        float tmp_y = ori.y - (dis * 4);
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            //Debug.Log("point1");
            vertical = true;
        }

        tmp_x = ori.x + (dis * 4);
        if (Mathf.Approximately(move_pos.x, tmp_x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            //Debug.Log("Point3");
            vertical = false;
        }
        tmp_y = ori.y - (dis * 9);
        if (Mathf.Approximately(move_pos.x, ori.x) && Mathf.Approximately(move_pos.y, tmp_y)) {
            //Debug.Log("point2");
            horizental = true;
        }
        
    }
}
