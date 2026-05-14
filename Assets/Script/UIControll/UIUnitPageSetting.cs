
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShapeDefenseSpace.GameData;

public class UIUnitPageSetting : SceneSingleton<UIUnitPageSetting>
{
    [SerializeField] private GameObject E;
    [SerializeField] private GameObject D;
    [SerializeField] private GameObject C;
    [SerializeField] private GameObject B;
    [SerializeField] private GameObject A;
    [SerializeField] private GameObject S;

    private readonly string max_text = "최대";
    private readonly Color normal_color = new(1f, 1f, 147 / 255f, 0f);
    private readonly Color can_upgrade_color = new(1f, 1f, 147 / 255f, 1f);
    private readonly Color Amalgate_color = Color.white;
    private void Setting(int size, GameObject grade) {
        
        for (int i = 0; i < size; i++) {
            Transform tmp = grade.transform.GetChild(i);

            if (tmp.name.Contains("_")) {
                // 현재 레벨 작성
                var unit = datahub.Unit_dic[int.Parse(tmp.name.Split("_")[0])] as Unit;

                tmp.Find("level").gameObject.GetComponent<TMP_Text>().text = unit.UpgradeValue == unit.MaxUpgradeValue ? max_text : unit.UpgradeValue.ToString();

                int need_piece = (unit.NeedPiece * (unit.UpgradeValue + 1));
                // 강화 가능인지 확인
                if (unit.Piece > 0 && 
                    unit.Piece >= need_piece &&
                    unit.UpgradeValue < unit.MaxUpgradeValue &&
                    datahub.User.Dot >= unit.NeedGold
                    ) {
                    // 가능하면 배경 ON!
                    tmp.gameObject.GetComponent<Image>().color = can_upgrade_color;
                }
                // 합성 가능한지 확인
                else if (unit.UpgradeValue == unit.MaxUpgradeValue && unit.Piece >= 100) {
                    tmp.gameObject.GetComponent<Image>().color = Amalgate_color;
                }

                else {
                    tmp.gameObject.GetComponent<Image>().color = normal_color;
                }
            }
        }
    }

    public void CheckUnitLevel() {

        // content 의 하위 오브젝트들로 탐색
        int size;
        // E
        size = E.transform.childCount;
        Setting(size, E);

        // D
        size = D.transform.childCount;
        Setting(size, D);

        // C
        size = C.transform.childCount;
        Setting(size, C);

        // B
        size = B.transform.childCount;
        Setting(size, B);

        // A
        size = A.transform.childCount;
        Setting(size, A);

        // S
        size = S.transform.childCount;
        Setting(size, S);

    }
}
