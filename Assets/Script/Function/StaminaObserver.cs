using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// stamina�� ���¸� Ȯ���ϰ� ������ �Լ�
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

    // stamina ���� ������Ʈ
    public void UpdateStamina() {
        // stmaina DB ������Ʈ
        string text = "UPDATE user SET stamina = " + datahub.User.Stamina;
        GameObject.Find("GameObject").GetComponent<ModifyDB>().ControllDB(text, "user");

        // stamina ȭ�� ��ǥ��
        if (datahub.RoundNumber < 1)
            GameObject.Find("Canvas").GetComponent<StaminaShow>().ReShow();
    }

    // stamina ȸ��
    // 3�д� 1 �� ȸ��
    IEnumerator StaminaCharge() {
        while (true) {
            yield return new WaitForSeconds(180f);
            if (datahub.User.Stamina < 20)
                datahub.User.Stamina += 1;
            UpdateStamina();
        }
        
    }
}
