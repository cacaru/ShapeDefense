using UnityEngine;

using ShapeDefenseSpace;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;

public class Field : MonoBehaviour
{
    [SerializeField] private int unit_id = 0;
    [SerializeField] private int type = 7000;

    public int UnitId { 
        get { return unit_id; } 
        set { 
            unit_id = value;
            // id가 들어오면서 type을 한번 초기화
            type = 7000;
            //Debug.Log("in field >> " + transform.name);
            //Debug.Log("in field >> " + unit_id);
            if ( unit_id != 0 ) {
                // summon effect on
                gameObject.GetComponent<SummonEffectSwitch>().EffectOn();
                gameObject.tag = "Unit_Active";

            }
            // 0 값이 들어오면 기존의 값을 지우고 초기화
            else if (unit_id == 0) {
                GetComponent<SpriteRenderer>().sprite = null;
                transform.Find("Border").gameObject.SetActive(false);
                gameObject.tag = "Unit_Wait";
            }
        } 
    }
    
    // type 에 따라 unitField의 Border에 특수형을 노출
    public int Type { 
        get { return type; } 
        set {
            type = value;
            //Debug.Log(type);
            // 기본형
            if(type == 7000) {
                transform.Find("Border").gameObject.SetActive(false);
            }
            // type에 따라 border의 스프라이트 이미지를 구성
            // 7001 : poison 독형
            // 7002 : blust 폭발형
            // 7003 : paralysis  마비형 
            else if (type >= 7001) {
                transform.Find("Border").gameObject.GetComponent<SpriteRenderer>().sprite = UtilityHub.GetSprite(type);
                //transform.Find("Border").gameObject.GetComponent<SpriteRenderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));
                transform.Find("Border").gameObject.GetComponent<SpriteRenderer>().color = type switch {
                    7001 => color_poison_border,
                    7002 => color_blust_border,
                    7003 => color_paralysis_border,
                    _ => color_poison_border
                };
                transform.Find("Border").gameObject.SetActive(true);
            }

        } 
    }


    // sprite renderer controll
    public void SettingField() {
        if(unit_id != 0) {
            //string path = UtilityHub.GetPath(unit_id);
            // change sprite image
            //Debug.Log(path);
            GetComponent<SpriteRenderer>().sprite = UtilityHub.GetSprite(unit_id); // Resources.Load<Sprite>(path);
            // renderer's material controll
            //GetComponent<SpriteRenderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(path));

            switch (UtilityHub.GetUnitGrade(unit_id)) {
                case "E":
                    Type = 1000;
                    GetComponent<SpriteRenderer>().color = color_e;
                    GetComponent<SpriteRenderer>().material.SetColor("OutlineColor", color_e);
                    GetComponent<SpriteRenderer>().material.SetColor("TextureColor", color_e);
                    break;
                case "D":
                    Type = 1000;
                    GetComponent<SpriteRenderer>().color = color_d;
                    GetComponent<SpriteRenderer>().material.SetColor("OutlineColor", color_d);
                    GetComponent<SpriteRenderer>().material.SetColor("TextureColor", color_d);
                    break;
                case "C":
                    Type = Random.Range(7000, 7004); // (int)Time.time % 4 + 1000;
                    GetComponent<SpriteRenderer>().color = color_c;
                    GetComponent<SpriteRenderer>().material.SetColor("OutlineColor", color_c);
                    GetComponent<SpriteRenderer>().material.SetColor("TextureColor", color_c);
                    break;
                case "B":
                    Type = Random.Range(7000, 7004); // (int)Time.time % 4 + 1000;
                    GetComponent<SpriteRenderer>().color = color_b;
                    GetComponent<SpriteRenderer>().material.SetColor("OutlineColor", color_b);
                    GetComponent<SpriteRenderer>().material.SetColor("TextureColor", color_b);
                    break;
                case "A":
                    Type = Random.Range(7000, 7004); // (int)Time.time % 4 + 1000;
                    GetComponent<SpriteRenderer>().color = color_a;
                    GetComponent<SpriteRenderer>().material.SetColor("OutlineColor", color_a);
                    GetComponent<SpriteRenderer>().material.SetColor("TextureColor", color_a);
                    break;
                case "S":
                    Type = Random.Range(7000, 7004); // (int)Time.time % 4 + 1000;
                    GetComponent<SpriteRenderer>().color = color_s;
                    GetComponent<SpriteRenderer>().material.SetColor("OutlineColor", color_s);
                    GetComponent<SpriteRenderer>().material.SetColor("TextureColor", color_s);
                    break;
                case "IC":
                    GetComponent<SpriteRenderer>().color = core_color;
                    GetComponent<SpriteRenderer>().material.SetColor("OutlineColor", core_color);
                    GetComponent<SpriteRenderer>().material.SetColor("TextureColor", core_color);
                    break;
                case "IB":
                    GetComponent<SpriteRenderer>().color = unicore_color;
                    GetComponent<SpriteRenderer>().material.SetColor("OutlineColor", unicore_color);
                    GetComponent<SpriteRenderer>().material.SetColor("TextureColor", unicore_color);
                    break;
                case "IA":
                    GetComponent<SpriteRenderer>().color = crystal_color;
                    GetComponent<SpriteRenderer>().material.SetColor("OutlineColor", crystal_color);
                    GetComponent<SpriteRenderer>().material.SetColor("TextureColor", crystal_color) ;
                    break;
            }

            GetComponent<UnitAttack>().Id = unit_id;
        }
    }
}
