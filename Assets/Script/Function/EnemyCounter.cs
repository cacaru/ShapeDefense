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
        // counter�� ������ datahub�� �����ؼ� ����
        if ( counter > 120) {
            Debug.Log("gameover");
            // ���� ���� ȭ�� ����
            GameOver.SetActive(true);

            // ���� ����
            this.gameObject.GetComponent<EnemySpawner>().StopSpawn();
            this.gameObject.GetComponent<EnemySpawner>().enabled = false;
            
            // ���ּ� ī��Ʈ ����
            this.gameObject.GetComponent<EnemyCounter>().enabled = false;
            // ���� ���� ����
            this.gameObject.GetComponent<RoundProgress>().Pause();
            this.gameObject.GetComponent<RoundProgress>().enabled = false;

            //��� enemy ����
            var enemies = this.gameObject.GetComponentInChildren<Transform>();
            foreach(Transform t in enemies) {
                if ( t !=  this.transform ) {
                    Destroy(t.gameObject);
                }
            }

        }
    }
}
