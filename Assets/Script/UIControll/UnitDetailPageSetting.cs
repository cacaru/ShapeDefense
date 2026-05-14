using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShapeDefenseSpace.PublicData;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;
public class UnitDetailPageSetting : SceneSingleton<UnitDetailPageSetting>
{

    // 보여질 정보들을 주관할 오브젝트들
    [SerializeField] private Image Background;                  // 배경색
    [SerializeField] private Image Image;                       // 이미지
    [SerializeField] private TMP_Text Nick_Name;                // 이름
    [SerializeField] private TMP_Text Level;                    // 현재 강화도 (0부터 시작)
    [SerializeField] private TMP_Text Attack;                   // 공격력
    [SerializeField] private TMP_Text Attack_Speed;             // 공격속도
    [SerializeField] private TMP_Text Piece;                    // 소지조각
    [SerializeField] private TMP_Text Need_Piece;               // 강화 필요 조각
    [SerializeField] private TMP_Text Need_Gold;                // 강화에 필요한 비용 표기
    [SerializeField] private GameObject Upgrade_Btn;             // 강화버튼 활성화
    [SerializeField] private GameObject Amalgate_Btn;             // 합성버튼 활성화
    [SerializeField] private TMP_Text Amalgate_announce;        // 합성안내 

    private readonly string normal_amalgation_announce = "최대 강화가 완료되었을 때,\r\n100조각당 1개의 랜덤 상위 등급 조각으로 합성할 수 있습니다.";
    private readonly string s_amalgation_announce = "최대 강화가 완료되었을 때,\r\n100조각당 1개의 다른 S등급 조각으로 합성할 수 있습니다.";

    public void SettingPage() {
        // 현재 페이지의 주 정보 Unit
        Unit unit = (Unit)datahub.Unit_dic[datahub.UnitidWithPage];
        //Debug.Log(unit.NickName);

        // 페이지에 보여질 내용 설정
        Nick_Name.text = unit.NickName;
        Level.text = unit.UpgradeValue.ToString();
        Attack.text = (unit.Attack + (unit.UpgradeValue * unit.UpgradeFigures)).ToString();
        Attack_Speed.text = unit.AttackSpeed.ToString();
        Need_Piece.text = (unit.NeedPiece * (unit.UpgradeValue+1)).ToString();
        Piece.text = unit.Piece.ToString();

        Need_Gold.text = unit.NeedGold.ToString();

        // 이미지 설정
        Image.sprite = UtilityHub.GetSprite(unit.Id);
        Background.color = unit.Grade switch {
            "E" => color_e,
            "D" => color_d,
            "C" => color_c,
            "B" => color_b,
            "A" => color_a,
            "S" => color_s,
            _ => core_color,
        };
        var tmp = Background.color;
        tmp.a = 120 / 255f;
        Background.color = tmp;

        // 강화 가능 여부 확인 
        // max value 이상이라면 초월 진행
        if (datahub.User.Dot >= unit.NeedGold && 
            unit.UpgradeValue < unit.MaxUpgradeValue &&
            unit.Piece >= (unit.NeedPiece * (unit.UpgradeValue + 1))
            ) {
            Need_Gold.color = Possible_announce;
            Upgrade_Btn.SetActive(true);
        }
        else {
            if(unit.UpgradeValue >= unit.MaxUpgradeValue) {
                Need_Gold.color = ACTIVE_TEXT;
            }
            else {
                Need_Gold.color = Impossible_announce;
            }
            Upgrade_Btn.SetActive(false);
        }

        //합성 버튼 활성화 
        // 조건 -> 강화가 맥스상태인 상황
        if(unit.UpgradeValue != unit.MaxUpgradeValue) {
            // 색을 진하게해서 못누르는것처럼 보이는게 나을듯
            Amalgate_Btn.GetComponent<Image>().color = Amalgation_Impossible;
            Amalgate_Btn.GetComponent<Button>().enabled = false;
        }
        // 합성 가능하면
        else {
            Amalgate_Btn.GetComponent<Image>().color = Can_Amalgation;
            Amalgate_Btn.GetComponent<Button>().enabled = true;
        }

        // 현 유닛의 등급이 S랭크라면 합성으로 상위가 나올 수 없기에 동일 등급보상으로 지급
        if (unit.Grade.Equals("S")) {
            Amalgate_announce.text = s_amalgation_announce;
        }
        else {
            Amalgate_announce.text = normal_amalgation_announce;
        }
    }

}
