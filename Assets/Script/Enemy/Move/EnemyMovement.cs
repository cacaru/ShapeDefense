using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class EnemyMovement
{
    protected float dis;
    // 이동 방향 결정
    // vertical = true > 우측 , false > 좌측
    // horizental = true > 상 , false > 하
    public bool vertical = false, horizental = false;

    // 기본 위치
    protected Vector2 ori;
    protected Vector2 move_pos;

    // 체크할 변수들
    protected RaycastHit2D hit;
    protected TileBase tile;

    protected Tilemap map;
    
    public Tilemap Map { get { return map; } set { map = value; } }
    public float Dis { get { return dis; } }
    /// <summary>
    /// 이동 방향 전환 분기점 확인 함수`
    /// </summary>
    protected abstract void BifurcationChecker();

    /// <summary>
    ///  이동 위치 계산 함수
    /// </summary>
    /// <param name="pos">현재 위치</param>
    /// <param name="speed">이동 속도</param>
    /// <param name="speed_rate">속도 배율</param>
    private Vector2 tmp = new(0,0);
    public virtual Vector2 MoveResult(Vector2 pos, float speed, int speed_rate) {
        // 좌우 우선 체크
        if (vertical) {
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
        else {
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
        if (horizental) {
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
        else {
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
