using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text counter_text;
    [SerializeField]
    private GameObject GameOver;
    private int counter = 0;

    // Update is called once per frame
    void Update()
    {
        counter = this.transform.childCount;
        counter_text.text = counter.ToString();
        // counter의 기준은 datahub와 연동해서 진행
        if ( counter > 120) {
            Debug.Log("gameover");
            // 게임 오버 화면 띄우기
            GameOver.SetActive(true);

            // 스폰 종료
            this.gameObject.GetComponent<EnemySpawner>().StopSpawn();
            this.gameObject.GetComponent<EnemySpawner>().enabled = false;
            
            // 유닛수 카운트 종료
            this.gameObject.GetComponent<EnemyCounter>().enabled = false;
            // 라운드 진행 종료
            this.gameObject.GetComponent<RoundProgress>().Pause();
            this.gameObject.GetComponent<RoundProgress>().enabled = false;

            //모든 enemy 제거
            var enemies = this.gameObject.GetComponentInChildren<Transform>();
            foreach(Transform t in enemies) {
                if ( t !=  this.transform ) {
                    Destroy(t.gameObject);
                }
            }

        }
    }
}
