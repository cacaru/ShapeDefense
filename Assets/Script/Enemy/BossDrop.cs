using UnityEngine;
using static ShapeDefenseSpace.GameData;

/// <summary>
/// 10라운드 마다 등장하는 보스가 죽을 때 제작 유닛을 드랍할 함수
/// </summary>
public class BossDrop : MonoBehaviour
{
    private bool droped = false;
    public bool Droped { set { droped = value; } }
    
    public void Drop() {
        if(droped) return;
        droped = true;

        // 현재 라운드에 따라 결정과 조화를 드랍
        // 40라운드까지 결정
        // 결정 드랍
        // 유닛을 생성
        datahub.CoreCount++;
        // 40라운드부터 조화를 추가 드랍
        if (datahub.RoundNumber >= 40) {
            datahub.UnicoreCount++;
        }
        EnemySpawner.Instance.StopTimer();
        CoreObserver.Instance.GetCoreOn();
        
        // 3회 소환 가능 분의 dot 추가
        datahub.Dot += datahub.NeedDot * 3 + 4;

        // 보스 킬 업적 횟수 증가
        achieve_observer.KillBoss(datahub.RoundNumber);

        // 마지막 보스라면 변수제어
        if(datahub.RoundNumber >= datahub.EndRound - 5) {
            datahub.KillLastBoss = true;
        }

        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }
}
