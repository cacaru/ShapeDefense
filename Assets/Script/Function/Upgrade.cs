using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    private GameObject Announce;
    [SerializeField]
    private TMP_Text AnnounceText;

    // 정보 저장소 연결
    private DataHub datahub;
    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
    }

    public void UpgradeUnit() {
        // Unit 조각을 모아 기초 강화 업그레이드 진행
        // 필요 정보 :: Unit id
        // 골드와 조각의 갯수가 충분한지 확인하기 위한 dot / piece/ need_piece 값 
        //이 함수를 실행할 수있는 시점에서 null이 아님이 보장
        Unit select_unit = (Unit)datahub.Unit[datahub.Unitid];
        // 골드가 충분하지 않으면 안내문구를 보여주고 종료
        if ( select_unit.NeedGold > datahub.User.Dot) {
            // 안내 페이지 보여주고
            Announce.SetActive(true);
            AnnounceText.text = "골드가 부족합니다.";
            return;
        }
        
        // 골드가 충분하면
        // 조각의 갯수가 충분하지 않으면 안내문구를 보여주고 종료
        if ( select_unit.NeedPiece > select_unit.Piece) {
            // 안내 문구 보여주고
            Announce.SetActive(true);
            AnnounceText.text = "필요한 조각이 부족합니다.";
            return;
        }
        // 조각도 충분하면

        // 강화
        Debug.Log("강화 성공");            

    }

    public void NotEnoughConfirm() {
        Announce.SetActive(false);
    }
}
