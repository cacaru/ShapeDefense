using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InGameUnitUpgradeInUI : MonoBehaviour, IPointerClickHandler {

    private DataHub datahub;

    [SerializeField] private TMP_Text normal_upgrade_dot;
    [SerializeField] private TMP_Text advanced_upgrade_dot;
    [SerializeField] private TMP_Text luxury_upgrade_dot;
    [SerializeField] private TMP_Text limit_upgrade_dot;
    [SerializeField] private TMP_Text custom_upgrade_dot;

    // Start is called before the first frame update
    void Start() {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
    }

    // Update is called once per frame
    void Update() {

    }


    public void OnPointerClick(PointerEventData eventData) {
        GameObject now = eventData.pointerCurrentRaycast.gameObject;
        if (now != null) {
            string name = now.name;
            if (name.Contains("_")) {
                string[] part = name.Split('_');
                int need_dot;
                switch (part[1]) {
                    case "no":
                        // dot 재화 검증
                        need_dot = int.Parse(normal_upgrade_dot.text);
                        // 10강 일 때 누르면 아무 일도 일어나지 않음
                        if (datahub.Dot >= need_dot && datahub.UpgradeNoValue < 10) {
                            datahub.Dot -= need_dot;
                            datahub.UpgradeNoValue++;
                            GameObject.Find("Upgrade_no_Text").GetComponent<TMP_Text>().text = "Lv." + datahub.UpgradeNoValue.ToString();
                            if (datahub.UpgradeNoValue >= 10) {
                                normal_upgrade_dot.text = "End";
                            }
                            else {
                                normal_upgrade_dot.text = (need_dot + 50).ToString();
                            }
                        }
                        break;
                    case "ad":
                        // dot 재화 검증
                        need_dot = int.Parse(advanced_upgrade_dot.text);
                        if (datahub.Dot >= need_dot && datahub.UpgradeAdValue < 10) {
                            datahub.Dot -= need_dot;
                            datahub.UpgradeAdValue++;
                            GameObject.Find("Upgrade_ad_Text").GetComponent<TMP_Text>().text = "Lv." + datahub.UpgradeAdValue.ToString();
                            if (datahub.UpgradeAdValue >= 10) {
                                advanced_upgrade_dot.text = "End";
                            }
                            else {
                                advanced_upgrade_dot.text = (need_dot + 50).ToString();
                            }
                            
                        }
                        break;
                    case "lu":
                        // dot 재화 검증
                        need_dot = int.Parse(luxury_upgrade_dot.text);
                        if (datahub.Dot >= need_dot && datahub.UpgradeLuValue < 10) {
                            datahub.Dot -= need_dot;
                            datahub.UpgradeLuValue++;
                            GameObject.Find("Upgrade_lu_Text").GetComponent<TMP_Text>().text = "Lv." + datahub.UpgradeLuValue.ToString();
                            if (datahub.UpgradeLuValue >= 10) {
                                luxury_upgrade_dot.text = "End";
                            }
                            else {
                                luxury_upgrade_dot.text = (need_dot + 50).ToString();
                            }
                            
                        }
                        break;
                    case "li":
                        // dot 재화 검증
                        need_dot = int.Parse(limit_upgrade_dot.text);
                        if(datahub.Dot >= need_dot && datahub.UpgradeLiValue < 10) {
                            datahub.Dot -= need_dot;
                            datahub.UpgradeLiValue++;
                            GameObject.Find("Upgrade_li_Text").GetComponent<TMP_Text>().text = "Lv." + datahub.UpgradeLiValue.ToString();
                            if(datahub.UpgradeLiValue >= 10) {
                                limit_upgrade_dot.text = "End";
                            }
                            else {
                                limit_upgrade_dot.text = (need_dot + 50).ToString();
                            }
                            
                        }
                        
                        break;
                    case "cu":
                        // dot 재화 검증
                        need_dot = int.Parse(custom_upgrade_dot.text);
                        if(datahub.Dot >= need_dot && datahub.UpgradeCuValue < 10) {
                            datahub.Dot -= need_dot;
                            datahub.UpgradeCuValue++;
                            GameObject.Find("Upgrade_cu_Text").GetComponent<TMP_Text>().text = "Lv." + datahub.UpgradeCuValue.ToString();
                            if(datahub.UpgradeCuValue >= 10) {
                                custom_upgrade_dot.text = "End";
                            }
                            else {
                                custom_upgrade_dot.text = (need_dot + 50).ToString();
                            }
                        }
                        break;
                    default: break;
                }
            }
        }
    }

}
