using UnityEngine;

/// <summary>
/// 적 이동 지정 스크립트 5
/// 맵에 따라 이동을 변경할 수 있도록 지정하기
/// </summary>

public class EnemyMove5 : EnemyMovement {

    public EnemyMove5(Vector2 ori, Vector2 move_pos) {
        this.ori = ori;
        this.move_pos = move_pos;
        dis = 0.51f;
    }

    protected override void BifurcationChecker() {

        // 원점일때 좌 -> 하
        // point1 == 우 -> 하
        // point2 == 좌 -> 하
        // point3 == 좌 -> 상
        // point4 == 우 -> 상
        // point5 == 좌 -> 상
        if(ori == move_pos) {
            horizental = false;
            vertical = false;
        }

        //point1
        if(Mathf.Approximately(move_pos.x, ori.x - dis) && Mathf.Approximately(move_pos.y , ori.y - dis)) {
            vertical = true;
        }

        //point2
        if(Mathf.Approximately(move_pos.x, ori.x + dis * 6) && Mathf.Approximately(move_pos.y, ori.y - dis * 8)) {
            vertical = false;
        }

        // point3
        if(Mathf.Approximately(move_pos.x, ori.x) && Mathf.Approximately(move_pos.y, ori.y - dis * 9)) {
            horizental = true;
        }

        // point4
        if(Mathf.Approximately(move_pos.x, ori.x - dis) && Mathf.Approximately(move_pos.y, ori.y - dis * 8)) {
            vertical = true;
        }

        // point5
        if(Mathf.Approximately(move_pos.x, ori.x + dis * 6) && Mathf.Approximately(move_pos.y, ori.y - dis)) {
            vertical = false;
        }

    }

}
