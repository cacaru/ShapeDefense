using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 10라운드 마다 등장하는 보스가 죽을 때 제작 유닛을 드랍할 함수
/// </summary>
public class BossDrop : MonoBehaviour
{
    private DataHub datahub;

    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
    }

    public void Drop() {
        // 현재 라운드에 따라 결정과 조화를 드랍
        // 40라운드까지 결정
        // 결정 드랍
        if (datahub.RoundNumber <= 40) {
            // 유닛을 생성
            UtilityHub.UnitCreateRandomField(40, 41, 42, 43);
        }

        // 50라운드부터 조화
        else {
            UtilityHub.UnitCreateRandomField(44, 45, 46, 47);
        }
        
    }
}
