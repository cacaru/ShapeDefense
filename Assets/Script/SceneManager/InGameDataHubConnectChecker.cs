using UnityEngine;
using static ShapeDefenseSpace.GameData;

public class InGameDataHubConnectChecker : MonoBehaviour
{
    // Update is called once per frame
    void Update() {
        if (datahub.UserConnectEnd) {
            GetComponent<FieldChecker>().enabled = true;
            // 정보 로딩 이후 이 스크립트가 필요 없으므로 바로 제거
            GetComponent<InGameDataHubConnectChecker>().enabled = false;
        }
    }
}
