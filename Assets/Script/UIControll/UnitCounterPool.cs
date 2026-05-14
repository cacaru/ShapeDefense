using ShapeDefenseSpace;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShapeDefenseSpace.GameData;
using static ShapeDefenseSpace.PublicData;

public class UnitCounterPool : SceneSingleton<UnitCounterPool>
{
    [SerializeField] private GameObject Prefabs;

    private readonly ArrayList counter_array = new();

    delegate void ShowObectDelegate();
    ShowObectDelegate show_del;

    void OnEnable() {
        show_del += OffObject;
        show_del += ShowUnitObj;
        Initialize(65);
    }

    private GameObject CreateCounter() {
        var counter = Instantiate(Prefabs, transform);
        // 부모 설정은 bullet을 받을 때 설정
        counter.SetActive(false);
        return counter;
    }

    private void Initialize(int count) {
        for (int i = 0; i < count; i++) {
            counter_array.Add(CreateCounter());
        }
    }

    /// <summary>
    /// 외부에서 pool에 들어있는 오브젝트에 접근
    /// </summary>
    /// <returns>counter 오브젝트</returns>
    public GameObject OnObject(int id) {
        int z = 0;
        for(int i = 0; i < datahub.Unit_Number; i++) {
            if (datahub.Unit_Ids[i] == id) {
                z = i; break;
            }
        }
        GameObject counter = counter_array[z] as GameObject;

        counter.transform.Find("Unit").gameObject.GetComponent<Image>().sprite = UtilityHub.GetSprite(id);
        counter.transform.Find("Grade").gameObject.GetComponent<Image>().color = id switch {
            >= 1000 and <= 1999 => color_e,
            >= 2000 and <= 2999 => color_d,
            >= 3000 and <= 3999 => color_c,
            >= 4000 and <= 4999 => color_b,
            >= 5000 and <= 5999 => color_a,
            >= 6000 and <= 6999 => color_s,
            >= 300 and <= 399 => core_color,
            >= 400 and <= 499 => unicore_color,
            >= 500 and <= 599 => crystal_color,
            _ => core_color
        };
        counter.transform.Find("Count").gameObject.GetComponent<TMP_Text>().text = datahub.UnitCounter[id].ToString();
        counter.SetActive(true);
        return counter;
    }

    /// <summary>
    /// 사용 완료된 오브젝트 반환
    /// </summary>
    /// <param name="bullet"></param>
    public void OffObject() {
        int size = counter_array.Count;
        for(int i = 0; i < size; i++) {
            (counter_array[i] as GameObject).SetActive(false);
        }
    }

    public void ShowUnitCount() {
        show_del();
    }
    public void ShowUnitObj() {
        /*
        for (int i = 1; i <= UNIT_COUNTER; i++) {
            if (datahub.UnitCounter[i] > 0) {
                OnObject(i);
            }
        }
        */
        var keys = new List<int>(datahub.Unit_dic.Keys);
        for(int i = 0; i < datahub.Unit_dic.Count; i++) {
            if (datahub.UnitCounter[keys[i]] > 0) {
                OnObject(keys[i]);
            }
        }
    }
}
