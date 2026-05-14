using System.Collections;
using TMPro;
using UnityEngine;

using static ShapeDefenseSpace.GameData;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text CounterText;
    [SerializeField] private TMP_Text MaxText;
    [SerializeField] private GameObject GameOver;
    private int counter = 0;

    //private Color FAIL_COLOR = new(1f, 110 / 255f, 110 / 255f, 1f);

    private int max_enemy_count;
    private void Start() {

        // max enemy text setting
        max_enemy_count = datahub.MaxEnemyCounter;
        MaxText.text = max_enemy_count.ToString();

        StartCoroutine(Counting());
    }

    IEnumerator Counting() {
        while (true) {
            if (!datahub.Pause) {
                counter = GameObject.FindGameObjectsWithTag("Enemy").Length;
                CounterText.text = counter.ToString();
                // counter의 기준은 datahub와 연동해서 진행
                if (counter >= max_enemy_count) {

                    // 게임 오버 화면 띄우기
                    gameObject.GetComponent<RoundProgress>().RoundEnd();

                    // 스폰 종료
                    gameObject.GetComponent<EnemySpawner>().StopSpawn();
                    gameObject.GetComponent<EnemySpawner>().enabled = false;

                    // 유닛수 카운트 종료
                    gameObject.GetComponent<EnemyCounter>().enabled = false;
                    // 라운드 진행 종료
                    gameObject.GetComponent<RoundProgress>().Pause();
                    gameObject.GetComponent<RoundProgress>().enabled = false;

                    // speed 원래대로 복귀
                    datahub.SpeedRate = 1;

                    //모든 enemy 제거
                    var enemies = gameObject.GetComponentInChildren<Transform>();
                    foreach (Transform t in enemies) {
                        if (t != transform) {
                            Destroy(t.gameObject);
                        }
                    }
                    break;
                }
                
            }
            yield return wfs_1;
        }
        
    }

}
