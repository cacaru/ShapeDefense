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
        // 10���� �غ� ���� �� ��ƾ ����
        yield return new WaitForSeconds(10f);
        StartCoroutine(Checker());
    }

    IEnumerator Checker() {
        while (true) {
            // ���� ���忡 ���� �ߴ��� Ȯ��
            int going = UtilityHub.EndRoundChecker(datahub.StageNumber, datahub.RoundNumber);
            if (going == 2) { break; }
            // 50�� ���� �� ���� ����
            gameObject.GetComponent<EnemySpawner>().NextSpawn();
            Debug.Log("New round start");
            // �� ���� �ؽ�Ʈ ����
            datahub.RoundNumber++;
            round.text = datahub.RoundNumber.ToString();
            //round.text = (int.Parse(round.text) + 1).ToString();
            yield return new WaitForSeconds(50f);
        }
        // ���� ���ᰡ �Ǿ��� �� ������ ��µ� �����ߴٸ� == enemy_boss�� �������� ������ ���� ���� �߰� 
        // Enemy boss�� Ȯ��
        int ending = 0;
        var enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        foreach(var enemy in enemies) {
            // ���� �ִ� ���� ����
            if (enemy.name.Equals("enemy_boss")) {
                ending = 1;
                break;
            }
        }

        // �����ߴٸ� ���� ���� ȭ�鿡�� ���� announce 
        if (ending == 1) {
            string text = "������ �����߽��ϴ�.";
            
        }
        // ����
        else {
            string text = "���� �Ϸ�";
        }
        
    }

    public void Pause() {
        StopCoroutine(Checker());
    }

    public void ReStart() {
        StartCoroutine(Checker());
    }
}
