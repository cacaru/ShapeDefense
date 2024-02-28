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
            // 정보 로딩 이후 이 스크립트가 필요 없으므로 바로 제거
            this.gameObject.GetComponent<InGameDataHubConnectChecker>().enabled = false;
        }
    }
}
