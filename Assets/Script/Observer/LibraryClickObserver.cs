using UnityEngine;
using UnityEngine.EventSystems;

using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;
using static ShapeDefenseSpace.CombineTableShowObserver;

public class LibraryClickObserver : SceneSingleton<LibraryClickObserver>, IPointerClickHandler {

    [SerializeField] private GameObject CombineShowContent;

    // Start is called before the first frame update
    public void OnPointerClick(PointerEventData eventData) {
        GameObject unit = eventData.pointerCurrentRaycast.gameObject;
        if (unit != null) {
            int unit_id = 0;
            string name = unit.name;
            bool type_is_random = false;
            if (name.Contains("_")) {
                string[] part = name.Split('_');
                switch (part[0]) {
                    case "E":
                        // circle
                        if (part[1].Equals(CIRCLE))
                            unit_id = 1001;
                        // triangle
                        else if (part[1].Equals(TRIANGLE))
                            unit_id = 1002;
                        // square
                        else if (part[1].Equals(SQUARE))
                            unit_id = 1003;
                        break;

                    case "D":
                        // circle
                        if (part[1].Equals(CIRCLE))
                            unit_id = 2001;
                        // triangle
                        else if (part[1].Equals(TRIANGLE))
                            unit_id = 2002;
                        // square
                        else if (part[1].Equals(SQUARE))
                            unit_id = 2003;
                        break;

                    case "C":
                        type_is_random = true;
                        // circle
                        if (part[1].Equals(CIRCLE)) {
                            if (part[2] == "1")
                                unit_id = 3001;
                            else if (part[2] == "2")
                                unit_id = 3002;
                            else if (part[2] == "3")
                                unit_id = 3003;
                        }

                        // triangle
                        else if (part[1].Equals(TRIANGLE)) {
                            if (part[2] == "1")
                                unit_id = 3004;
                            else if (part[2] == "2")
                                unit_id = 3005;
                            else if (part[2] == "3")
                                unit_id = 3006;
                        }

                        // square
                        else if (part[1].Equals(SQUARE)) {
                            if (part[2] == "1")
                                unit_id = 3007;
                            else if (part[2] == "2")
                                unit_id = 3008;
                            else if (part[2] == "3")
                                unit_id = 3009;
                        }

                        // amalgation
                        else if (part[1].Equals(AMAL)) {
                            if (part[2] == "1")
                                unit_id = 3010;
                            else if (part[2] == "2")
                                unit_id = 3011;
                            else if (part[2] == "3")
                                unit_id = 3012;
                        }

                        // star
                        else if (part[1].Equals(STAR)) {
                            unit_id = 3013;
                        }

                        // moon
                        else if (part[1].Equals(MOON)) {
                            unit_id = 3014;
                        }

                        // sun
                        else if (part[1].Equals(SUN)) {
                            unit_id = 3015;
                        }
                        break;

                    case "B":
                        type_is_random = true;
                        // circle
                        if (part[1].Equals(CIRCLE)) {
                            if (part[2] == "1")
                                unit_id = 4001;
                            else if (part[2] == "2")
                                unit_id = 4002;
                            else if (part[2] == "3")
                                unit_id = 4003;
                        }

                        // triangle
                        else if (part[1].Equals(TRIANGLE)) {
                            if (part[2] == "1")
                                unit_id = 4004;
                            else if (part[2] == "2")
                                unit_id = 4005;
                            else if (part[2] == "3")
                                unit_id = 4006;
                        }

                        // square
                        else if (part[1].Equals(SQUARE)) {
                            if (part[2] == "1")
                                unit_id = 4007;
                            else if (part[2] == "2")
                                unit_id = 4008;
                            else if (part[2] == "3")
                                unit_id = 4009;
                        }

                        // star
                        else if (part[1].Equals(STAR)) {
                            unit_id = 4010;
                        }

                        // moon
                        else if (part[1].Equals(MOON)) {
                            unit_id = 4011;
                        }

                        // sun
                        else if (part[1].Equals(SUN)) {
                            unit_id = 4012;
                        }
                        break;

                    case "A":
                        type_is_random = true;
                        // circle
                        if (part[1].Equals(CIRCLE))
                            unit_id = 5001;
                        // triangle
                        else if (part[1].Equals(TRIANGLE))
                            unit_id = 5002;
                        // square
                        else if (part[1].Equals(SQUARE))
                            unit_id = 5003;
                        // star
                        else if (part[1].Equals(STAR))
                            unit_id = 5004;
                        // moon
                        else if (part[1].Equals(MOON)) 
                            unit_id = 5005;
                        // sun
                        else if (part[1].Equals(SUN))
                            unit_id = 5006;
                        break;

                    case "S":
                        type_is_random = true;
                        // circle
                        if (part[1].Equals(CIRCLE))
                            unit_id = 6001;
                        // triangle
                        else if (part[1].Equals(TRIANGLE))
                            unit_id = 6002;
                        // square
                        else if (part[1].Equals(SQUARE))
                            unit_id = 6003;
                        // star
                        else if (part[1].Equals(STAR))
                            unit_id = 6004;
                        // moon
                        else if (part[1].Equals(MOON)) 
                            unit_id = 6005;
                        // sun
                        else if (part[1].Equals(SUN))
                            unit_id = 6006;
                        break;
                    default: break;
                }

                if (type_is_random) {
                    // 상세 패널 조작
                    InfoPanelControll.Instance.InfoPanelActivate(unit_id, -1, 2);
                }
                else {
                    // 상세 패널 조작
                    InfoPanelControll.Instance.InfoPanelActivate(unit_id, 1000, 2);
                }

                // 조합식 생성
                CombineWithDetailShow(unit_id);
            }
        }
    }

    public void DetailInit() {
        // 유닛 상세 닫기
        InfoPanelControll.Instance.InfoPanelDown(2);
        // 조합식 초기화
        CleanContent(CombineShowContent);
    }

    public void CombineWithDetailShow(int cur_id) {
        ShowCombineTable(cur_id, true, CombineShowContent, 0);
        /*
        ShowCombineToResultTable(cur_id);
        ShowCombineFromMaterialTable(cur_id);
        */
        var tmp = datahub.Unit_dic[cur_id] as Unit;
        if(tmp.Grade.Equals("C") || tmp.Grade.Equals("B") || tmp.Grade.Equals("A") || tmp.Grade.Equals("S")) {
            // 상세 패널 조작
            InfoPanelControll.Instance.InfoPanelActivate(cur_id, -1, 2);
        }
        else {
            // 상세 패널 조작
            InfoPanelControll.Instance.InfoPanelActivate(cur_id, 1000, 2);
        }
    }
}
