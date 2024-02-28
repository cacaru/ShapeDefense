using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundProgress : MonoBehaviour
{
    [SerializeField]
    private TMP_Text round;

    private DataHub datahub;
    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
        StartCoroutine(First());
    }

    IEnumerator First() {
        // 10초의 준비 이후 새 루틴 시작
        yield return new WaitForSeconds(10f);
        StartCoroutine(Checker());
    }

    IEnumerator Checker() {
        while (true) {
            // 최종 라운드에 도달 했는지 확인
            int going = UtilityHub.EndRoundChecker(datahub.StageNumber, datahub.RoundNumber);
            if (going == 2) { break; }
            // 50초 이후 새 라운드 진행
            gameObject.GetComponent<EnemySpawner>().NextSpawn();
            Debug.Log("New round start");
            // 현 라운드 텍스트 변경
            datahub.RoundNumber++;
            round.text = datahub.RoundNumber.ToString();
            //round.text = (int.Parse(round.text) + 1).ToString();
            yield return new WaitForSeconds(50f);
        }
        // 라운드 종료가 되었을 때 보스를 잡는데 성공했다면 == enemy_boss가 존재하지 않으면 게임 보상 추가 
        // Enemy boss를 확인
        int ending = 0;
        var enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        foreach(var enemy in enemies) {
            // 남아 있는 것이 있음
            if (enemy.name.Equals("enemy_boss")) {
                ending = 1;
                break;
            }
        }

        // 실패했다면 게임 종료 화면에서 실패 announce 
        if (ending == 1) {
            string text = "공략에 실패했습니다.";
            
        }
        // 성공
        else {
            string text = "공략 완료";
        }
        
    }

    public void Pause() {
        StopCoroutine(Checker());
    }

    public void ReStart() {
        StartCoroutine(Checker());
    }
}
