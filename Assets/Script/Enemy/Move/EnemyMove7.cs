using UnityEngine;


/// <summary>
/// 적 이동 지정 스크립트 7
/// 맵에 따라 이동을 변경할 수 있도록 지정하기
/// </summary>

public class EnemyMove7 : EnemyMovement {

    // true -> 왼쪽 필드
    // false -> 오른쪽 필드
    private bool field_selecter;

    public EnemyMove7(Vector2 ori, Vector2 move_pos, bool field_selecter) {
        this.ori = ori;
        this.move_pos = move_pos;
        this.field_selecter = field_selecter;

        dis = 0.51f;
    }

    protected override void BifurcationChecker() {
        
        // 좌측일 때
        if (field_selecter) {
            // 기본 우측->아래
            if (ori == move_pos) {
                horizental = false;
                vertical = true;
            }
            // 우->좌
            if(Mathf.Approximately(move_pos.x , ori.x + (dis * 3)) && Mathf.Approximately(move_pos.y, ori.y - dis * 4)) {
                vertical = false;
            }
            // 아래 -> 위로
            if(Mathf.Approximately(move_pos.x , ori.x + dis) && Mathf.Approximately(move_pos.y , ori.y - dis * 7)) {
                horizental = true;
            }
        }

        // 우측일 때
        else {
            // 하 / 좌
            if (Mathf.Approximately(move_pos.x, ori.x - dis) && Mathf.Approximately(move_pos.y, ori.y)) {
                vertical = false;
                horizental = false;
            }
            // 좌 -> 우
            if (Mathf.Approximately(move_pos.x , ori.x - (dis * 4)) && Mathf.Approximately(move_pos.y, ori.y - (dis * 4))) {
                vertical = true;
            }
            // 아래 -> 위로
            if (Mathf.Approximately(move_pos.x , ori.x - dis) && Mathf.Approximately(move_pos.y , ori.y - dis * 7)) {
                horizental = true;
            }
        }
    }
}
