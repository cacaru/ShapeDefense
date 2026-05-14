using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using ShapeDefenseSpace;
using static ShapeDefenseSpace.PublicData;
using static ShapeDefenseSpace.GameData;

public class InGameUnitUpgradeInUI : MonoBehaviour, IPointerClickHandler {

    [SerializeField] private GameObject e_obj;
    [SerializeField] private GameObject d_obj;
    [SerializeField] private GameObject c_obj;
    [SerializeField] private GameObject b_obj;
    [SerializeField] private GameObject a_obj;
    [SerializeField] private GameObject s_obj;

    private TMP_Text dot_e;
    private TMP_Text dot_d;
    private TMP_Text dot_c;
    private TMP_Text dot_b;
    private TMP_Text dot_a;
    private TMP_Text dot_s;

    private TMP_Text text_e;
    private TMP_Text text_d;
    private TMP_Text text_c;
    private TMP_Text text_b;
    private TMP_Text text_a;
    private TMP_Text text_s;

    private readonly string LEVEL_STRING = "Lv.";

    // Start is called before the first frame update
    void Start() {

        dot_e = e_obj.transform.Find("Upgrade_Dot_E").GetComponent<TMP_Text>();
        dot_d = d_obj.transform.Find("Upgrade_Dot_D").GetComponent<TMP_Text>();
        dot_c = c_obj.transform.Find("Upgrade_Dot_C").GetComponent<TMP_Text>();
        dot_b = b_obj.transform.Find("Upgrade_Dot_B").GetComponent<TMP_Text>();
        dot_a = a_obj.transform.Find("Upgrade_Dot_A").GetComponent<TMP_Text>();
        dot_s = s_obj.transform.Find("Upgrade_Dot_S").GetComponent<TMP_Text>();

        text_e = e_obj.transform.Find("Upgrade_Text_E").GetComponent<TMP_Text>();
        text_d = d_obj.transform.Find("Upgrade_Text_D").GetComponent<TMP_Text>();
        text_c = c_obj.transform.Find("Upgrade_Text_C").GetComponent<TMP_Text>();
        text_b = b_obj.transform.Find("Upgrade_Text_B").GetComponent<TMP_Text>();
        text_a = a_obj.transform.Find("Upgrade_Text_A").GetComponent<TMP_Text>();
        text_s = s_obj.transform.Find("Upgrade_Text_S").GetComponent<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        GameObject now = eventData.pointerCurrentRaycast.gameObject;
        if (now != null) {
            string name = now.name;
            if (name.Contains("_")) {
                string[] part = name.Split('_');
                int need_dot;
                switch (part[1]) {
                    case "E":
                        // dot 재화 검증
                        if (dot_e.text.Equals(Complete_Text)) { return; }
                        need_dot = int.Parse(dot_e.text);
                        // 10강 일 때 누르면 아무 일도 일어나지 않음
                        if (datahub.Dot >= need_dot && datahub.UpgradeValueE < 10) {
                            datahub.Dot -= need_dot;
                            datahub.UpgradeValueE++;
                            text_e.text = UtilityHub.query_builder.Append(LEVEL_STRING)
                                                                  .Append(datahub.UpgradeValueE.ToString())
                                                                  .ToString();
                            UtilityHub.query_builder.Clear();
                            if (datahub.UpgradeValueE >= 10) {
                                dot_e.text = Complete_Text;
                            }
                            else {
                                dot_e.text = (need_dot + 50).ToString();
                            }
                        }
                        break;
                    case "D":
                        // dot 재화 검증
                        if (dot_d.text.Equals(Complete_Text)) { return; }
                        need_dot = int.Parse(dot_d.text);
                        if (datahub.Dot >= need_dot && datahub.UpgradeValueD < 10) {
                            datahub.Dot -= need_dot;
                            datahub.UpgradeValueD++;
                            text_d.text = UtilityHub.query_builder.Append(LEVEL_STRING)
                                                                  .Append(datahub.UpgradeValueD.ToString())
                                                                  .ToString();
                            UtilityHub.query_builder.Clear();
                            if (datahub.UpgradeValueD >= 10) {
                                dot_d.text = Complete_Text;
                            }
                            else {
                                dot_d.text = (need_dot + 50).ToString();
                            }
                        }
                        break;
                    case "C":
                        // dot 재화 검증
                        if (dot_c.text.Equals(Complete_Text)) { return; }
                        need_dot = int.Parse(dot_c.text);
                        if (datahub.Dot >= need_dot && datahub.UpgradeValueC < 10) {
                            datahub.Dot -= need_dot;
                            datahub.UpgradeValueC++;
                            text_c.text = UtilityHub.query_builder.Append(LEVEL_STRING)
                                                                  .Append(datahub.UpgradeValueC.ToString())
                                                                  .ToString();
                            UtilityHub.query_builder.Clear();
                            if (datahub.UpgradeValueC >= 10) {
                                dot_c.text = Complete_Text;
                            }
                            else {
                                dot_c.text = (need_dot + 50).ToString();
                            }
                        }
                        break;
                    case "B":
                        // dot 재화 검증
                        if (dot_b.text.Equals(Complete_Text)) { return; }
                        need_dot = int.Parse(dot_b.text);
                        if(datahub.Dot >= need_dot && datahub.UpgradeValueB < 10) {
                            datahub.Dot -= need_dot;
                            datahub.UpgradeValueB++;
                            text_b.text = UtilityHub.query_builder.Append(LEVEL_STRING)
                                                                  .Append(datahub.UpgradeValueB.ToString())
                                                                  .ToString();
                            UtilityHub.query_builder.Clear();
                            if(datahub.UpgradeValueB >= 10) {
                                dot_b.text = Complete_Text;
                            }
                            else {
                                dot_b.text = (need_dot + 50).ToString();
                            }
                        }
                        
                        break;
                    case "A":
                        // dot 재화 검증
                        if (dot_a.text.Equals(Complete_Text)) { return; }
                        need_dot = int.Parse(dot_a.text);
                        if(datahub.Dot >= need_dot && datahub.UpgradeValueA < 10) {
                            datahub.Dot -= need_dot;
                            datahub.UpgradeValueA++;
                            text_a.text = UtilityHub.query_builder.Append(LEVEL_STRING)
                                                                  .Append(datahub.UpgradeValueA.ToString())
                                                                  .ToString();
                            UtilityHub.query_builder.Clear();
                            if(datahub.UpgradeValueA >= 10) {
                                dot_a.text = Complete_Text;
                            }
                            else {
                                dot_a.text = (need_dot + 50).ToString();
                            }
                        }
                        break;
                    case "S":
                        // dot 재화 검증
                        if (dot_s.text.Equals(Complete_Text)) { return; }
                        need_dot = int.Parse(dot_s.text);
                        if (datahub.Dot >= need_dot && datahub.UpgradeValueS < 10) {
                            datahub.Dot -= need_dot;
                            datahub.UpgradeValueS++;
                            text_s.text = UtilityHub.query_builder.Append(LEVEL_STRING)
                                                                       .Append(datahub.UpgradeValueS.ToString())
                                                                       .ToString();
                            UtilityHub.query_builder.Clear();
                            if (datahub.UpgradeValueS >= 10) {
                                dot_s.text = Complete_Text;
                            }
                            else {
                                dot_s.text = (need_dot + 50).ToString();
                            }
                        }
                        break;
                    default: break;
                }

                // observer에 신호 보내기
                DamageObserver.Instance.DamageUpdate();
            }
        }
    }

}
