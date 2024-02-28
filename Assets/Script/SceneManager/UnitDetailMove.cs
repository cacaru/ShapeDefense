using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UnitDetailMove : MonoBehaviour, IPointerClickHandler {

    // datahub 연동
    private DataHub datahub;
    // Start is called before the first frame update
    void Start()
    {
        datahub = GameObject.Find("GameObject").GetComponent<DataHub>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData) {
        GameObject now = eventData.pointerCurrentRaycast.gameObject;
        if (now != null) {
            string name = now.name;
            if ( name.Contains("_") ) {
                string[] part = name.Split('_');
                switch(part[0]) {
                    case "N":
                        // circle
                        if (part[1] == "circle")
                            datahub.Unitid = 1;
                        // triangle
                        else if (part[1] == "triangle")
                            datahub.Unitid = 2;
                        // square
                        else if (part[1] == "square")
                            datahub.Unitid = 3;
                        break;

                    case "Ad":
                        // circle
                        if (part[1] == "circle") {
                            if (part[2] == "1") 
                                datahub.Unitid = 5;
                            else if (part[2] == "2")
                                datahub.Unitid = 6;
                            else if (part[2] == "3")
                                datahub.Unitid = 7;
                        }
                            
                        // triangle
                        else if (part[1] == "triangle") {
                            if (part[2] == "1")
                                datahub.Unitid = 8;
                            else if (part[2] == "2")
                                datahub.Unitid = 9;
                            else if (part[2] == "3")
                                datahub.Unitid = 10;
                        }
                            
                        // square
                        else if (part[1] == "square") {
                            if (part[2] == "1")
                                datahub.Unitid = 11;
                            else if (part[2] == "2")
                                datahub.Unitid = 12;
                            else if (part[2] == "3")
                                datahub.Unitid = 13;
                        }
                        break;

                    case "Lu":
                        // circle
                        if (part[1] == "circle") {
                            if (part[2] == "1")
                                datahub.Unitid = 14;
                            else if (part[2] == "2")
                                datahub.Unitid = 15;
                            else if (part[2] == "3")
                                datahub.Unitid = 16;
                        }

                        // triangle
                        else if (part[1] == "triangle") {
                            if (part[2] == "1")
                                datahub.Unitid = 17;
                            else if (part[2] == "2")
                                datahub.Unitid = 18;
                            else if (part[2] == "3")
                                datahub.Unitid = 19;
                        }

                        // square
                        else if (part[1] == "square") {
                            if (part[2] == "1")
                                datahub.Unitid = 20;
                            else if (part[2] == "2")
                                datahub.Unitid = 21;
                            else if (part[2] == "3")
                                datahub.Unitid = 22;
                        }

                        // star
                        else if (part[1] == "star") {
                            datahub.Unitid = 23;
                        }
                        break;

                    case "Li":
                        // circle
                        if (part[1] == "circle") {
                            if (part[2] == "1")
                                datahub.Unitid = 24;
                            else if (part[2] == "2")
                                datahub.Unitid = 25;
                            else if (part[2] == "3")
                                datahub.Unitid = 26;
                        }

                        // triangle
                        else if (part[1] == "triangle") {
                            if (part[2] == "1")
                                datahub.Unitid = 27;
                            else if (part[2] == "2")
                                datahub.Unitid = 28;
                            else if (part[2] == "3")
                                datahub.Unitid = 29;
                        }

                        // square
                        else if (part[1] == "square") {
                            if (part[2] == "1")
                                datahub.Unitid = 30;
                            else if (part[2] == "2")
                                datahub.Unitid = 31;
                            else if (part[2] == "3")
                                datahub.Unitid = 32;
                        }

                        // star
                        else if (part[1] == "star") {
                            if (part[2] == "1")
                                datahub.Unitid = 33;
                            else if (part[2] == "2")
                                datahub.Unitid = 34;
                            else if (part[2] == "3")
                                datahub.Unitid = 35;
                        }
                        break;

                    case "Cu":
                        // circle
                        if (part[1] == "circle")
                            datahub.Unitid = 36;
                        // triangle
                        else if (part[1] == "triangle")
                            datahub.Unitid = 37;
                        // square
                        else if (part[1] == "square")
                            datahub.Unitid = 38;
                        // star
                        else if (part[1] == "star")
                            datahub.Unitid = 39;
                        break;

                    default: break;
                }

                
                if ( datahub.Unitid > 0 && datahub.Unitid < 40) {
                    // 유닛 디테일 표시 페이지로 이동
                    SceneManager.LoadScene("UnitDetailScene");
                }
            }

            
        }
    }
}
