using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

using static ShapeDefenseSpace.GameData;


public class EnemyMove : MonoBehaviour {

    public float speed;

    private Tilemap map;

    private EnemyMovement move_type;
    private Vector2 next;

    public bool Active { set {
            if (value) {
                StartCoroutine(Move());
            }
        } 
    }

    public void DefaultSetting(Tilemap map, float speed) {
        this.map = map;
        this.speed = speed;
        GetComponent<EnemyHit>().speed = speed;
        DecideMoveType(datahub.StageNumber);
    }

    public void ShowSetting() {
        Debug.Log("enemy pool back 후 resetting => " + datahub.StageNumber + " // map : " + map.name);
    }

    IEnumerator Move() {
        while (true) {
            if (!datahub.Pause) {
                next = move_type.MoveResult(transform.position, speed, datahub.SpeedRate);
                transform.position = next;
            }
            yield return wff;
        }
    }

    private void DecideMoveType(int stage_number) {

        int random_selecter = Random.Range(1, 3);
        bool bool_selecter = random_selecter == 1;
        // stage number에 따라 움직일 동선을 결정
        switch (stage_number) {
            case 1:
                move_type = new EnemyMove1(transform.position, transform.position);
                break;
            case 2:
                move_type = new EnemyMove2(transform.position, transform.position, bool_selecter);
                break;
            case 3:
                move_type = new EnemyMove3(transform.position, transform.position);
                break;
            case 4:
                move_type = new EnemyMove4(transform.position, transform.position);
                break;
            case 5:
                move_type = new EnemyMove5(transform.position, transform.position);
                break;
            case 6:
                move_type = new EnemyMove6(transform.position, transform.position);
                break;
            case 7:
                if (!bool_selecter) {
                    Vector2 set_pos = transform.position;
                    set_pos.x += 4.59f;
                    transform.position = set_pos;
                }
                move_type = new EnemyMove7(transform.position, transform.position, bool_selecter);
                break;
            case 8:
                move_type = new EnemyMove8(transform.position, transform.position, bool_selecter);
                break;

        }
        move_type.Map = map;
    }
}