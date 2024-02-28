using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneObserver : MonoBehaviour
{
    private DataHub datahub;

    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();    
    }

    // Update is called once per frame
    void Update()
    {
        if ( datahub.UserConnectEnd) {
            GameObject.Find("Canvas").GetComponent<HeaderSetting>().enabled = true;
            GameObject.Find("Canvas").GetComponent<StaminaShow>().enabled = true;
            GameObject.Find("GameObject").GetComponent<StaminaObserver>().enabled = true;
            // ���� �ε� ���� �� ��ũ��Ʈ�� �ʿ� �����Ƿ� �ٷ� ����
            this.gameObject.GetComponent<FirstSceneObserver>().enabled = false;
        }
    }
}
