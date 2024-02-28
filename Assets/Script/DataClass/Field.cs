using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    private int unit_id = 0;
    private Color normal_color = new(145 / 255f, 145 / 255f, 145 / 255f, 1);
    private Color advanced_color = new(28 / 255f, 195 / 255f, 0, 1);
    private Color luxury_color = new(50 / 255f, 116 / 255f, 215 / 255f, 1);
    private Color limit_color = new(135 / 255f, 106 / 255f, 217 / 255f, 1);
    private Color custom_color = new(224 / 255f, 82 / 255f, 90 / 255f, 1);

    public int UnitId { 
        get { return unit_id; } 
        set { 
            unit_id = value;
            //Debug.Log("in field >> " + transform.name);
            //Debug.Log("in field >> " + unit_id);
            // sprite renderer controll
            string path;
            if ( unit_id != 0 ) {
                path = UtilityHub.GetPath( unit_id );
                // change sprite image
                //Debug.Log(path);
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path);
                // renderer's material controll
                GetComponent<SpriteRenderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
                // id에 따라 색 변경
                if (unit_id <= 4) {
                    GetComponent<SpriteRenderer>().color = normal_color;
                    GetComponent<SpriteRenderer>().material.SetColor("OutlineColor", normal_color);
                }
                else if (unit_id > 4 && unit_id <= 13) {
                    GetComponent<SpriteRenderer>().color = advanced_color;
                    GetComponent<SpriteRenderer>().material.SetColor("OutlineColor", advanced_color);
                }
                else if (unit_id > 13 && unit_id <= 23) {
                    GetComponent<SpriteRenderer>().color = luxury_color;
                    GetComponent<SpriteRenderer>().material.SetColor("OutlineColor", luxury_color);
                }
                else if (unit_id > 23 && unit_id <= 35) {
                    GetComponent<SpriteRenderer>().color = limit_color;
                    GetComponent<SpriteRenderer>().material.SetColor("OutlineColor", limit_color);
                }
                else if (unit_id > 36 && unit_id <= 39) {
                    GetComponent<SpriteRenderer>().color = custom_color;
                    GetComponent<SpriteRenderer>().material.SetColor("OutlineColor", custom_color);
                }
            }
            // 0 값이 들어오면 기존의 값을 지우고 초기화
            else if (unit_id == 0) {
                GetComponent<SpriteRenderer>().sprite = null;
            }
        } 
    }
}
