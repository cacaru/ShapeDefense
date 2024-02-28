using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// stamina의 상태를 확인하고 수정할 함수
/// </summary>
/// 
public class StaminaObserver : MonoBehaviour
{
    private DataHub datahub;

    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
        StartCoroutine(StaminaCharge());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // stamina 관련 업데이트
    public void UpdateStamina() {
        // stmaina DB 업데이트
        string text = "UPDATE user SET stamina = " + datahub.User.Stamina;
        GameObject.Find("GameObject").GetComponent<ModifyDB>().ControllDB(text, "user");

        // stamina 화면 재표기
        if (datahub.RoundNumber < 1)
            GameObject.Find("Canvas").GetComponent<StaminaShow>().ReShow();
    }

    // stamina 회복
    // 3분당 1 씩 회복
    IEnumerator StaminaCharge() {
        while (true) {
            yield return new WaitForSeconds(180f);
            if (datahub.User.Stamina < 20)
                datahub.User.Stamina += 1;
            UpdateStamina();
        }
        
    }
}
