using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터가 맞았을 때 hp 줄이기 및 죽었을 때 인게임 소환재화 및 enemy 삭제 이벤트
/// </summary>
public class EnemyHit : MonoBehaviour {

    private DataHub datahub;

    // 공격받은 피해량 
    private int damage;

    void Start() {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
    }

    public int Damage { 
        set {
            // 공격을 맞았을 때 데미지 만큼 hp를 줄이기
            int hp = transform.Find("Canvas").Find("Hp").gameObject.GetComponent<EnemyHp>().Hp - value;
//Debug.Log(hp);
            // 가산된 hp가 0 <= 일 경우 
            if (hp <= 0) {
                // datahub.Dot를 증가시킴
                datahub.Dot += 2;
                // 이 enemy를 제거 
                // 이 enemy가 보스면 - drop을 활성화
                if (gameObject.name.Equals("enemy_boss")) {
                    GetComponent<BossDrop>().Drop();
                }
                //Debug.Log("Die");
                Destroy(this.gameObject);
            }
            //아니면 hp를 설정하고 넘김
            else {
                transform.Find("Canvas").Find("Hp").gameObject.GetComponent<EnemyHp>().Hp = hp;
            }
        } 
    }
}

