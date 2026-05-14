using TMPro;
using UnityEngine;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour {
    [SerializeField] private GameObject Announce;
    [SerializeField] private TMP_Text AnnounceText;
    [SerializeField] private GameObject AmalgateArea;
    [SerializeField] private Image AmalgateResult;
    [SerializeField] private Image AmalgateGrade;

    [SerializeField] private GameObject Upgrade_Pannel;

    //private readonly string NEED_MORE_GOLD = "골드가 부족합니다.";
    private readonly string NEED_MORE_PIECE = "필요한 조각이 부족합니다.";
    //private readonly string ALREADY_MAX_UPGRADE = "이미 최대강화 상태입니다.";

    private Unit select_unit;
    //private int need_piece, need_dot;

    public void UpgradePannelOpen() {
        Upgrade_Pannel.GetComponent<UpgradeUISetter>().SetUnit = datahub.UnitidWithPage;
        Upgrade_Pannel.SetActive(true);
    }
    /*
    public void UpgradeUnit() {
        // Unit 조각을 모아 기초 강화 업그레이드 진행
        // 필요 정보 :: Unit id
        // 골드와 조각의 갯수가 충분한지 확인하기 위한 dot / piece/ need_piece 값 
        //이 함수를 실행할 수있는 시점에서 null이 아님이 보장
        select_unit = (Unit)datahub.Unit[datahub.UnitidWithPage];
        // 골드가 충분하지 않으면 안내문구를 보여주고 종료
        need_dot = select_unit.NeedGold;
        if (need_dot > datahub.User.Dot) {
            // 안내 문구 보여주기
            AnnounceText.text = NEED_MORE_GOLD;
            Announce.SetActive(true);
            return;
        }

        // 골드가 충분하면
        // 조각의 갯수가 충분하지 않으면 안내문구를 보여주고 종료
        need_piece = select_unit.NeedPiece * (select_unit.UpgradeValue + 1);
        if (need_piece > select_unit.Piece) {
            // 안내 문구 보여주기
            AnnounceText.text = NEED_MORE_PIECE;
            Announce.SetActive(true);
            return;
        }
        // 조각도 충분하면
        // 강화가 최대 강화 상태인지 확인
        if (select_unit.UpgradeValue >= select_unit.MaxUpgradeValue) {
            // 안내 문구 보여주기
            AnnounceText.text = ALREADY_MAX_UPGRADE;
            Announce.SetActive(true);
            return;
        }
        UpgradePannelOpen();
    }
    */

    public void UpdateUnit(int unit_id, int need_piece, int need_dot, int upgrade_value) {
        //Debug.Log(unit_id + " // " + need_piece + " // " + need_dot + " // " + upgrade_value);
        // 강화 업적 진행
        achieve_observer.UpgradeUnitCheck(upgrade_value);
        // 강화
        // modify db
        string query;
        select_unit = datahub.Unit_dic[unit_id] as Unit;
        // piece 개수 줄이기
        select_unit.Piece -= need_piece;
        select_unit.UpgradeValue += upgrade_value;

        query = UtilityHub.query_builder.Append("UPDATE unit SET piece=")
                                        .Append(select_unit.Piece)
                                        .Append(", upgrade_value=")
                                        .Append(select_unit.UpgradeValue)
                                        .Append(" WHERE id = ")
                                        .Append(select_unit.Id)
                                        .ToString();
        modifyDB.ControllDB(query, "unit");
        UtilityHub.query_builder.Clear();

        // dot 차감
        datahub.User.Dot -= need_dot;
        query = UtilityHub.query_builder.Append("UPDATE user SET dot=")
                                        .Append(datahub.User.Dot)
                                        .ToString();
        UtilityHub.query_builder.Clear();
        modifyDB.ControllDB(query, "user");

        ReShowPage();
        // 모든 확인이 끝난 후 완료 팝업 생성
        Upgrade_Pannel.GetComponent<UpgradeUISetter>().Upgrade_Complete();
    }

    private void ReShowPage() {
        // 온전히 강화가 완료되었으므로 페이지의 정보 다시 넣기
        UnitDetailPageSetting.Instance.SettingPage();
        // header reload
        UtilityHub.PageHeaderSetting();

    }


    public void AmalgateUnit() {
        // 조각 100개를 사용하여 상위 등급의 조각 1개로 합성하기
        Unit select_unit = datahub.Unit_dic[datahub.UnitidWithPage];
        if (100 > select_unit.Piece) {
            // 안내 문구 보여주기
            AnnounceText.text = NEED_MORE_PIECE;
            Announce.SetActive(true);
            return;
        }
        // D5 C4 B3 A2 S1
        // 현 유닛 등급에 따라 생성 대상 등급 결정
        int random_item = select_unit.Grade switch {
            "E" => Random.Range(2001, 2004),
            "D" => Random.Range(3001, 3015),
            "C" => Random.Range(4001, 4012),
            "B" => Random.Range(5001, 5006),
            "A" or "S" => Random.Range(6001, 6006),
            _ => 0
        };

        // modify db
        // modify db
        string query;
        // piece 개수 줄이기
        select_unit.Piece -= 100;
        query = UtilityHub.query_builder.Append("UPDATE unit SET piece=")
                                        .Append(select_unit.Piece)
                                        .Append(" WHERE id = ")
                                        .Append(select_unit.Id)
                                        .ToString();
        modifyDB.ControllDB(query, "unit");
        UtilityHub.query_builder.Clear();

        // target unit piece 올리기
        Unit target = datahub.Unit_dic[random_item] as Unit;
        target.Piece += 1;
        query = UtilityHub.query_builder.Append("UPDATE unit SET piece=")
                                        .Append(target.Piece)
                                        .Append(" WHERE id = ")
                                        .Append(target.Id)
                                        .ToString();
        modifyDB.ControllDB(query, "unit");
        UtilityHub.query_builder.Clear();

        AmalgateResult.sprite = UtilityHub.GetSprite(random_item); //Resources.Load<Sprite>(UtilityHub.GetPath(random_item));
        AmalgateGrade.color = target.Grade switch {
            "D" => color_d,
            "C" => color_c,
            "B" => color_b,
            "A" => color_a,
            "S" => color_s,
            _ => color_e
        }; ;

        UnitDetailPageSetting.Instance.SettingPage();

        AnnounceText.gameObject.SetActive(false);
        AmalgateArea.SetActive(true);
        Announce.SetActive(true);
    }

    public void NotEnoughConfirm() {
        AnnounceText.gameObject.SetActive(true);
        AmalgateArea.SetActive(false);
        Announce.SetActive(false);
        Upgrade_Pannel.SetActive(false);
    }
}
