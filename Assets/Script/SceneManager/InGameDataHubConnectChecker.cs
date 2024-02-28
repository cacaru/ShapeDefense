using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameDataHubConnectChecker : MonoBehaviour
{
    private DataHub datahub;

    // Start is called before the first frame update
    void Start() {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
    }

    // Update is called once per frame
    void Update() {
        if (datahub.UserConnectEnd) {
            this.gameObject.GetComponent<FieldChecker>().enabled = true;
            // ���� �ε� ���� �� ��ũ��Ʈ�� �ʿ� �����Ƿ� �ٷ� ����
            this.gameObject.GetComponent<InGameDataHubConnectChecker>().enabled = false;
        }
    }
}
